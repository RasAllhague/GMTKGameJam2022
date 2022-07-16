using TMPro;
using UnityEngine;

namespace GMTKGameJam2022.Managers
{
    /// <summary>
    /// Gamemanager singleton. Put all important stuff for game managing here.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private int currentScore = 0;
        private TextMeshProUGUI time;
        private TextMeshProUGUI score;
        private float startTime = 0;
        private float roundTime = 300;
        private GameObject dice;
        private DiceHintVisualizer diceHintVisualizer;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        /**
         * Restores default values
         */
        private void StartNewRound()
        {
            currentScore = 0;
            startTime = Time.time;
            GameObject[] uiElements = GameObject.FindGameObjectsWithTag("UI");
            foreach (GameObject uiElement in uiElements)
            {
                switch (uiElement.name)
                {
                    case "Time":
                        time = uiElement.GetComponent<TextMeshProUGUI>();
                        break;
                    case "Score":
                        score = uiElement.GetComponent<TextMeshProUGUI>();
                        break;
                    case "DiceHintVisualizer":
                        diceHintVisualizer = uiElement.GetComponent<DiceHintVisualizer>();
                        break;
                }
            }
            SetScore();

            //Select first dice (for test purposes)
            dice = GameObject.FindGameObjectWithTag("Dice");
            diceHintVisualizer.SetDice(dice);
        }

        private void Start()
        {
            //TODO: Trigger start as it should be started
            StartNewRound();
        }

        private void Update()
        {
            time.text = "Time: " + ((startTime + roundTime) - Time.time).ToString("0.0");
        }

        private void SetScore()
        {
            score.text = "Score: " + currentScore;
        }


    }
}