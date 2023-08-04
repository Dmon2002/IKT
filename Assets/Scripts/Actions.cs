using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionSystem
{
    public class Actions : MonoBehaviour
    {
        [SerializeField] private List<Action> _actionList;

        public void Perform()
        {
            StartCoroutine(PerformCoroutine());
        }

        private IEnumerator PerformCoroutine()
        {
            foreach (var action in _actionList)
            {
                yield return StartCoroutine(action.Perform());
            }
        }
    }
}
