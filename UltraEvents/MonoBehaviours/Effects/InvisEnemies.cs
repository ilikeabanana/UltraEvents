using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x0200001B RID: 27
    public class InvisEnemies : Effect
    {
        // Token: 0x060000B1 RID: 177 RVA: 0x00006FDC File Offset: 0x000051DC
        private void Update()
        {
            EnemyIdentifier[] array = Object.FindObjectsOfType<EnemyIdentifier>();
            foreach (EnemyIdentifier enemyIdentifier in array)
            {
                Renderer[] componentsInChildren = enemyIdentifier.gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in componentsInChildren)
                {
                    renderer.enabled = false;
                }
            }
        }

        // Token: 0x060000B2 RID: 178 RVA: 0x00007040 File Offset: 0x00005240
        public override void RemoveEffect()
        {
            EnemyIdentifier[] array = Object.FindObjectsOfType<EnemyIdentifier>();
            foreach (EnemyIdentifier enemyIdentifier in array)
            {
                Renderer[] componentsInChildren = enemyIdentifier.gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in componentsInChildren)
                {
                    renderer.enabled = true;
                }
            }
            base.RemoveEffect();
        }
    }
}
