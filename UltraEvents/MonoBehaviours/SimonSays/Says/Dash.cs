using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraEvents.MonoBehaviours.SimonSays.Says
{
    internal class Dash : SimonSaysIt
    {
        public override string task => "Dash";

        public override bool checkIfDone()
        {
            if (MonoSingleton<InputManager>.Instance.InputSource.Dodge.WasPerformedThisFrame)
            {
                return true;
            }
            return false;
        }
    }

}
