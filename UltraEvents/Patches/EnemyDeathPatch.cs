using System;
using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace UltraEvents.Patches
{
    // Token: 0x0200000A RID: 10
    [HarmonyPatch(typeof(EnemyIdentifier), "Death", new Type[]
    {

    })]
    public class EnemyDeathPatch
    {
        // Token: 0x06000070 RID: 112 RVA: 0x00005D14 File Offset: 0x00003F14
        public static void Postfix()
        {
            bool flag = !UltraEventsPlugin.OnEnemyDeath.Value;
            if (!flag)
            {
                UltraEventsPlugin.Log.LogInfo(EnemyDeathPatch._isProcessing);
                bool isProcessing = EnemyDeathPatch._isProcessing;
                if (!isProcessing)
                {
                    UltraEventsPlugin.Instance.StartCoroutine(EnemyDeathPatch.DelayedEvent());
                }
            }
        }

        // Token: 0x06000071 RID: 113 RVA: 0x00005D65 File Offset: 0x00003F65
        private static IEnumerator DelayedEvent()
        {
            EnemyDeathPatch._isProcessing = true;
            yield return new WaitForSecondsRealtime(0.1f);
            EnemyDeathPatch._isProcessing = false;
            UltraEventsPlugin.Instance.UseRandomEventAndRemoveEffects();
            yield break;
        }

        // Token: 0x04000061 RID: 97
        private static bool _isProcessing;
    }
}
