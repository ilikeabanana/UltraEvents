using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000025 RID: 37
    internal class VirtualInsanityEffect : Effect
    {
        // Token: 0x060000D3 RID: 211 RVA: 0x00007AC4 File Offset: 0x00005CC4
        private void Awake()
        {
            GameObject[] array = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject gameObject in array)
            {
                bool flag = gameObject.scene != SceneManager.GetActiveScene();
                if (!flag)
                {
                    bool flag2 = (gameObject.layer == 8 && gameObject.tag == "Untagged") || gameObject.tag == "" || string.IsNullOrEmpty(gameObject.name);
                    if (flag2)
                    {
                        bool flag3 = gameObject.GetComponent<Door>() != null || gameObject.GetComponent<DoorBlocker>() || gameObject.GetComponent<DoorController>();
                        if (!flag3)
                        {
                            this.turns.Add(gameObject.AddComponent<MoveAndTurn>());
                        }
                    }
                }
            }
        }

        // Token: 0x060000D4 RID: 212 RVA: 0x00007B98 File Offset: 0x00005D98
        public override void RemoveEffect()
        {
            base.RemoveEffect();
            foreach (MoveAndTurn moveAndTurn in this.turns)
            {
                bool flag = moveAndTurn.gameObject != null;
                if (flag)
                {
                    Object.Destroy(moveAndTurn);
                }
            }
        }

        // Token: 0x04000089 RID: 137
        private List<MoveAndTurn> turns = new List<MoveAndTurn>();
    }
}
