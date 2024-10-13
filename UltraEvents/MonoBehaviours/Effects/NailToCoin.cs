using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x0200001D RID: 29
    public class NailToCoin : Effect
    {
        // Token: 0x060000B7 RID: 183 RVA: 0x000070EB File Offset: 0x000052EB
        private void Start()
        {
            base.StartCoroutine(this.GetCoin());
        }

        // Token: 0x060000B8 RID: 184 RVA: 0x000070FB File Offset: 0x000052FB
        private IEnumerator GetCoin()
        {
            string prefabKey = "Assets/Prefabs/Attacks and Projectiles/Coin.prefab";
            AsyncOperationHandle<GameObject> RodHandle = Addressables.LoadAssetAsync<GameObject>(prefabKey);
            yield return new WaitUntil(() => RodHandle.IsDone);
            this.lecoin = RodHandle.Result;
            yield break;
        }

        // Token: 0x060000B9 RID: 185 RVA: 0x0000710C File Offset: 0x0000530C
        private void Update()
        {
            bool flag = this.lecoin == null;
            if (!flag)
            {
                Nail[] array = Object.FindObjectsOfType<Nail>();
                foreach (Nail nail in array)
                {
                    GameObject gameObject = Object.Instantiate<GameObject>(this.lecoin, nail.transform.position, nail.transform.rotation);
                    Rigidbody component = gameObject.GetComponent<Rigidbody>();
                    component.velocity = nail.rb.velocity;
                    Object.Destroy(nail.gameObject);
                }
            }
        }

        // Token: 0x0400007C RID: 124
        private GameObject lecoin;
    }
}
