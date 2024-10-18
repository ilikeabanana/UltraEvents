using System.Collections.Generic;
using System.Linq;
using ULTRAKILL.Cheats;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
	// Token: 0x02000023 RID: 35
	public class SomethingWickedThisWayComes : Effect
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00007578 File Offset: 0x00005778
		private void Start()
		{
			this.originalAmbientLight = RenderSettings.ambientLight;
			this.originalFog = RenderSettings.fog;
			this.originalFogDensity = RenderSettings.fogDensity;
			this.originalFogColor = RenderSettings.fogColor;
			this.BeginEffect();
			this.spawns = new DisableEnemySpawns();
			this.spawns.Enable();
			Transform transform = Object.FindObjectOfType<FirstRoomPrefab>().transform;
			EnemyIdentifier[] array = Object.FindObjectsOfType<EnemyIdentifier>();
			foreach (EnemyIdentifier enemyIdentifier in array)
			{
				enemyIdentifier.InstaKill();
			}
			bool flag = transform == null;
			if (flag)
			{
				transform = Object.FindObjectOfType<DoorController>().transform;
			}
			Vector3 normalized = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
			RaycastHit raycastHit;
			Physics.Raycast(MonoSingleton<NewMovement>.Instance.transform.position + normalized * (float)Random.Range(10, 10), Vector3.down, out raycastHit, 25f, LayerMaskDefaults.Get(LMD.Environment));
			Vector3 point = raycastHit.point;
			this.spawnedWicked = Object.Instantiate<GameObject>(UltraEventsPlugin.WickedObject, point, UltraEventsPlugin.WickedObject.transform.rotation);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000076C0 File Offset: 0x000058C0
		public void BeginEffect()
		{
			this.effectActive = true;
			this.flashLight = MonoSingleton<CameraController>.Instance.gameObject.AddComponent<Light>();
			this.flashLight.type = 0;
			this.flashLight.range = 30f;
			this.flashLight.spotAngle = 80f;
			this.flashLight.intensity = 2f;
			this.SlowTickGetLights();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00007734 File Offset: 0x00005934
		private void SlowTickGetLights()
		{
			Light[] array = Resources.FindObjectsOfTypeAll<Light>();
			for (int i = 0; i < array.Length; i++)
			{
				this.ProcessLight(array[i]);
			}
			base.Invoke("SlowTickGetLights", 5f);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007778 File Offset: 0x00005978
		private void LateUpdate()
		{
			bool flag = !this.effectActive;
			bool flag2 = !flag;
			if (flag2)
			{
                var lights = this.instances.Keys.ToList();
                for (int i = 0; i < lights.Count; i++)
                {
                    bool flag3 = lights[i] == null;
                    bool flag4 = !flag3;
                    if (flag4)
                    {
                        bool flag5 = lights[i] == null;
                        bool flag6 = !flag5;
                        if (flag6)
                        {
                            lights[i].enabled = false;
                            RenderSettings.ambientLight = Color.black;
                            RenderSettings.fog = false;
                        }
                    }
                }
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007820 File Offset: 0x00005A20
		public bool CanBeginEffect()
		{
			return true;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007834 File Offset: 0x00005A34
		private void ProcessLight(Light light)
		{
			bool flag = this.lightsChecked.Contains(light.GetInstanceID());
			bool flag2 = !flag;
			if (flag2)
			{
				this.lightsChecked.Add(light.GetInstanceID());
                bool flag3 = (light.hideFlags & HideFlags.NotEditable) != 0 || (light.hideFlags & (HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontUnloadUnusedAsset)) != 0;
                bool flag4 = !flag3;
				if (flag4)
				{
					bool flag5 = (light == MonoSingleton<NewMovement>.Instance.pointLight || light == this.flashLight) && light.enabled;
					bool flag6 = !flag5;
					if (flag6)
					{
						this.instances.Add(light, light.enabled);
					}
				}
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000078D8 File Offset: 0x00005AD8
		public override void RemoveEffect()
		{
			this.effectActive = false;
			bool flag = this.flashLight != null;
			if (flag)
			{
				Object.Destroy(this.flashLight);
				this.flashLight = null;
			}
            var lights = this.instances.Keys.ToList();
            for (int i = 0; i < lights.Count; i++)
            {
                if (lights[i] != null)
                {
                    lights[i].enabled = instances[lights[i]];
                }
            }
            RenderSettings.ambientLight = this.originalAmbientLight;
			RenderSettings.fog = this.originalFog;
			RenderSettings.fogDensity = this.originalFogDensity;
			RenderSettings.fogColor = this.originalFogColor;
			this.instances.Clear();
			this.lightsChecked.Clear();
			this.spawns.Disable();
			base.CancelInvoke("SlowTickGetLights");
			Object.Destroy(this.spawnedWicked);
		}

		// Token: 0x0400007E RID: 126
		private GameObject spawnedWicked;

		// Token: 0x0400007F RID: 127
		private DisableEnemySpawns spawns;

		// Token: 0x04000080 RID: 128
		private Color originalAmbientLight;

		// Token: 0x04000081 RID: 129
		private bool originalFog;

		// Token: 0x04000082 RID: 130
		private float originalFogDensity;

		// Token: 0x04000083 RID: 131
		private Color originalFogColor;

		// Token: 0x04000084 RID: 132
		private Dictionary<Light, bool> instances = new Dictionary<Light, bool>();

		// Token: 0x04000085 RID: 133
		private Light flashLight;

		// Token: 0x04000086 RID: 134
		private bool effectActive = false;

		// Token: 0x04000087 RID: 135
		private HashSet<int> lightsChecked = new HashSet<int>();
	}
}
