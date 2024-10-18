﻿using HarmonyLib;
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
                punch.force = 1000;
            }
        }
        public override void RemoveEffect()
        {
            foreach (Punch punch in punches)
            {
                FistType fistType = punch.type;
                if (fistType == FistType.Standard)
                {

                    punch.force = 25f;
                    return;
                }
                if (fistType != FistType.Heavy)
                {
                    return;
                }
                punch.force = 100f;
            }
            base.RemoveEffect();
        }
    }
}
