using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine;

using UnityEngine;

public class TriggeredExitAnimation : MonoBehaviour
{
    public GameObject textObject; // Przypisz obiekt z animacj� tekstow�
    public float activationDistance = 0.5f; // Minimalna odleg�o��, w jakiej gracz aktywuje animacj�
    public float deactivationDistance = 10.0f; // Odleg�o��, po kt�rej animacja zostanie dezaktywowana
    public string playerTag = "Player"; // Tag obiektu gracza

    private bool isActivated = false;
    private GameObject player;

    void Start()
    {
        // Znajd� obiekt gracza na podstawie tagu
        player = GameObject.FindGameObjectWithTag(playerTag);

        if (player == null)
        {
            Debug.LogError("Nie znaleziono obiektu gracza o tagu: " + playerTag);
        }
    }

    void Update()
    {
        if (player == null)
        {
            return; // Zako�cz funkcj�, je�li obiekt gracza nie zosta� znaleziony
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (!isActivated && distanceToPlayer < activationDistance)
        {
            isActivated = true;

            // Pobierz komponent TextMeshPro z obiektu tekstu
            TextMeshPro textMeshPro = textObject.GetComponent<TextMeshPro>();

            if (textMeshPro != null)
            {
                // Zainicjuj zmienne animacyjne
                float exitYPosition = textMeshPro.transform.position.y + 10.0f; // Ustaw docelow� pozycj� Y
                float fadeDuration = 1.0f;
                float animationSpeed = 1.0f;
                float fadePerStep = 0.01f;

                // Rozpocznij animacj�
                StartCoroutine(ExitAnimation(textMeshPro, exitYPosition, fadeDuration, animationSpeed, fadePerStep));
            }
        }
        else if (isActivated && distanceToPlayer > deactivationDistance)
        {
            isActivated = false;
        }
    }

    IEnumerator ExitAnimation(TextMeshPro textMeshPro, float exitYPosition, float fadeDuration, float animationSpeed, float fadePerStep)
    {
        float initialAlpha = textMeshPro.alpha;

        while (textMeshPro.transform.position.y < exitYPosition)
        {
            float newYPosition = textMeshPro.transform.position.y + animationSpeed * Time.deltaTime;
            textMeshPro.transform.position = new Vector3(textMeshPro.transform.position.x, newYPosition, textMeshPro.transform.position.z);

            float alpha = Mathf.Lerp(initialAlpha, 0.0f, Time.deltaTime / fadeDuration);
            alpha -= fadePerStep;
            textMeshPro.alpha = Mathf.Max(alpha, 0.0f);

            yield return null;
        }

        // Po osi�gni�ciu docelowej pozycji, zniszcz obiekt
        Destroy(textObject);
    }
}