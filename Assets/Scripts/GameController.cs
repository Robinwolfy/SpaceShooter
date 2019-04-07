using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public GameObject[] hazards;
	public Vector3 spawnValue;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
    public Text creditText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
        creditText.text = "";
		StartCoroutine (spawnWaves ());
		score = 0;
		UpdateScore ();
	}


	IEnumerator spawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, hazard.transform.rotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) 
			{
				restart = true;
				restartText.text = "Restart?";
				break;
			}
		}
	}

	public void GameOver()
	{
		gameOver = true;
		gameOverText.text = "GAME OVER!";
	}	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Points: " + score;
        if(score >= 100)
        {
            Win();
        }
	}

    void Win()
    {
        gameOver = true;
        gameOverText.text = "YOU WIN!";
        creditText.text = "GAME CREATED BY ROBIN NETTLES";
    }


	void Update ()
	{
		if (restart) 
		{
			restartText.text = "Press 'P' to Restart";
			if (Input.GetKeyDown (KeyCode.P))
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}