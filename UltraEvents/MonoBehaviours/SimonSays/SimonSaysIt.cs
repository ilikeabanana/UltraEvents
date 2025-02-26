using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputRemoting;

namespace UltraEvents.MonoBehaviours.SimonSays
{
    internal class SimonSaysIt : MonoBehaviour
    {
        protected bool simonSaidIt;
        protected bool playerResponded;
        public float timeLimit = 5f; // Default time limit
        private Coroutine timerCoroutine;
        private float currentTimer;

        public virtual string task => "STANDARD TEXT";

        protected virtual void Start()
        {
            simonSaidIt = Random.Range(0, 100) > 50 ? true : false;
            StartTimer();
        }

        protected void StartTimer()
        {
            if (timerCoroutine != null)
                StopCoroutine(timerCoroutine);
            currentTimer = timeLimit;
            timerCoroutine = StartCoroutine(TimerCountdown());
        }

        void Update()
        {
            if (checkIfDone())
            {
                playerResponded = true;
            }
        }

        public virtual bool checkIfDone()
        {
            return false;
        }

        private IEnumerator TimerCountdown()
        {
            while (currentTimer > 0 && !playerResponded)
            {
                yield return null;
                currentTimer -= Time.deltaTime;
            }

            if (!playerResponded)
            {
                if (!simonSaidIt)
                {
                    DidIt();
                }
                else
                {
                    Fail();
                }
            }
            else
            {
                if (simonSaidIt)
                {
                    DidIt();
                }
                else
                {
                    Fail();
                }
            }
        }

        public virtual void DidIt()
        {
            Destroy(this);
        }

        public virtual void Fail()
        {
            MonoSingleton<HudMessageReceiver>.Instance.SendHudMessage("You failed Simon Says!", "", "", 0, false);
            MonoSingleton<NewMovement>.Instance.GetHurt(100000, false);
            Debug.Log("You failed Simon Says!");
            Destroy(this);
        }

        public void SimonSaysCommand(bool didSimonSayIt)
        {
            simonSaidIt = didSimonSayIt;
            playerResponded = false;
            StartTimer();
        }

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                fontSize = 24,
                alignment = TextAnchor.UpperCenter
            };

            string message = simonSaidIt ? $"Simon Says: {task}, {currentTimer:F1}" : $"Do: {task}, {currentTimer:F1}";
            Rect rect = new Rect(0, 20, Screen.width, 50);
            GUI.Label(rect, message, style);
        }
    }
}
