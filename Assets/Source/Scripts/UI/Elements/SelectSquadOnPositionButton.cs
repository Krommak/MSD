using Game.Start;
using Scellecs.Morpeh;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI.Battle
{
    [RequireComponent(typeof(Button))]
    public class SelectSquadOnPositionButton : UIElement
    {
        [SerializeField]
        private Image _icon;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public async override Task<UIElement> InitElement(RuntimeData data, World thisPlayerWorld, int indexInHierarchy)
        {
            await base.InitElement(data, thisPlayerWorld, indexInHierarchy);

            return this;
        }

        internal void SetAction(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        internal void RemoveActions()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}