using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraEvents.MonoBehaviours.Effects
{
    [HarmonyPatch(typeof(NewMovement), nameof(NewMovement.GetHurt))]
    internal class LessDamageEffect : Effect
    {
        static bool actived;
        void Start()
        {
            actived = true;
        }
        public override void RemoveEffect()
        {
            actived = false;
            base.RemoveEffect();
        }
        public static bool Prefix(ref int damage)
        {
            if (!actived) return true;
            damage /= 2;
            return true;
        }
    }
}
