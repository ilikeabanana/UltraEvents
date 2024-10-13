using System.Collections.Generic;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000024 RID: 36
    internal class sSchizophreniaUpdateEffect : Effect
    {
        // Token: 0x060000D0 RID: 208 RVA: 0x000079EB File Offset: 0x00005BEB
        private void Update()
        {
            this.OptimizeObjectType<EnemyIdentifier>();
            this.OptimizeObjectType<GoreSplatter>();
            this.OptimizeObjectType<ExplosionController>();
        }

        // Token: 0x060000D1 RID: 209 RVA: 0x00007A04 File Offset: 0x00005C04
        private void OptimizeObjectType<T>() where T : Component
        {
            T[] array = Object.FindObjectsOfType<T>();
            foreach (T t in array)
            {
                Renderer[] componentsInChildren = t.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in componentsInChildren)
                {
                    bool flag = !this.alreadyDoneRenderers.Contains(renderer);
                    if (flag)
                    {
                        RemoveOnUnseen removeOnUnseen = renderer.gameObject.AddComponent<RemoveOnUnseen>();
                        removeOnUnseen.theObject = t.gameObject;
                        this.alreadyDoneRenderers.Add(renderer);
                    }
                }
            }
        }

        // Token: 0x04000088 RID: 136
        private HashSet<Renderer> alreadyDoneRenderers = new HashSet<Renderer>();
    }
}
