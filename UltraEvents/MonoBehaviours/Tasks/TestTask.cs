using UltraEvents.Utils;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Tasks
{
    // Token: 0x02000014 RID: 20
    public class TestTask : Task
    {
        // Token: 0x17000009 RID: 9
        // (get) Token: 0x0600009B RID: 155 RVA: 0x0000680D File Offset: 0x00004A0D
        // (set) Token: 0x0600009C RID: 156 RVA: 0x00006814 File Offset: 0x00004A14
        public override string ToDo
        {
            get
            {
                return "Get 50 Style";
            }
            set
            {
                base.ToDo = value;
            }
        }

        // Token: 0x0600009D RID: 157 RVA: 0x00006820 File Offset: 0x00004A20
        public override void TheirAwake()
        {
            this.amount = Random.Range(MonoSingleton<StatsManager>.Instance.stylePoints, MonoSingleton<StatsManager>.Instance.stylePoints + 1000);
            TaskManager.Instance.ChangeToDo("Get 50 Style", "Get " + this.amount.ToString() + " style");
            this.WhatToDo = "Get " + this.amount.ToString() + " style";
            this.TimeToFinish = 25f;
            MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("you have 25 seconds to get " + (this.amount - MonoSingleton<StatsManager>.Instance.stylePoints).ToString() + " style", "", "", 0, false);
            base.TheirAwake();
        }

        // Token: 0x0600009E RID: 158 RVA: 0x000068F0 File Offset: 0x00004AF0
        public override void CheckIfWon()
        {
            bool flag = MonoSingleton<StatsManager>.Instance.stylePoints >= this.amount;
            if (flag)
            {
                MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("YOU RECEIVE: DUAL WIELD", "", "", 0, false);
                int num = Random.Range(1, 15);
                for (int i = 0; i < num; i++)
                {
                    bool flag2 = MonoSingleton<GunControl>.Instance;
                    if (flag2)
                    {
                        MonoSingleton<CameraController>.Instance.CameraShake(0.35f);
                        bool flag3 = MonoSingleton<PlayerTracker>.Instance.playerType == PlayerType.Platformer;
                        if (flag3)
                        {
                            MonoSingleton<PlatformerMovement>.Instance.AddExtraHit(3);
                            return;
                        }
                        GameObject gameObject = new GameObject();
                        gameObject.transform.SetParent(MonoSingleton<GunControl>.Instance.transform, true);
                        gameObject.transform.localRotation = Quaternion.identity;
                        DualWield[] componentsInChildren = MonoSingleton<GunControl>.Instance.GetComponentsInChildren<DualWield>();
                        bool flag4 = componentsInChildren != null && componentsInChildren.Length % 2 == 0;
                        if (flag4)
                        {
                            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                        else
                        {
                            gameObject.transform.localScale = Vector3.one;
                        }
                        bool flag5 = componentsInChildren == null || componentsInChildren.Length == 0;
                        if (flag5)
                        {
                            gameObject.transform.localPosition = Vector3.zero;
                        }
                        else
                        {
                            bool flag6 = componentsInChildren.Length % 2 == 0;
                            if (flag6)
                            {
                                gameObject.transform.localPosition = new Vector3((float)(componentsInChildren.Length / 2) * -1.5f, 0f, 0f);
                            }
                            else
                            {
                                gameObject.transform.localPosition = new Vector3((float)((componentsInChildren.Length + 1) / 2) * 1.5f, 0f, 0f);
                            }
                        }
                        DualWield dualWield = gameObject.AddComponent<DualWield>();
                        dualWield.delay = 0.05f;
                        dualWield.juiceAmount = 30f;
                        bool flag7 = componentsInChildren != null && componentsInChildren.Length != 0;
                        if (flag7)
                        {
                            dualWield.delay += (float)componentsInChildren.Length / 20f;
                        }
                    }
                }
                this.Won();
            }
            base.CheckIfWon();
        }

        // Token: 0x0600009F RID: 159 RVA: 0x00006B1E File Offset: 0x00004D1E
        public override void TimeRanOut()
        {
            ModUtils.GetPlayerTransform().GetHurt(int.MaxValue, false, 1f, false, false, 0.35f, false);
            base.TimeRanOut();
        }

        // Token: 0x04000074 RID: 116
        private bool done = false;

        // Token: 0x04000075 RID: 117
        private int amount;
    }
}
