using Game.Start;
using Scellecs.Morpeh;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.UI
{
    public abstract class UIElement : MonoBehaviour, IDisposable
    {
        public abstract Task<UIElement> InitElement(RuntimeData data, World thisPlayerWorld);

        public virtual void Dispose()
        {

        }
    }
}
