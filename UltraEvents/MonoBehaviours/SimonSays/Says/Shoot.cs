using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraEvents.MonoBehaviours.SimonSays.Says
{
    internal class Shoot : SimonSaysIt
    {
        public override string task => "Shoot";
        public override bool checkIfDone()
        {
            if(MonoSingleton<InputManager>.Instance.InputSource.Fire1.IsPressed || MonoSingleton<InputManager>.Instance.InputSource.Fire2.IsPressed)
            {
                return true;
            }
            return false;
        }
    }
}
