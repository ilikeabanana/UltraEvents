using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000015 RID: 21
    public class aboohwaer : Effect
    {
        // Token: 0x060000A1 RID: 161 RVA: 0x00006B56 File Offset: 0x00004D56
        private void Awake()
        {
            this.MakeWater();
        }

        // Token: 0x060000A2 RID: 162 RVA: 0x00006B60 File Offset: 0x00004D60
        public void MakeWater()
        {
            this.llewaer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.llewaer.name = "UE WATER!!!";
            this.llewaer.AddComponent<Rigidbody>();
            this.llewaer.GetComponent<Rigidbody>().isKinematic = true;
            this.llewaer.GetComponent<Collider>().isTrigger = true;
            this.llewaer.AddComponent<Water>();
            //this.llewaer.GetComponent<Water>().bub = new GameObject();
            this.llewaer.GetComponent<Water>().clr = new Color(0f, 0.5f, 1f);
            this.llewaer.GetComponent<MeshRenderer>().enabled = false;
            this.llewaer.transform.localScale = Vector3.one * 1E+10f;
        }

        // Token: 0x060000A3 RID: 163 RVA: 0x00006C32 File Offset: 0x00004E32
        public override void RemoveEffect()
        {
            Object.Destroy(this.llewaer);
            base.RemoveEffect();
        }

        // Token: 0x04000076 RID: 118
        private GameObject llewaer;
    }
}
