using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    public float speed = 0.1f; // Szybkoœæ obrotu
    public Vector3 spinSpeed = Vector3.zero; // Prêdkoœæ obrotu
    Vector3 spinAxis = Vector3.up; // Oœ obrotu

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

