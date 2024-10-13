using System;
using HarmonyLib;

namespace UltraEvents.Patches
{
    // Token: 0x0200000B RID: 11
    [HarmonyPatch(typeof(NewMovement), "GetHurt")]
    public class HurtPlayerPatch
    {
        // Token: 0x06000073 RID: 115 RVA: 0x00005D78 File Offset: 0x00003F78
        public static void Postfix()
        {
            bool flag = !UltraEventsPlugin.GetHurt.Value;
            if (!flag)
            {
                UltraEventsPlugin.Instance.UseRandomEventAndRemoveEffects();
            }
        }
    }
}
