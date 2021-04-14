using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public Text coinsLabel;
    public int coins = 1000;

    public Text waveLabel;
    public bool gameOver = false;
    private int wave;

    public Text healthLabel;
    public GameObject[] healthIndicator;    
    private int health;

    public static GameManager singleton;

    public GameObject gameOverPanel;
    public GameObject playPanel;
    public GameObject pausePanel;
    public GameObject resumePanel;
    public GameObject helpPanel;

    public int gameplayHighscore;
    public int gameOverCurrentScore;
    public int highScore;

    public Text GameOverCurrentScore;
    public Text gamePlayHighScore;
    public Text gameOverHighScore;
    public Text resumeCurrentScore;
    public Text resumeHighScore;
    public Text winCurrentScore;
    public Text winHighScore;

    public string nextLevel = "Level03";
    public int levelToUnlock = 3;

    private void Awake()
    {
        Time.timeScale = 0.0f;

        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetWave(0);
        SetHealth(10);
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        gamePlayHighScore.text = highScore.ToString();
        gameOverHighScore.text = highScore.ToString();
        resumeHighScore.text = highScore.ToString();

        AudioManager.instance.titleScreenMusic.loop = true;
        AudioManager.instance.titleScreenMusic.Play();
        AudioManager.instance.titleScreenMusic.loop = true;
        AudioManager.instance.titleScreenMusic.Play();
    }

    public void SetWave(int value)
    {
        wave = value;
    }

    public void ShowWave()
    {
        waveLabel.text = "Wave: " + wave;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetWave()
    {
        return wave;
    }

    public void GameOver()
    {
        gameOver = true;

        Time.timeScale = 0.0f;                    //setzt Zeit auf 0 sodass sich nichts mehr bewegt 
        pausePanel.SetActive(false);              //Der Pause Panel wird deaktiviert

        //1 Coin ist 1 Punkt
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 0);

        if (coins > highScore)
        {
            //Wir setzen unserern jetzigen Coin Counter auf unseren Highscore falls der Highscore unter den jetzigen Coins liegt.
            highScore = coins;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
        }

        GameOverCurrentScore.text = coins.ToString();
        gameOverHighScore.text = highScore.ToString();
        gamePlayHighScore.text = highScore.ToString();
        winCurrentScore.text = coins.ToString();
        winHighScore.text = highScore.ToString();

        AudioManager.instance.inGameMusic.Stop();
        AudioManager.instance.titleScreenMusic.Play();
        AudioManager.instance.titleScreenMusic.loop = true;
    }

    public void SetHealth(int value)
    {
        health = value;

        if (health <= 0 && !gameOver)
        {
            gameOverPanel.SetActive(true);

            GameOver();

            AudioManager.instance.gameOverMusic.Play();
        }

        for (int i = 0; i < healthIndicator.Length; i++)
        {
            if (i < health)
            {
                healthIndicator[i].SetActive(true);
            }
            else
            {
                healthIndicator[i].SetActive(false);
            }
        }
    }

    public void ShowHealth()
    {
        healthLabel.text = ": " + health;
    }

    // Update is called once per frame
    void Update()
    {
        ShowCoins();
        ShowWave();
        ShowHealth();
    }

    public void SetCoins(int value)
    {
        coins = value;
    }

    public int GetCoins()
    {
        return coins;
    }

    public void ShowCoins()
    {
        coinsLabel.text = ": " + coins;
    }

    public void OnPlayButton()
    {
        playPanel.SetActive(false);
        Time.timeScale = 1.0f;

        AudioManager.instance.titleScreenMusic.Stop();
        AudioManager.instance.inGameMusic.loop = true;
        AudioManager.instance.inGameMusic.Play();
    }

    public void OnResumeButton()
    {
        pausePanel.SetActive(true);
        resumePanel.SetActive(false);
        Time.timeScale = 1.0f;

        AudioManager.instance.titleScreenMusic.Stop();
        AudioManager.instance.inGameMusic.loop = true;
        AudioManager.instance.inGameMusic.Play();
    }

    public void OnPauseButton()
    {
        Time.timeScale = 0.0f;
        pausePanel.SetActive(false);
        resumePanel.SetActive(true);
        resumeCurrentScore.text = coins.ToString();

        AudioManager.instance.inGameMusic.Stop();
        AudioManager.instance.titleScreenMusic.loop = true;
        AudioManager.instance.titleScreenMusic.Play();
    }

    public void OnRestartButton()
    {
        AudioManager.instance.titleScreenMusic.loop = true;
        AudioManager.instance.titleScreenMusic.Play();

        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnHelpButton()
    {
        Time.timeScale = 0.0f;

        playPanel.SetActive(false);
        helpPanel.SetActive(true);
    }

    public void OnBackButton()
    {
        Time.timeScale = 0.0f;

        helpPanel.SetActive(false);
        playPanel.SetActive(true);
    }

    public void OnCloseButton()
    {
        Application.Quit();
    }

    public void OnBackToTitleButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnDesertMapButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnGrassMapButton()
    {
        SceneManager.LoadScene(2);
    }

    public void OnGrassMapHardModeButton()
    {
        SceneManager.LoadScene(3);
    }

    public void On2xSpeedButton()
    {
        Time.timeScale = 2.0f;
    }

    public void On1xSpeedButton()
    {
        Time.timeScale = 1.0f;
    }

    public void LevelWon()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }

    public void OnStatsReset()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.GetActiveScene();
        SceneManager.LoadScene(0);
    }
    
    /*
    public void OnSkinSelect()
    {
        TankPlacement.tankPlacement.tankPrefab = TankPlacement.tankPlacement.tankPrefabSkin;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("lol");
    }
    */
}
