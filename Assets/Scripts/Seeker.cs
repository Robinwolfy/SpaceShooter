using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    public float minDistance;
    public float maxDistance;
    public float speed;

    private float range;
    private Rigidbody rb;
    private Transform target;
    GameController gc;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gc = gameControllerObject.GetComponent<GameController>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        range = Vector3.Distance(transform.position, target.position);
        transform.forward = target.position - transform.position;
    }

    void LateUpdate()
    {
        if (range > minDistance && range < maxDistance && !gc.gameOver)
        {
            rb.AddForce(transform.forward * speed);
        }
    }
}
