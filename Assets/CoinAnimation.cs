using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    public float speed = 0.1f; // Szybko�� obrotu
    public Vector3 spinSpeed = Vector3.zero; // Pr�dko�� obrotu
    Vector3 spinAxis = Vector3.up; // O� obrotu

    void Start()
    {
        spinSpeed = new Vector3(Random.value, Random.value, Random.value);
        spinAxis = Vector3.up;
    }

    void Update()
    {
        this.transform.Rotate(spinSpeed);
        this.transform.RotateAround(Vector3.zero, spinAxis, speed);
    }
}

