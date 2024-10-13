using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x0200001F RID: 31
    internal class PixelReducer : Effect
    {
        // Token: 0x060000BD RID: 189 RVA: 0x000071F4 File Offset: 0x000053F4
        public void Awake()
        {
            this.NormalPixelization = MonoSingleton<PrefsManager>.Instance.GetInt("pixelization", 0);
            int content = Random.Range(0, 7);
            MonoSingleton<PrefsManager>.Instance.SetInt("pixelization", content);
            float num = 0f;
            switch (content)
            {
                case 0:
                    num = 0f;
                    break;
                case 1:
                    num = 720f;
                    break;
                case 2:
                    num = 480f;
                    break;
                case 3:
                    num = 360f;
                    break;
                case 4:
                    num = 240f;
                    break;
                case 5:
                    num = 144f;
                    break;
                case 6:
                    num = 36f;
                    break;
            }
            Shader.SetGlobalFloat("_ResY", num);
            PostProcessV2_Handler instance = MonoSingleton<PostProcessV2_Handler>.Instance;
            bool flag = instance;
            if (flag)
            {
                instance.downscaleResolution = num;
            }
            DownscaleChangeSprite[] array = Object.FindObjectsOfType<DownscaleChangeSprite>();
            for (int i = 0; i < array.Length; i++)
            {
                array[i].CheckScale();
            }
        }

        // Token: 0x060000BE RID: 190 RVA: 0x000072EC File Offset: 0x000054EC
        public override void RemoveEffect()
        {
            int normalPixelization = this.NormalPixelization;
            MonoSingleton<PrefsManager>.Instance.SetInt("pixelization", normalPixelization);
            float num = 0f;
            switch (normalPixelization)
            {
                case 0:
                    num = 0f;
                    break;
                case 1:
                    num = 720f;
                    break;
                case 2:
                    num = 480f;
                    break;
                case 3:
                    num = 360f;
                    break;
                case 4:
                    num = 240f;
                    break;
                case 5:
                    num = 144f;
                    break;
                case 6:
                    num = 36f;
                    break;
            }
            Shader.SetGlobalFloat("_ResY", num);
            PostProcessV2_Handler instance = MonoSingleton<PostProcessV2_Handler>.Instance;
            bool flag = instance;
            if (flag)
            {
                instance.downscaleResolution = num;
            }
            DownscaleChangeSprite[] array = Object.FindObjectsOfType<DownscaleChangeSprite>();
            for (int i = 0; i < array.Length; i++)
            {
                array[i].CheckScale();
            }
            base.RemoveEffect();
        }

        // Token: 0x0400007D RID: 125
        private int NormalPixelization = 0;
    }
}
