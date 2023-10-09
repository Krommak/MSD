using Game.Start;
using Scellecs.Morpeh;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Battle
{
    [RequireComponent(typeof(Button))]
    public class SquadPositionButton : UIElementWithChild
    {
        [SerializeField]
        private Image _icon;
        private Button _button;

        public UIElement[] ChildElements { get; set; }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public async override Task<UIElement> InitElement(RuntimeData data, World thisPlayerWorld, int indexInHierarchy)
        {
            await base.InitElement(data, thisPlayerWorld, indexInHierarchy);

            _button.onClick.AddListener(() => ShowButtons());

            return this;
        }

        private void SelectSquadPosition(int indexOfElement)
        {
            var squad = Data.GetSquadByWorldAndIndex(World, indexOfElement);
            _icon.sprite = squad.SquadIcon;

            // Create ECS entity of set squad on selected position

            CloseButtons();
        }

        private void ShowButtons()
        {
            foreach (var element in ChildElements)
            {
                if (element is SelectSquadOnPositionButton selectButton)
                {
                    selectButton.gameObject.SetActive(true);
                    selectButton.SetAction(() => SelectSquadPosition(Array.IndexOf(ChildElements, element)));
                }
            }
        }

        private void CloseButtons()
        {
            foreach (var element in ChildElements)
            {
                if (element is SelectSquadOnPositionButton selectButton)
                {
                    selectButton.RemoveActions();
                    selectButton.gameObject.SetActive(false);
                }
            }
        }
    }
}