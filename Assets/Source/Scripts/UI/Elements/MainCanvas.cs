using Game.Start;
using Scellecs.Morpeh;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.UI
{
    public class MainCanvas : UIElement
    {
        [SerializeField]
        private UIElement[] _childElements;

        private RuntimeData _data;
        private World _world;

        public async override Task<UIElement> InitElement(RuntimeData data, World thisPlayerWorld)
        {
            _data = data;
            _world = thisPlayerWorld;

            var task = new Task<List<UIElement>>(() =>
            {
                var result = new List<UIElement>();
                foreach (var item in _childElements)
                {
                    result.Add(item.InitElement(_data, _world).Result);
                }
                return result;
            });

            await task;

            //

            return this;
        }
    }
}
