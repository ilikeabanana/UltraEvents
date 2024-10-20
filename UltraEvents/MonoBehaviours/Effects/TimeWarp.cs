using UnityEngine;
using System.Collections;

namespace UltraEvents.MonoBehaviours.Effects
{
    internal class TimeWarp : Effect
    {
        private bool isWarpActive = true;

        private void Start()
        {
            StartCoroutine(TimeWarpRoutine());
        }

        private IEnumerator TimeWarpRoutine()
        {
            while (isWarpActive)
            {
                float time = Random.Range(0f, 3f);
                float delay = Random.Range(0f, 1.5f); // Random delay between changes

                // Apply the random time scale
                Time.timeScale = time;
                MonoSingleton<TimeController>.Instance.timeScaleModifier = time;

                // Wait for the random delay before the next warp
                yield return new WaitForSecondsRealtime(delay);
            }
        }

        public void Update()
        {
            // Additional logic can be placed here if necessary
        }

        public override void RemoveEffect()
        {
            // Stop the time warp and reset to default values
            isWarpActive = false;
            StopCoroutine(TimeWarpRoutine());

            Time.timeScale = 1f;
            MonoSingleton<TimeController>.Instance.timeScaleModifier = 1f;
            base.RemoveEffect();
        }
    }
}
