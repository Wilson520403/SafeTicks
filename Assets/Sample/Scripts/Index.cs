using System;
using System.Collections;
using System.Collections.Generic;
using SafeTicks;
using TMPro;
using UnityEngine;

namespace Sample
{
    public class Index : MonoBehaviour
    {
        public TextMeshProUGUI textSec;
        public TextMeshProUGUI textMillisecond;

        private void Awake ()
        {
            MgrTicks.local.Init (MgrTicks.ParseTicksToTimestampMillisecond (DateTime.Now.Ticks));
            MgrTicks.local.OnEmitAble ();

            MgrTicks.local.OnUpdatedSecond += (pass) =>
            {
                textSec.text = $"Sec: {pass}";
            };

            MgrTicks.local.OnUpdatedMillisecond += (pass) =>
            {
                textMillisecond.text = $"Millisecond: {pass}";
            };
        }

        private void Update ()
        {
            MgrTicks.local.OnUpdate ();
        }
    }
}