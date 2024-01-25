using UnityEngine;

using UnityEngine;

public class TreeWindAnimation : MonoBehaviour
{
    public string treeTag = "Tree";
    public float maxRotationAngle = 2.0f;
    public float rotationSpeed = 1.0f;

    private Quaternion[] initialRotations;

    void Start()
    {
        // Pobierz rotacje pocz�tkowe wszystkich obiekt�w z danym tagiem
        GameObject[] trees = GameObject.FindGameObjectsWithTag(treeTag);
        initialRotations = new Quaternion[trees.Length];

        for (int i = 0; i < trees.Length; i++)
        {
            initialRotations[i] = trees[i].transform.rotation;
        }
    }

    void Update()
    {
        // Pobierz ponownie obiekty z danym tagiem
        GameObject[] trees = GameObject.FindGameObjectsWithTag(treeTag);

        // Zastosuj losow� rotacj� w osi Z dla ka�dego drzewa
        for (int i = 0; i < trees.Length; i++)
        {
            float rotationAmount = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;

            // Ogranicz zakres rotacji do -maxRotationAngle do maxRotationAngle
            rotationAmount = Mathf.Clamp(rotationAmount, -maxRotationAngle, maxRotationAngle);

            // Dodaj losow� rotacj� do rotacji pocz�tkowej
            Quaternion newRotation = initialRotations[i] * Quaternion.Euler(0f, 0f, rotationAmount);

            // Ustaw now� rotacj�
            trees[i].transform.rotation = newRotation;
        }
    }
}


