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
using UnityEngine.Video;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using UltraEvents.Utils;
using UltraEvents.MonoBehaviours;
using UltraEvents.MonoBehaviours.Effects;
using UltraEvents.MonoBehaviours.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UltraEvents
{
    // TODO Review this file and update to your own requirements.

    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class UltraEventsPlugin : BaseUnityPlugin
    {
        // Token: 0x04000005 RID: 5
        private const string MyGUID = "com.michi.UltraEvents";

        // Token: 0x04000006 RID: 6
        private const string PluginName = "UltraEvents";

        // Token: 0x04000007 RID: 7
        private const string VersionString = "1.0.0";
        public static UltraEventsPlugin Instance { get; private set; }

        // Token: 0x0600000D RID: 13 RVA: 0x000020D0 File Offset: 0x000002D0
        public void SetConfigs()
        {
            this.YEETEvent = base.Config.Bind<bool>("Events", "YEETEvent", true, "When this event is triggered it will yeet you");
            this.TPEnemiesEvent = base.Config.Bind<bool>("Events", "TPEnemiesEvent", true, "When this event is triggered it will tp all enemies to you");
            this.RemoveWeaponEvent = base.Config.Bind<bool>("Events", "RemoveWeaponEvent", true, "When this event is triggered it will remove the weapon you have currently equipped");
            this.usePreviousWeaponEvent = base.Config.Bind<bool>("Events", "usePreviousWeaponEvent", true, "When this event is triggered it will automatically select your previous weapon");
            this.KaboomEvent = base.Config.Bind<bool>("Events", "KaboomEvent", true, "When this event is triggered it will explode every enemy");
            this.DupeEnemyEvent = base.Config.Bind<bool>("Events", "DupeEnemyEvent", true, "When this event is triggered it will dupilcate a random enemy");
            this.BuffEnemyEvent = base.Config.Bind<bool>("Events", "BuffEnemyEvent", true, "When this event is triggered it will buff a random enemy");
            this.giveRandomWeaponEvent = base.Config.Bind<bool>("Events", "giveRandomWeaponEvent", true, "When this event is triggered it will choose a random weapon for you");
            this.ReverseGravityEvent = base.Config.Bind<bool>("Events", "ReverseGravityEvent", true, "When this event is triggered it will reverse gravity");
            this.KillRandomEnemyEvent = base.Config.Bind<bool>("Events", "KillRandomEnemyEvent", true, "When this event is triggered it will kill a random enemy");
            this.KillAllEnemyEvent = base.Config.Bind<bool>("Events", "KillAllEnemyEvent", true, "When this event is triggered it will kill every enemy");
            this.UseRandomInputEvent = base.Config.Bind<bool>("Events", "UseRandomInputEvent", true, "When this event is triggered it will use a random input (currently broken)");
            this.RemoveRandomObjectEvent = base.Config.Bind<bool>("Events", "RemoveRandomObjectEvent", true, "When this event is triggered it will remove a random object");
            this.AddRBRandomObjectEvent = base.Config.Bind<bool>("Events", "AddRBRandomObjectEvent", true, "When this event is triggered it will add gravity to a random object");
            this.AddRBRandomObjectsEvent = base.Config.Bind<bool>("Events", "AddRBRandomObjectsEvent", true, "When this event is triggered it will add gravity to a multitude random objects");
            this.SpawnItemEvent = base.Config.Bind<bool>("Events", "PlushieRain", true, "When this event is triggered it will make a plushie rain");
            this.SlowMotionEvent = base.Config.Bind<bool>("Events", "SlowMotionEvent", true, "When this event is triggered it will go into 0.3 times slowmotion");
            this.GiveDualWieldEvent = base.Config.Bind<bool>("Events", "GiveDualWieldEvent", true, "When this event is triggered it will give you a random amount of dual wields");
            this.SpawnRandomEnemyEvent = base.Config.Bind<bool>("Events", "SpawnRandomEnemyEvent", true, "When this event is triggered it will spawn a random enemy");
            this.LagEvent = base.Config.Bind<bool>("Events", "LagEvent", true, "When this event is triggered it will teleport you back to your previous position every few seconds");
            this.BlessthemAllEvent = base.Config.Bind<bool>("Events", "BlessthemAllEvent", true, "When this event is triggered it will bless all the enemies until the next event");
            this.waerEvent = base.Config.Bind<bool>("Events", "waerEvent", true, "When this event is triggered it will put everything under water");
            this.noHealsEvent = base.Config.Bind<bool>("Events", "noHealsEvent", true, "When this event is triggered it will sandify every current enemy");
            this.GetRodEvent = base.Config.Bind<bool>("Events", "GetRodEvent", true, "When this event is triggered it will give you the fishing rod");
            this.TurnEnemyIntoPuppetEvent = base.Config.Bind<bool>("Events", "TurnEnemyIntoPuppetEvent", true, "When this event is triggered it will turn a random enemy into a puppet");
            this.MoreTroubleEvent = base.Config.Bind<bool>("Events", "MoreTroubleEvent", true, "When this event is triggered it will activate 2 events");
            this.TeleportToEnemyEvent = base.Config.Bind<bool>("Events", "TeleportToEnemyEvent", true, "When this event is triggered it will teleport you to an enemy");
            this.SwapPosEvent = base.Config.Bind<bool>("Events", "SwapPosEvent", true, "When this event is triggered it will swap 2 objects position");
            this.FastMotionEvent = base.Config.Bind<bool>("Events", "FastMotionEvent", true, "When this event is triggered it will go in 2 times speed");
            this.SwitchArmEvent = base.Config.Bind<bool>("Events", "SwitchArmEvent", true, "When this event is triggered it will switch your arm");
            this.RemoveStyleEvent = base.Config.Bind<bool>("Events", "RemoveStyleEvent", true, "When this event is triggered it will remove a random amount of your style points");
            this.DiesEvent = base.Config.Bind<bool>("Events", "DieEvent", true, "When this event is triggered it will kill you");
            this.FakeParryEvent = base.Config.Bind<bool>("Events", "FakeParryEvent", true, "When this event is triggered it will trigger a flash");
            this.SpawnAdEvent = base.Config.Bind<bool>("Events", "SpawnAdEvent", true, "When this event is triggered it will spawn a random video you have in the videos folder (look at the dll file location)");
            this.LoadCatEvent = base.Config.Bind<bool>("Events", "LoadCatEvent", true, "When this event is triggered it will make a cube with a randomm cat image");
            this.AlakablamEvent = base.Config.Bind<bool>("Events", "AlakablamEvent", true, "When this event is triggered it will virtue beam every enemy");
            this.AirStrikeEvent = base.Config.Bind<bool>("Events", "AirStrikeEvent", true, "When this event is triggered it will virtue beam you");
            this.DupeAllEnemyEvent = base.Config.Bind<bool>("Events", "DupeAllEnemyEvent", true, "When this event is triggered it will duplicate every enemy");
            this.RemoveStaminaEvent = base.Config.Bind<bool>("Events", "RemoveStaminaEvent", true, "When this event is triggered it will remove your stamina");
            this.RemoveChargeEvent = base.Config.Bind<bool>("Events", "RemoveChargeEvent", true, "When this event is triggered it will remove your railcannon charge");
            this.OpenRandomLaLinkEvent = base.Config.Bind<bool>("Events", "OpenRandomLaLinkEvent", true, "When this event is triggered it will open a random link from the links json (look at the dll file location)");
            this.RemoveRandomObjectsEvent = base.Config.Bind<bool>("Events", "RemoveRandomObjectsEvent", true, "When this event is triggered it will remove a few random objects");
            this.PixelizeScreenEvent = base.Config.Bind<bool>("Events", "PixelizeScreenEvent", true, "When this event is triggered it will pixelize your screen");
            this.GetTaskEvent = base.Config.Bind<bool>("Events", "GetTaskEvent", true, "When this event is triggered it give you a task");
            this.MakeEnemyOutOfSomethingEvent = base.Config.Bind<bool>("Events", "MakeEnemyOutOfSomethingEvent", true, "When this event is triggered it will make a random object an enemy");
            this.InvisibleEnemiesEvent = base.Config.Bind<bool>("Events", "InvisibleEnemiesEvent", true, "When this event is triggered it will turn every enemy invisible");
            this.SchizophreniaUpdateEvent = base.Config.Bind<bool>("Events", "SchizophreniaUpdateEvent", true, "When this event is triggered it will remove every enemy that you dont see");
            this.BulletsExplodeNowEvent = base.Config.Bind<bool>("Events", "BulletsExplodeNowEvent", true, "When this event is triggered it will make every bullet explode on contact");
            this.BulletsAfraidEnemiesEvent = base.Config.Bind<bool>("Events", "BulletsAfraidEnemiesEvent", true, "When this event is triggered it will make every bullet avoid enemies");
            this.ReadEvent = base.Config.Bind<bool>("Events", "ReadEvent", true, "When this event is triggered it will make you read");
            this.MoveEverythingEvent = base.Config.Bind<bool>("Events", "MoveEverythingEvent", true, "When this event is triggered it will move everything to a random direction");
            this.BossBarForEveryoneEvent = base.Config.Bind<bool>("Events", "BossBarForEveryoneEvent", true, "When this event is triggered it will give every enemy a bossbar");
            this.OilUpEvent = base.Config.Bind<bool>("Events", "OilUpEvent", true, "When this event is triggered it will oil every enemy completely");
            this.NailToCoinEvent = base.Config.Bind<bool>("Events", "NailToCoinEvent", true, "When this event is triggered it will turn all nails into coins");
            this.AutomaticWeaponsEffectEvent = base.Config.Bind<bool>("Events", "AutomaticWeaponsEffectEvent", true, "When this event is triggered it will turn all weapons automatic");
            this.NoRicoshotsEvent = base.Config.Bind<bool>("Events", "NoRicoshotsEvent", true, "When this event is triggered it will make every coin target you");
            this.AttachEverythingToPlayerEvent = base.Config.Bind<bool>("Events", "AttachEverythingToPlayerEvent", true, "When this event is triggered it will basically make you a magnet for everything");
            this.RulesOfNatureEvent = base.Config.Bind<bool>("Events", "RulesOfNatureEvent", true, "When this event is triggered it will make an enemy 10 times bigger and make him 3 times stronger");
            this.nanoMachinesSonEvent = base.Config.Bind<bool>("Events", "nanoMachinesSonEvent", true, "When this event is triggered it will make the closest enemy have 10 times more health and twice the damage");
            this.AllyEvent = base.Config.Bind<bool>("Events", "AllyEvent", true, "When this event is triggered it will make a random enemy attack other enemies and ignore you");
            this.SomethingWickedThisWayComesEvent = base.Config.Bind<bool>("Events", "SomethingWickedThisWayComesEvent", true, "When this event is triggered it will spawn something wicked and deactivate all triggers");
            AmountOfTime = Config.Bind<float>("Values", "Time Between Events", 5f);
            this.maxAmountOfDeletedOjects = base.Config.Bind<int>("Values", "max amount of deleted objects", 20, "tied to the 'RemoveRandomObjectsEvent' you can choose what the maximum amount is");
            this.rmeoveEffects = base.Config.Bind<bool>("Values", "remove effects", true, "when this is disabled it wont remove any effects. (NOT RECOMMENDED DONT DO THIS VERY LAGGY!!!)");
            this.announceEvents = base.Config.Bind<bool>("Values", "announce events", true, "when this is disabled it wont announce what event itll activate no more");
            this.everyFewSeconds = base.Config.Bind<bool>("Triggers", "every few seconds", true, "every few seconds an event will trigger");
            UltraEventsPlugin.OnSecretReceived = base.Config.Bind<bool>("Triggers", "On Secret Found", false, "will trigger an event when you find a secret");
            UltraEventsPlugin.OnParry = base.Config.Bind<bool>("Triggers", "On Parry", false, "will trigger an event when you parry");
            UltraEventsPlugin.OnEnemyDeath = base.Config.Bind<bool>("Triggers", "On Enemy Death", false, "will trigger an event when you kill an enemy");
            UltraEventsPlugin.GetHurt = base.Config.Bind<bool>("Triggers", "On Get Hurt", false, "will trigger an event when you receive damage");
            UltraEventsPlugin.GetStyle = base.Config.Bind<bool>("Triggers", "On Get Style", false, "will trigger an event when you receive Style");
            base.Logger.LogInfo("loadedAllConfigs");
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00002A14 File Offset: 0x00000C14
        private void Awake()
        {
            UltraEventsPlugin.Instance = this;
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
        public void UseRandomEvent()
        {
            int key = Random.Range(0, 61);
            Dictionary<int, ConfigEntry<bool>> dictionary = new Dictionary<int, ConfigEntry<bool>>
            {
                {
                    0,
                    this.YEETEvent
                },
                {
                    1,
                    this.TPEnemiesEvent
                },
                {
                    2,
                    this.RemoveWeaponEvent
                },
                {
                    3,
                    this.usePreviousWeaponEvent
                },
                {
                    4,
                    this.KaboomEvent
                },
                {
                    5,
                    this.DupeEnemyEvent
                },
                {
                    6,
                    this.BuffEnemyEvent
                },
                {
                    7,
                    this.giveRandomWeaponEvent
                },
                {
                    8,
                    this.ReverseGravityEvent
                },
                {
                    9,
                    this.KillRandomEnemyEvent
                },
                {
                    10,
                    this.KillAllEnemyEvent
                },
                {
                    11,
                    this.UseRandomInputEvent
                },
                {
                    12,
                    this.RemoveRandomObjectEvent
                },
                {
                    13,
                    this.AddRBRandomObjectEvent
                },
                {
                    14,
                    this.SpawnItemEvent
                },
                {
                    15,
                    this.SlowMotionEvent
                },
                {
                    16,
                    this.GiveDualWieldEvent
                },
                {
                    17,
                    this.SpawnRandomEnemyEvent
                },
                {
                    18,
                    this.LagEvent
                },
                {
                    19,
                    this.BlessthemAllEvent
                },
                {
                    20,
                    this.waerEvent
                },
                {
                    21,
                    this.noHealsEvent
                },
                {
                    22,
                    this.GetRodEvent
                },
                {
                    23,
                    this.TurnEnemyIntoPuppetEvent
                },
                {
                    24,
                    this.MoreTroubleEvent
                },
                {
                    25,
                    this.TeleportToEnemyEvent
                },
                {
                    26,
                    this.SwapPosEvent
                },
                {
                    27,
                    this.FastMotionEvent
                },
                {
                    28,
                    this.SwitchArmEvent
                },
                {
                    29,
                    this.RemoveStyleEvent
                },
                {
                    30,
                    this.DiesEvent
                },
                {
                    31,
                    this.FakeParryEvent
                },
                {
                    32,
                    this.SpawnAdEvent
                },
                {
                    33,
                    this.LoadCatEvent
                },
                {
                    34,
                    this.AlakablamEvent
                },
                {
                    35,
                    this.AirStrikeEvent
                },
                {
                    36,
                    this.DupeAllEnemyEvent
                },
                {
                    37,
                    this.RemoveStaminaEvent
                },
                {
                    38,
                    this.RemoveChargeEvent
                },
                {
                    39,
                    this.OpenRandomLaLinkEvent
                },
                {
                    40,
                    this.RemoveRandomObjectsEvent
                },
                {
                    41,
                    this.PixelizeScreenEvent
                },
                {
                    42,
                    this.AddRBRandomObjectsEvent
                },
                {
                    43,
                    this.GetTaskEvent
                },
                {
                    44,
                    this.MakeEnemyOutOfSomethingEvent
                },
                {
                    45,
                    this.InvisibleEnemiesEvent
                },
                {
                    46,
                    this.SchizophreniaUpdateEvent
                },
                {
                    47,
                    this.BulletsExplodeNowEvent
                },
                {
                    48,
                    this.BulletsAfraidEnemiesEvent
                },
                {
                    49,
                    this.ReadEvent
                },
                {
                    50,
                    this.MoveEverythingEvent
                },
                {
                    51,
                    this.BossBarForEveryoneEvent
                },
                {
                    52,
                    this.OilUpEvent
                },
                {
                    53,
                    this.NailToCoinEvent
                },
                {
                    54,
                    this.AutomaticWeaponsEffectEvent
                },
                {
                    55,
                    this.NoRicoshotsEvent
                },
                {
                    56,
                    this.AttachEverythingToPlayerEvent
                },
                {
                    57,
                    this.RulesOfNatureEvent
                },
                {
                    58,
                    this.nanoMachinesSonEvent
                },
                {
                    59,
                    this.AllyEvent
                },
                {
                    60,
                    this.SomethingWickedThisWayComesEvent
                }
            };
            bool flag = dictionary.Any((KeyValuePair<int, ConfigEntry<bool>> pair) => pair.Value.Value);
            bool flag2 = !flag;
            if (!flag2)
            {
                bool flag3 = dictionary.ContainsKey(key) && dictionary[key].Value;
                if (flag3)
                {
                    switch (key)
                    {
                        case 0:
                            this.YEET();
                            break;
                        case 1:
                            this.TPEnemies();
                            break;
                        case 2:
                            this.RemoveWeapon();
                            break;
                        case 3:
                            this.usePreviousWeapon();
                            break;
                        case 4:
                            this.Kaboom();
                            break;
                        case 5:
                            this.DupeEnemy();
                            break;
                        case 6:
                            this.BuffEnemy();
                            break;
                        case 7:
                            this.giveRandomWeapon();
                            break;
                        case 8:
                            this.ReverseGravity();
                            break;
                        case 9:
                            this.KillRandomEnemy();
                            break;
                        case 10:
                            this.KillAllEnemy();
                            break;
                        case 11:
                            this.UseRandomInput();
                            break;
                        case 12:
                            this.RemoveRandomObject();
                            break;
                        case 13:
                            this.AddRBRandomObject();
                            break;
                        case 14:
                            this.SpawnItem();
                            break;
                        case 15:
                            this.SlowMotion();
                            break;
                        case 16:
                            this.GiveDualWield();
                            break;
                        case 17:
                            this.SpawnRandomEnemy();
                            break;
                        case 18:
                            this.Lag();
                            break;
                        case 19:
                            this.BlessthemAll();
                            break;
                        case 20:
                            this.waer();
                            break;
                        case 21:
                            this.noHeals();
                            break;
                        case 22:
                            this.GetRod();
                            break;
                        case 23:
                            this.TurnEnemyIntoPuppet();
                            break;
                        case 24:
                            this.MoreTrouble();
                            break;
                        case 25:
                            this.TeleportToEnemy();
                            break;
                        case 26:
                            this.SwapPos();
                            break;
                        case 27:
                            this.FastMotion();
                            break;
                        case 28:
                            this.SwitchArm();
                            break;
                        case 29:
                            this.RemoveStyle();
                            break;
                        case 30:
                            this.Dies();
                            break;
                        case 31:
                            this.FakeParry();
                            break;
                        case 32:
                            this.SpawnAd();
                            break;
                        case 33:
                            this.LoadCat();
                            break;
                        case 34:
                            this.Alakablam();
                            break;
                        case 35:
                            this.AirStrike();
                            break;
                        case 36:
                            this.DupeAllEnemy();
                            break;
                        case 37:
                            this.RemoveStamina();
                            break;
                        case 38:
                            this.RemoveCharge();
                            break;
                        case 39:
                            this.OpenRandomLaLink();
                            break;
                        case 40:
                            this.RemoveRandomObjects();
                            break;
                        case 41:
                            this.PixelizeScreen();
                            break;
                        case 42:
                            this.AddRBRandomObjects();
                            break;
                        case 43:
                            this.GiveTask();
                            break;
                        case 44:
                            this.makeEnemyOutOfSomething();
                            break;
                        case 45:
                            this.InvisibleEnemies();
                            break;
                        case 46:
                            this.SchizophreniaUpdate();
                            break;
                        case 47:
                            this.BulletsExplodeNow();
                            break;
                        case 48:
                            this.BulletsAfraidNow();
                            break;
                        case 49:
                            this.ReadLol();
                            break;
                        case 50:
                            this.MoveEverythingToRight();
                            break;
                        case 51:
                            this.BossBarForEveryone();
                            break;
                        case 52:
                            this.OilUp();
                            break;
                        case 53:
                            this.CoinsAreNowNails();
                            break;
                        case 54:
                            this.AutomaticWeapons();
                            break;
                        case 55:
                            this.NoRicoshotsVoid();
                            break;
                        case 56:
                            this.AttachEverything();
                            break;
                        case 57:
                            this.RulesOfNature();
                            break;
                        case 58:
                            this.nanoMachinesSon();
                            break;
                        case 59:
                            this.Ally();
                            break;
                        case 60:
                            this.SomethingWickedThisWayComesVoid();
                            break;
                    }
                }
                else
                {
                    this.UseRandomEvent();
                }
            }
        }

        // Token: 0x0600001D RID: 29 RVA: 0x000037CF File Offset: 0x000019CF
        private void SomethingWickedThisWayComesVoid()
        {
            this.AnnounceEvent("Something wicked this way comes");
            this.EffectManager.AddComponent<SomethingWickedThisWayComes>();
        }

        // Token: 0x0600001E RID: 30 RVA: 0x000037EC File Offset: 0x000019EC
        public bool IsChild(GameObject objectToCheck, GameObject parentObject)
        {
            Transform transform = parentObject.transform;
            Transform transform2 = objectToCheck.transform;
            while (transform2 != null)
            {
                bool flag = transform2 == transform;
                if (flag)
                {
                    return true;
                }
                transform2 = transform2.parent;
            }
            return false;
        }

        // Token: 0x0600001F RID: 31 RVA: 0x00003838 File Offset: 0x00001A38
        private void AnnounceEvent(string message)
        {
            bool flag = !this.announceEvents.Value;
            if (!flag)
            {
                MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage(message, "", "", 0, false);
            }
        }

        // Token: 0x06000020 RID: 32 RVA: 0x00003874 File Offset: 0x00001A74
        private void MoveEverythingToRight()
        {
            float num = Random.Range(-1f, 1f);
            float num2 = Random.Range(-1f, 1f);
            float num3 = Random.Range(-1f, 1f);
            Debug.Log(num.ToString() + " " + num2.ToString() + num3.ToString());
            GameObject[] array = Resources.FindObjectsOfTypeAll<GameObject>();
            GameObject[] array2 = array;
            int i = 0;
            while (i < array2.Length)
            {
                GameObject gameObject = array2[i];
                bool flag = gameObject.scene != SceneManager.GetActiveScene();
                if (!flag)
                {
                    bool flag2 = this.IsChild(MonoSingleton<NewMovement>.Instance.gameObject, gameObject);
                    if (!flag2)
                    {
                        Vector3 normalized = new Vector3(num, num2, num3).normalized;
                        gameObject.transform.position += normalized;
                        i++;
                        continue;
                    }
                }
                return;
            }
            this.AnnounceEvent("i moved everything a lil");
        }

        // Token: 0x06000021 RID: 33 RVA: 0x0000396C File Offset: 0x00001B6C
        private void Ally()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            bool flag = list.Count <= 0;
            if (!flag)
            {
                EnemyIdentifier enemyIdentifier = list[Random.Range(0, list.Count)];
                enemyIdentifier.ignorePlayer = true;
                enemyIdentifier.attackEnemies = true;
                this.AnnounceEvent(enemyIdentifier.FullName + " is now your ally");
            }
        }

        // Token: 0x06000022 RID: 34 RVA: 0x000039F8 File Offset: 0x00001BF8
        private void nanoMachinesSon()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            bool flag = list.Count <= 0;
            if (!flag)
            {
                Vector3 playerPosition = MonoSingleton<NewMovement>.Instance.transform.position;
                EnemyIdentifier enemyIdentifier = (from enemy in list
                                                   orderby Vector3.Distance(playerPosition, enemy.transform.position)
                                                   select enemy).FirstOrDefault<EnemyIdentifier>();
                bool flag2 = enemyIdentifier != null;
                if (flag2)
                {
                    this.AnnounceEvent("'NANO MACHINES SON' -" + enemyIdentifier.FullName);
                    enemyIdentifier.damageBuffModifier += 2f;
                    enemyIdentifier.healthBuffModifier += 10f;
                    enemyIdentifier.healthBuff = true;
                    enemyIdentifier.damageBuff = true;
                }
            }
        }

        // Token: 0x06000023 RID: 35 RVA: 0x00003AD8 File Offset: 0x00001CD8
        private void RulesOfNature()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            bool flag = list.Count <= 0;
            if (!flag)
            {
                EnemyIdentifier enemyIdentifier = list[Random.Range(0, list.Count)];
                enemyIdentifier.gameObject.transform.localScale *= 10f;
                enemyIdentifier.radianceTier += 3f;
                enemyIdentifier.speedBuffModifier += 1f;
                enemyIdentifier.damageBuffModifier += 1f;
                enemyIdentifier.healthBuffModifier += 1f;
                enemyIdentifier.speedBuff = true;
                enemyIdentifier.healthBuff = true;
                enemyIdentifier.damageBuff = true;
                bool flag2 = enemyIdentifier.GetComponent<BossHealthBar>();
                BossHealthBar bossHealthBar;
                if (flag2)
                {
                    bossHealthBar = enemyIdentifier.GetComponent<BossHealthBar>();
                }
                else
                {
                    bossHealthBar = enemyIdentifier.gameObject.AddComponent<BossHealthBar>();
                }
                bossHealthBar.bossName = enemyIdentifier.FullName + " destroyer of worlds";
            }
        }

        // Token: 0x06000024 RID: 36 RVA: 0x00003BFE File Offset: 0x00001DFE
        private void AttachEverything()
        {
            this.AnnounceEvent("Everything is now attracted to you");
            this.EffectManager.AddComponent<AttachEverythingToPlayer>();
        }

        // Token: 0x06000025 RID: 37 RVA: 0x00003C19 File Offset: 0x00001E19
        private void NoRicoshotsVoid()
        {
            this.AnnounceEvent("Coins don't like you anymore");
            this.EffectManager.AddComponent<NoRicoshots>();
        }

        // Token: 0x06000026 RID: 38 RVA: 0x00003C34 File Offset: 0x00001E34
        private void AutomaticWeapons()
        {
            base.Logger.LogInfo("full auto");
            this.AnnounceEvent("Full auto");
            this.EffectManager.AddComponent<AutomaticWeaponsEffectcs>();
        }

        // Token: 0x06000027 RID: 39 RVA: 0x00003C60 File Offset: 0x00001E60
        private void CoinsAreNowNails()
        {
            this.AnnounceEvent("i turned every nail into a coin :P");
            this.EffectManager.AddComponent<NailToCoin>();
        }

        // Token: 0x06000028 RID: 40 RVA: 0x00003C7C File Offset: 0x00001E7C
        private void BossBarForEveryone()
        {
            EnemyIdentifier[] array = Object.FindObjectsOfType<EnemyIdentifier>();
            foreach (EnemyIdentifier enemyIdentifier in array)
            {
                enemyIdentifier.gameObject.AddComponent<BossHealthBar>();
            }
            this.AnnounceEvent("Everyone is a boss now");
        }

        // Token: 0x06000029 RID: 41 RVA: 0x00003CC0 File Offset: 0x00001EC0
        private void OilUp()
        {
            EnemyIdentifier[] array = Object.FindObjectsOfType<EnemyIdentifier>();
            foreach (EnemyIdentifier enemyIdentifier in array)
            {
                for (int j = 0; j < 1000; j++)
                {
                    enemyIdentifier.AddFlammable(0.1f);
                }
            }
            this.AnnounceEvent("Did you pray today");
        }

        // Token: 0x0600002A RID: 42 RVA: 0x00003D20 File Offset: 0x00001F20
        private void ReadLol()
        {
            GameObject gameObject = new GameObject("red");
            ItemIdentifier itemIdentifier = gameObject.AddComponent<ItemIdentifier>();
            itemIdentifier.pickUpSound = new GameObject();
            itemIdentifier.itemType = ItemType.Readable;
            gameObject.AddComponent<Rigidbody>();
            gameObject.AddComponent<CheckForScroller>();
            Readable obj = gameObject.AddComponent<Readable>();
            Type typeFromHandle = typeof(Readable);
            FieldInfo field = typeFromHandle.GetField("content", BindingFlags.Instance | BindingFlags.NonPublic);
            bool flag = field != null;
            if (flag)
            {
                FieldInfo field2 = typeFromHandle.GetField("instantScan", BindingFlags.Instance | BindingFlags.NonPublic);
                bool flag2 = field2 != null;
                if (flag2)
                {
                    field2.SetValue(obj, true);
                    List<string> list = new List<string>
                    {
                        "You like reading, right?",
                        "Reading is fun!",
                        "Books are cool!",
                        "Are you a bookworm?",
                        "Reading expands the mind.",
                        "What's your favorite book genre?",
                        "Reading is a great way to learn.",
                        "Books take you on adventures."
                    };
                    field.SetValue(obj, list[Random.Range(0, list.Count)]);
                    MonoSingleton<PlayerUtilities>.Instance.ForceHoldObject(itemIdentifier);
                }
            }
        }

        // Token: 0x0600002B RID: 43 RVA: 0x00003E55 File Offset: 0x00002055
        private void BulletsAfraidNow()
        {
            this.EffectManager.AddComponent<BulletsAfraidEnemies>();
            this.AnnounceEvent("Bullets are afraid of enemies now");
        }

        // Token: 0x0600002C RID: 44 RVA: 0x00003E70 File Offset: 0x00002070
        private void BulletsExplodeNow()
        {
            this.EffectManager.AddComponent<ExplodingBulletsEffect>();
            this.AnnounceEvent("Bullets now explode");
        }

        // Token: 0x0600002D RID: 45 RVA: 0x00003E8B File Offset: 0x0000208B
        private void SchizophreniaUpdate()
        {
            this.EffectManager.AddComponent<sSchizophreniaUpdateEffect>();
        }

        // Token: 0x0600002E RID: 46 RVA: 0x00003E9A File Offset: 0x0000209A
        private void InvisibleEnemies()
        {
            this.EffectManager.AddComponent<InvisEnemies>();
            this.AnnounceEvent("Enemies are now invisible");
        }

        // Token: 0x0600002F RID: 47 RVA: 0x00003EB8 File Offset: 0x000020B8
        private void makeEnemyOutOfSomething()
        {
            try
            {
                List<MeshRenderer> list = Object.FindObjectsOfType<MeshRenderer>().ToList<MeshRenderer>();
                list.RemoveAll((MeshRenderer x) => x.name.Contains("Bloodstain"));
                GameObject gameObject = list[Random.Range(0, list.Count)].gameObject;
                List<SpawnableObject> list2 = Resources.FindObjectsOfTypeAll<SpawnableObject>().ToList<SpawnableObject>();
                list2.RemoveAll((SpawnableObject x) => x.spawnableObjectType != SpawnableObject.SpawnableObjectDataType.Enemy);
                SpawnableObject spawnableObject = list2[Random.Range(0, list2.Count)];
                GameObject gameObject2 = Object.Instantiate<GameObject>(spawnableObject.gameObject, gameObject.transform.position, spawnableObject.gameObject.transform.rotation);
                foreach (Renderer renderer in gameObject2.transform.GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = false;
                }
                bool flag = gameObject2.GetComponent<Renderer>() != null;
                if (flag)
                {
                    gameObject2.GetComponent<Renderer>().enabled = false;
                }
                gameObject2.transform.position = gameObject.transform.position;
                Transform transform = gameObject2.transform;
                gameObject.transform.position = transform.transform.position;
                gameObject.transform.parent = transform;
                gameObject.transform.localPosition = Vector3.zero;
                bool flag2 = gameObject.GetComponent<Collider>();
                if (flag2)
                {
                    gameObject.GetComponent<Collider>().enabled = false;
                }
                bool flag3 = gameObject.GetComponent<Rigidbody>();
                if (flag3)
                {
                    Object.Destroy(gameObject.GetComponent<Rigidbody>());
                }
                gameObject.transform.rotation = transform.transform.rotation;
                gameObject.transform.localRotation = Quaternion.identity;
                gameObject.gameObject.transform.localPosition = Vector3.zero;
                foreach (Collider collider in gameObject.transform.GetComponentsInChildren<Collider>())
                {
                    collider.enabled = false;
                }
                gameObject2.GetComponent<EnemyIdentifier>().spawnIn = false;
                this.AnnounceEvent(gameObject.name + " hates you now");
            }
            catch (Exception ex)
            {
                this.AnnounceEvent(ex.Message);
            }
        }

        // Token: 0x06000030 RID: 48 RVA: 0x00004134 File Offset: 0x00002334
        private void GiveTask()
        {
            int num = Random.Range(0, 2);
            int num2 = num;
            int num3 = num2;
            if (num3 != 0)
            {
                if (num3 == 1)
                {
                    Task task = TaskManager.Instance.Tasker.AddComponent<TestTask>();
                    TaskManager.Instance.AddTask(task);
                }
            }
            else
            {
                Task task = TaskManager.Instance.Tasker.AddComponent<KillEnemyTask>();
                TaskManager.Instance.AddTask(task);
            }
        }

        // Token: 0x06000031 RID: 49 RVA: 0x00004198 File Offset: 0x00002398
        private void AddRBRandomObjects()
        {
            List<MeshRenderer> list = Object.FindObjectsOfType<MeshRenderer>().ToList<MeshRenderer>();
            int value = this.maxAmountOfDeletedOjects.Value;
            int num = Random.Range(1, Mathf.Min(list.Count, value) + 1);
            list = (from obj in list
                    where !SceneManager.GetSceneAt(0).GetRootGameObjects().Contains(obj.gameObject)
                    select obj).ToList<MeshRenderer>();
            int num2 = 0;
            while (num2 < num && list.Count > 0)
            {
                GameObject gameObject = list[Random.Range(0, list.Count)].gameObject;
                gameObject.AddComponent<Rigidbody>();
                num2++;
            }
            this.AnnounceEvent(num.ToString() + " objects have discovered gravity");
        }

        // Token: 0x06000032 RID: 50 RVA: 0x00004256 File Offset: 0x00002456
        private void PixelizeScreen()
        {
            this.EffectManager.AddComponent<PixelReducer>();
            this.AnnounceEvent("go go gadget pixel reducer!");
        }

        // Token: 0x06000033 RID: 51 RVA: 0x00004274 File Offset: 0x00002474
        private void RemoveRandomObjects()
        {
            List<MeshRenderer> list = Object.FindObjectsOfType<MeshRenderer>().ToList<MeshRenderer>();
            int value = this.maxAmountOfDeletedOjects.Value;
            int num = Random.Range(1, Mathf.Min(list.Count, value) + 1);
            list = (from obj in list
                    where !SceneManager.GetSceneAt(0).GetRootGameObjects().Contains(obj.gameObject)
                    select obj).ToList<MeshRenderer>();
            int num2 = 0;
            while (num2 < num && list.Count > 0)
            {
                GameObject gameObject = list[Random.Range(0, list.Count)].gameObject;
                MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
                Object.Destroy(gameObject);
                list.Remove(component);
                num2++;
            }
            this.AnnounceEvent("i removed " + num.ToString() + " objects");
        }

        // Token: 0x06000034 RID: 52 RVA: 0x0000434C File Offset: 0x0000254C
        private void OpenRandomLaLink()
        {
            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string text = Path.Combine(directoryName, "JSONFiles");
            bool flag = !Directory.Exists(text);
            if (flag)
            {
                Directory.CreateDirectory(text);
            }
            bool flag2 = !Directory.Exists(text);
            if (flag2)
            {
                Debug.LogError("Folder path does not exist: " + text);
            }
            else
            {
                string[] files = Directory.GetFiles(text, "*.json");
                bool flag3 = files.Length == 0;
                if (flag3)
                {
                    this.CreateJsonFolder();
                }
                else
                {
                    string text2 = File.ReadAllText(text + "/" + this.jsonFilePath);
                    this.links = JsonConvert.DeserializeObject<List<UltraEventsPlugin.LinkData>>(text2);
                    this.OpenRandomLink();
                }
            }
        }

        // Token: 0x06000035 RID: 53 RVA: 0x000043FC File Offset: 0x000025FC
        private void OpenRandomLink()
        {
            bool flag = this.links.Count > 0;
            if (flag)
            {
                UltraEventsPlugin.LinkData linkData = this.links[Random.Range(0, this.links.Count)];
                Application.OpenURL(linkData.link);
            }
            else
            {
                Debug.LogWarning("No links found in the JSON file.");
            }
        }

        // Token: 0x06000036 RID: 54 RVA: 0x00004456 File Offset: 0x00002656
        private void RemoveCharge()
        {
            this.AnnounceEvent("no charge?");
            MonoSingleton<WeaponCharges>.Instance.raicharge = 0f;
        }

        // Token: 0x06000037 RID: 55 RVA: 0x00004474 File Offset: 0x00002674
        private void RemoveStamina()
        {
            this.AnnounceEvent("no stamina?");
            ModUtils.GetPlayerTransform().EmptyStamina();
        }

        // Token: 0x06000038 RID: 56 RVA: 0x00004490 File Offset: 0x00002690
        private void DupeAllEnemy()
        {
            this.AnnounceEvent("ever heard of mitosis?");
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            foreach (EnemyIdentifier enemyIdentifier in list)
            {
                EnemyIdentifier enemyIdentifier2 = Object.Instantiate<EnemyIdentifier>(enemyIdentifier, enemyIdentifier.transform.position, enemyIdentifier.transform.rotation);
                enemyIdentifier2.transform.localScale = enemyIdentifier.transform.localScale;
            }
        }

        // Token: 0x06000039 RID: 57 RVA: 0x00004550 File Offset: 0x00002750
        private void AirStrike()
        {
            this.AnnounceEvent("By the magic of the angels. I cast thee away");
            VirtueInsignia virtueInsignia = Resources.FindObjectsOfTypeAll<VirtueInsignia>()[0];
            NewMovement playerTransform = ModUtils.GetPlayerTransform();
            VirtueInsignia virtueInsignia2 = Object.Instantiate<VirtueInsignia>(virtueInsignia, playerTransform.transform.position, Quaternion.identity);
            EnemyTarget target = new EnemyTarget(playerTransform.transform);
            virtueInsignia2.target = target;
        }

        // Token: 0x0600003A RID: 58 RVA: 0x000045A4 File Offset: 0x000027A4
        private void Alakablam()
        {
            this.AnnounceEvent("Alakablam");
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            VirtueInsignia virtueInsignia = Resources.FindObjectsOfTypeAll<VirtueInsignia>()[0];
            foreach (EnemyIdentifier enemyIdentifier in list)
            {
                VirtueInsignia virtueInsignia2 = Object.Instantiate<VirtueInsignia>(virtueInsignia, enemyIdentifier.transform.position, Quaternion.identity);
                virtueInsignia2.windUpSpeedMultiplier = 5f;
                EnemyTarget target = new EnemyTarget(enemyIdentifier);
                virtueInsignia2.target = target;
            }
        }

        // Token: 0x0600003B RID: 59 RVA: 0x0000466C File Offset: 0x0000286C
        private void LoadCat()
        {
            this.AnnounceEvent("Spawn cat");
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.transform.position = ModUtils.GetPlayerTransform().transform.position;
            gameObject.AddComponent<Rigidbody>();
            this.catRenderer = gameObject.GetComponent<Renderer>();
            Material material = new Material(this.unlitShader);
            this.catRenderer.material = material;
            base.StartCoroutine(this.LoadCatImage());
        }

        // Token: 0x0600003C RID: 60 RVA: 0x000046E1 File Offset: 0x000028E1
        private IEnumerator LoadCatImage()
        {
            using (UnityWebRequest www = UnityWebRequest.Get(this.apiUrl))
            {
                yield return www.SendWebRequest();
                bool flag = www.isNetworkError || www.isHttpError;
                if (flag)
                {
                    Debug.LogError("Failed to fetch cat image: " + www.error);
                }
                else
                {
                    string jsonResponse = www.downloadHandler.text;
                    base.Logger.LogInfo(jsonResponse);
                    List<Root> images = JsonConvert.DeserializeObject<List<Root>>(jsonResponse);
                    bool flag2 = images != null && images.Count > 0;
                    if (flag2)
                    {
                        string imageUrl = images[0].url;
                        base.StartCoroutine(this.LoadImageTexture(imageUrl));
                        imageUrl = null;
                    }
                    else
                    {
                        Debug.LogError("No cat images found in the API response.");
                    }
                    jsonResponse = null;
                    images = null;
                }
            }
            yield break;
        }

        // Token: 0x0600003D RID: 61 RVA: 0x000046F0 File Offset: 0x000028F0
        private IEnumerator LoadImageTexture(string url)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();
                bool flag = www.isNetworkError || www.isHttpError;
                if (flag)
                {
                    Debug.LogError("Failed to fetch cat image texture: " + www.error);
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(www);
                    bool flag2 = texture != null;
                    if (flag2)
                    {
                        this.catRenderer.material.mainTexture = texture;
                        this.catRenderer.material.SetTexture("_MainTex", texture);
                        float width = (float)texture.width;
                        float height = (float)texture.height;
                        float scaleFactor = 0.01f;
                        this.catRenderer.gameObject.transform.localScale = new Vector3(width * scaleFactor, height * scaleFactor, 1f);
                    }
                    else
                    {
                        Debug.LogError("Failed to load cat image texture.");
                    }
                    texture = null;
                }
            }
            yield break;
        }

        // Token: 0x0600003E RID: 62 RVA: 0x00004708 File Offset: 0x00002908
        private void SpawnAd()
        {
            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string text = Path.Combine(directoryName, "Videos");
            bool flag = !Directory.Exists(text);
            if (flag)
            {
                Directory.CreateDirectory(text);
            }
            bool flag2 = !Directory.Exists(text);
            if (flag2)
            {
                Debug.LogError("Folder path does not exist: " + text);
            }
            else
            {
                string[] files = Directory.GetFiles(text, "*.mp4");
                this.LoadRandomVideo(files, text);
            }
        }

        // Token: 0x0600003F RID: 63 RVA: 0x00004780 File Offset: 0x00002980
        private void LoadRandomVideo(string[] videoFiles, string folderPath)
        {
            bool flag = videoFiles.Length == 0;
            if (flag)
            {
                Debug.LogError("No mp4 files found in folder: " + folderPath);
                this.CreateVideoFolder();
            }
            else
            {
                string path = videoFiles[Random.Range(0, videoFiles.Length)];
                List<Canvas> source = ModUtils.GetPlayerTransform().transform.GetComponentsInChildren<Canvas>().ToList<Canvas>();
                Canvas canvas = source.First((Canvas x) => x.name.ToLower() == "finishcanvas");
                this.LoadVideo(path, canvas);
            }
        }

        // Token: 0x06000040 RID: 64 RVA: 0x00004804 File Offset: 0x00002A04
        private void LoadVideo(string path, Canvas canvas)
        {
            GameObject videoDisplayObject = new GameObject("VideoDisplay");
            videoDisplayObject.transform.SetParent(canvas.transform, false);
            RawImage rawImage = videoDisplayObject.AddComponent<RawImage>();
            RectTransform component = rawImage.GetComponent<RectTransform>();
            component.sizeDelta = new Vector2(canvas.pixelRect.width, canvas.pixelRect.height);
            GameObject gameObject = new GameObject("VideoPlayer");
            VideoPlayer videoPlayer = gameObject.AddComponent<VideoPlayer>();
            videoPlayer.url = path;
            videoPlayer.targetTexture = new RenderTexture((int)component.rect.width, (int)component.rect.height, 0);
            rawImage.texture = videoPlayer.targetTexture;
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            float num = Mathf.Min(canvas.pixelRect.width, canvas.pixelRect.height);
            Debug.Log("e");
            float num2 = Random.Range(100f, num);
            float num3 = Random.Range(100f, num);
            Debug.Log("e");
            bool flag = num3 > num;
            if (flag)
            {
                num3 = num;
                num2 = num3;
            }
            Debug.Log("e");
            float num4 = Random.Range(-canvas.pixelRect.width / 2f, canvas.pixelRect.width / 2f);
            float num5 = Random.Range(-canvas.pixelRect.height / 2f, canvas.pixelRect.height / 2f);
            component.sizeDelta = new Vector2(num2, num3);
            Debug.Log("e");
            component.anchoredPosition = new Vector2(num4, num5);
            Debug.Log("e");
            videoPlayer.loopPointReached += delegate
            {
                this.OnVideoFinished(videoDisplayObject);
            };
            Debug.Log("e");
            videoPlayer.Play();
            Debug.Log("e");
        }

        // Token: 0x06000041 RID: 65 RVA: 0x00004A29 File Offset: 0x00002C29
        private void OnVideoFinished(GameObject videoDisplayObject)
        {
            Object.Destroy(videoDisplayObject);
        }

        // Token: 0x06000042 RID: 66 RVA: 0x00004A33 File Offset: 0x00002C33
        private void FakeParry()
        {
            MonoSingleton<TimeController>.Instance.ParryFlash();
        }

        // Token: 0x06000043 RID: 67 RVA: 0x00004A41 File Offset: 0x00002C41
        private void Dies()
        {
            this.AnnounceEvent("DIE");
            ModUtils.GetPlayerTransform().GetHurt(int.MaxValue, false, 1f, false, false, 0.35f, false);
        }

        // Token: 0x06000044 RID: 68 RVA: 0x00004A70 File Offset: 0x00002C70
        private void RemoveStyle()
        {
            this.AnnounceEvent("im gonna take some style points real quick");
            int num = Random.Range(0, MonoSingleton<StatsManager>.Instance.stylePoints);
            MonoSingleton<StatsManager>.Instance.stylePoints -= num;
        }

        // Token: 0x06000045 RID: 69 RVA: 0x00004AB0 File Offset: 0x00002CB0
        private void SwitchArm()
        {
            FistControl fistControl = Object.FindObjectOfType<FistControl>();
            fistControl.ScrollArm();
        }

        // Token: 0x06000046 RID: 70 RVA: 0x00004ACC File Offset: 0x00002CCC
        private void SwapPos()
        {
            List<MeshRenderer> list = Object.FindObjectsOfType<MeshRenderer>().ToList<MeshRenderer>();
            GameObject gameObject = list[Random.Range(0, list.Count)].gameObject;
            GameObject gameObject2 = list[Random.Range(0, list.Count)].gameObject;
            base.StartCoroutine(this.SwapCoroutine(gameObject, gameObject2, this.AmountOfTime.Value));
            this.AnnounceEvent(gameObject.name + " and " + gameObject2.name + " swapped places");
        }

        // Token: 0x06000047 RID: 71 RVA: 0x00004B51 File Offset: 0x00002D51
        private IEnumerator SwapCoroutine(GameObject object1, GameObject object2, float swapDuration)
        {
            Vector3 startPos = object1.transform.position;
            Vector3 startPos2 = object2.transform.position;
            float timeElapsed = 0f;
            while (timeElapsed < swapDuration)
            {
                float t = timeElapsed / swapDuration;
                object1.transform.position = Vector3.Lerp(startPos, startPos2, t);
                object2.transform.position = Vector3.Lerp(startPos2, startPos, t);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            object1.transform.position = startPos2;
            object2.transform.position = startPos;
            yield break;
        }

        // Token: 0x06000048 RID: 72 RVA: 0x00004B78 File Offset: 0x00002D78
        private void TeleportToEnemy()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            EnemyIdentifier enemyIdentifier = list[Random.Range(0, list.Count)];
            ModUtils.GetPlayerTransform().transform.position = enemyIdentifier.transform.position;
            this.AnnounceEvent("teleports behind " + enemyIdentifier.gameObject.name);
        }

        // Token: 0x06000049 RID: 73 RVA: 0x00004C01 File Offset: 0x00002E01
        private void MoreTrouble()
        {
            base.StopCoroutine("overTimeEvents");
            this.AnnounceEvent("prepare for trouble. And make it double!");
            base.StartCoroutine(this.overTimeEvents(2));
        }

        // Token: 0x0600004A RID: 74 RVA: 0x00004C2A File Offset: 0x00002E2A
        private IEnumerator overTimeEvents(int amount)
        {
            yield return new WaitForSeconds(1.5f);
            int num;
            for (int i = 0; i < amount; i = num + 1)
            {
                this.UseRandomEvent();
                yield return new WaitForSeconds(0.5f);
                num = i;
            }
            this.timer = this.AmountOfTime.Value;
            yield break;
        }

        // Token: 0x0600004B RID: 75 RVA: 0x00004C40 File Offset: 0x00002E40
        private void TurnEnemyIntoPuppet()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            EnemyIdentifier enemyIdentifier = list[Random.Range(0, list.Count)];
            enemyIdentifier.puppet = true;
            enemyIdentifier.PuppetSpawn();
            enemyIdentifier.dontCountAsKills = false;
            this.AnnounceEvent(enemyIdentifier.gameObject.name + " is now a puppet");
        }

        // Token: 0x0600004C RID: 76 RVA: 0x00004CC4 File Offset: 0x00002EC4
        private void GetRod()
        {
            try
            {
                this.AnnounceEvent("its fishing time");
                bool flag = Object.FindObjectOfType<FishingHUD>() == null;
                if (flag)
                {
                    Object.Instantiate<GameObject>(this.fishingCanvas, ModUtils.GetPlayerTransform().transform.position, Quaternion.identity);
                }
                GunSetter gs = Object.FindObjectOfType<GunSetter>();
                ModUtils.AttachWeapon(1, "", this.rot, gs);
            }
            catch (Exception ex)
            {
                this.AnnounceEvent(ex.Message);
            }
        }

        // Token: 0x0600004D RID: 77 RVA: 0x00004D50 File Offset: 0x00002F50
        private void noHeals()
        {
            this.AnnounceEvent("no heals?");
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            foreach (EnemyIdentifier enemyIdentifier in list)
            {
                enemyIdentifier.Sandify(false);
            }
        }

        // Token: 0x0600004E RID: 78 RVA: 0x00004DE4 File Offset: 0x00002FE4
        private void waer()
        {
            this.AnnounceEvent("hello how are you? i am under the water");
            this.EffectManager.AddComponent<aboohwaer>();
        }

        // Token: 0x0600004F RID: 79 RVA: 0x00004DFF File Offset: 0x00002FFF
        private void BlessthemAll()
        {
            this.AnnounceEvent("enemies now are protected by god");
            this.EffectManager.AddComponent<BlessAll>();
        }

        // Token: 0x06000050 RID: 80 RVA: 0x00004E1A File Offset: 0x0000301A
        private void Lag()
        {
            this.AnnounceEvent("your ping is so high");
            this.EffectManager.AddComponent<Lagging>();
        }

        // Token: 0x06000051 RID: 81 RVA: 0x00004E38 File Offset: 0x00003038
        private void SpawnRandomEnemy()
        {
            List<SpawnableObject> list = Resources.FindObjectsOfTypeAll<SpawnableObject>().ToList<SpawnableObject>();
            list.RemoveAll((SpawnableObject x) => x.spawnableObjectType != SpawnableObject.SpawnableObjectDataType.Enemy);
            SpawnableObject spawnableObject = list[Random.Range(0, list.Count)];
            Object.Instantiate<GameObject>(spawnableObject.gameObject, ModUtils.GetPlayerTransform().transform.position, Quaternion.identity);
            this.AnnounceEvent("spawned " + spawnableObject.objectName);
        }

        // Token: 0x06000052 RID: 82 RVA: 0x00004EC4 File Offset: 0x000030C4
        private void GiveDualWield()
        {
            this.AnnounceEvent("its dual wielding time!!! *dual wields all over the place*");
            int num = Random.Range(1, 10);
            for (int i = 0; i < num; i++)
            {
                bool flag = MonoSingleton<GunControl>.Instance;
                if (flag)
                {
                    MonoSingleton<CameraController>.Instance.CameraShake(0.35f);
                    bool flag2 = MonoSingleton<PlayerTracker>.Instance.playerType == PlayerType.Platformer;
                    if (flag2)
                    {
                        MonoSingleton<PlatformerMovement>.Instance.AddExtraHit(3);
                        break;
                    }
                    GameObject gameObject = new GameObject();
                    gameObject.transform.SetParent(MonoSingleton<GunControl>.Instance.transform, true);
                    gameObject.transform.localRotation = Quaternion.identity;
                    DualWield[] componentsInChildren = MonoSingleton<GunControl>.Instance.GetComponentsInChildren<DualWield>();
                    bool flag3 = componentsInChildren != null && componentsInChildren.Length % 2 == 0;
                    if (flag3)
                    {
                        gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                    }
                    else
                    {
                        gameObject.transform.localScale = Vector3.one;
                    }
                    bool flag4 = componentsInChildren == null || componentsInChildren.Length == 0;
                    if (flag4)
                    {
                        gameObject.transform.localPosition = Vector3.zero;
                    }
                    else
                    {
                        bool flag5 = componentsInChildren.Length % 2 == 0;
                        if (flag5)
                        {
                            gameObject.transform.localPosition = new Vector3((float)(componentsInChildren.Length / 2) * -1.5f, 0f, 0f);
                        }
                        else
                        {
                            gameObject.transform.localPosition = new Vector3((float)((componentsInChildren.Length + 1) / 2) * 1.5f, 0f, 0f);
                        }
                    }
                    DualWield dualWield = gameObject.AddComponent<DualWield>();
                    dualWield.delay = 0.05f;
                    dualWield.juiceAmount = 30f;
                    bool flag6 = componentsInChildren != null && componentsInChildren.Length != 0;
                    if (flag6)
                    {
                        dualWield.delay += (float)componentsInChildren.Length / 20f;
                    }
                }
            }
        }

        // Token: 0x06000053 RID: 83 RVA: 0x000050AD File Offset: 0x000032AD
        private void SlowMotion()
        {
            this.AnnounceEvent("wow this is slow");
            this.EffectManager.AddComponent<Slowmotion>();
        }

        // Token: 0x06000054 RID: 84 RVA: 0x000050C8 File Offset: 0x000032C8
        private void FastMotion()
        {
            this.AnnounceEvent("wow this is Fast");
            this.EffectManager.AddComponent<Fastmotion>();
        }

        // Token: 0x06000055 RID: 85 RVA: 0x000050E3 File Offset: 0x000032E3
        private void SpawnItem()
        {
            this.AnnounceEvent("Plush rain!!!!");
            this.EffectManager.AddComponent<PlushRain>();
        }

        // Token: 0x06000056 RID: 86 RVA: 0x00005100 File Offset: 0x00003300
        private void AddRBRandomObject()
        {
            List<GameObject> list = Object.FindObjectsOfType<GameObject>().ToList<GameObject>();
            GameObject gameObject = list[Random.Range(0, list.Count)];
            this.AnnounceEvent(gameObject.name + " discovered gravity");
            gameObject.AddComponent<Rigidbody>();
        }

        // Token: 0x06000057 RID: 87 RVA: 0x0000514C File Offset: 0x0000334C
        private void RemoveRandomObject()
        {
            List<GameObject> list = Object.FindObjectsOfType<GameObject>().ToList<GameObject>();
            GameObject gameObject = list[Random.Range(0, list.Count)];
            this.AnnounceEvent("i removed something");
            Object.Destroy(gameObject);
        }

        // Token: 0x06000058 RID: 88 RVA: 0x0000518C File Offset: 0x0000338C
        private void UseRandomInput()
        {
            this.AnnounceEvent("gonna fire your gun");
            GunControl gunControl = Object.FindObjectOfType<GunControl>();
            GameObject currentWeapon = gunControl.currentWeapon;
            bool flag = currentWeapon.GetComponent<Revolver>();
            if (flag)
            {
                Revolver component = currentWeapon.GetComponent<Revolver>();
                Type typeFromHandle = typeof(Revolver);
                MethodInfo method = typeFromHandle.GetMethod("Shoot", BindingFlags.Instance | BindingFlags.NonPublic);
                bool flag2 = method != null;
                if (flag2)
                {
                    int num = Random.Range(1, 3);
                    method.Invoke(component, new object[]
                    {
                        num
                    });
                }
                else
                {
                    base.Logger.LogInfo("Shoot method not found.");
                }
            }
            else
            {
                bool flag3 = currentWeapon.GetComponent<Shotgun>();
                if (flag3)
                {
                    Shotgun component2 = currentWeapon.GetComponent<Shotgun>();
                    Type typeFromHandle2 = typeof(Shotgun);
                    MethodInfo method2 = typeFromHandle2.GetMethod("Shoot", BindingFlags.Instance | BindingFlags.NonPublic);
                    bool flag4 = method2 != null;
                    if (flag4)
                    {
                        method2.Invoke(component2, null);
                    }
                    else
                    {
                        base.Logger.LogInfo("Shoot method not found.");
                    }
                }
                else
                {
                    bool flag5 = currentWeapon.GetComponent<Nailgun>();
                    if (flag5)
                    {
                        Nailgun component3 = currentWeapon.GetComponent<Nailgun>();
                        Type typeFromHandle3 = typeof(Nailgun);
                        MethodInfo method3 = typeFromHandle3.GetMethod("Shoot", BindingFlags.Instance | BindingFlags.NonPublic);
                        bool flag6 = method3 != null;
                        if (flag6)
                        {
                            method3.Invoke(component3, null);
                        }
                        else
                        {
                            base.Logger.LogInfo("Shoot method not found.");
                        }
                    }
                    else
                    {
                        bool flag7 = currentWeapon.GetComponent<RocketLauncher>();
                        if (flag7)
                        {
                            RocketLauncher component4 = currentWeapon.GetComponent<RocketLauncher>();
                            Type typeFromHandle4 = typeof(RocketLauncher);
                            MethodInfo method4 = typeFromHandle4.GetMethod("Shoot", BindingFlags.Instance | BindingFlags.NonPublic);
                            bool flag8 = method4 != null;
                            if (flag8)
                            {
                                method4.Invoke(component4, null);
                            }
                            else
                            {
                                base.Logger.LogInfo("Shoot method not found.");
                            }
                        }
                        else
                        {
                            bool flag9 = currentWeapon.GetComponent<Railcannon>();
                            if (flag9)
                            {
                                Railcannon component5 = currentWeapon.GetComponent<Railcannon>();
                                Type typeFromHandle5 = typeof(Railcannon);
                                MethodInfo method5 = typeFromHandle5.GetMethod("Shoot", BindingFlags.Instance | BindingFlags.NonPublic);
                                bool flag10 = method5 != null;
                                if (flag10)
                                {
                                    method5.Invoke(component5, null);
                                }
                                else
                                {
                                    base.Logger.LogInfo("Shoot method not found.");
                                }
                            }
                        }
                    }
                }
            }
        }

        // Token: 0x06000059 RID: 89 RVA: 0x000053DC File Offset: 0x000035DC
        private void ReverseGravity()
        {
            Physics.gravity *= -1f;
            bool flag = Physics.gravity.y > 0f;
            if (flag)
            {
                this.AnnounceEvent("Why is my apple falling upwards");
            }
            else
            {
                this.AnnounceEvent("Why is my apple falling downwards");
            }
        }

        // Token: 0x0600005A RID: 90 RVA: 0x00005434 File Offset: 0x00003634
        private void KillRandomEnemy()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            EnemyIdentifier enemyIdentifier = list[Random.Range(0, list.Count)];
            this.AnnounceEvent("fuck you in particular " + enemyIdentifier.gameObject.name);
            enemyIdentifier.InstaKill();
        }

        // Token: 0x0600005B RID: 91 RVA: 0x000054AC File Offset: 0x000036AC
        private void KillAllEnemy()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            foreach (EnemyIdentifier enemyIdentifier in list)
            {
                enemyIdentifier.InstaKill();
            }
            this.AnnounceEvent("DIE EVERYONE!");
        }

        // Token: 0x0600005C RID: 92 RVA: 0x0000553C File Offset: 0x0000373C
        private void giveRandomWeapon()
        {
            this.AnnounceEvent("Here let me choose for you");
            GunControl gunControl = Object.FindObjectOfType<GunControl>();
            int num = Random.Range(0, gunControl.slots.Count);
            gunControl.SwitchWeapon(num, gunControl.slots[num - 1], true, false, false, false);
        }

        // Token: 0x0600005D RID: 93 RVA: 0x00005588 File Offset: 0x00003788
        private void BuffEnemy()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            EnemyIdentifier enemyIdentifier = list[Random.Range(0, list.Count)];
            enemyIdentifier.BuffAll();
            this.AnnounceEvent("\"will you lose?\" \"nah id win\" -" + enemyIdentifier.gameObject.name);
        }

        // Token: 0x0600005E RID: 94 RVA: 0x00005600 File Offset: 0x00003800
        private void DupeEnemy()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            EnemyIdentifier enemyIdentifier = list[Random.Range(0, list.Count)];
            EnemyIdentifier enemyIdentifier2 = Object.Instantiate<EnemyIdentifier>(enemyIdentifier, enemyIdentifier.transform.position, enemyIdentifier.transform.rotation);
            enemyIdentifier2.transform.localScale = enemyIdentifier.transform.localScale;
            this.AnnounceEvent("i added another " + enemyIdentifier.enemyType.ToString());
        }

        // Token: 0x0600005F RID: 95 RVA: 0x000056A8 File Offset: 0x000038A8
        private void Kaboom()
        {
            this.AnnounceEvent("KABOOOOOOM");
            EnemyIdentifier[] array = Object.FindObjectsOfType<EnemyIdentifier>();
            List<ExplosionController> list = Resources.FindObjectsOfTypeAll<ExplosionController>().ToList<ExplosionController>();
            list.RemoveAll((ExplosionController x) => x.gameObject.name.ToLower().Contains("fire"));
            foreach (EnemyIdentifier enemyIdentifier in array)
            {
                ExplosionController explosionController = list[Random.Range(0, list.Count)];
                Object.Instantiate<ExplosionController>(explosionController, enemyIdentifier.transform.position, Quaternion.identity);
            }
        }

        // Token: 0x06000060 RID: 96 RVA: 0x00005740 File Offset: 0x00003940
        private void usePreviousWeapon()
        {
            this.AnnounceEvent("go back to the other weapon");
            GunControl gunControl = Object.FindObjectOfType<GunControl>();
            gunControl.SwitchWeapon(gunControl.lastUsedSlot, gunControl.slots[gunControl.lastUsedSlot - 1], true, false, false, false);
        }

        // Token: 0x06000061 RID: 97 RVA: 0x00005784 File Offset: 0x00003984
        private void RemoveWeapon()
        {
            this.AnnounceEvent("you dont need this right?");
            GunControl gunControl = Object.FindObjectOfType<GunControl>();
            GameObject currentWeapon = gunControl.currentWeapon;
            gunControl.allWeapons.Remove(currentWeapon);
            gunControl.slotDict.Remove(currentWeapon);
            Object.Destroy(currentWeapon);
        }

        // Token: 0x06000062 RID: 98 RVA: 0x000057CC File Offset: 0x000039CC
        private void TPEnemies()
        {
            this.AnnounceEvent("teleports behind you");
            EnemyIdentifier[] array = Object.FindObjectsOfType<EnemyIdentifier>();
            foreach (EnemyIdentifier enemyIdentifier in array)
            {
                enemyIdentifier.transform.position = ModUtils.GetPlayerTransform().transform.position;
            }
        }

        // Token: 0x06000063 RID: 99 RVA: 0x0000581E File Offset: 0x00003A1E
        private void YEET()
        {
            this.AnnounceEvent("welcome to space :O");
            ModUtils.GetPlayerTransform().LaunchFromPoint(ModUtils.GetPlayerTransform().transform.position, 50000000f, 1f);
        }



        // Token: 0x04000009 RID: 9
        public static bool AutomaticFireEffectActive;

        // Token: 0x0400000A RID: 10
        private GameObject EffectManager;

        // Token: 0x0400000B RID: 11
        private GameObject TaskManagerObject;

        // Token: 0x0400000C RID: 12
        private static readonly Harmony Harmony = new Harmony("com.michi.UltraEvents");

        // Token: 0x0400000D RID: 13
        public static ManualLogSource Log = new ManualLogSource("UltraEvents");

        // Token: 0x0400000E RID: 14
        public static List<GameObject> plushies = new List<GameObject>();

        // Token: 0x0400000F RID: 15
        private GameObject fishingCanvas;

        // Token: 0x04000010 RID: 16
        public Shader unlitShader;

        // Token: 0x04000011 RID: 17
        private float timer = 5f;

        // Token: 0x04000012 RID: 18
        public ConfigEntry<float> AmountOfTime;

        // Token: 0x04000013 RID: 19
        public ConfigEntry<int> maxAmountOfDeletedOjects;

        // Token: 0x04000014 RID: 20
        public ConfigEntry<bool> rmeoveEffects;

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

        // Token: 0x0400001C RID: 28
        private GameObject rot = null;

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
        #region configs
        // Token: 0x04000020 RID: 32
        public ConfigEntry<bool> YEETEvent;

        // Token: 0x04000021 RID: 33
        public ConfigEntry<bool> TPEnemiesEvent;

        // Token: 0x04000022 RID: 34
        public ConfigEntry<bool> RemoveWeaponEvent;

        // Token: 0x04000023 RID: 35
        public ConfigEntry<bool> usePreviousWeaponEvent;

        // Token: 0x04000024 RID: 36
        public ConfigEntry<bool> KaboomEvent;

        // Token: 0x04000025 RID: 37
        public ConfigEntry<bool> DupeEnemyEvent;

        // Token: 0x04000026 RID: 38
        public ConfigEntry<bool> BuffEnemyEvent;

        // Token: 0x04000027 RID: 39
        public ConfigEntry<bool> giveRandomWeaponEvent;

        // Token: 0x04000028 RID: 40
        public ConfigEntry<bool> ReverseGravityEvent;

        // Token: 0x04000029 RID: 41
        public ConfigEntry<bool> KillRandomEnemyEvent;

        // Token: 0x0400002A RID: 42
        public ConfigEntry<bool> KillAllEnemyEvent;

        // Token: 0x0400002B RID: 43
        public ConfigEntry<bool> UseRandomInputEvent;

        // Token: 0x0400002C RID: 44
        public ConfigEntry<bool> RemoveRandomObjectEvent;

        // Token: 0x0400002D RID: 45
        public ConfigEntry<bool> AddRBRandomObjectEvent;

        // Token: 0x0400002E RID: 46
        public ConfigEntry<bool> SpawnItemEvent;

        // Token: 0x0400002F RID: 47
        public ConfigEntry<bool> SlowMotionEvent;

        // Token: 0x04000030 RID: 48
        public ConfigEntry<bool> GiveDualWieldEvent;

        // Token: 0x04000031 RID: 49
        public ConfigEntry<bool> SpawnRandomEnemyEvent;

        // Token: 0x04000032 RID: 50
        public ConfigEntry<bool> LagEvent;

        // Token: 0x04000033 RID: 51
        public ConfigEntry<bool> BlessthemAllEvent;

        // Token: 0x04000034 RID: 52
        public ConfigEntry<bool> waerEvent;

        // Token: 0x04000035 RID: 53
        public ConfigEntry<bool> noHealsEvent;

        // Token: 0x04000036 RID: 54
        public ConfigEntry<bool> GetRodEvent;

        // Token: 0x04000037 RID: 55
        public ConfigEntry<bool> TurnEnemyIntoPuppetEvent;

        // Token: 0x04000038 RID: 56
        public ConfigEntry<bool> MoreTroubleEvent;

        // Token: 0x04000039 RID: 57
        public ConfigEntry<bool> TeleportToEnemyEvent;

        // Token: 0x0400003A RID: 58
        public ConfigEntry<bool> SwapPosEvent;

        // Token: 0x0400003B RID: 59
        public ConfigEntry<bool> FastMotionEvent;

        // Token: 0x0400003C RID: 60
        public ConfigEntry<bool> SwitchArmEvent;

        // Token: 0x0400003D RID: 61
        public ConfigEntry<bool> RemoveStyleEvent;

        // Token: 0x0400003E RID: 62
        public ConfigEntry<bool> DiesEvent;

        // Token: 0x0400003F RID: 63
        public ConfigEntry<bool> FakeParryEvent;

        // Token: 0x04000040 RID: 64
        public ConfigEntry<bool> SpawnAdEvent;

        // Token: 0x04000041 RID: 65
        public ConfigEntry<bool> LoadCatEvent;

        // Token: 0x04000042 RID: 66
        public ConfigEntry<bool> AlakablamEvent;

        // Token: 0x04000043 RID: 67
        public ConfigEntry<bool> AirStrikeEvent;

        // Token: 0x04000044 RID: 68
        public ConfigEntry<bool> DupeAllEnemyEvent;

        // Token: 0x04000045 RID: 69
        public ConfigEntry<bool> RemoveStaminaEvent;

        // Token: 0x04000046 RID: 70
        public ConfigEntry<bool> RemoveChargeEvent;

        // Token: 0x04000047 RID: 71
        public ConfigEntry<bool> OpenRandomLaLinkEvent;

        // Token: 0x04000048 RID: 72
        public ConfigEntry<bool> RemoveRandomObjectsEvent;

        // Token: 0x04000049 RID: 73
        public ConfigEntry<bool> PixelizeScreenEvent;

        // Token: 0x0400004A RID: 74
        public ConfigEntry<bool> AddRBRandomObjectsEvent;

        // Token: 0x0400004B RID: 75
        public ConfigEntry<bool> GetTaskEvent;

        // Token: 0x0400004C RID: 76
        public ConfigEntry<bool> MakeEnemyOutOfSomethingEvent;

        // Token: 0x0400004D RID: 77
        public ConfigEntry<bool> InvisibleEnemiesEvent;

        // Token: 0x0400004E RID: 78
        public ConfigEntry<bool> SchizophreniaUpdateEvent;

        // Token: 0x0400004F RID: 79
        public ConfigEntry<bool> BulletsExplodeNowEvent;

        // Token: 0x04000050 RID: 80
        public ConfigEntry<bool> BulletsAfraidEnemiesEvent;

        // Token: 0x04000051 RID: 81
        public ConfigEntry<bool> ReadEvent;

        // Token: 0x04000052 RID: 82
        public ConfigEntry<bool> MoveEverythingEvent;

        // Token: 0x04000053 RID: 83
        private ConfigEntry<bool> BossBarForEveryoneEvent;

        // Token: 0x04000054 RID: 84
        private ConfigEntry<bool> OilUpEvent;

        // Token: 0x04000055 RID: 85
        public ConfigEntry<bool> NailToCoinEvent;

        // Token: 0x04000056 RID: 86
        public ConfigEntry<bool> AutomaticWeaponsEffectEvent;

        // Token: 0x04000057 RID: 87
        public ConfigEntry<bool> NoRicoshotsEvent;

        // Token: 0x04000058 RID: 88
        public ConfigEntry<bool> AttachEverythingToPlayerEvent;

        // Token: 0x04000059 RID: 89
        public ConfigEntry<bool> RulesOfNatureEvent;

        // Token: 0x0400005A RID: 90
        public ConfigEntry<bool> nanoMachinesSonEvent;

        // Token: 0x0400005B RID: 91
        public ConfigEntry<bool> AllyEvent;

        // Token: 0x0400005C RID: 92
        public ConfigEntry<bool> SomethingWickedThisWayComesEvent;
        #endregion
        // Token: 0x0400005D RID: 93
        public string jsonFilePath = "UltraEvents.Jsons.Links.json";

        // Token: 0x0400005E RID: 94
        private List<UltraEventsPlugin.LinkData> links = new List<UltraEventsPlugin.LinkData>();

        // Token: 0x0400005F RID: 95
        public string apiUrl = "https://api.thecatapi.com/v1/images/search?limit=1&breed_ids=beng&api_key=REPLACE_ME";

        // Token: 0x04000060 RID: 96
        public Renderer catRenderer;

        // Token: 0x02000027 RID: 39
        [Serializable]
        public class LinkData
        {
            // Token: 0x0400008B RID: 139
            public string link;
        }
    }
}
