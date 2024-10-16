using HarmonyLib;

namespace UltraEvents.MonoBehaviours.Effects
{
    // TODO Review this file and update to your own requirements, or remove it altogether if not required

    /// <summary>
    /// Sample Harmony Patch class. Suggestion is to use one file per patched class
    /// though you can include multiple patch classes in one file.
    /// Below is included as an example, and should be replaced by classes and methods
    /// for your mod.
    /// </summary>
    [HarmonyPatch(typeof(CameraController))]
    internal class CameraShake : Effect
    {
        static bool IsActive = false;
        void Start()
        {
            MonoSingleton<CameraController>.instance.cameraShaking = 50;
            IsActive = true;
        }
        void Update()
        {
            MonoSingleton<CameraController>.instance.cameraShaking = 50;
        }
        public override void RemoveEffect()
        {
            IsActive = false;
            CameraController.instance.StopShake();
        }

        [HarmonyPatch(nameof(CameraController.StopShake))]
        public static bool Prefix()
        {
            return !IsActive;
        }
    }
}