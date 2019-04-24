using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMover : MonoBehaviour
{
    public float impulse;
    public float delay;
    public float accel;
    public float accelTime;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * impulse, ForceMode.Impulse);
        StartCoroutine(Boost());
    }

    IEnumerator Boost()
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < accelTime; i++)
        {
            rb.AddForce(transform.forward * accel);
        }
    }
}
