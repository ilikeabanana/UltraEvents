using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000018 RID: 24
    public class BlessAll : Effect
    {
        // Token: 0x060000AA RID: 170 RVA: 0x00006D6C File Offset: 0x00004F6C
        private void Update()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.blessed);
            bool flag = list.Count > 0;
            if (flag)
            {
                foreach (EnemyIdentifier enemyIdentifier in list)
                {
                    enemyIdentifier.Bless(false);
                }
            }
        }

        // Token: 0x060000AB RID: 171 RVA: 0x00006E00 File Offset: 0x00005000
        public override void RemoveEffect()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => !x.blessed);
            bool flag = list.Count > 0;
            if (flag)
            {
                foreach (EnemyIdentifier enemyIdentifier in list)
                {
                    enemyIdentifier.Unbless(false);
                }
            }
            base.RemoveEffect();
        }
    }
}
