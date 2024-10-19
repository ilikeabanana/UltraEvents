using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    internal class GottaGoQuick : Effect
    {
        GameObject trail;

        void Start()
        {
            MonoSingleton<NewMovement>.instance.walkSpeed *= 5;
            trail = Instantiate(UltraEventsPlugin.BlueTrail, MonoSingleton<NewMovement>.instance.transform.position, Quaternion.identity);
            trail.transform.parent = MonoSingleton<NewMovement>.instance.transform;
        }
        public override void RemoveEffect()
        {
            if (trail == null) return;
            MonoSingleton<NewMovement>.instance.walkSpeed /= 5;
            Destroy(trail);
        }
    }
}
