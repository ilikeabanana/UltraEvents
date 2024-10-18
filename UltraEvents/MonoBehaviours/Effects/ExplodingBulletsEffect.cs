using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UltraEvents.Utils;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    [HarmonyPatch(typeof(Projectile), nameof(Projectile.Start))]
    internal class ExplodingBulletsEffect : Effect
    {
        static List<ExplosionController> explosions;
        static bool IsActive = false;
        void Start()
        {
            IsActive = true;
            explosions = ModUtils.GetEverythingOfType<ExplosionController>((ExplosionController x) => x.name.ToLower().Contains("fire"));
        }
        public override void RemoveEffect()
        {
            IsActive = false;
            base.RemoveEffect();
        }
        public static bool Prefix(Projectile __instance)
        {
            if (!IsActive) return true;
            __instance.explosionEffect = explosions[Random.Range(0, explosions.Count)].gameObject;
            
            return true;
        }
    }
}
