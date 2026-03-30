using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Components.UI
{
    public class ArrowHandler : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        private VisualElement _arrow;
        
        private void Start()
        {
            _arrow = _uiDocument.rootVisualElement.Q<VisualElement>("arrow");
        }

        public void Show()
        {
            _arrow.style.display = DisplayStyle.Flex;
        }
        
        public void Hide()
        {
            _arrow.style.display = DisplayStyle.None;
        }
        
        public async Awaitable Rotate(bool right)
        {
            float start = right ? 180f : 0f;
            float target = right ? 0f : 180f;

            const float duration = 0.3f;
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
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;

            return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
        }
    }
}