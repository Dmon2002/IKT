using System;
using System.Collections;
using UnityEngine;

namespace ActionSystem
{
    public abstract class Action : MonoBehaviour
    {
        public abstract IEnumerator Perform();
    }
}
