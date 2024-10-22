using System.Collections;
using System.Collections.Generic;
using UltraEvents.Utils;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    public class Lagging : Effect
    {
        private void Awake()
        {
            positionBuffer = new Queue<PositionSnapshot>();
            base.StartCoroutine("lag");
        }

        private class PositionSnapshot
        {
            public Vector3 position;
            public Vector3 velocity;
            public float timeStamp;
            public float boostCharge;
            public int health;
            public WeaponCharges weaponCharges;

            public PositionSnapshot(Transform transform, Rigidbody rb, float boost, int hp)
            {
                position = transform.position;
                velocity = rb.velocity;
                timeStamp = Time.time;
                boostCharge = boost;
                health = hp;
                weaponCharges = new WeaponCharges
                {
                    raicharge = MonoSingleton<WeaponCharges>.instance.raicharge,
                    naiAmmo = MonoSingleton<WeaponCharges>.instance.naiAmmo,
                    rev0charge = MonoSingleton<WeaponCharges>.instance.rev0charge,
                    rev1charge = MonoSingleton<WeaponCharges>.instance.rev1charge,
                    rev2charge = MonoSingleton<WeaponCharges>.instance.rev2charge,
                    rocketCannonballCharge = MonoSingleton<WeaponCharges>.instance.rocketCannonballCharge,
                    rocketFreezeTime = MonoSingleton<WeaponCharges>.instance.rocketFreezeTime,
                    rocketNapalmFuel = MonoSingleton<WeaponCharges>.instance.rocketNapalmFuel
                };
            }
        }

        private IEnumerator lag()
        {
            float lastUpdateTime = 0;
            float nextStutterTime = 0;

            while (isEnabled)
            {
                // Record current position every frame
                var playerTransform = ModUtils.GetPlayerTransform();
                positionBuffer.Enqueue(new PositionSnapshot(
                    playerTransform.transform,
                    playerTransform.rb,
                    playerTransform.boostCharge,
                    playerTransform.hp
                ));

                // Keep only last 2 seconds of positions
                while (positionBuffer.Count > 0 &&
                       Time.time - positionBuffer.Peek().timeStamp > maxBufferTime)
                {
                    positionBuffer.Dequeue();
                }

                // Create random lag spikes
                if (Time.time >= nextStutterTime)
                {
                    float stutterDuration = Random.Range(minStutterDuration, maxStutterDuration);
                    yield return StartCoroutine(CreateLagSpike(stutterDuration));

                    // Schedule next stutter
                    nextStutterTime = Time.time + Random.Range(minTimeBetweenStutters, maxTimeBetweenStutters);
                }

                yield return null;
            }
        }

        private IEnumerator CreateLagSpike(float duration)
        {
            if (positionBuffer.Count < 2) yield break;

            float startTime = Time.time;
            var oldestSnapshot = positionBuffer.Peek();
            var playerTransform = ModUtils.GetPlayerTransform();
            var newMovement = playerTransform.GetComponent<NewMovement>();

            // Freeze player in place
            Vector3 originalPosition = playerTransform.transform.position;
            Vector3 originalVelocity = playerTransform.rb.velocity;
            float originalBoostCharge = newMovement.boostCharge;

            while (Time.time - startTime < duration)
            {
                // Create stuttering effect
                if (Random.value < stutterChance)
                {
                    playerTransform.transform.position = oldestSnapshot.position;
                    playerTransform.rb.velocity = oldestSnapshot.velocity;
                    newMovement.boostCharge = oldestSnapshot.boostCharge;

                    // Apply old weapon charges for consistency
                    MonoSingleton<WeaponCharges>.instance.raicharge = oldestSnapshot.weaponCharges.raicharge;
                    MonoSingleton<WeaponCharges>.instance.naiAmmo = oldestSnapshot.weaponCharges.naiAmmo;
                    MonoSingleton<WeaponCharges>.instance.rev0charge = oldestSnapshot.weaponCharges.rev0charge;
                    MonoSingleton<WeaponCharges>.instance.rev1charge = oldestSnapshot.weaponCharges.rev1charge;
                    MonoSingleton<WeaponCharges>.instance.rev2charge = oldestSnapshot.weaponCharges.rev2charge;
                    MonoSingleton<WeaponCharges>.instance.rocketCannonballCharge = oldestSnapshot.weaponCharges.rocketCannonballCharge;
                    MonoSingleton<WeaponCharges>.instance.rocketFreezeTime = oldestSnapshot.weaponCharges.rocketFreezeTime;
                    MonoSingleton<WeaponCharges>.instance.rocketNapalmFuel = oldestSnapshot.weaponCharges.rocketNapalmFuel;

                    // Health rubber-banding - now using GetHurt() for damage
                    int healthDiff = oldestSnapshot.health - playerTransform.hp;
                    if (healthDiff < 0)  // Player took damage
                    {
                        playerTransform.GetHurt(Mathf.Abs(healthDiff), false);
                    }
                    else if (healthDiff > 0)  // Player gained health
                    {
                        playerTransform.GetHealth(healthDiff, true);
                    }

                    yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
                }

                // Occasionally jump forward to catch up
                if (Random.value < catchupChance)
                {
                    playerTransform.transform.position = originalPosition;
                    playerTransform.rb.velocity = originalVelocity;
                    newMovement.boostCharge = originalBoostCharge;
                    yield return new WaitForSeconds(Random.Range(0.02f, 0.08f));
                }

                yield return null;
            }
        }

        private Queue<PositionSnapshot> positionBuffer;
        private bool isEnabled = true;

        // Configurable parameters
        private float maxBufferTime = 2f; // Maximum time to keep position history
        private float minStutterDuration = 0.2f;
        private float maxStutterDuration = 0.8f;
        private float minTimeBetweenStutters = 0.5f;
        private float maxTimeBetweenStutters = 2f;
        private float stutterChance = 0.7f; // Chance to stutter during a lag spike
        private float catchupChance = 0.3f; // Chance to briefly catch up to current position
    }
}