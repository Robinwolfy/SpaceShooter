using UnityEngine;

using System.Collections;


public class DestroyByContact : MonoBehaviour

{
	
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
    public int subCount;
    public GameObject[] smallRocks;
	private GameController gameController;


	void Start ()

	{

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		if (gameControllerObject != null)
 
		{

			gameController = gameControllerObject.GetComponent <GameController> ();

		}

		if (gameController == null)
 
		{

			Debug.Log ("Cannot find 'Game Controller' script");

		}

	}

	void OnTriggerEnter(Collider other)

	{

        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) 

		{

			return;

		}		

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            StartCoroutine(subSpawn());
            Destroy(gameObject);
        }

		if (other.tag == "Player" && !gameController.gameOver)
 
		{

			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
            Destroy(gameObject);
        }

		gameController.AddScore (scoreValue);
        Destroy(other.gameObject);
        
    }

    IEnumerator subSpawn()
    {
        while (true)
        {

            for (int i = 0; i < subCount; i++)
            {
                GameObject smallRock = smallRocks[Random.Range(0, smallRocks.Length)];
                float xSpread = Random.Range(-1, 1);
                float zSpread = Random.Range(-1, 1);
                Vector3 spawnSpot = new Vector3(transform.position.x + xSpread, 0.0f, transform.position.z + zSpread);
                Instantiate(smallRock, spawnSpot, transform.rotation);
            }
            yield break;
        }


    }
}
