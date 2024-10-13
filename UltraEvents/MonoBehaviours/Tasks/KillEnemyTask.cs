using System.Collections.Generic;
using System.Linq;
using UltraEvents.Utils;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Tasks
{
    // Token: 0x02000013 RID: 19
    public class KillEnemyTask : Task
    {
        // Token: 0x17000008 RID: 8
        // (get) Token: 0x06000095 RID: 149 RVA: 0x0000664D File Offset: 0x0000484D
        // (set) Token: 0x06000096 RID: 150 RVA: 0x00006654 File Offset: 0x00004854
        public override string ToDo
        {
            get
            {
                return "Kill an enemy";
            }
            set
            {
                base.ToDo = value;
            }
        }

        // Token: 0x06000097 RID: 151 RVA: 0x00006660 File Offset: 0x00004860
        public override void TheirAwake()
        {
            this.amount = MonoSingleton<StatsManager>.Instance.kills + 1;
            this.TimeToFinish = 15f;
            MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("you have 15 seconds to kill an enemy", "", "", 0, false);
            base.TheirAwake();
        }

        // Token: 0x06000098 RID: 152 RVA: 0x000066B0 File Offset: 0x000048B0
        public override void CheckIfWon()
        {
            bool flag = MonoSingleton<StatsManager>.Instance.kills >= this.amount;
            if (flag)
            {
                MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("YOU RECEIVE: EVERYONE DEAD", "", "", 0, false);
                foreach (EnemyIdentifier enemyIdentifier in MonoSingleton<EnemyTracker>.Instance.GetCurrentEnemies())
                {
                    enemyIdentifier.InstaKill();
                }
                this.Won();
            }
            base.CheckIfWon();
        }

        // Token: 0x06000099 RID: 153 RVA: 0x00006754 File Offset: 0x00004954
        public override void TimeRanOut()
        {
            bool flag = Physics.gravity.y < 0f;
            if (flag)
            {
                Physics.gravity *= -1f;
            }
            else
            {
                List<SpawnableObject> list = Resources.FindObjectsOfTypeAll<SpawnableObject>().ToList<SpawnableObject>();
                list.RemoveAll((SpawnableObject x) => x.spawnableObjectType != SpawnableObject.SpawnableObjectDataType.Enemy);
                SpawnableObject spawnableObject = list[Random.Range(0, list.Count)];
                Object.Instantiate<GameObject>(spawnableObject.gameObject, ModUtils.GetPlayerTransform().transform.position, Quaternion.identity);
            }
            base.TimeRanOut();
        }

        // Token: 0x04000072 RID: 114
        private bool done = false;

        // Token: 0x04000073 RID: 115
        private int amount;
    }
}
