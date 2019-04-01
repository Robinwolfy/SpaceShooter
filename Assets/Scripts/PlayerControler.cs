using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;	
}

public class PlayerControler : MonoBehaviour
{
	public float speed;
	public GameObject shot;
	public Transform shotspawn;
	public float tiltz;
	public float tiltx;
	public float firerate;
	public Boundary boundary;
	private Rigidbody rb;
	private float nextFire;
	private AudioSource ac;


	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		ac = GetComponent<AudioSource> ();
	}
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
			);
		rb.rotation = Quaternion.Euler (rb.velocity.z * tiltx, 0.0f, rb.velocity.x * -tiltz);
	}

	void Update ()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire || Input.GetKey(KeyCode.Space) && Time.time > nextFire)
		{
			nextFire = Time.time + firerate;
			Instantiate (shot, shotspawn.position, shotspawn.rotation);
			ac.Play ();
		}
	}

}
