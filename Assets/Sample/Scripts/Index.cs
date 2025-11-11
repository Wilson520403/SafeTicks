using System;
using System.Collections;
using System.Collections.Generic;
using SafeTicks;
using UnityEngine;

namespace Sample
{
    public class Index : MonoBehaviour
    {
        private void Awake ()
        {
            MgrTicks.local.Init (MgrTicks.ParseTicksToTimestampMillisecond (DateTime.Now.Ticks));
            MgrTicks.local.OnEmitAble ();
        }

        private void Update ()
        {
            MgrTicks.local.OnUpdate ();
        }
    }
}