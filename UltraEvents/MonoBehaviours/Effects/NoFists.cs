using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    internal class NoFists : Effect
    {
        void Start()
        {
            if(MonoSingleton<PlayerUtilities>.instance == null)
                gameObject.AddComponent<PlayerUtilities>();
            MonoSingleton<PlayerUtilities>.instance.NoFist();
        }
        public override void RemoveEffect()
        {
            MonoSingleton<PlayerUtilities>.instance.YesFist();
        }
    }
}
