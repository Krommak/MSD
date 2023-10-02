using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class PositionButton : UIElement
    {
        private PositionButtonData data;

        internal override Task BeforeOpen(UIElementData uiData)
        {
            return new Task(() =>
            {

            });
        }

        internal override Task AfterOpen(UIElementData uiData)
        {
            return new Task(() =>
            {
                data = (PositionButtonData)uiData;
                foreach (var item in data.Actions)
                {
                    GetComponent<Button>().onClick.AddListener(item);
                }
            });
        }

        internal override bool CompareData(UIElementData data) => data is PositionButtonData;

        internal override Task Close()
        {
            return new Task(() =>
            {

            });
        }

        public override void Dispose()
        {
            var button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            data = null;
        }
    }

    public class PositionButtonData : UIElementData
    {
        internal List<UnityAction> Actions;
    }
}