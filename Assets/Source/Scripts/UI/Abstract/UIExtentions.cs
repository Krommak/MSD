using System;
using System.Threading.Tasks;

namespace Game.UI
{
    public static class UIExtentions
    {
        public async static Task<T> OpenElement<T>(UIElementData data) where T : UIElement
        {
            var result = Activator.CreateInstance(typeof(T)) as T;

            if (result.CompareData(data))
            {
                await result.InitElement(data);
            }
            else
            {
                UnityEngine.Debug.LogError($"Data {data.GetType()} is not compare for {typeof(T)}");
            }

            return result;
        }

        private async static Task<T> InitElement<T>(this T element, UIElementData data) where T : UIElement
        {
            await element.BeforeOpen(data);
            await element.AfterOpen(data);

            return element;
        }

        public static void CloseElement<T>(this T element) where T : UIElement
        {
            element.Close()
                   .Dispose();
        }
    }
}
