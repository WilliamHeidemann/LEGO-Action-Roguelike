using UnityEngine;
using UnityEngine.InputSystem;

namespace UserInput
{
    public class InputReader : MonoBehaviour
    {
        private ButtonActions _buttonActions;
    
        public void Initialize(ButtonActions buttonActions)
        {
            _buttonActions = buttonActions;
        }
    
        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                _buttonActions.OnPress?.Invoke();
            }

            if (Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                _buttonActions.OnRelease?.Invoke();
            }
        }
    }
}