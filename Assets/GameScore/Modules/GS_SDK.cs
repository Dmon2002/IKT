using UnityEngine;
using UnityEngine.Events;

namespace GameScore
{
    public class GS_SDK : MonoBehaviour
    {
        public static event UnityAction OnReady;
        private void CallSDKReady() => OnReady?.Invoke();
    }

}