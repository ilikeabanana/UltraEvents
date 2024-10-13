using System.Collections.Generic;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000019 RID: 25
    public class BulletsAfraidEnemies : Effect
    {
        // Token: 0x060000AD RID: 173 RVA: 0x00006EA8 File Offset: 0x000050A8
        private void Update()
        {
            EnemyIdentifier[] array = Object.FindObjectsOfType<EnemyIdentifier>();
            foreach (EnemyIdentifier enemyIdentifier in array)
            {
                bool flag = this.enemiesIveDone.Contains(enemyIdentifier);
                if (!flag)
                {
                    enemyIdentifier.gameObject.AddComponent<BounceOffProjectiles>();
                    this.enemiesIveDone.Add(enemyIdentifier);
                }
            }
        }

        // Token: 0x04000078 RID: 120
        private List<EnemyIdentifier> enemiesIveDone = new List<EnemyIdentifier>();
    }
}
