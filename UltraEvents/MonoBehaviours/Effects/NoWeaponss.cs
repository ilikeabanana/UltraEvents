using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraEvents.MonoBehaviours.Effects
{
    internal class NoWeaponss : Effect
    {
        void Start()
        {
            if (MonoSingleton<PlayerUtilities>.instance == null)
                gameObject.AddComponent<PlayerUtilities>();
            MonoSingleton<PlayerUtilities>.instance.NoWeapon();
        }
        public override void RemoveEffect()
        {
            MonoSingleton<PlayerUtilities>.instance.YesWeapon();
        }
    }
}
