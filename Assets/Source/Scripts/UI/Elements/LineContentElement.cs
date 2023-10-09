using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Battle
{
    public class LineContentElement : UIElementWithChild
    {
        [SerializeField]
        private Button _closeButton;
        private RectTransform _transform;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
        }

        public void ShowContent()
        {
            var width = _transform.sizeDelta.x;

            _transform.anchoredPosition = new Vector2 (_transform.anchoredPosition.x + width, _transform.anchoredPosition.y);
            _closeButton.onClick.AddListener(() => CloseContent());
        }

        public void CloseContent()
        {
            var width = _transform.sizeDelta.x;

            _transform.anchoredPosition = new Vector2(_transform.anchoredPosition.x - width, _transform.anchoredPosition.y);
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}
