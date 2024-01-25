using UnityEngine;

public class BushWindAnimation : MonoBehaviour
{
    public string bushTag = "Bush";
    public float minScale = 0.7f;
    public float maxScale = 1.2f;
    public float scalingSpeed = 0.5f;
    public float scaleRandomness = 0.1f;

    void Update()
    {
        GameObject[] bushes = GameObject.FindGameObjectsWithTag(bushTag);

        foreach (GameObject bush in bushes)
        {
            // Losowe zmiany w skali
            float randomScale = Random.Range(1.0f - scaleRandomness, 1.0f + scaleRandomness);

            // Oblicz now¹ skalê w zakresie od minScale do maxScale
            float targetScale = Random.Range(minScale, maxScale) * randomScale;

            // Zastosuj animacjê skalowania
            float newScale = Mathf.Lerp(bush.transform.localScale.x, targetScale, scalingSpeed * Time.deltaTime);
            bush.transform.localScale = new Vector3(newScale, newScale, 1.0f);

            // Pobierz aktualn¹ pozycjê Y obiektu
            float currentYPosition = bush.transform.position.y;

            // Ustaw obiekt na aktualnej pozycji Y
            Vector3 newPosition = bush.transform.position;
            newPosition.y = currentYPosition;
            bush.transform.position = newPosition;
        }
    }
}
