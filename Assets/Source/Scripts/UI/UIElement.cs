using Game.Start;
using Scellecs.Morpeh;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.UI
{
    public class UIElement : MonoBehaviour, IDisposable
    {
        protected RuntimeData Data;
        protected World World;
        protected int IndexInHierarhy;

        public virtual async Task<UIElement> InitElement(RuntimeData data, World thisPlayerWorld, int indexInHierarchy = 0)
        {
            Data = data;
            World = thisPlayerWorld;
            IndexInHierarhy = indexInHierarchy;

            return this;
        }

        public virtual void Dispose()
        {
            Data = null;
            World = null;
        }
    }
}
