using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraEvents.MonoBehaviours.Effects
{
    internal class NoFists : Effect
    {
        void Start()
        {
            MonoSingleton<PlayerUtilities>.instance.NoFist();
        }
        public override void RemoveEffect()
        {
            MonoSingleton<PlayerUtilities>.instance.YesFist();
        }
    }
}
