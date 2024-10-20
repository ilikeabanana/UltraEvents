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
            if (MonoSingleton<NewMovement>.instance.jumpPower == 90) return;
            MonoSingleton<NewMovement>.instance.jumpPower /= 2;
            base.RemoveEffect();
        }
    }
}
