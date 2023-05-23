using UnityEngine;

namespace GS_Utilities.Component
{
    public class GS_Component : MonoBehaviour
    {
        private static GS_Component Component;

        private void Awake()
        {
            if (GS_Component.Component == null)
                GS_Component.Component = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
    }
}
