using System;
using UnityEngine;
using UnityEngine.UIElements;
using UserInput;

namespace Components.UI
{
    public class ProgressBarHandler : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        private ProgressBar _progressBar;
        private float _longPressTimeInSeconds;

        private DateTime _timeOfPress;
        public bool IsPresentedWithChoice { get; set; }

        public void Initialize(ButtonActions buttonActions, UserActions userActions, float longPressTimeInSeconds)
        {
            _longPressTimeInSeconds = longPressTimeInSeconds;

            buttonActions.OnPress += Show;
            buttonActions.OnRelease += Hide;

            userActions.OnShortPress += Hide;
            userActions.OnLongPress += Hide;
            
            _progressBar = _uiDocument.rootVisualElement.Q<ProgressBar>();
            Hide();
        }
        
        private void Show()
        {
            if (!IsPresentedWithChoice)
            {
                return;
            }
            
            _timeOfPress = DateTime.UtcNow;
            _progressBar.style.display = DisplayStyle.Flex;
        }

        private void Hide()
        {
            _progressBar.style.display = DisplayStyle.None;
        }

        private void Update()
        {
            TimeSpan timeLeft = DateTime.UtcNow - _timeOfPress;
            double percentageDone = timeLeft / TimeSpan.FromSeconds(_longPressTimeInSeconds);
            _progressBar.value = (float)percentageDone;
        }
    }
}