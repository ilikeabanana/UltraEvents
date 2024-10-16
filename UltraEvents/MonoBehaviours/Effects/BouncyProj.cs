using HarmonyLib;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // TODO Review __instance file and update to your own requirements, or remove it altogether if not required

    /// <summary>
    /// Sample Harmony Patch class. Suggestion is to use one file per patched class
    /// though you can include multiple patch classes in one file.
    /// Below is included as an example, and should be replaced by classes and methods
    /// for your mod.
    /// </summary>
    [HarmonyPatch(typeof(Projectile), nameof(Projectile.Collided))]
    internal class BouncyProj : Effect
    {
        static bool active;
        void Start()
        {
            active = true;
        }
        public override void RemoveEffect()
        {
            base.RemoveEffect();
            active = false;
        }
        [HarmonyPrefix]
        public static bool Prefix(Projectile __instance, Collider other)
        {
            if (!active) return true;
            // If colliding with an enemy or player, allow the original logic to execute
            if (IsEnemyOrPlayer(other))
            {
                return true; // Don't prevent the original method from running
            }

            // For other collisions, implement your bouncing logic here:

            // Calculate the reflection vector
            Vector3 reflection = Vector3.Reflect(__instance.rb.velocity, other.transform.up);

            // Set the projectile's velocity to the reflection vector
            __instance.rb.velocity = reflection;

            // Adjust the projectile's rotation to match the new direction
            __instance.transform.rotation = Quaternion.LookRotation(reflection);

            // Prevent original method execution (optional, depending on your needs)
            // return false; // Optional: If you want to completely override the behavior

            return true; // Allow the original method to run for potential cleanup (optional)
        }

        private static bool IsEnemyOrPlayer(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                return true;
            }

            // Check for enemy identifier component (adjust based on your enemy identification logic)
            EnemyIdentifierIdentifier enemyIdentifierIdentifier;
            if (other.gameObject.TryGetComponent<EnemyIdentifierIdentifier>(out enemyIdentifierIdentifier))
            {
                return true;
            }

            return false;
        }
    }
}