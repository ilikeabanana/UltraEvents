using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x0200001E RID: 30
    public class NoRicoshots : Effect
    {
        // Token: 0x060000BB RID: 187 RVA: 0x000071A4 File Offset: 0x000053A4
        private void Update()
        {
            Coin[] array = Object.FindObjectsOfType<Coin>();
            foreach (Coin coin in array)
            {
                bool shot = coin.shot;
                if (!shot)
                {
                    coin.shot = true;
                    coin.EnemyReflect();
                }
            }
        }
    }
}
