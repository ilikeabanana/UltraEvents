using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x0200001A RID: 26
    internal class ExplodingBulletsEffect : Effect
    {
        // Token: 0x060000AF RID: 175 RVA: 0x00006F14 File Offset: 0x00005114
        public void Update()
        {
            Projectile[] array = Object.FindObjectsOfType<Projectile>();
            foreach (Projectile projectile in array)
            {
                bool flag = projectile == null || this.projectilespo.Contains(projectile);
                if (!flag)
                {
                    List<ExplosionController> list = Resources.FindObjectsOfTypeAll<ExplosionController>().ToList<ExplosionController>();
                    list.RemoveAll((ExplosionController x) => x.name.ToLower().Contains("fire"));
                    ExplosionController explosionController = list[Random.Range(0, list.Count)];
                    projectile.explosionEffect = explosionController.gameObject;
                    this.projectilespo.Add(projectile);
                }
            }
        }

        // Token: 0x04000079 RID: 121
        private List<Projectile> projectilespo = new List<Projectile>();
    }
}
