using Game.Start;
using Scellecs.Morpeh;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Battle
{
    public class LineElement : UIElement
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private LineContentElement _line;

        private void Start()
        {
            _button.onClick.AddListener(() => _line.ShowContent());
        }

        public async override Task<UIElement> InitElement(RuntimeData data, World thisPlayerWorld, int indexInHierarchy)
        {
            await base.InitElement(data, thisPlayerWorld, indexInHierarchy);

            await _line.InitElement(data, thisPlayerWorld, indexInHierarchy);

            _button.onClick.AddListener(() => _line.ShowContent());

            return this;
        }
    }
}