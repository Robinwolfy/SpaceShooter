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
    public Text homeText;
    public AudioClip winSound;
    public AudioClip loseSound;
    public GameObject background;
    public GameObject stars;
    public GameObject backStars;
    public bool gameOver;
    public int maxScore;
    

    ParticleSystem starParts;
    ParticleSystem backStarParts;
    BackgroundScroller bgScroll;

	
	private bool restart;
    private AudioSource ac;
    private int score;

    void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
        creditText.text = "";
        homeText.text = "";
        ac = GetComponent<AudioSource>();
        StartCoroutine (spawnWaves ());
		score = 0;
		UpdateScore ();
        starParts = stars.GetComponent<ParticleSystem>();
        backStarParts = backStars.GetComponent<ParticleSystem>();
        bgScroll = background.GetComponent<BackgroundScroller>();
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
				break;
			}
		}
	}

	public void GameOver()
	{
		gameOver = true;
		gameOverText.text = "GAME OVER!";
        ac.clip = loseSound;
        ac.Play();
        var main = starParts.main;
        var backMain = backStarParts.main;
        main.simulationSpeed = 0f;
        backMain.simulationSpeed = 0f;
        bgScroll.scrollSpeed = 0;        
	}	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Points: " + score;
        if(score >= maxScore && !gameOver)
        {
            Win();
        }
	}

    void Win()
    {
        gameOver = true;
        gameOverText.text = "YOU WIN!";
        creditText.text = "GAME CREATED BY ROBIN NETTLES";
        var main = starParts.main;
        var backMain = backStarParts.main;
        main.simulationSpeed = 20f;
        backMain.simulationSpeed = 20f;
        bgScroll.scrollSpeed = -15;
        ac.clip = winSound;
        ac.Play();
    }


	void Update ()
	{
		if (restart) 
		{
			restartText.text = "Press 'Space' to Restart";
            homeText.text = "Press 'H' to return to the menu";
			if (Input.GetKeyDown (KeyCode.Space))
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
            if (Input.GetKeyDown(KeyCode.H))
                SceneManager.LoadScene("Main");
		}
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}