using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloudMovement : MonoBehaviour
{
    public string cloudTag = "Cloud";
    public float moveSpeed = 0.1f;
    public float randomFactor = 1.0f; // Wsp�czynnik losowo�ci w osi X
    public float verticalRandomFactor = 0.5f; // Wsp�czynnik losowo�ci w osi Y

    private Vector3[] initialPositions;

    void Start()
    {
        // Pobierz pozycje pocz�tkowe wszystkich obiekt�w z danym tagiem
        GameObject[] clouds = GameObject.FindGameObjectsWithTag(cloudTag);
        initialPositions = new Vector3[clouds.Length];

        for (int i = 0; i < clouds.Length; i++)
        {
            initialPositions[i] = clouds[i].transform.position;
        }
    }

    void Update()
    {
        // Pobierz ponownie obiekty z danym tagiem
        GameObject[] clouds = GameObject.FindGameObjectsWithTag(cloudTag);

        // Przesu� ka�dy obiekt z losowym wp�ywem w obu osiach
        for (int i = 0; i < clouds.Length; i++)
        {
            float xRandomness = Mathf.PerlinNoise(Time.time * randomFactor, i) * 2.0f - 1.0f; // Losowy wp�yw w osi X
            float yRandomness = Mathf.PerlinNoise(Time.time * verticalRandomFactor, i) * 2.0f - 1.0f; // Losowy wp�yw w osi Y

            float newPositionX = initialPositions[i].x + xRandomness + Mathf.Sin(Time.time * moveSpeed) * 2.0f;
            float newPositionY = initialPositions[i].y + yRandomness;

            clouds[i].transform.position = new Vector3(newPositionX, newPositionY, initialPositions[i].z);
        }
    }
}


