using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UltraEvents.MonoBehaviours.Effects;
using UltraEvents.MonoBehaviours.Tasks;
using UltraEvents.MonoBehaviours;
using UltraEvents.Utils;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace UltraEvents
{
    public class Events : MonoBehaviour
    {
        // Token: 0x0600001D RID: 29 RVA: 0x000037CF File Offset: 0x000019CF
        public void SomethingWickedThisWayComesVoid()
        {
            this.AnnounceEvent("Something wicked this way comes");
            UltraEventsPlugin.Instance.EffectManager.AddComponent<SomethingWickedThisWayComes>();
            
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
        public void AnnounceEvent(string message)
        {
            bool flag = !UltraEventsPlugin.Instance.announceEvents.Value;
            if (!flag)
            {
                MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage(message, "", "", 0, false);
            }
        }

        // Token: 0x06000020 RID: 32 RVA: 0x00003874 File Offset: 0x00001A74
        public void MoveEverythingToRight()
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
        public void Ally()
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
        public void nanoMachinesSon()
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
        public void RulesOfNature()
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
        public void AttachEverything()
        {
            this.AnnounceEvent("Everything is now attracted to you");
            UltraEventsPlugin.Instance.EffectManager.AddComponent<AttachEverythingToPlayer>();
        }

        // Token: 0x06000025 RID: 37 RVA: 0x00003C19 File Offset: 0x00001E19
        public void NoRicoshotsVoid()
        {
            this.AnnounceEvent("Coins don't like you anymore");
            UltraEventsPlugin.Instance.EffectManager.AddComponent<NoRicoshots>();
        }

        // Token: 0x06000026 RID: 38 RVA: 0x00003C34 File Offset: 0x00001E34
        public void AutomaticWeapons()
        {
            UltraEventsPlugin.Log.LogInfo("full auto");
            
            this.AnnounceEvent("Full auto");
            UltraEventsPlugin.Instance.EffectManager.AddComponent<AutomaticWeaponsEffectcs>();
        }

        // Token: 0x06000027 RID: 39 RVA: 0x00003C60 File Offset: 0x00001E60
        public void CoinsAreNowNails()
        {
            this.AnnounceEvent("i turned every nail into a coin :P");
            UltraEventsPlugin.Instance.EffectManager.AddComponent<NailToCoin>();
        }

        // Token: 0x06000028 RID: 40 RVA: 0x00003C7C File Offset: 0x00001E7C
        public void BossBarForEveryone()
        {
            EnemyIdentifier[] array = Object.FindObjectsOfType<EnemyIdentifier>();
            foreach (EnemyIdentifier enemyIdentifier in array)
            {
                enemyIdentifier.gameObject.AddComponent<BossHealthBar>();
            }
            this.AnnounceEvent("Everyone is a boss now");
        }

        // Token: 0x06000029 RID: 41 RVA: 0x00003CC0 File Offset: 0x00001EC0
        public void OilUp()
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
        public void ReadLol()
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
        public void BulletsAfraidNow()
        {
            UltraEventsPlugin.Instance.EffectManager.AddComponent<BulletsAfraidEnemies>();
            this.AnnounceEvent("Bullets are afraid of enemies now");
        }

        // Token: 0x0600002C RID: 44 RVA: 0x00003E70 File Offset: 0x00002070
        public void BulletsExplodeNow()
        {
            UltraEventsPlugin.Instance.EffectManager.AddComponent<ExplodingBulletsEffect>();
            this.AnnounceEvent("Bullets now explode");
        }

        // Token: 0x0600002D RID: 45 RVA: 0x00003E8B File Offset: 0x0000208B
        public void SchizophreniaUpdate()
        {
            UltraEventsPlugin.Instance.EffectManager.AddComponent<sSchizophreniaUpdateEffect>();
        }

        // Token: 0x0600002E RID: 46 RVA: 0x00003E9A File Offset: 0x0000209A
        public void InvisibleEnemies()
        {
            UltraEventsPlugin.Instance.EffectManager.AddComponent<InvisEnemies>();
            this.AnnounceEvent("Enemies are now invisible");
        }

        // Token: 0x0600002F RID: 47 RVA: 0x00003EB8 File Offset: 0x000020B8
        public void makeEnemyOutOfSomething()
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
        public void GiveTask()
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
        public void AddRBRandomObjects()
        {
            List<MeshRenderer> list = Object.FindObjectsOfType<MeshRenderer>().ToList<MeshRenderer>();
            int value = UltraEventsPlugin.Instance.maxAmountOfDeletedOjects.Value;
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
        public void PixelizeScreen()
        {
            UltraEventsPlugin.Instance.EffectManager.AddComponent<PixelReducer>();
            this.AnnounceEvent("go go gadget pixel reducer!");
        }

        // Token: 0x06000033 RID: 51 RVA: 0x00004274 File Offset: 0x00002474
        public void RemoveRandomObjects()
        {
            List<MeshRenderer> list = Object.FindObjectsOfType<MeshRenderer>().ToList<MeshRenderer>();
            int value = UltraEventsPlugin.Instance.maxAmountOfDeletedOjects.Value;
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
        public void OpenRandomLaLink()
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
                    UltraEventsPlugin.Instance.CreateJsonFolder();
                }
                else
                {
                    string text2 = File.ReadAllText(text + "/" + UltraEventsPlugin.Instance.jsonFilePath);
                    UltraEventsPlugin.Instance.links = JsonConvert.DeserializeObject<List<UltraEventsPlugin.LinkData>>(text2);
                    this.OpenRandomLink();
                }
            }
        }

        // Token: 0x06000035 RID: 53 RVA: 0x000043FC File Offset: 0x000025FC
        public void OpenRandomLink()
        {
            bool flag = UltraEventsPlugin.Instance.links.Count > 0;
            if (flag)
            {
                UltraEventsPlugin.LinkData linkData = UltraEventsPlugin.Instance.links[Random.Range(0, UltraEventsPlugin.Instance.links.Count)];
                Application.OpenURL(linkData.link);
            }
            else
            {
                Debug.LogWarning("No links found in the JSON file.");
            }
        }

        // Token: 0x06000036 RID: 54 RVA: 0x00004456 File Offset: 0x00002656
        public void RemoveCharge()
        {
            this.AnnounceEvent("no charge?");
            MonoSingleton<WeaponCharges>.Instance.raicharge = 0f;
        }

        // Token: 0x06000037 RID: 55 RVA: 0x00004474 File Offset: 0x00002674
        public void RemoveStamina()
        {
            this.AnnounceEvent("no stamina?");
            ModUtils.GetPlayerTransform().EmptyStamina();
        }

        // Token: 0x06000038 RID: 56 RVA: 0x00004490 File Offset: 0x00002690
        public void DupeAllEnemy()
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
        public void AirStrike()
        {
            this.AnnounceEvent("By the magic of the angels. I cast thee away");
            VirtueInsignia virtueInsignia = Resources.FindObjectsOfTypeAll<VirtueInsignia>()[0];
            NewMovement playerTransform = ModUtils.GetPlayerTransform();
            VirtueInsignia virtueInsignia2 = Object.Instantiate<VirtueInsignia>(virtueInsignia, playerTransform.transform.position, Quaternion.identity);
            EnemyTarget target = new EnemyTarget(playerTransform.transform);
            virtueInsignia2.target = target;
        }

        // Token: 0x0600003A RID: 58 RVA: 0x000045A4 File Offset: 0x000027A4
        public void Alakablam()
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
        public void LoadCat()
        {
            this.AnnounceEvent("Spawn cat");
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.transform.position = ModUtils.GetPlayerTransform().transform.position;
            gameObject.AddComponent<Rigidbody>();
            UltraEventsPlugin.Instance.catRenderer = gameObject.GetComponent<Renderer>();
            Material material = new Material(UltraEventsPlugin.Instance.unlitShader);
            UltraEventsPlugin.Instance.catRenderer.material = material;
            base.StartCoroutine(this.LoadCatImage());
        }

        // Token: 0x0600003C RID: 60 RVA: 0x000046E1 File Offset: 0x000028E1
        public IEnumerator LoadCatImage()
        {
            using (UnityWebRequest www = UnityWebRequest.Get(UltraEventsPlugin.Instance.apiUrl))
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
                    UltraEventsPlugin.Log.LogInfo(jsonResponse);
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
        public IEnumerator LoadImageTexture(string url)
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
                        UltraEventsPlugin.Instance.catRenderer.material.mainTexture = texture;
                        UltraEventsPlugin.Instance.catRenderer.material.SetTexture("_MainTex", texture);
                        float width = (float)texture.width;
                        float height = (float)texture.height;
                        float scaleFactor = 0.01f;
                        UltraEventsPlugin.Instance.catRenderer.gameObject.transform.localScale = new Vector3(width * scaleFactor, height * scaleFactor, 1f);
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
        public void SpawnAd()
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
        public void LoadRandomVideo(string[] videoFiles, string folderPath)
        {
            bool flag = videoFiles.Length == 0;
            if (flag)
            {
                Debug.LogError("No mp4 files found in folder: " + folderPath);
                UltraEventsPlugin.Instance.CreateVideoFolder();
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
        public void LoadVideo(string path, Canvas canvas)
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
        public void OnVideoFinished(GameObject videoDisplayObject)
        {
            Object.Destroy(videoDisplayObject);
        }

        // Token: 0x06000042 RID: 66 RVA: 0x00004A33 File Offset: 0x00002C33
        public void FakeParry()
        {
            MonoSingleton<TimeController>.Instance.ParryFlash();
        }

        // Token: 0x06000043 RID: 67 RVA: 0x00004A41 File Offset: 0x00002C41
        public void Dies()
        {
            this.AnnounceEvent("DIE");
            ModUtils.GetPlayerTransform().GetHurt(int.MaxValue, false, 1f, false, false, 0.35f, false);
        }

        // Token: 0x06000044 RID: 68 RVA: 0x00004A70 File Offset: 0x00002C70
        public void RemoveStyle()
        {
            this.AnnounceEvent("im gonna take some style points real quick");
            int num = Random.Range(0, MonoSingleton<StatsManager>.Instance.stylePoints);
            MonoSingleton<StatsManager>.Instance.stylePoints -= num;
        }

        // Token: 0x06000045 RID: 69 RVA: 0x00004AB0 File Offset: 0x00002CB0
        public void SwitchArm()
        {
            FistControl fistControl = Object.FindObjectOfType<FistControl>();
            fistControl.ScrollArm();
        }

        // Token: 0x06000046 RID: 70 RVA: 0x00004ACC File Offset: 0x00002CCC
        public void SwapPos()
        {
            List<MeshRenderer> list = Object.FindObjectsOfType<MeshRenderer>().ToList<MeshRenderer>();
            GameObject gameObject = list[Random.Range(0, list.Count)].gameObject;
            GameObject gameObject2 = list[Random.Range(0, list.Count)].gameObject;
            base.StartCoroutine(this.SwapCoroutine(gameObject, gameObject2, UltraEventsPlugin.Instance.AmountOfTime.Value));
            this.AnnounceEvent(gameObject.name + " and " + gameObject2.name + " swapped places");
        }

        // Token: 0x06000047 RID: 71 RVA: 0x00004B51 File Offset: 0x00002D51
        public IEnumerator SwapCoroutine(GameObject object1, GameObject object2, float swapDuration)
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
        public void TeleportToEnemy()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            EnemyIdentifier enemyIdentifier = list[Random.Range(0, list.Count)];
            ModUtils.GetPlayerTransform().transform.position = enemyIdentifier.transform.position;
            this.AnnounceEvent("teleports behind " + enemyIdentifier.gameObject.name);
        }

        // Token: 0x06000049 RID: 73 RVA: 0x00004C01 File Offset: 0x00002E01
        public void MoreTrouble()
        {
            base.StopCoroutine("overTimeEvents");
            this.AnnounceEvent("prepare for trouble. And make it double!");
            base.StartCoroutine(this.overTimeEvents(2));
        }

        // Token: 0x0600004A RID: 74 RVA: 0x00004C2A File Offset: 0x00002E2A
        public IEnumerator overTimeEvents(int amount)
        {
            yield return new WaitForSeconds(1.5f);
            int num;
            for (int i = 0; i < amount; i = num + 1)
            {
                UltraEventsPlugin.Instance.UseRandomEvent();
                yield return new WaitForSeconds(0.5f);
                num = i;
            }
            UltraEventsPlugin.Instance.timer = UltraEventsPlugin.Instance.AmountOfTime.Value;
            yield break;
        }

        // Token: 0x0600004B RID: 75 RVA: 0x00004C40 File Offset: 0x00002E40
        public void TurnEnemyIntoPuppet()
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
        public void GetRod()
        {
            try
            {
                this.AnnounceEvent("its fishing time");
                bool flag = Object.FindObjectOfType<FishingHUD>() == null;
                if (flag)
                {
                    Object.Instantiate<GameObject>(UltraEventsPlugin.Instance.fishingCanvas, ModUtils.GetPlayerTransform().transform.position, Quaternion.identity);
                }
                GunSetter gs = Object.FindObjectOfType<GunSetter>();
                ModUtils.AttachWeapon(1, "", UltraEventsPlugin.Instance.rot, gs);
            }
            catch (Exception ex)
            {
                this.AnnounceEvent(ex.Message);
            }
        }

        // Token: 0x0600004D RID: 77 RVA: 0x00004D50 File Offset: 0x00002F50
        public void noHeals()
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
        public void waer()
        {
            this.AnnounceEvent("hello how are you? i am under the water");
            UltraEventsPlugin.Instance.EffectManager.AddComponent<aboohwaer>();
        }

        // Token: 0x0600004F RID: 79 RVA: 0x00004DFF File Offset: 0x00002FFF
        public void BlessthemAll()
        {
            this.AnnounceEvent("enemies now are protected by god");
            UltraEventsPlugin.Instance.EffectManager.AddComponent<BlessAll>();
        }

        // Token: 0x06000050 RID: 80 RVA: 0x00004E1A File Offset: 0x0000301A
        public void Lag()
        {
            this.AnnounceEvent("your ping is so high");
            UltraEventsPlugin.Instance.EffectManager.AddComponent<Lagging>();
        }

        // Token: 0x06000051 RID: 81 RVA: 0x00004E38 File Offset: 0x00003038
        public void SpawnRandomEnemy()
        {
            List<SpawnableObject> list = Resources.FindObjectsOfTypeAll<SpawnableObject>().ToList<SpawnableObject>();
            list.RemoveAll((SpawnableObject x) => x.spawnableObjectType != SpawnableObject.SpawnableObjectDataType.Enemy);
            SpawnableObject spawnableObject = list[Random.Range(0, list.Count)];
            Object.Instantiate<GameObject>(spawnableObject.gameObject, ModUtils.GetPlayerTransform().transform.position, Quaternion.identity);
            this.AnnounceEvent("spawned " + spawnableObject.objectName);
        }

        // Token: 0x06000052 RID: 82 RVA: 0x00004EC4 File Offset: 0x000030C4
        public void GiveDualWield()
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
        public void SlowMotion()
        {
            this.AnnounceEvent("wow this is slow");
            UltraEventsPlugin.Instance.EffectManager.AddComponent<Slowmotion>();
        }

        // Token: 0x06000054 RID: 84 RVA: 0x000050C8 File Offset: 0x000032C8
        public void FastMotion()
        {
            this.AnnounceEvent("wow this is Fast");
            UltraEventsPlugin.Instance.EffectManager.AddComponent<Fastmotion>();
        }

        // Token: 0x06000055 RID: 85 RVA: 0x000050E3 File Offset: 0x000032E3
        public void SpawnItem()
        {
            this.AnnounceEvent("Plush rain!!!!");
            UltraEventsPlugin.Instance.EffectManager.AddComponent<PlushRain>();
        }

        // Token: 0x06000056 RID: 86 RVA: 0x00005100 File Offset: 0x00003300
        public void AddRBRandomObject()
        {
            List<GameObject> list = Object.FindObjectsOfType<GameObject>().ToList<GameObject>();
            GameObject gameObject = list[Random.Range(0, list.Count)];
            this.AnnounceEvent(gameObject.name + " discovered gravity");
            gameObject.AddComponent<Rigidbody>();
        }

        // Token: 0x06000057 RID: 87 RVA: 0x0000514C File Offset: 0x0000334C
        public void RemoveRandomObject()
        {
            List<GameObject> list = Object.FindObjectsOfType<GameObject>().ToList<GameObject>();
            GameObject gameObject = list[Random.Range(0, list.Count)];
            this.AnnounceEvent("i removed something");
            Object.Destroy(gameObject);
        }

        // Token: 0x06000058 RID: 88 RVA: 0x0000518C File Offset: 0x0000338C
        public void UseRandomInput()
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
                    UltraEventsPlugin.Log.LogInfo("Shoot method not found.");
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
                        UltraEventsPlugin.Log.LogInfo("Shoot method not found.");
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
                            UltraEventsPlugin.Log.LogInfo("Shoot method not found.");
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
                                UltraEventsPlugin.Log.LogInfo("Shoot method not found.");
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
                                    UltraEventsPlugin.Log.LogInfo("Shoot method not found.");
                                }
                            }
                        }
                    }
                }
            }
        }

        // Token: 0x06000059 RID: 89 RVA: 0x000053DC File Offset: 0x000035DC
        public void ReverseGravity()
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
        public void KillRandomEnemy()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            EnemyIdentifier enemyIdentifier = list[Random.Range(0, list.Count)];
            this.AnnounceEvent("fuck you in particular " + enemyIdentifier.gameObject.name);
            enemyIdentifier.InstaKill();
        }

        // Token: 0x0600005B RID: 91 RVA: 0x000054AC File Offset: 0x000036AC
        public void KillAllEnemy()
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
        public void giveRandomWeapon()
        {
            this.AnnounceEvent("Here let me choose for you");
            GunControl gunControl = Object.FindObjectOfType<GunControl>();
            int num = Random.Range(0, gunControl.slots.Count);
            gunControl.SwitchWeapon(num, gunControl.slots[num - 1], true, false, false, false);
        }

        // Token: 0x0600005D RID: 93 RVA: 0x00005588 File Offset: 0x00003788
        public void BuffEnemy()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            EnemyIdentifier enemyIdentifier = list[Random.Range(0, list.Count)];
            enemyIdentifier.BuffAll();
            this.AnnounceEvent("\"will you lose?\" \"nah id win\" -" + enemyIdentifier.gameObject.name);
        }

        // Token: 0x0600005E RID: 94 RVA: 0x00005600 File Offset: 0x00003800
        public void DupeEnemy()
        {
            List<EnemyIdentifier> list = Object.FindObjectsOfType<EnemyIdentifier>().ToList<EnemyIdentifier>();
            list.RemoveAll((EnemyIdentifier x) => x.dead);
            EnemyIdentifier enemyIdentifier = list[Random.Range(0, list.Count)];
            EnemyIdentifier enemyIdentifier2 = Object.Instantiate<EnemyIdentifier>(enemyIdentifier, enemyIdentifier.transform.position, enemyIdentifier.transform.rotation);
            enemyIdentifier2.transform.localScale = enemyIdentifier.transform.localScale;
            this.AnnounceEvent("i added another " + enemyIdentifier.enemyType.ToString());
        }

        // Token: 0x0600005F RID: 95 RVA: 0x000056A8 File Offset: 0x000038A8
        public void Kaboom()
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
        public void usePreviousWeapon()
        {
            this.AnnounceEvent("go back to the other weapon");
            GunControl gunControl = Object.FindObjectOfType<GunControl>();
            gunControl.SwitchWeapon(gunControl.lastUsedSlot, gunControl.slots[gunControl.lastUsedSlot - 1], true, false, false, false);
        }

        // Token: 0x06000061 RID: 97 RVA: 0x00005784 File Offset: 0x00003984
        public void RemoveWeapon()
        {
            this.AnnounceEvent("you dont need this right?");
            GunControl gunControl = Object.FindObjectOfType<GunControl>();
            GameObject currentWeapon = gunControl.currentWeapon;
            gunControl.allWeapons.Remove(currentWeapon);
            gunControl.slotDict.Remove(currentWeapon);
            Object.Destroy(currentWeapon);
        }

        // Token: 0x06000062 RID: 98 RVA: 0x000057CC File Offset: 0x000039CC
        public void TPEnemies()
        {
            this.AnnounceEvent("teleports behind you");
            EnemyIdentifier[] array = Object.FindObjectsOfType<EnemyIdentifier>();
            foreach (EnemyIdentifier enemyIdentifier in array)
            {
                enemyIdentifier.transform.position = ModUtils.GetPlayerTransform().transform.position;
            }
        }

        // Token: 0x06000063 RID: 99 RVA: 0x0000581E File Offset: 0x00003A1E
        public void YEET()
        {
            this.AnnounceEvent("welcome to space :O");
            ModUtils.GetPlayerTransform().LaunchFromPoint(ModUtils.GetPlayerTransform().transform.position, 50000000f, 1f);
        }
    }
}
