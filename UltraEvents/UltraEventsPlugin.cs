using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
//using System.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using UltraEvents.Utils;
using UltraEvents.MonoBehaviours;
using Configgy;

namespace UltraEvents
{
    // TODO Review this file and update to your own requirements.

    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class UltraEventsPlugin : BaseUnityPlugin
    {
        public static Dictionary<string, (MethodInfo Method, ConfigEntry<bool> Config)> events = new Dictionary<string, (MethodInfo, ConfigEntry<bool>)>();
        Events Theevents;
        // Token: 0x04000005 RID: 5
        private const string MyGUID = "com.michi.UltraEvents";

        // Token: 0x04000006 RID: 6
        private const string PluginName = "UltraEvents";

        // Token: 0x04000007 RID: 7
        private const string VersionString = "1.0.0";
        // Token: 0x04000009 RID: 9
        public static bool AutomaticFireEffectActive;

        // Token: 0x0400000A RID: 10
        public GameObject EffectManager;

        // Token: 0x0400000B RID: 11
        public GameObject TaskManagerObject;

        // Token: 0x0400000C RID: 12
        private static readonly Harmony Harmony = new Harmony("com.michi.UltraEvents");

        // Token: 0x0400000D RID: 13
        public static ManualLogSource Log = new ManualLogSource("UltraEvents");

        // Token: 0x0400000E RID: 14
        public static List<GameObject> plushies = new List<GameObject>();

        // Token: 0x0400000F RID: 15
        public GameObject fishingCanvas;
        public GameObject Zombie;

        // Token: 0x04000010 RID: 16
        public Shader unlitShader;

        // Token: 0x04000011 RID: 17
        public float timer = 5f;

        // Token: 0x04000012 RID: 18
        public ConfigEntry<float> AmountOfTime;

        // Token: 0x04000013 RID: 19
        public ConfigEntry<int> maxAmountOfDeletedOjects;

        // Token: 0x04000014 RID: 20
        public ConfigEntry<bool> rmeoveEffects;
        public ConfigEntry<bool> DebugThing;

        // Token: 0x04000015 RID: 21
        public ConfigEntry<bool> announceEvents;

        // Token: 0x04000016 RID: 22
        public ConfigEntry<bool> everyFewSeconds;

        // Token: 0x04000017 RID: 23
        public static ConfigEntry<bool> OnSecretReceived;

        // Token: 0x04000018 RID: 24
        public static ConfigEntry<bool> OnParry;

        // Token: 0x04000019 RID: 25
        public static ConfigEntry<bool> OnEnemyDeath;

        // Token: 0x0400001A RID: 26
        public static ConfigEntry<bool> GetHurt;

        // Token: 0x0400001B RID: 27
        public static ConfigEntry<bool> GetStyle;
        public static ConfigEntry<bool> WeaponSwap;
        public static ConfigEntry<bool> PickUp;
        // Token: 0x0400001C RID: 28
        public GameObject rot = null;

        // Token: 0x0400001D RID: 29
        public static GameObject WickedObject;

        // Token: 0x0400001E RID: 30
        public static Shader VertexLit;

        // Token: 0x0400001F RID: 31
        private string[] plushieKeys = new string[]
        {
            "DevPlushie (Jacob)",
            "DevPlushie (Mako)",
            "DevPlushie (HEALTH - Jake)",
            "DevPlushie (Dalia)",
            "DevPlushie",
            "DevPlushie (Jericho)",
            "DevPlushie (Meganeko)",
            "DevPlushie (Tucker)",
            "DevPlushie (BigRock)",
            "DevPlushie (Dawg)",
            "DevPlushie (Sam)",
            "Mandy Levitating",
            "DevPlushie (Cameron)",
            "DevPlushie (Gianni)",
            "DevPlushie (Salad)",
            "DevPlushie (Mandy)",
            "DevPlushie (Joy)",
            "DevPlushie (Weyte)",
            "DevPlushie (Heckteck)",
            "DevPlushie (Hakita)",
            "DevPlushie (Lenval)",
            "DevPlushie (CabalCrow) Variant",
            "DevPlushie (Quetzal)",
            "DevPlushie (HEALTH - John)",
            "Glasses",
            "DevPlushie (PITR)",
            "DevPlushie (HEALTH - BJ)",
            "DevPlushie (Francis)",
            "DevPlushie (Vvizard)",
            "DevPlushie (Lucas)",
            "DevPlushie (Scott)",
            "DevPlushie (KGC)"
        };

        // Token: 0x0400005D RID: 93
        public string jsonFilePath = "UltraEvents.Jsons.Links.json";

        // Token: 0x0400005E RID: 94
        public List<UltraEventsPlugin.LinkData> links = new List<UltraEventsPlugin.LinkData>();

        // Token: 0x0400005F RID: 95
        public string apiUrl = "https://api.thecatapi.com/v1/images/search?limit=1&breed_ids=beng&api_key=REPLACE_ME";

        // Token: 0x04000060 RID: 96
        public Renderer catRenderer;
        public static UltraEventsPlugin Instance { get; private set; }
        public static ConfigBuilder configBuilder { get; private set; }
        [Configgable(path: "Events Buttons", displayName: "Enable All Button")]
        public static ConfigButton EnableAll = new ConfigButton(() =>
        {
            Log.LogInfo("hi");
            foreach (var eventEntry in events)
            {
                Log.LogInfo(eventEntry.Key);
                try
                {
                    eventEntry.Value.Config.Value = true;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to enable event {eventEntry.Key}: {e.Message}");
                }
            }
        });

        [Configgable(path: "Events Buttons", displayName: "Disable All Button")]
        public static ConfigButton DisableAll = new ConfigButton(() =>
        {
            Log.LogInfo("hi");
            foreach (var eventEntry in events)
            {
                Log.LogInfo(eventEntry.Key);
                try
                {
                    eventEntry.Value.Config.Value = false;  // Should be false here for disabling
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to disable event {eventEntry.Key}: {e.Message}");
                }
            }
        });
        public ShaderApplier shaderApplier;
        public Material upsideDownMaterial;


        public void UpsideDown()
        {
            // Apply the upside-down effect
            upsideDownMaterial.SetFloat("_Intensity", 1f);
        }

        public void ResetScreen()
        {
            // Remove the upside-down effect
            upsideDownMaterial.SetFloat("_Intensity", 0f);
        }
        // Token: 0x0600000D RID: 13 RVA: 0x000020D0 File Offset: 0x000002D0
        public void SetConfigs()
        {
            
            AmountOfTime = Config.Bind<float>("Values", "Time Between Events", 5f);
            this.maxAmountOfDeletedOjects = base.Config.Bind<int>("Values", "max amount of deleted objects", 20, "tied to the 'RemoveRandomObjectsEvent' you can choose what the maximum amount is");
            this.rmeoveEffects = base.Config.Bind<bool>("Values", "remove effects", true, "when this is disabled it wont remove any effects. (NOT RECOMMENDED DONT DO THIS VERY LAGGY!!!)");
            this.announceEvents = base.Config.Bind<bool>("Values", "announce events", true, "when this is disabled it wont announce what event itll activate no more");
            this.everyFewSeconds = base.Config.Bind<bool>("Triggers", "every few seconds", true, "every few seconds an event will trigger");
            this.DebugThing = base.Config.Bind<bool>("Values", "Debug", false, "This is for the developer to see if events trigger correctly");
            UltraEventsPlugin.OnSecretReceived = base.Config.Bind<bool>("Triggers", "On Secret Found", false, "will trigger an event when you find a secret");
            UltraEventsPlugin.OnParry = base.Config.Bind<bool>("Triggers", "On Parry", false, "will trigger an event when you parry");
            UltraEventsPlugin.OnEnemyDeath = base.Config.Bind<bool>("Triggers", "On Enemy Death", false, "will trigger an event when you kill an enemy");
            UltraEventsPlugin.GetHurt = base.Config.Bind<bool>("Triggers", "On Get Hurt", false, "will trigger an event when you receive damage");
            UltraEventsPlugin.GetStyle = base.Config.Bind<bool>("Triggers", "On Get Style", false, "will trigger an event when you receive Style");
            UltraEventsPlugin.WeaponSwap = base.Config.Bind<bool>("Triggers", "On Weapon Swap", false, "will trigger an event when you swap weapons");
            UltraEventsPlugin.PickUp = base.Config.Bind<bool>("Triggers", "On Item Pick Up", false, "will trigger an event when grab an item");
           
            base.Logger.LogInfo("loadedAllConfigs");
            configBuilder = new ConfigBuilder();
            configBuilder.BuildAll();
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00002A14 File Offset: 0x00000C14
        private void Awake()
        {
            UltraEventsPlugin.Instance = this;
            Theevents = gameObject.AddComponent<Events>();
            InitializeEvents();
            this.SetConfigs();
            Object.DontDestroyOnLoad(base.gameObject);
            base.Logger.LogInfo(this.AmountOfTime.Value);
            this.timer = this.AmountOfTime.Value;
            this.EffectManager = new GameObject("EffectManager");
            Object.DontDestroyOnLoad(this.EffectManager);
            this.EffectManager.transform.parent = base.transform;
            this.TaskManagerObject = new GameObject("TaskManager");
            Object.DontDestroyOnLoad(this.TaskManagerObject);
            this.TaskManagerObject.transform.parent = base.transform;
            this.TaskManagerObject.AddComponent<TaskManager>();
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            base.Logger.LogInfo("PluginName: UltraEvents, VersionString: 1.0.0 is loading...");
            UltraEventsPlugin.Harmony.PatchAll();
            this.unlitShader = Addressables.LoadAssetAsync<Shader>("Assets/Shaders/Main/ULTRAKILL-unlit.shader").WaitForCompletion();
            SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.SceneManager_sceneLoaded);
            Shader leShader = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("UltraEvents.Bundles.upsidedown")).LoadAllAssets()[0] as Shader;
            upsideDownMaterial = new Material(leShader);
        }

        // Token: 0x0600000F RID: 15 RVA: 0x00002B28 File Offset: 0x00000D28
        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            bool flag = this.rot == null;
            if (flag)
            {
                base.StartCoroutine(this.loadRod());
            }
            bool flag2 = UltraEventsPlugin.plushies.Count < this.plushieKeys.Length;
            if (flag2)
            {
                base.StartCoroutine(this.LoadPlushies());
            }
            bool flag3 = this.fishingCanvas == null;
            if (flag3)
            {
                base.StartCoroutine(this.LoadUI());
            }
            bool flag4 = UltraEventsPlugin.WickedObject == null;
            if (flag4)
            {
                base.StartCoroutine(this.LoadWicked());
            }
            bool flag5 = UltraEventsPlugin.VertexLit == null;
            if (flag5)
            {
                base.StartCoroutine(this.LoadLit());
            }
            bool flag6 = UltraEventsPlugin.Instance.Zombie == null;
            if (flag6)
            {
                base.StartCoroutine(this.LoadFilth());
            }

            // Set the initial intensity to 0 (normal view)
            upsideDownMaterial.SetFloat("_Intensity", 0f);
            GameObject gameObject = null;
            foreach (object obj in MonoSingleton<CameraController>.Instance.transform)
            {
                Transform transform = (Transform)obj;
                bool thflag = transform.name.ToLower().Contains("virtual");
                if (thflag)
                {
                    gameObject = transform.gameObject;
                    break;
                }
            }
            bool thflag2 = gameObject == null;
            if (!thflag2)
            {
                ShaderApplier shaderApplier = gameObject.AddComponent<ShaderApplier>();
                shaderApplier.material = upsideDownMaterial;
            }
            base.Logger.LogInfo("no issues at all");
        }

        // Token: 0x06000010 RID: 16 RVA: 0x00002BEA File Offset: 0x00000DEA
        private IEnumerator LoadPlushies()
        {
            foreach (string key in this.plushieKeys)
            {
                // Construct the prefab key for the Addressable
                string prefabKey = "Assets/Prefabs/Items/DevPlushies/" + key + ".prefab";

                // Load the prefab asynchronously
                var plushieHandle = Addressables.LoadAssetAsync<GameObject>(prefabKey);

                // Wait until the loading is done
                yield return new WaitUntil(() => plushieHandle.IsDone);

                if (plushieHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    // Get the result if loading was successful
                    GameObject plushie = plushieHandle.Result;

                    // Add the plushie to the collection
                    UltraEventsPlugin.plushies.Add(plushie);
                }
                else
                {
                    // Handle the error (you can log or take any necessary action)
                    Debug.LogError($"Failed to load plushie: {prefabKey}");
                }

                // Release the handle when done to avoid memory leaks
                Addressables.Release(plushieHandle);
            }

            yield break;
        }

        // Token: 0x06000011 RID: 17 RVA: 0x00002BF9 File Offset: 0x00000DF9
        private IEnumerator loadRod()
        {
            base.Logger.LogInfo("rooddddd");
            string prefabKey = "Assets/Prefabs/Fishing/Fishing Rod Weapon.prefab";
            base.Logger.LogInfo("rooddddd");
            AsyncOperationHandle<GameObject> RodHandle = Addressables.LoadAssetAsync<GameObject>(prefabKey);
            base.Logger.LogInfo("rooddddd");
            yield return new WaitUntil(() => RodHandle.IsDone);
            base.Logger.LogInfo("rooddddd");
            bool flag = RodHandle.Status == AsyncOperationStatus.Succeeded && RodHandle.Result != null;
            if (flag)
            {
                this.rot = RodHandle.Result;
            }
            else
            {
                base.Logger.LogError("Failed to load fishing rod: " + RodHandle.Status.ToString());
            }
            yield break;
        }

        // Token: 0x06000012 RID: 18 RVA: 0x00002C08 File Offset: 0x00000E08
        private IEnumerator LoadUI()
        {
            string prefabKey = "Assets/Prefabs/UI/FishingCanvas.prefab";
            AsyncOperationHandle<GameObject> RodHandle = Addressables.LoadAssetAsync<GameObject>(prefabKey);
            yield return new WaitUntil(() => RodHandle.IsDone);
            this.fishingCanvas = RodHandle.Result;
            yield break;
        }
        private IEnumerator LoadFilth()
        {
            string prefabKey = "Assets/Prefabs/Enemies/Zombie.prefab";
            AsyncOperationHandle<GameObject> RodHandle = Addressables.LoadAssetAsync<GameObject>(prefabKey);
            yield return new WaitUntil(() => RodHandle.IsDone);
            this.Zombie = RodHandle.Result;
            yield break;
        }

        // Token: 0x06000013 RID: 19 RVA: 0x00002C17 File Offset: 0x00000E17
        private IEnumerator LoadWicked()
        {
            string prefabKey = "Assets/Prefabs/Enemies/Wicked.prefab";
            AsyncOperationHandle<GameObject> RodHandle = Addressables.LoadAssetAsync<GameObject>(prefabKey);
            yield return new WaitUntil(() => RodHandle.IsDone);
            UltraEventsPlugin.WickedObject = RodHandle.Result;
            yield break;
        }

        // Token: 0x06000014 RID: 20 RVA: 0x00002C26 File Offset: 0x00000E26
        private IEnumerator LoadLit()
        {
            string prefabKey = "Assets/Shaders/Main/ULTRAKILL-vertexlit.shader";
            AsyncOperationHandle<Shader> RodHandle = Addressables.LoadAssetAsync<Shader>(prefabKey);
            yield return new WaitUntil(() => RodHandle.IsDone);
            UltraEventsPlugin.VertexLit = RodHandle.Result;
            yield break;
        }

        // Token: 0x06000015 RID: 21 RVA: 0x00002C38 File Offset: 0x00000E38
        private void RemoveEffect()
        {
            bool flag = !this.rmeoveEffects.Value;
            if (!flag)
            {
                Effect component = this.EffectManager.GetComponent<Effect>();
                bool flag2 = component != null;
                if (flag2)
                {
                    component.RemoveEffect();
                    Object.Destroy(component);
                }
            }
        }

        // Token: 0x06000016 RID: 22 RVA: 0x00002C84 File Offset: 0x00000E84
        public static bool IsGameplayScene()
        {
            string[] source = new string[]
            {
                "Intro",
                "Bootstrap",
                "Main Menu",
                "Level 2-S",
                "Intermission1",
                "Intermission2"
            };
            return !source.Contains(SceneHelper.CurrentScene);
        }

        // Token: 0x06000017 RID: 23 RVA: 0x00002CDC File Offset: 0x00000EDC
        public void CreateJsonFolder()
        {
            string text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JSONFiles");
            bool flag = !Directory.Exists(text);
            if (flag)
            {
                Directory.CreateDirectory(text);
            }
            else
            {
                bool flag2 = Directory.GetFiles(text).Length != 0;
                if (flag2)
                {
                    Console.WriteLine("Video folder already exists and is not empty.");
                    return;
                }
            }
            string[] manifestResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (string text2 in manifestResourceNames)
            {
                bool flag3 = text2.EndsWith(".json");
                if (flag3)
                {
                    using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(text2))
                    {
                        string path = Path.Combine(text, Path.GetFileName(text2));
                        using (FileStream fileStream = File.Create(path))
                        {
                            manifestResourceStream.CopyTo(fileStream);
                        }
                    }
                }
            }
            Console.WriteLine("Jsons copied to the folder successfully.");
        }

        // Token: 0x06000018 RID: 24 RVA: 0x00002DF8 File Offset: 0x00000FF8
        public void CreateVideoFolder()
        {
            string text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Videos");
            bool flag = !Directory.Exists(text);
            if (flag)
            {
                Directory.CreateDirectory(text);
            }
            else
            {
                bool flag2 = Directory.GetFiles(text).Length != 0;
                if (flag2)
                {
                    Console.WriteLine("Video folder already exists and is not empty.");
                    return;
                }
            }
            string[] manifestResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (string text2 in manifestResourceNames)
            {
                bool flag3 = text2.EndsWith(".mp4");
                if (flag3)
                {
                    using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(text2))
                    {
                        string path = Path.Combine(text, Path.GetFileName(text2));
                        using (FileStream fileStream = File.Create(path))
                        {
                            manifestResourceStream.CopyTo(fileStream);
                        }
                    }
                }
            }
            Console.WriteLine("Videos copied to the folder successfully.");
        }

        // Token: 0x06000019 RID: 25 RVA: 0x00002F14 File Offset: 0x00001114
        private Transform GetChildything(GameObject clonedThing)
        {
            Transform transform = clonedThing.transform.Find("ZombieFilth");
            Transform transform2 = transform.Find("Armature.001");
            Transform transform3 = transform2.Find("Bone001");
            Transform transform4 = transform3.Find("Spine_01");
            return transform4.Find("TheEnemyObject");
        }

        // Token: 0x0600001A RID: 26 RVA: 0x00002F6C File Offset: 0x0000116C
        private void Update()
        {
            bool flag = ModUtils.GetPlayerTransform() == null;
            if (!flag)
            {
                bool flag2 = !ModUtils.GetPlayerTransform().enabled || !UltraEventsPlugin.IsGameplayScene();
                if (!flag2)
                {
                    bool flag3 = !this.everyFewSeconds.Value;
                    if (!flag3)
                    {
                        this.timer -= Time.fixedDeltaTime;
                        bool flag4 = this.timer <= 0f;
                        if (flag4)
                        {
                            this.UseRandomEventAndRemoveEffects();
                            this.timer = this.AmountOfTime.Value;
                        }
                    }
                }
            }
        }

        // Token: 0x0600001B RID: 27 RVA: 0x00002FFC File Offset: 0x000011FC
        public void UseRandomEventAndRemoveEffects()
        {
            this.RemoveEffect();
            this.UseRandomEvent();
        }

        // Token: 0x0600001C RID: 28 RVA: 0x00003010 File Offset: 0x00001210
        private void InitializeEvents()
        {
            var eventMethods = typeof(Events).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetParameters().Length == 0 && m.DeclaringType == typeof(Events));

            foreach (var method in eventMethods)
            {
                string eventName = method.Name;
                var configEntry = Config.Bind("Events", eventName, true, $"Enable or disable the {eventName}");
                events[eventName] = (method, configEntry);
            }
            Debug.Log(events.Count);
        }

        public void UseRandomEvent()
        {
            var enabledEvents = events.Where(e => e.Value.Config.Value).ToList();
            if (enabledEvents.Count == 0)
            {
                Console.WriteLine("No events are enabled.");
                return;
            }

            var randomEvent = enabledEvents[Random.Range(0, enabledEvents.Count)];
            Console.WriteLine($"Triggering event: {randomEvent.Key}");
            if (DebugThing.Value)
            {
                MonoSingleton<HudMessageReceiver>.instance.SendHudMessage($"Triggering event: {randomEvent.Key}");
            }

            randomEvent.Value.Method.Invoke(Theevents, null);

            
            
        }

        

        // Token: 0x02000027 RID: 39
        [Serializable]
        public class LinkData
        {
            // Token: 0x0400008B RID: 139
            public string link;
        }
    }
}
