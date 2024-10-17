using HarmonyLib;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    [HarmonyPatch]
    internal class FireBullets : Effect
    {
        static bool IsActive = false;
        public void Start()
        {
            IsActive = true;
        }
        public override void RemoveEffect()
        {
            IsActive = false;
            base.RemoveEffect();
        }

        [HarmonyPatch(typeof(Projectile), nameof(Projectile.Collided))]
        public static void Postfix(Projectile __instance, Collider other)
        {
            if (!IsActive) return;
            if(other.gameObject.GetComponentsInChildren<Flammable>() != null)
            {
                foreach (var item in other.gameObject.GetComponentsInChildren<Flammable>())
                {
                    item.Burn(4);
                }
            }
        } 
    }
}
