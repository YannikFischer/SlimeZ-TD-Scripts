using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject slimeyPrefab;
    public float spawnInterval;
    public int maxSlimeys;
}

public class SpawnSlimey : MonoBehaviour
{
    public GameObject[] waypoints;
    public Wave[] waves;
    public int timeBetweenWaves = 5;
    public GameObject winMessagePanel;
    private float lastSpawnTime;
    private int slimeysSpawned = 0;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        int currentWave = GameManager.singleton.GetWave();

        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;

            //zu lange zu erklären, checkt aber ob schon alle Slimeys gespawned wurden und mehr.
            if (((slimeysSpawned == 0 && timeInterval > timeBetweenWaves) || timeInterval > spawnInterval) && slimeysSpawned < waves[currentWave].maxSlimeys)
            {
                lastSpawnTime = Time.time;

                GameObject newSlimey = Instantiate(waves[currentWave].slimeyPrefab);
                newSlimey.GetComponent<Slimey>().waypoints = waypoints;
                slimeysSpawned++;
            }

            //geht zur nächsten Wave über
            if (slimeysSpawned == waves[currentWave].maxSlimeys && GameObject.FindGameObjectWithTag("Slimey") == null)
            {
                GameManager.singleton.SetWave(GameManager.singleton.GetWave() + 1);
                //gibt dir nach der Wave 50% von deinen jetzigen Coins extra als Belohnung
                GameManager.singleton.SetCoins(Mathf.RoundToInt(GameManager.singleton.GetCoins() * 1.5f));

                slimeysSpawned = 0;

                lastSpawnTime = Time.time;
            }
        }
        else
        {
            //stoppt die Zeit sodass alles auf dem Screen freezed
            Time.timeScale = 0.0f;
            //stoppt das Spiel, gameOver heißt hier nicht verloren sondern einfach nur dass das Spiel aus ist da wir gewonnen haben
            GameManager.singleton.GameOver();
            winMessagePanel.SetActive(true);

            gameManager.LevelWon();
        }
    }
}
