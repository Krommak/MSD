using UnityEngine;

namespace Game.UI
{
    public abstract class UIElement : MonoBehaviour
    {
        public abstract UIElement BeforeOpen(UIElementData data);
        public abstract UIElement AfterOpen(UIElementData data);
        public abstract bool CompareData(UIElementData data);
    }

    public interface UIElementData
    {
    }
}