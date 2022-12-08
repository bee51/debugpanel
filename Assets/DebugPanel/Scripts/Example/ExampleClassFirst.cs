using UnityEngine;

namespace DebugPanel.Scripts
{
    public class ExampleClassFirst : MonoBehaviour
    {
        #region Fields

        [DebugPanel("15", "SecondaryDamage", false)]
        public float damageSecondary;

        #endregion
    }
}