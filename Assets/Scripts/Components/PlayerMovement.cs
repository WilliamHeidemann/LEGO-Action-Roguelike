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
        [SerializeField] private UIDocument _arrowUIDocument;
        
        private bool _isMoving;
        private bool _right = true;
        private VisualElement _arrow;

        public void Initialize(ButtonActions buttonActions)
        {
            buttonActions.OnPress += StartMoving;
            buttonActions.OnRelease += StopMoving;
            _arrow = _arrowUIDocument.rootVisualElement.Q<VisualElement>("arrow");
        }

        private void StartMoving()
        {
            _isMoving = true;
        }

        private void StopMoving()
        {
            _isMoving = false;
            _right = !_right;
//            _arrow.style.rotate = new Rotate(_right ? 0f : 180f);
            _ = AnimateArrow(_right);
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
        
        private async Awaitable AnimateArrow(bool right)
        {
            float start = right ? 180f : 0f;
            float target = right ? 0f : 180f;

            float duration = 0.3f;
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                float easedT = EaseOutBack(t);
                float angle = Mathf.LerpUnclamped(start, target, easedT);

                _arrow.style.rotate = new Rotate(angle);

                await Awaitable.NextFrameAsync();
            }

            _arrow.style.rotate = new Rotate(target);
        }

        private static float EaseOutBack(float t)
        {
            float c1 = 1.70158f;
            float c3 = c1 + 1f;

            return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
        }
    }
}