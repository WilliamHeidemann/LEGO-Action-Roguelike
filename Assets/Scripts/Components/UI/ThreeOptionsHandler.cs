using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using UserInput;

namespace Components.UI
{
    public class ThreeOptionsHandler : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private ProgressBarHandler _progressBarHandler;
        [SerializeField] private ArrowHandler _arrowHandler;
        
        private List<VisualElement> _options;

        private int _selectedIndex;

        [CanBeNull] private Choice _choice;
        
        public void Initialize(UserActions userActions)
        {
            _options = _uiDocument.rootVisualElement.Query<VisualElement>("option").ToList();

            userActions.OnShortPress += SelectNext;
            userActions.OnLongPress += Choose;

            Select(0);
            
            Hide();
        }

        public void Propose(Choice choice)
        {
            if (_choice != null)
            {
                Debug.LogWarning("Multiple choices are not supported.");
                return;
            }

            _arrowHandler.Hide();
            _progressBarHandler.IsPresentedWithChoice = true;
            _choice = choice;
            Show();
        }

        private void Show()
        {
            _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        private void Hide()
        {
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        }

        private void Choose()
        {
            _choice?.Invoke(_selectedIndex);
            _choice = null;
            PauseManager.PauseMode = PauseMode.Playing;
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            _arrowHandler.Show();
            _progressBarHandler.IsPresentedWithChoice = false;
        }

        private void SelectNext()
        {
            int nextIndex = _selectedIndex == _options.Count - 1 ? 0 : _selectedIndex + 1;

            Deselect(_selectedIndex);

            Select(nextIndex);
        }

        private void Deselect(int index)
        {
            _selectedIndex = index;

            Color32 transparent = new Color32(0, 0, 0, 0);
            _options[index].style.borderBottomColor = new StyleColor(transparent);
            _options[index].style.borderTopColor = new StyleColor(transparent);
            _options[index].style.borderLeftColor = new StyleColor(transparent);
            _options[index].style.borderRightColor = new StyleColor(transparent);
        }

        private void Select(int index)
        {
            _selectedIndex = index;

            _options[index].style.borderBottomColor = Color.black;
            _options[index].style.borderTopColor = Color.black;
            _options[index].style.borderLeftColor = Color.black;
            _options[index].style.borderRightColor = Color.black;
        }
    }
}