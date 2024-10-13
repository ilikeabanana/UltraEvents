using System;
using HarmonyLib;

namespace UltraEvents.Patches
{
    // Token: 0x02000006 RID: 6
    [HarmonyPatch(typeof(Revolver), "Update")]
    internal class AutoFire
    {
        // Token: 0x06000068 RID: 104 RVA: 0x00005C1C File Offset: 0x00003E1C
        public static bool Prefix(ref Revolver __instance)
        {
            bool flag = !UltraEventsPlugin.AutomaticFireEffectActive;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                __instance.ReadyGun();
                result = true;
            }
            return result;
        }
    }
    [HarmonyPatch(typeof(Shotgun), "Update")]
    internal class AutoFireShot
    {
        // Token: 0x0600006A RID: 106 RVA: 0x00005C50 File Offset: 0x00003E50
        public static bool Prefix(ref Shotgun __instance)
        {
            bool flag = !UltraEventsPlugin.AutomaticFireEffectActive;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                __instance.ReadyGun();
                result = true;
            }
            return result;
        }
    }
}
