using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.UI
{
    public abstract class UIElement : MonoBehaviour, IDisposable
    {
        internal abstract Task BeforeOpen(UIElementData data);
        internal abstract Task AfterOpen(UIElementData data);
        internal abstract Task Close();
        internal abstract bool CompareData(UIElementData data);
        public abstract void Dispose();
    }

    public interface UIElementData
    {
    }
}