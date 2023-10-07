using Game.Start;
using Scellecs.Morpeh;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.UI
{
    public class UIElementWithChild : UIElement
    {
        [SerializeField]
        protected UIElement[] ChildElements;

        public override async Task<UIElement> InitElement(RuntimeData data, World thisPlayerWorld, int indexInHierarchy = 0)
        {
            await base.InitElement(data, thisPlayerWorld, indexInHierarchy);

            if (ChildElements.Length > 0)
                await InitializeChild();

            return this;
        }

        protected async Task<UIElement> InitializeChild()
        {
            var task = new Task<List<UIElement>>(() =>
            {
                var result = new List<UIElement>();
                for (int i = 0; i < ChildElements.Length; i++)
                {
                    ChildElements[i].InitElement(Data, World, i).ContinueWith((x) => result.Add(x.Result));
                }
                return result;
            });

            await task;

            return this;
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (var item in ChildElements)
            {
                item.Dispose();
            }
        }
    }
}
