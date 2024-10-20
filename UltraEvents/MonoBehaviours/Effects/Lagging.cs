using System.Collections;
using UltraEvents.Utils;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x0200001C RID: 28
    public class Lagging : Effect
    {
        // Token: 0x060000B4 RID: 180 RVA: 0x000070B2 File Offset: 0x000052B2
        private void Awake()
        {
            base.StartCoroutine("lag");
        }

        // Token: 0x060000B5 RID: 181 RVA: 0x000070C1 File Offset: 0x000052C1
        private IEnumerator lag()
        {
            while (this.isgud)
            {
                // Store initial player transform data
                Vector3 pos = ModUtils.GetPlayerTransform().transform.position;
                Vector3 vel = ModUtils.GetPlayerTransform().rb.velocity;
                float charge = ModUtils.GetPlayerTransform().boostCharge;
                int health = ModUtils.GetPlayerTransform().hp;

                // Store weapon charge data
                float railCooldown = MonoSingleton<WeaponCharges>.instance.raicharge;
                float ammo = MonoSingleton<WeaponCharges>.instance.naiAmmo;
                float rev0charge = MonoSingleton<WeaponCharges>.instance.rev0charge;
                float rev1charge = MonoSingleton<WeaponCharges>.instance.rev1charge;
                float rev2charge = MonoSingleton<WeaponCharges>.instance.rev2charge;
                float rocketCannonballCharge = MonoSingleton<WeaponCharges>.instance.rocketCannonballCharge;
                float freezeTime = MonoSingleton<WeaponCharges>.instance.rocketFreezeTime;
                float fuel = MonoSingleton<WeaponCharges>.instance.rocketNapalmFuel;

                // Wait for a random interval
                yield return new WaitForSeconds(Random.Range(0, seconds));

                // Reset the player's position, velocity, and health
                ModUtils.GetPlayerTransform().transform.position = pos;
                ModUtils.GetPlayerTransform().rb.velocity = vel;

                // Restore player's health
                if (health - ModUtils.GetPlayerTransform().hp > 1)
                {
                    ModUtils.GetPlayerTransform().GetHealth(health - ModUtils.GetPlayerTransform().hp, true);
                }

                // Reset boost charge
                ModUtils.GetPlayerTransform().boostCharge = charge;

                // Reset weapon charges
                MonoSingleton<WeaponCharges>.instance.raicharge = railCooldown;
                MonoSingleton<WeaponCharges>.instance.naiAmmo = ammo;
                MonoSingleton<WeaponCharges>.instance.rev0charge = rev0charge;
                MonoSingleton<WeaponCharges>.instance.rev1charge = rev1charge;
                MonoSingleton<WeaponCharges>.instance.rev2charge = rev2charge;
                MonoSingleton<WeaponCharges>.instance.rocketCannonballCharge = rocketCannonballCharge;
                MonoSingleton<WeaponCharges>.instance.rocketFreezeTime = freezeTime;
                MonoSingleton<WeaponCharges>.instance.rocketNapalmFuel = fuel;

                // Wait for another random interval before looping
                yield return new WaitForSeconds(Random.Range(0, seconds));

                // Reset position to default
                pos = default(Vector3);
            }

            yield break;
        }

        // Token: 0x0400007A RID: 122
        private bool isgud = true;
        // Token: 0x0400007B RID: 123
        private float seconds = 1f;
    }
}
