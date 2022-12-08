using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DebugPanel.Scripts
{
    public class DebugPanelSlot : MonoBehaviour
    {
        //it is for find displayname and value
        [SerializeField] private List<UISlot> uiSlotsList;
        [SerializeField] private DebugFinder debugFinder;
        private Dictionary<string, string> _valuesDictionary = new Dictionary<string, string>();


        public void CreateDictionary(string displayName, string value)
        {
            if (_valuesDictionary.ContainsKey(displayName))
            {
                return;
            }
            int currentIndex = _valuesDictionary.Count;
            uiSlotsList[currentIndex].explainText.text = displayName;
            uiSlotsList[currentIndex].inputField.text = value;
            _valuesDictionary.Add(displayName, value);
        }

        public void ClearDictionary()
        {
            foreach (var uiSlot in uiSlotsList)
            {
                uiSlot.explainText.text = "";
                uiSlot.inputField.text = "";
                uiSlot.button.onClick.RemoveAllListeners();
            }

            _valuesDictionary.Clear();
        }

        public void SetButtons()
        {
            for (int i = 0; i < uiSlotsList.Count; i++)
            {
                string explainText = uiSlotsList[i].explainText.text;
                Debug.Log(explainText);
                uiSlotsList[i].button.onClick.AddListener(() => { SetValue(explainText); });
            }
        }


        public void SetValue(string indexName)
        {
            int index = 0;
            foreach (var uıSlot in uiSlotsList)
            {
                if (uıSlot.explainText.text == indexName)
                {
                    index = uiSlotsList.IndexOf(uıSlot);
                }
            }

            debugFinder.SetAttribute(indexName, uiSlotsList[index].inputField.text);
        }
    }

    [Serializable]
    public struct UISlot
    {
        public Button button;
        public TMP_InputField inputField;
        public TextMeshProUGUI explainText;
    }
}