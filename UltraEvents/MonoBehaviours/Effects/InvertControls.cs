using HarmonyLib;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UltraEvents.MonoBehaviours.Effects
{

    [HarmonyPatch]
    internal class InvertControls : Effect
    {
        static bool IsActive = false;
        void Start()
        {
            IsActive = true;
        }
        public override void RemoveEffect()
        {
            IsActive = false;
        }

        static MethodBase TargetMethod()
        {
            return typeof(InputActionState).GetMethod("ReadValue").MakeGenericMethod(typeof(Vector2));
        }

        static void Postfix(ref Vector2 __result, InputActionState __instance)
        {
            if (!IsActive) return;
            if (__instance.Action.name != "Move") return;
            __result = new Vector2(-__result.x, -__result.y);
        }
    }
}
