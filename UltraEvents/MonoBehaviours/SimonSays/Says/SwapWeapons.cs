using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraEvents.MonoBehaviours.SimonSays.Says
{
    internal class SwapWeapons : SimonSaysIt
    {
        public override string task => "Swap your weapon";

        public static bool checking;

        void Awake()
        {
            checking = true;
        }

        public override bool checkIfDone()
        {
            if (OnWeaponSwap.swapped)
            {
                OnWeaponSwap.swapped = false;
                checking = false;
                return true;
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(GunControl))]
    public class OnWeaponSwap
    {
        public static bool swapped;
        [HarmonyPatch(nameof(GunControl.SwitchWeapon))]
        public static void Postfix()
        {
            if(SwapWeapons.checking)
                swapped = true;
        }


    }
}
