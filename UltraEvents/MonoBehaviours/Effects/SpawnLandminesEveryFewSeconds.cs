using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    public class SpawnLandminesEveryFewSeconds : Effect
    {
        float AmountOfLandmines;
        void Start()
        {
            AmountOfLandmines = Random.Range(10, 30);
            StartCoroutine(spawnLandmines());
        }
        IEnumerator spawnLandmines()
        {
            while (true)
            {
                Instantiate(UltraEventsPlugin.Ladnmine, MonoSingleton<NewMovement>.instance.transform.position, Quaternion.identity);
                yield return new WaitForSecondsRealtime(UltraEventsPlugin.Instance.AmountOfTime.Value / AmountOfLandmines);
            }
        }
        public override void RemoveEffect()
        {
            StopAllCoroutines();
        }
    }
}
