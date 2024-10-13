using System;
using HarmonyLib;

namespace UltraEvents.Patches
{
    // Token: 0x02000009 RID: 9
    [HarmonyPatch(typeof(TimeController), "ParryFlash")]
    public class ParryPatch
    {
        // Token: 0x0600006E RID: 110 RVA: 0x00005CDC File Offset: 0x00003EDC
        public static void Postfix()
        {
            bool flag = !UltraEventsPlugin.OnParry.Value;
            if (!flag)
            {
                UltraEventsPlugin.Instance.UseRandomEventAndRemoveEffects();
            }
        }
    }
}
