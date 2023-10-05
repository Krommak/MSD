using Game.Start;
using Scellecs.Morpeh;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.UI
{
    public class UIElement : MonoBehaviour, IDisposable
    {
        [SerializeField]
        protected UIElement[] ChildElements;

        protected RuntimeData Data;
        protected World World;
        protected int IndexInHierarhy;

        public virtual async Task<UIElement> InitElement(RuntimeData data, World thisPlayerWorld, int indexInHierarchy = 0)
        {
            Data = data;
            World = thisPlayerWorld;
            IndexInHierarhy = indexInHierarchy;

            if (ChildElements.Length > 0)
                await InitializeChild();

            return this;
        }

        protected async Task<UIElement> InitializeChild()
        {
            var task = new Task<List<UIElement>>(() =>
            {
                var result = new List<UIElement>();
                for(int i = 0; i < ChildElements.Length; i++)
                {
                    ChildElements[i].InitElement(Data, World, i).ContinueWith((x) => result.Add(x.Result));
                }
                return result;
            });

            await task;

            return this;
        }

        public void Dispose()
        {
            foreach (var item in ChildElements)
            {
                item.Dispose();
            }
            Data = null;
            World = null;
        }
    }
}
