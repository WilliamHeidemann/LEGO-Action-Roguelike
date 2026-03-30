using Components.UI;
using UnityEngine;
using UnityEngine.UIElements;
using UserInput;

namespace Components
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _maxXValue;
        [SerializeField] private ArrowHandler _arrowHandler;
        
        private bool _isMoving;
        private bool _right = true;

        public void Initialize(ButtonActions buttonActions)
        {
            buttonActions.OnPress += StartMoving;
            buttonActions.OnRelease += StopMoving;
        }

        private void StartMoving()
        {
            _isMoving = true;
        }

        private void StopMoving()
        {
            _isMoving = false;
            _right = !_right;
            _ = _arrowHandler.Rotate(_right);
        }


        private void Update()
        {
            if (PauseManager.PauseMode != PauseMode.Playing) return;
            
            if (!_isMoving)
            {
                return;
            }
            
            float direction = _right ? 1f : -1f;
            float slide = direction * (_moveSpeed * Time.deltaTime);
            float x = _player.position.x + slide;
            float clampedXValue = Mathf.Clamp(x, -_maxXValue, _maxXValue);
            _player.transform.position = new Vector3(clampedXValue, _player.transform.position.y, _player.transform.position.z);
        }
    }
}