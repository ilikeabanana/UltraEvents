using System;
using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace UltraEvents.Patches
{
    // Token: 0x0200000C RID: 12
    [HarmonyPatch(typeof(StyleHUD), "AddPoints")]
    public class GetStylePatch
    {
        // Token: 0x06000075 RID: 117 RVA: 0x00005DB0 File Offset: 0x00003FB0
        public static void Postfix()
        {
            bool flag = !UltraEventsPlugin.GetStyle.Value;
            if (!flag)
            {
                UltraEventsPlugin.Log.LogInfo(GetStylePatch._isProcessing);
                bool isProcessing = GetStylePatch._isProcessing;
                if (!isProcessing)
                {
                    UltraEventsPlugin.Instance.StartCoroutine(GetStylePatch.DelayedEvent());
                }
            }
        }

        // Token: 0x06000076 RID: 118 RVA: 0x00005E01 File Offset: 0x00004001
        private static IEnumerator DelayedEvent()
        {
            GetStylePatch._isProcessing = true;
            yield return new WaitForSecondsRealtime(0.1f);
            GetStylePatch._isProcessing = false;
            UltraEventsPlugin.Instance.UseRandomEventAndRemoveEffects();
            yield break;
        }

        // Token: 0x04000062 RID: 98
        private static bool _isProcessing;
    }
}
