using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine;

using UnityEngine;

public class TriggeredExitAnimation : MonoBehaviour
{
    public GameObject textObject; // Przypisz obiekt z animacj¹ tekstow¹
    public float activationDistance = 0.5f; // Minimalna odleg³oœæ, w jakiej gracz aktywuje animacjê
    public float deactivationDistance = 10.0f; // Odleg³oœæ, po której animacja zostanie dezaktywowana
    public string playerTag = "Player"; // Tag obiektu gracza

    private bool isActivated = false;
    private GameObject player;

    void Start()
    {
        // ZnajdŸ obiekt gracza na podstawie tagu
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
            return; // Zakoñcz funkcjê, jeœli obiekt gracza nie zosta³ znaleziony
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
                float exitYPosition = textMeshPro.transform.position.y + 10.0f; // Ustaw docelow¹ pozycjê Y
                float fadeDuration = 1.0f;
                float animationSpeed = 1.0f;
                float fadePerStep = 0.01f;

                // Rozpocznij animacjê
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

        // Po osi¹gniêciu docelowej pozycji, zniszcz obiekt
        Destroy(textObject);
    }
}