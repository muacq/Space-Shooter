using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValue;
    public int hazardCount;

    public float startDelay;
    public float hazardDelay;
    public float waveDelay;

    private int score;
    public Text scoreText;

    public Text gameOverText;
    private bool gameOver;

    public Text restartText;
    private bool restart;

    // Use this for initialization
    void Start () {
        gameOverText.text = "";
        gameOver = false;
        restartText.text = "";
        restart = false;
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //Application.LoadLevel(Application.loadedLevel);  deprecated
                SceneManager.LoadScene("Main");
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over";
    }
	
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void addScore(int value)
    {
        score += value;
        UpdateScore();
    }

	IEnumerator SpawnWave () {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x),
                    spawnValue.y,
                    spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(hazardDelay);

                if (gameOver)
                {
                    restart = true;
                    restartText.text = "Press 'R' to Restart";
                }
            }
            yield return new WaitForSeconds(waveDelay);
        }
	}
}
