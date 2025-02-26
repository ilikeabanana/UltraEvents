using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // TODO Review this file and update to your own requirements, or remove it altogether if not required
    /// <summary>
    /// Template MonoBehaviour class. Use this to add new functionality and behaviours to
    /// the game.
    /// </summary>
    internal class ScreenDistortion : Effect
    {
        public void Awake()
        {
            ScreenDistortionField distortion = MonoSingleton<CameraController>.Instance.gameObject.AddComponent<ScreenDistortionField>();
            distortion.distance = 5000;
        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
            Destroy(MonoSingleton<CameraController>.Instance.gameObject.GetComponent<ScreenDistortionField>());
        }
    }
}
