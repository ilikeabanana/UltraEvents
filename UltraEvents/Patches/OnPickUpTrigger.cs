using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraEvents.Patches
{
    [HarmonyPatch(typeof(ItemIdentifier), nameof(ItemIdentifier.PickUp))]
    public class OnPickUpTrigger
    {
        public static void Postfix()
        {
            if (!UltraEventsPlugin.PickUp.Value) return;
            UltraEventsPlugin.Instance.UseRandomEventAndRemoveEffects();
        }
    }
}
