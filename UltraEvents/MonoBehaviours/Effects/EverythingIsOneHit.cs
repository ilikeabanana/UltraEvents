using HarmonyLib;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    
    [HarmonyPatch]
    internal class EverythingIsOneHit : Effect
    {
        public static bool IsActive;

        void Start()
        {
            IsActive = true;
        }
        public override void RemoveEffect()
        {
            IsActive = false;
        }

        [HarmonyPatch(typeof(NewMovement), nameof(NewMovement.GetHurt))]
        public static bool Prefix(ref int damage, bool invincible, float scoreLossMultiplier = 1f, bool explosion = false, bool instablack = false, float hardDamageMultiplier = 0.35f, bool ignoreInvincibility = false)
        {
            if (!IsActive) return true;
            damage = 500000;
            return true;
        }
        [HarmonyPatch(typeof(EnemyIdentifier), nameof(EnemyIdentifier.DeliverDamage))]
        [HarmonyPrefix]
        public static bool PrefixENemy(EnemyIdentifier __instance, ref float multiplier)
        {
            if (!IsActive) return true;
            multiplier = 50000000;
            return true;
        }
    }
}