using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Number of dice that should spawn at startup
    public uint startupDieCount = 1;

    // Number of dice that should respawn during gameplay
    public uint respawnDieCount = 0;

    // Dice Prefabs to choose from
    public List<GameObject> dice;

    // ID of the cube die to target next
    public string nextTargetID = "";

    // score the player has
    public int playerScore = 0;

    // time remaining for this level
    public int timeRemaining = 500;

    // Start is called before the first frame update
    void Start()
    {
        if(dice.Count == 0)
        {
            Debug.LogError("Game Manager: List of Dice has no entries");
        }

        if (startupDieCount > dice.Count)
        {
            Debug.LogError("Game Manager: Cannot spawn more dice than dice prefabs available due to enforcing unique dice");
        }

        var rnd = new System.Random();
        List<GameObject> list = dice.OrderBy(item => rnd.Next()).ToList();

        string nextTarget = "";
        for (int i = 0; i < startupDieCount; i++)
        {
            GameObject gameObject = list[i];

            string uuid = System.Guid.NewGuid().ToString();

            gameObject.GetComponent<Die>().id = uuid;
            nextTarget = uuid;

            Instantiate(gameObject, new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5)), Quaternion.identity);
        }

        this.nextTargetID = nextTarget;
    }

    public void OnPlayerCollideWithDie(string cubeID) 
    { 
        if(cubeID == nextTargetID)
        {
            Debug.Log("OK");

            this.playerScore += 1;
            // Remove cube
            // Spawn new cube (unique!)

        } else
        {
            Debug.Log("Wrong Cube Bro");

            this.timeRemaining -= 15;
            // Coroutine for invincilbe?
        }
    }

}
