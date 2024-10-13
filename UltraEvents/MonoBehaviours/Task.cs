using System;
using UnityEngine;

namespace UltraEvents.MonoBehaviours
{
    // Token: 0x02000011 RID: 17
    public abstract class Task : MonoBehaviour
    {
        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000084 RID: 132 RVA: 0x00006250 File Offset: 0x00004450
        // (set) Token: 0x06000085 RID: 133 RVA: 0x00006268 File Offset: 0x00004468
        public virtual string ToDo
        {
            get
            {
                return this._ToDo;
            }
            set
            {
                this._ToDo = value;
            }
        }

        // Token: 0x06000086 RID: 134 RVA: 0x00006272 File Offset: 0x00004472
        private void Awake()
        {
            this.WhatToDo = this.ToDo;
            base.Invoke("TheirAwake", 0.01f);
        }

        // Token: 0x06000087 RID: 135 RVA: 0x00006292 File Offset: 0x00004492
        public virtual void TheirAwake()
        {
        }

        // Token: 0x06000088 RID: 136 RVA: 0x00006298 File Offset: 0x00004498
        private void Update()
        {
            this.TimeToFinish -= Time.deltaTime;
            this.CheckIfWon();
            bool flag = this.TimeToFinish <= 0f;
            if (flag)
            {
                this.TimeRanOut();
            }
        }

        // Token: 0x06000089 RID: 137 RVA: 0x000062DC File Offset: 0x000044DC
        public virtual void CheckIfWon()
        {
        }

        // Token: 0x0600008A RID: 138 RVA: 0x000062DF File Offset: 0x000044DF
        public virtual void Won()
        {
            TaskManager.Instance.RemoveTask(this);
        }

        // Token: 0x0600008B RID: 139 RVA: 0x000062EE File Offset: 0x000044EE
        public virtual void TimeRanOut()
        {
            TaskManager.Instance.RemoveTask(this);
            MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("Times up!!!!", "", "", 0, false);
        }

        // Token: 0x0400006A RID: 106
        private string _ToDo;

        // Token: 0x0400006B RID: 107
        public string WhatToDo;

        // Token: 0x0400006C RID: 108
        public float TimeToFinish = 5f;
    }
}
