using UnityEngine;

public class FlowerWindAnimation : MonoBehaviour
{
    public string flowerTag = "Flower";
    public float maxRotationAngle = 5.0f;
    public float rotationSpeed = 1.0f;

    private Quaternion[] initialRotations;

    void Start()
    {
        // Pobierz rotacje pocz�tkowe wszystkich obiekt�w z danym tagiem
        GameObject[] flowers = GameObject.FindGameObjectsWithTag(flowerTag);
        initialRotations = new Quaternion[flowers.Length];

        for (int i = 0; i < flowers.Length; i++)
        {
            initialRotations[i] = flowers[i].transform.rotation;
        }
    }

    void Update()
    {
        // Pobierz ponownie obiekty z danym tagiem
        GameObject[] flowers = GameObject.FindGameObjectsWithTag(flowerTag);

        // Zastosuj losow� rotacj� w osi Z dla ka�dego kwiatu
        for (int i = 0; i < flowers.Length; i++)
        {
            float rotationAmount = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;

            // Ogranicz zakres rotacji do -maxRotationAngle do maxRotationAngle
            rotationAmount = Mathf.Clamp(rotationAmount, -maxRotationAngle, maxRotationAngle);

            // Dodaj losow� rotacj� do rotacji pocz�tkowej
            Quaternion newRotation = initialRotations[i] * Quaternion.Euler(0f, 0f, rotationAmount);

            // Ustaw now� rotacj�
            flowers[i].transform.rotation = newRotation;
        }
    }
}

