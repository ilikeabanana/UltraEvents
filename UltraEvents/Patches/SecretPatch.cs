using System;
using HarmonyLib;
using UnityEngine;

namespace UltraEvents.Patches
{
    // Token: 0x02000008 RID: 8
    [HarmonyPatch(typeof(Bonus), "OnTriggerEnter")]
    public class SecretPatch
    {
        // Token: 0x0600006C RID: 108 RVA: 0x00005C84 File Offset: 0x00003E84
        [HarmonyPostfix]
        private static void post(ref bool ___activated, Collider other)
        {
            bool flag = !UltraEventsPlugin.OnSecretReceived.Value;
            if (!flag)
            {
                bool flag2 = other.gameObject.CompareTag("Player") && !___activated;
                if (flag2)
                {
                    UltraEventsPlugin.Instance.UseRandomEventAndRemoveEffects();
                }
            }
        }
    }
}
