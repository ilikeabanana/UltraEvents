using HarmonyLib;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    [HarmonyPatch(typeof(Projectile), nameof(Projectile.Start))]
    internal class ProjectilesHomeAtEnemies : Effect
    {
        static bool active;
        void Start()
        {
            active = true;
        }
        public override void RemoveEffect()
        {
            active = false;
            base.RemoveEffect();
        }
        public static bool Prefix(Projectile __instance)
        {
            if (!active || !__instance.playerBullet) return true;
            __instance.homingType = HomingType.Instant;
            __instance.target = new EnemyTarget(UltraEvents.Utils.ModUtils.getRandomEnemyThatIsAlive());
            return true;
        }
    }
}
