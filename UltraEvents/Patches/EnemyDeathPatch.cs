using System;
using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace UltraEvents.Patches
{
    // Token: 0x0200000A RID: 10
    [HarmonyPatch(typeof(EnemyIdentifier), nameof(EnemyIdentifier.Start))]
    public class EnemyDeathPatch
    {
        // Token: 0x06000070 RID: 112 RVA: 0x00005D14 File Offset: 0x00003F14
        public static void Postfix(EnemyIdentifier __instance)
        {
            __instance.onDeath.AddListener(() =>
            {
                if (!UltraEventsPlugin.OnEnemyDeath.Value) return;
                UltraEventsPlugin.Instance.UseRandomEventAndRemoveEffects();
            });

        }

    }
}
