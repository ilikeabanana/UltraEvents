using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraEvents.Utils;

namespace UltraEvents.MonoBehaviours.Effects
{
    internal class FALCONPUNCH : Effect
    {
        List<Punch> punches = new List<Punch>();
        void Start()
        {
            punches = ModUtils.GetEverythingOfType<Punch>();
        }

        void Update()
        {
            foreach(Punch punch in punches)
            {
                punch.force = UltraEventsPlugin.Instance.FalconPunchPower.Value * 100;
                punch.damage = UltraEventsPlugin.Instance.FalconPunchPower.Value;
            }
        }
        public override void RemoveEffect()
        {
            foreach (Punch punch in punches)
            {
                FistType fistType = punch.type;
                if (fistType == FistType.Standard)
                {
                    punch.damage = 1;
                    punch.force = 25f;
                    return;
                }
                if (fistType != FistType.Heavy)
                {
                    return;
                }
                punch.force = 100f;
                punch.damage = 2.5f;
            }
            base.RemoveEffect();
        }
    }
}
