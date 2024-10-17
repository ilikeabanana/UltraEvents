using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraEvents.MonoBehaviours.Effects
{
    internal class InfiniteDash : Effect
    {
        void Update()
        {
            MonoSingleton<NewMovement>.instance.boostCharge = 300;
        }
    }
}
