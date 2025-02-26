using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UltraEvents.MonoBehaviours
{
    // Token: 0x02000012 RID: 18
    public class TaskManager : MonoBehaviour
    {
        // Token: 0x17000007 RID: 7
        // (get) Token: 0x0600008D RID: 141 RVA: 0x0000632D File Offset: 0x0000452D
        // (set) Token: 0x0600008E RID: 142 RVA: 0x00006334 File Offset: 0x00004534
        public static TaskManager Instance { get; private set; }

        // Token: 0x0600008F RID: 143 RVA: 0x0000633C File Offset: 0x0000453C
        private void Awake()
        {
            TaskManager.Instance = this;
            this.Tasker = new GameObject("tasker");
            this.Tasker.transform.parent = base.transform;
            SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.SceneManager_sceneLoaded);
        }

        // Token: 0x06000090 RID: 144 RVA: 0x0000638C File Offset: 0x0000458C
        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            List<Canvas> source = Object.FindObjectsOfType<Canvas>().ToList<Canvas>();
            Canvas canvas = source.First((Canvas x) => x.name.ToLower() == "canvas");
            this.tasksText = new GameObject("tasksText");
            this.tasksText.transform.parent = canvas.transform;
            this.tasksText.transform.localPosition = new Vector3(-527.5945f, 313f, 0f);
            this.tasksText.transform.localScale = Vector3.one;
            this.text = this.tasksText.AddComponent<TextMeshProUGUI>();
            this.text.alignment = TextAlignmentOptions.Left;
            this.text.text = "tasks:";
            this.text.fontSize = 63f;
            this.text.enableWordWrapping = false;
            this.tasksText.AddComponent<HudOpenEffect>();
            this.tasksText.SetActive(false);
        }

        // Token: 0x06000091 RID: 145 RVA: 0x00006497 File Offset: 0x00004697
        public void AddTask(Task task)
        {
            TextMeshProUGUI textMeshProUGUI = this.text;
            textMeshProUGUI.text = textMeshProUGUI.text + "\n-" + task.ToDo;
            this.tasks.Add(task);
            this.tasksText.SetActive(true);
        }

        // Token: 0x06000092 RID: 146 RVA: 0x000064D8 File Offset: 0x000046D8
        public void RemoveTask(Task task)
        {
            string text = this.text.text;
            string whatToDo = task.WhatToDo;
            Debug.Log(whatToDo);
            string text2 = "\n-" + whatToDo;
            int num = text.IndexOf(text2);
            bool flag = num != -1;
            if (flag)
            {
                string str = text.Remove(num, text2.Length);
                this.text.text = str;
                Debug.Log("Modified Text: " + str);
            }
            else
            {
                Debug.Log("Pattern not found in the text.");
            }
            this.tasks.Remove(task);
            Object.Destroy(task);
            bool flag2 = this.tasks.Count == 0;
            if (flag2)
            {
                this.tasksText.SetActive(false);
            }
        }

        // Token: 0x06000093 RID: 147 RVA: 0x00006598 File Offset: 0x00004798
        public void ChangeToDo(string Prev, string New)
        {
            string text = this.text.text;
            string text2 = "\n-" + Prev;
            int num = text.IndexOf(text2);
            bool flag = num != -1;
            if (flag)
            {
                string str = text.Substring(0, text.IndexOf(text2));
                string str2 = text.Substring(text.IndexOf(text2) + text2.Length);
                string str3 = str + "\n-" + New + str2;
                this.text.text = str3;
                Debug.Log("Modified Text: " + str3);
            }
            else
            {
                Debug.Log("Pattern not found in the text.");
            }
        }

        // Token: 0x0400006D RID: 109
        public List<Task> tasks = new List<Task>();

        // Token: 0x0400006E RID: 110
        public GameObject Tasker;

        // Token: 0x0400006F RID: 111
        private GameObject tasksText;

        // Token: 0x04000070 RID: 112
        private TextMeshProUGUI text;
    }
}
