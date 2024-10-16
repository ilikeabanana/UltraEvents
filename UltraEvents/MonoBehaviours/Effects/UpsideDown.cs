using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // TODO Review this file and update to your own requirements, or remove it altogether if not required
    /// <summary>
    /// Template MonoBehaviour class. Use this to add new functionality and behaviours to
    /// the game.
    /// </summary>
    internal class UpsideDown : Effect
    {
        

        void Start()
        {
                UltraEventsPlugin.Instance.UpsideDown();
        }
        public override void RemoveEffect()
        {
            UltraEventsPlugin.Instance.ResetScreen();
        }
    }
}
