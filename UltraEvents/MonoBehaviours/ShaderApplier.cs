using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraEvents.MonoBehaviours
{
    // Token: 0x02000004 RID: 4
    public class ShaderApplier : MonoBehaviour
    {
        // Token: 0x06000005 RID: 5 RVA: 0x000020BF File Offset: 0x000002BF
        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, this.material);
        }

        // Token: 0x04000001 RID: 1
        public Material material;
    }
}
