using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace UltraEvents.Patches
{
    // TODO Review this file and update to your own requirements, or remove it altogether if not required

    /// <summary>
    /// Sample Harmony Patch class. Suggestion is to use one file per patched class
    /// though you can include multiple patch classes in one file.
    /// Below is included as an example, and should be replaced by classes and methods
    /// for your mod.
    /// </summary>
    [HarmonyPatch(typeof(GunControl))]
    internal class OnWeaponSwapTrigger
    {
        [HarmonyPatch(nameof(GunControl.SwitchWeapon))]
        public static void Postfix()
        {
            if (!UltraEventsPlugin.WeaponSwap.Value) return;
            UltraEventsPlugin.Instance.UseRandomEventAndRemoveEffects();
        }

        
    }
}