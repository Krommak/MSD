using Game.Start;
using Scellecs.Morpeh;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI.Battle
{
    internal class SquadUIElement : UIElement, IPointerEnterHandler
    {
        [SerializeField]
        private Image _icon;

        public async override Task<UIElement> InitElement(RuntimeData data, World thisPlayerWorld, int indexInHierarchy)
        {
            await base.InitElement(data, thisPlayerWorld, indexInHierarchy);

            var squad = Data.GetSquadByWorldAndIndex(World, IndexInHierarhy);
            _icon.sprite = squad.SquadIcon;

            return this;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Show squad info");
        }
    }
}