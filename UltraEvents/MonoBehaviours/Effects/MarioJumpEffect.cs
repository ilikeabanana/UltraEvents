using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraEvents.MonoBehaviours.Effects
{
    internal class MarioJumpEffect : Effect
    {
        void Start()
        {
            MonoSingleton<NewMovement>.instance.jumpPower *= 2;
        }
        public override void RemoveEffect()
        {
            MonoSingleton<NewMovement>.instance.jumpPower /= 2;
            base.RemoveEffect();
        }
    }
}
