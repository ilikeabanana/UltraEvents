using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    internal class LowGravity : Effect
    {
        void Start()
        {
            Physics.gravity /= 2;
        }
        public override void RemoveEffect()
        {
            Physics.gravity *= 2;
            base.RemoveEffect();
        }
    }
}
