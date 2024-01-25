
using System.Collections;
using UnityEngine;
using TMPro;

public class ExitAnimation : MonoBehaviour
{
    public float exitYPosition = 500.0f;
    public float fadeDuration = 1.0f;
    public float animationSpeed = 1.0f;
    public float fadePerStep = 0.2f; // Stopniowe zanikanie

    private TextMeshPro textMeshPro;
    private float initialAlpha;
    private bool isAnimating = false;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        initialAlpha = textMeshPro.alpha;

        // OpóŸnij rozpoczêcie animacji o 3 sekundy
        StartCoroutine(StartExitAnimationDelayed());
    }

    IEnumerator StartExitAnimationDelayed()
    {
        yield return new WaitForSeconds(3.0f);
        isAnimating = true;
    }

    void Update()
    {
        if (isAnimating)
        {
            float newYPosition = transform.position.y + animationSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            float alpha = Mathf.Lerp(initialAlpha, 0.0f, Time.deltaTime / fadeDuration);

            // Dodaj stopniowe zanikanie w kierunku przezroczystoœci
            alpha -= fadePerStep;

            textMeshPro.alpha = Mathf.Max(alpha, 0.0f); // Zapobiegaj ujemnej wartoœci przezroczystoœci

            if (transform.position.y > exitYPosition)
            {
                // Po osi¹gniêciu docelowej pozycji, zniszcz obiekt
                Destroy(gameObject);
            }
        }
    }
}


