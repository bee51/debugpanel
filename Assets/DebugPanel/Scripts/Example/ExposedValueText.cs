using UnityEngine;

namespace DebugPanel.Scripts
{
    public class ExposedValueText : MonoBehaviour
    {
        [DebugPanel("12","Damage",true)] 
        public string damage ;
   
    }
}