using System.Collections.Generic;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000019 RID: 25
    public class BulletsAfraidEnemies : Effect
    {
        private HashSet<EnemyIdentifier> enemiesProcessed = new HashSet<EnemyIdentifier>();
        private List<BounceOffProjectiles> addedComponents = new List<BounceOffProjectiles>();

        private void Update()
        {
            var enemies = Object.FindObjectsOfType<EnemyIdentifier>();
            foreach (var enemy in enemies)
            {
                if (!enemiesProcessed.Contains(enemy) && !enemy.dead)
                {
                    var bounceComponent = enemy.gameObject.AddComponent<BounceOffProjectiles>();
                    addedComponents.Add(bounceComponent);
                    enemiesProcessed.Add(enemy);
                }
            }
        }

        public override void RemoveEffect()
        {
            foreach (var component in addedComponents)
            {
                if (component != null)
                {
                    Destroy(component);
                }
            }
            addedComponents.Clear();
            enemiesProcessed.Clear();
            base.RemoveEffect();
        }
    }
}
