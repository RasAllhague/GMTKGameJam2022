using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region UI
    private TextMeshProUGUI time;
    private TextMeshProUGUI score;
    private DiceHintVisualizer diceHintVisualizer;
    private GameObject player;
    public GameObject gameOverScreen;
    public GameObject gameWonScreen;
    #endregion

    #region
    private AudioSource audioSource;
    public AudioClip collectedDiceCorrect;
    public AudioClip collectedDiceWrong;
    #endregion

    #region player data
    private int currentScore = 0;
    #endregion

    #region level settings
    private float roundTime = 300;
    private bool isRoundActive = true;
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

        if(gameOverScreen == null)
        {
            Debug.LogWarning("GameManager: Missing Game Over Screen");
        }
        if (gameWonScreen == null)
        {
            Debug.LogWarning("GameManager: Missing Game Won Screen");
        }

        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.LogWarning("No Audio Source in Game Manager!");
        }

        if(collectedDiceCorrect == null || collectedDiceWrong == null)
        {
            Debug.LogWarning("Missing Audio File for GameManager");
        }

        time = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        diceHintVisualizer = GameObject.Find("DiceHintVisualizer").GetComponent<DiceHintVisualizer>();
        player = GameObject.Find("Player");

        if (time == null)
        {
            Debug.LogError("GameManager: UI Time not found");
        }
        if (score == null)
        {
            Debug.LogError("GameManager: UI Score not found");
        }
        if (diceHintVisualizer == null)
        {
            Debug.LogError("GameManager: UI DiceHintVisualizer not found");
        }

        System.Random rnd = new System.Random();
        List<GameObject> diceListRandomized = dice.OrderBy(item => rnd.Next()).ToList();

        diceIDsInUse = new List<string>();
        diceTypesInUse = new List<string>();

        for (int i = 0; i < startupDiceCount; i++)
        {
            GameObject obj = diceListRandomized[i];
            SpawnDie(obj);
        }

        FindNextTarget(new string[] { });
        SetScore();

        player.GetComponent<PlayerController>().FallOutOfBounce += PlayerFellOutOfBounds;
    }

    private void Update()
    {
        if (isRoundActive)
        {
            roundTime -= Time.deltaTime;

            if (roundTime <= 0)
            {
                isRoundActive = false;
                TriggerGameEnd(false, false, true);
            }
        }
        else
        {
            roundTime = 0;
        }


        SetTime();
    }

    private void SetScore()
    {
        score.text = "Score: " + currentScore;
    }

    private void SetTime()
    {
        time.text = "Time: " + roundTime.ToString("F1");
    }

    private void TriggerGameEnd(bool isGameOver, bool isGameWon, bool isTimeout)
    {
        //Persists even after scene change, so that it corrupts further actions
        //Time.timeScale = 0f;

        if (isGameOver)
        {
            // Game Over (Fall Damage)
            player.GetComponent<PlayerController>().FallOutOfBounce -= PlayerFellOutOfBounds;
            ShowGameOverScreen("You lost contact with earth and yourself");
        }

        if (isGameWon)
        {
            // Won
            ShowGameWonScreen();
        }
        SetScore();

        if (isTimeout)
        {
            // Timeout Game Over
            ShowGameOverScreen("Your legs ran out of time");
        }
    }

    private void ShowGameOverScreen(string reason)
    {
        try
        {
            gameOverScreen.transform.GetChild(0).Find("TextReason").GetComponent<TextMeshProUGUI>().text = reason;

        }
        catch (System.Exception e)
        {
            Debug.LogError(e.ToString());
        }

        Instantiate(gameOverScreen, new Vector3(0, 0, 0), Quaternion.identity);

        var levelBGM = GameObject.Find("LevelBGM");
        if (levelBGM != null)
        {
            levelBGM.GetComponent<AudioSource>().volume = 0.3f;
        }
    }
    private void ShowGameWonScreen()
    {
        Instantiate(gameWonScreen, new Vector3(0, 0, 0), Quaternion.identity);

        var levelBGM = GameObject.Find("LevelBGM");
        if (levelBGM != null)
        {
            levelBGM.GetComponent<AudioSource>().Stop();
        }
    }

    private void PlayerFellOutOfBounds()
    {
        TriggerGameEnd(true, false, false);
    }

    public bool OnPlayerCollideWithDie(string cubeID, string typeName)
    {
            audioSource.clip = collectedDiceCorrect;
            audioSource.Play();

        currentScore += 1;
        SetScore();

        diceIDsInUse.Remove(cubeID);
        diceTypesInUse.Remove(typeName);

        if (cubeID == nextTargetID)
        {
            currentScore += 1;
            SetScore();

            diceIDsInUse.Remove(cubeID);
            diceTypesInUse.Remove(typeName);

            if (respawnDiceCount > 0)
            {
                List<GameObject> availableDice = dice.FindAll(item => !diceTypesInUse.Contains(item.GetComponent<Die>().typeName));
                GameObject nextSpawnObject = availableDice[Random.Range(0, availableDice.Count - 1)];

                SpawnDie(nextSpawnObject);

                respawnDiceCount--;
            }

            List<GameObject> presentObjects = GameObject.FindGameObjectsWithTag("Dice").ToList().FindAll(item => item.GetComponent<Die>().id != cubeID);
            string[] excludeIDs = new string[] { cubeID };
            if (!FindNextTarget(excludeIDs))
            {
                TriggerGameEnd(false, true, false);
            }

            player.GetComponent<PlayerController>().IncreaseSpeed();

            return true;
        }
        else
        {
            audioSource.clip = collectedDiceWrong;
            audioSource.Play();

            roundTime -= 15;
        return false;
        }
    }

    private void SpawnDie(GameObject cube)
    {
        string uuid = System.Guid.NewGuid().ToString();

        cube.GetComponent<Die>().id = uuid;
        cube.layer = LayerMask.NameToLayer("Map");

        diceIDsInUse.Add(uuid);
        diceTypesInUse.Add(cube.GetComponent<Die>().typeName);

        Instantiate(cube, new Vector3(Random.Range(spawnDiceXMin, spawnDiceXMax + 1), Random.Range(2, 5), Random.Range(spawnDiceYMin, spawnDiceYMax + 1)), Quaternion.identity);
        score.text = "Score: " + currentScore;
    }

    private bool FindNextTarget(string[] excludeIDs)
    {
        List<GameObject> presentObjects = GameObject.FindGameObjectsWithTag("Dice").ToList().FindAll(item => !excludeIDs.Contains(item.GetComponent<Die>().id));
        if (presentObjects.Count == 0)
        {
            return false;
        }

        GameObject nextTargetElement = presentObjects[Random.Range(0, presentObjects.Count - 1)];
        diceHintVisualizer.SetDice(nextTargetElement);
        nextTargetID = nextTargetElement.GetComponent<Die>().id;

        return true;
    }
}