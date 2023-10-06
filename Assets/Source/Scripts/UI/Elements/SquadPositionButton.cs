using Game.Start;
using Scellecs.Morpeh;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Battle
{
    [RequireComponent(typeof(Button))]
    public class SquadPositionButton : UIElement
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

            _button.onClick.AddListener(() => ShowButtons());

            return this;
        }

        private void UpdateIcon(int indexOfElement)
        {
            var squad = Data.GetSquadByWorldAndIndex(World, indexOfElement);
            _icon.sprite = squad.SquadIcon;

            CloseButtons();
        }

        private void ShowButtons()
        {
            foreach (var element in ChildElements)
            {
                if (element is SelectSquadOnPositionButton selectButton)
                {
                    selectButton.gameObject.SetActive(true);
                    selectButton.SetAction(() => UpdateIcon(Array.IndexOf(ChildElements, element)));
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