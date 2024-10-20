using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    internal class NoHudEffect : Effect
    {
        void Start()
        {
            
            HudController.Instance.gameObject.SetActive(false);
            
        }
        public override void RemoveEffect()
        {
            HudController.Instance.gameObject.SetActive(true);
            base.RemoveEffect();
        }
    }
}
