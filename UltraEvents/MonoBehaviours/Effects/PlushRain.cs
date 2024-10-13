using System.Collections.Generic;
using UltraEvents.Utils;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000020 RID: 32
    public class PlushRain : Effect
    {
        // Token: 0x060000C0 RID: 192 RVA: 0x000073E4 File Offset: 0x000055E4
        private void Update()
        {
            List<GameObject> plushies = UltraEventsPlugin.plushies;
            plushies.RemoveAll((GameObject x) => !x.name.ToLower().Contains("plushie"));
            GameObject gameObject = plushies[Random.Range(0, plushies.Count)];
            int num = 1 << LayerMask.NameToLayer("Environment");
            int num2 = 1 << LayerMask.NameToLayer("Outdoors");
            int num3 = num | num2;
            RaycastHit raycastHit;
            bool flag = Physics.Raycast(ModUtils.GetPlayerTransform().transform.position, Vector3.up, out raycastHit, 50f, num3);
            if (flag)
            {
                Object.Instantiate<GameObject>(gameObject, raycastHit.point, Quaternion.identity);
            }
            else
            {
                Object.Instantiate<GameObject>(gameObject, ModUtils.GetPlayerTransform().transform.position + Vector3.up * 50f, Quaternion.identity);
            }
        }
    }
}
