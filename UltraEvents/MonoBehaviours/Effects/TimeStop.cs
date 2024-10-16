using HarmonyLib;
using ULTRAKILL.Cheats;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

namespace UltraEvents.MonoBehaviours.Effects
{
    // TODO Review this file and update to your own requirements, or remove it altogether if not required
    /// <summary>
    /// Template MonoBehaviour class. Use this to add new functionality and behaviours to
    /// the game.
    /// </summary>
    internal class TimeStop : Effect
    {
        public static bool IsActive;
        private List<AnimatorState> pausedAnimators = new List<AnimatorState>();
        private Dictionary<Rigidbody, RigidbodyState> rbs = new Dictionary<Rigidbody, RigidbodyState>();
        public Harmony Harmony;

        private struct AnimatorState
        {
            public Animator animator;
            public float speed;
        }
        private struct RigidbodyState
        {
            public Vector3 velocity;
            public Vector3 angularVelocity;
            public bool wasKinematic;
        }
        void Start()
        {
            IsActive = true;
        }
        void Update()
        {
            if (IsActive)
            {
                ApplyTimeStop();
            }
        }

        private void ApplyTimeStop()
        {
            var rigidbodies = FindObjectsOfType<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                if (rb != null && rb != MonoSingleton<NewMovement>.Instance?.rb)
                {
                    if (rbs.ContainsKey(rb) && rb.isKinematic == true)
                    {
                        rbs[rb] = new RigidbodyState
                        {
                            velocity = rbs[rb].velocity,
                            angularVelocity = rbs[rb].angularVelocity,
                            wasKinematic = rbs[rb].wasKinematic
                        };
                    }
                    else
                    {
                        rbs[rb] = new RigidbodyState
                        {
                            velocity = rb.velocity,
                            angularVelocity = rb.angularVelocity,
                            wasKinematic = rb.isKinematic
                        };
                    }

                    rb.isKinematic = true;
                }
            }

            var particleSystems = FindObjectsOfType<ParticleSystem>();
            for (int i = 0; i < particleSystems.Length; i++)
            {
                if (particleSystems[i].isPlaying == true)
                {
                    particleSystems[i]?.Pause();
                }

            }

            var newMovement = MonoSingleton<NewMovement>.Instance;
            var animators = FindObjectsOfType<Animator>();
            for (int i = 0; i < animators.Length; i++)
            {
                var animator = animators[i];
                if (animator == null) continue;
                if (ShouldSkipAnimator(animator, newMovement)) continue;

                if (!ContainsAnimator(pausedAnimators, animator))
                {
                    pausedAnimators.Add(new AnimatorState { animator = animator, speed = animator.speed });
                    animator.speed = 0;
                }
            }
            NavMeshAgent[] ais = FindObjectsOfType<NavMeshAgent>();
            foreach (var ai in ais)
            {
                if (ai != null && ai.isOnNavMesh)
                {
                    ai.isStopped = true;
                }
            }
        }

        private void ResetTimeStop()
        {

            foreach (var kvp in rbs)
            {
                Rigidbody rb = kvp.Key;
                RigidbodyState state = kvp.Value;

                if (rb != null && rb != MonoSingleton<NewMovement>.Instance?.rb)
                {
                    rb.velocity = state.velocity;
                    rb.angularVelocity = state.angularVelocity;
                    rb.isKinematic = state.wasKinematic;
                }
            }
            rbs.Clear();

            var particleSystems = FindObjectsOfType<ParticleSystem>();
            for (int i = 0; i < particleSystems.Length; i++)
            {
                var ps = particleSystems[i];
                if (ps != null && (!ps.main.loop || !ps.isStopped))
                {
                    if (ps.isPaused)
                    {
                        ps.Play();
                    }

                }
            }

            for (int i = 0; i < pausedAnimators.Count; i++)
            {
                if (pausedAnimators[i].animator != null)
                {
                    pausedAnimators[i].animator.speed = pausedAnimators[i].speed;
                }
            }
            pausedAnimators.Clear();
            NavMeshAgent[] ais = FindObjectsOfType<NavMeshAgent>();
            foreach (var ai in ais)
            {
                if (ai != null && ai.isOnNavMesh)
                {
                    ai.isStopped = false;
                }
            }
        }
        public override void RemoveEffect()
        {
            ResetTimeStop();
            IsActive = false;
        }
        private bool ShouldSkipAnimator(Animator animator, NewMovement newMovement)
        {
            if (newMovement?.punch?.currentPunch?.anim == animator) return true;

            var gunControl = MonoSingleton<GunControl>.instance;
            if (gunControl?.currentWeapon != null)
            {
                var weaponAnimator = gunControl.currentWeapon.GetComponentInChildren<Animator>();
                if (weaponAnimator == animator) return true;
            }

            return false;
        }

        private bool ContainsAnimator(List<AnimatorState> list, Animator animator)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].animator == animator) return true;
            }
            return false;
        }
    }
    [HarmonyPatch(typeof(Explosion), nameof(Explosion.FixedUpdate))]
    public class StopExplosion
    {
        public static bool Prefix()
        {
            return !TimeStop.IsActive;
        }
    }
    [HarmonyPatch(typeof(TimeBomb), nameof(TimeBomb.Update))]
    public class StopMagnet
    {
        public static bool Prefix()
        {
            return !TimeStop.IsActive;
        }

    }
    [HarmonyPatch(typeof(PhysicalShockwave))]
    public class PhysicalShockwavePatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostfix(PhysicalShockwave __instance)
        {
            // Replace the original Invoke call with a coroutine
            if (__instance.fading)
            {
                __instance.StartCoroutine(TimeStopCoroutine(__instance, __instance.GetDestroyed, __instance.speed / 10f));
            }
        }

        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        public static bool UpdatePrefix(PhysicalShockwave __instance)
        {
            if (!TimeStop.IsActive)
            {
                // Only update when TimeStop is not active
                __instance.transform.localScale = new Vector3(
                    __instance.transform.localScale.x + Time.deltaTime * __instance.speed,
                    __instance.transform.localScale.y,
                    __instance.transform.localScale.z + Time.deltaTime * __instance.speed
                );

                if (!__instance.fading && (__instance.transform.localScale.x > __instance.maxSize || __instance.transform.localScale.z > __instance.maxSize))
                {
                    __instance.fading = true;
                    ScaleNFade[] array = __instance.GetComponentsInChildren<ScaleNFade>();
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i].enabled = true;
                    }
                    __instance.StartCoroutine(TimeStopCoroutine(__instance, __instance.GetDestroyed, __instance.speed / 10f));
                }
            }
            return false; // Skip the original Update method
        }

        private static IEnumerator TimeStopCoroutine(MonoBehaviour instance, System.Action action, float delay)
        {
            float elapsedTime = 0f;

            while (elapsedTime < delay)
            {
                if (!TimeStop.IsActive)
                {
                    elapsedTime += Time.deltaTime;
                }
                yield return null;
            }

            action.Invoke();
        }
    }
    [HarmonyPatch(typeof(Countdown), nameof(Countdown.Update))]
    public class StopCountdown
    {
        public static bool Prefix()
        {
            return !TimeStop.IsActive;
        }
    }
    [HarmonyPatch(typeof(Vector3), nameof(Vector3.MoveTowards))]
    public class StopMoveTowards
    {
        public static bool Prefix(ref Vector3 __result, Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            if (TimeStop.IsActive)
            {
                __result = current;
                return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(Quaternion), nameof(Quaternion.RotateTowards))]
    public class StopMoveTowardsQuaternion
    {
        public static bool Prefix(ref Quaternion __result, Quaternion from, Quaternion to, float maxDegreesDelta)
        {
            if (TimeStop.IsActive)
            {
                __result = from;
                return false;
            }
            return true;
        }
    }
    /*
    [HarmonyPatch(typeof(Mathf), nameof(Mathf.MoveTowards))]
    public class StopMoveTowardsMathf
    {
        public static bool Prefix(ref float __result, float current, float target, float maxDelta)
        {
            if (TimeStop.IsActive)
            {
                __result = current;
                return false;
            }
            return true;
        }
    }*/
    [HarmonyPatch(typeof(Nail), "Start")]
    public class SawbladeStartPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(Nail __instance)
        {
            // Replace the original method
            __instance.StartCoroutine(ModifiedStart(__instance));
            return false; // Skip the original method
        }

        private static IEnumerator ModifiedStart(Nail instance)
        {
            if (instance.sawblade)
            {
                instance.removeTimeMultiplier = 3f;
            }

            if (instance.magnets.Count == 0)
            {
                yield return TimeStopCoroutine(instance, () => instance.RemoveTime(), 5f * instance.removeTimeMultiplier);
            }

            yield return TimeStopCoroutine(instance, () => instance.MasterRemoveTime(), 60f);

            instance.startPosition = instance.transform.position;

            yield return TimeStopCoroutine(instance, () => instance.SlowUpdate(), 2f);
        }

        private static IEnumerator TimeStopCoroutine(MonoBehaviour instance, System.Action action, float delay)
        {
            float elapsedTime = 0f;

            while (elapsedTime < delay)
            {
                if (!TimeStop.IsActive)
                {
                    elapsedTime += Time.deltaTime;
                }
                yield return null;
            }

            action.Invoke();
        }
    }
    [HarmonyPatch(typeof(RevolverBeam), nameof(RevolverBeam.Update))]
    public class StopBeamDisappear
    {
        public static bool Prefix()
        {
            return !TimeStop.IsActive;
        }
    }
    [HarmonyPatch(typeof(EnemyIdentifier))]
    public class EnemyBehaviorPatch
    {
        [HarmonyPatch("AttackEnemies", MethodType.Getter)]
        [HarmonyPostfix]
        public static void AttackEnemiesPostfix(ref bool __result)
        {
            if (TimeStop.IsActive)
            {
                __result = false;
            }
        }

        [HarmonyPatch("IgnorePlayer", MethodType.Getter)]
        [HarmonyPostfix]
        public static void IgnorePlayerPostfix(ref bool __result)
        {
            if (TimeStop.IsActive)
            {
                __result = true;
            }
        }
    }
    [HarmonyPatch(typeof(RemoveOnTime))]
    public class RemoveOnTimePatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostfix(RemoveOnTime __instance)
        {
            if (__instance.useAudioLength)
            {
                // For audio-based removal, we'll use a coroutine
                __instance.StartCoroutine(RemoveCoroutine(__instance));
            }
            else
            {
                // For time-based removal, we'll replace the original Invoke
                __instance.CancelInvoke("Remove");
                __instance.StartCoroutine(RemoveCoroutine(__instance));
            }
        }

        private static IEnumerator RemoveCoroutine(RemoveOnTime instance)
        {
            float elapsedTime = 0f;
            float targetTime;

            if (instance.useAudioLength)
            {
                AudioSource audioSource = instance.GetComponent<AudioSource>();
                targetTime = audioSource.clip.length * audioSource.pitch;
            }
            else
            {
                targetTime = instance.time + UnityEngine.Random.Range(-instance.randomizer, instance.randomizer);
            }

            while (elapsedTime < targetTime)
            {
                // Check if TimeStop is active
                if (!TimeStop.IsActive)
                {
                    elapsedTime += Time.deltaTime;
                }
                yield return null;
            }

            // Call the original Remove method
            instance.SendMessage("Remove");
        }

        [HarmonyPatch("Remove")]
        [HarmonyPrefix]
        public static bool RemovePrefix(RemoveOnTime __instance)
        {
            if (__instance.affectedByNoCooldowns && NoWeaponCooldown.NoCooldown)
            {
                __instance.StartCoroutine(RemoveCoroutine(__instance));
                return false; // Skip the original method
            }
            return true; // Continue with the original method
        }
    }
}
