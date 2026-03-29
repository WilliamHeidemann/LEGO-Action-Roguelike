using System;
using UnityEngine;

namespace UserInput
{
    public class UserActions
    {
        public Action OnShortPress;
        public Action OnLongPress;

        private readonly float _longPressTimeInSeconds;
        private bool _isPressing;
        private DateTime _lastPressTime;

        public UserActions(ButtonActions actions, float longPressTimeInSeconds)
        {
            _longPressTimeInSeconds = longPressTimeInSeconds;
            actions.OnPress += OnPress;
            actions.OnRelease += OnRelease;
        }

        private void OnPress()
        {
            _ = OnPressAsync();
        }

        private async Awaitable OnPressAsync()
        {
            _isPressing = true;

            DateTime timeOfRecording = DateTime.UtcNow;

            _lastPressTime = timeOfRecording;

            await Awaitable.WaitForSecondsAsync(_longPressTimeInSeconds);

            if (_isPressing && timeOfRecording == _lastPressTime)
            {
                OnLongPress?.Invoke();
            }
        }

        private void OnRelease()
        {
            _isPressing = false;

            if (DateTime.UtcNow - _lastPressTime < TimeSpan.FromSeconds(_longPressTimeInSeconds))
            {
                OnShortPress?.Invoke();
            }
        }
    }
}