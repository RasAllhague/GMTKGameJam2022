using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GMTKGameJam2022.Managers
{
    /// <summary>
    /// Gamemanager singleton. Put all important stuff for game managing here.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region UI
        private TextMeshProUGUI time;
        private TextMeshProUGUI score;
        private DiceHintVisualizer diceHintVisualizer;
        #endregion

        #region player data
        private int currentScore = 0;
        #endregion

        #region level settings
        private float roundTime = 300;
        public uint startupDiceCount = 1;
        public uint respawnDiceCount = 0;
        public int spawnDiceXMin = 0;
        public int spawnDiceXMax = 1;
        public int spawnDiceYMin = 0;
        public int spawnDiceYMax = 1;
        public List<GameObject> dice;
        private string nextTargetID = "";
        #endregion

        private List<string> diceTypesInUse;
        private List<string> diceIDsInUse;

        private void Start()
        {
            if (startupDiceCount == 0)
            {
                Debug.LogWarning("GameManager: StartUpDice is zero!");
            }

            if (dice.Count == 0 || dice == null)
            {
                Debug.LogWarning("GameManager: List of dice is null or empty");
            }

            if (startupDiceCount > dice.Count)
            {
                Debug.LogWarning("GameManager: Cannot spawn more dice at once than are uniquely available");
            }

            time = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
            score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
            diceHintVisualizer = GameObject.Find("DiceHintVisualizer").GetComponent<DiceHintVisualizer>();

            if(time == null)
            {
                Debug.LogError("GameManager: UI Time not found");
            }
            if(score == null)
            {
                Debug.LogError("GameManager: UI Score not found");
            }
            if(diceHintVisualizer == null)
            {
                Debug.LogError("GameManager: UI DiceHintVisualizer not found");
            }

            System.Random rnd = new System.Random();
            List<GameObject> diceListRandomized = dice.OrderBy(item => rnd.Next()).ToList();

            diceIDsInUse = new List<string>();
            diceTypesInUse = new List<string>();

            string nextTarget = "";
            for (int i = 0; i < startupDiceCount; i++)
            {
                GameObject obj = diceListRandomized[i];
                string uuid = System.Guid.NewGuid().ToString();
                obj.GetComponent<Die>().id = uuid;
                nextTarget = uuid;

                diceIDsInUse.Add(uuid);
                diceTypesInUse.Add(obj.GetComponent<Die>().typeName);

                Instantiate(obj, new Vector3(Random.Range(spawnDiceXMin, spawnDiceXMax+1), Random.Range(2, 5), Random.Range(spawnDiceYMin, spawnDiceYMax+1)), Quaternion.identity);
            }
            nextTargetID = nextTarget;

            diceHintVisualizer.SetDice(diceListRandomized[(int)startupDiceCount - 1]);
            SetScore();
        }

        private void Update()
        {
            // Coroutine
            //time.text = "Time: " + ((startTime + roundTime) - Time.time).ToString("0.0");
        }

        private void SetScore()
        {
            score.text = "Score: " + currentScore;
        }

        public bool OnPlayerCollideWithDie(string cubeID, string typeName)
        {
            if (cubeID == nextTargetID)
            {
                currentScore += 1;
                SetScore();

                diceIDsInUse.Remove(cubeID);
                diceTypesInUse.Remove(typeName);

                if(respawnDiceCount > 0)
                {
                    List<GameObject> availableDice = dice.FindAll(item => !diceTypesInUse.Contains(item.GetComponent<Die>().typeName));
                    GameObject nextSpawnObject = availableDice[Random.Range(0, availableDice.Count - 1)];

                    string uuid = System.Guid.NewGuid().ToString();
                    nextSpawnObject.GetComponent<Die>().id = uuid;
                    diceIDsInUse.Add(uuid);
                    diceTypesInUse.Add(nextSpawnObject.GetComponent<Die>().typeName);

                    Instantiate(nextSpawnObject, new Vector3(Random.Range(spawnDiceXMin, spawnDiceXMax + 1), Random.Range(2, 5), Random.Range(spawnDiceYMin, spawnDiceYMax + 1)), Quaternion.identity);

                    respawnDiceCount--;
                }

                List<GameObject> presentObjects = GameObject.FindGameObjectsWithTag("Dice").ToList().FindAll(item => item.GetComponent<Die>().id != cubeID);

                if (presentObjects.Count != 0)
                {
                    GameObject nextTargetElement = presentObjects[Random.Range(0, presentObjects.Count - 1)];
                    diceHintVisualizer.SetDice(nextTargetElement);
                    nextTargetID = nextTargetElement.GetComponent<Die>().id;
                }
                else
                {
                    Debug.Log("We have a winner!");
                }

                return true;
            }
            else
            {
                roundTime -= 15;
                return false;
                // Coroutine for invincilbe?
            }
        }
    }
}