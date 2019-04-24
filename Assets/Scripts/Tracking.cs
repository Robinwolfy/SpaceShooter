using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{

    public Transform target;
    public bool locked;
    public Transform shotSpawn;
    public float fireRate;
    public GameObject shot;
    private float nextFire;
    private AudioSource ac;

    void Start()
    {
        ac = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetButton("Fire1") && locked && Time.time > nextFire)
        { 
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            ac.Play();
        }
    }

    void LateUpdate()
    {
        if (locked)
        {
            transform.forward = target.position - transform.position;
        }
    }

}
