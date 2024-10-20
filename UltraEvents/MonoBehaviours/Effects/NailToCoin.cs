using HarmonyLib;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UltraEvents.MonoBehaviours.Effects
{
    [HarmonyPatch(typeof(Nail), nameof(Nail.Start))]
    public class NailToCoin : Effect
    {
        // Token: 0x060000B7 RID: 183 RVA: 0x000070EB File Offset: 0x000052EB
        static bool IsActive = false;
        private void Start()
        {
            IsActive = true;
            base.StartCoroutine(this.GetCoin());
        }

        // Token: 0x060000B8 RID: 184 RVA: 0x000070FB File Offset: 0x000052FB
        private IEnumerator GetCoin()
        {
            string prefabKey = "Assets/Prefabs/Attacks and Projectiles/Coin.prefab";
            AsyncOperationHandle<GameObject> RodHandle = Addressables.LoadAssetAsync<GameObject>(prefabKey);
            yield return new WaitUntil(() => RodHandle.IsDone);
            lecoin = RodHandle.Result;
            yield break;
        }

        
        public override void RemoveEffect()
        {
            IsActive = false;
            base.RemoveEffect();
        }

        // Token: 0x0400007C RID: 124
        private static GameObject lecoin;
        public static bool Prefix(Nail __instance)
        {
            if(!IsActive) return true;
            GameObject gameObject = Object.Instantiate<GameObject>(lecoin, __instance.transform.position, __instance.transform.rotation);
            Rigidbody component = gameObject.GetComponent<Rigidbody>();
            component.velocity = __instance.rb.velocity;
            Object.Destroy(__instance.gameObject);
            return false;
        }
    }
}
