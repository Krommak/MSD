using System;

namespace Game.UI
{
    public sealed class UIManager
    {
        public static UIManager Instance;

        public UIManager()
        {
            Instance = this;
        }

        public bool TryGetElement<T>(Type elementType, UIElementData data, out T element) where T : UIElement
        {
            var result = Activator.CreateInstance(elementType) as T;

            if (!result.CompareData(data))
            {
                element = null;
                return false;
            }

            result.BeforeOpen(data)
                  .AfterOpen(data);

            element = result;
            return true;
        }
    }
}
