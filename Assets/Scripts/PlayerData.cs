using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]


//Ta klasa przechowuje wszystkie parametry ruchu postaci
public class PlayerData : ScriptableObject
{
    [Header("Grawitacja")] //Latwe podzielenie inspektora i kodu na czesci
    //Daje wieksza przejrzystosc kodu
    public float silaGrawitacji;
    public float skalaGrawitacji;


    [Space(5)]
    public float multiplikatorSpadGrawitacji;
    public float maksPredSpadania;

    /*
   	public float multiplikatorSzybkiegoSpadania; //Kontrola szybkiego spadania przez gracza
	public float maksPredSzybkiegoSpadania;
    */

    [Space(10)] //[Space()] tworzy odstep pomiedzy wierszami w Inspektorze
    [Header("Bieganie")]
    public float maksPredBiegania;
    public float akceleracjaBiegania;
    [HideInInspector] public float silaAkceleracjiBiegania;
    public float deakceleracjaBiegania;
    [HideInInspector] public float silaDeakceleracjiBiegania;
    
    [Space(5)]
    [Range(0f, 1)] public float akceleracjaWPowietrzu;
    [Range(0f, 1)] public float deakceleracjaWPowietrzu;
    //[Range(0f,1)] sprawia ze zmienne sa w zakresie od 0 do 1 float, jako slider w inspektorze

    //public bool podtrzymanieMomentum = true;


    [Header("Skakanie")]
    public float wysokoscSkoku;
    public float czasWierzcholkuSkoku; //Czas z jakim gracz ma osiagnac najwyzszy punkt skoku
    public float silaSkoku;

    //Do dodania:
    //[Header("Wall Jump")]

    //[Header("Slide")]


    [Header("Usprawnienia")]
    public float coyoteTime; //Czas kiedy gracz moze skoczyc po 'spadnieciu' z podloza
    public float czasBuforuSkoku; //Bufor kiedy gracz moze zkolejkowac 
                                  //kolejny skok tak aby osiagnieciu podloza mogl skoczyc od razu.



    //Wywolywane w momemcnie update'u inspektora
    private void OnValidate()
    {
        silaGrawitacji = (2*wysokoscSkoku) / (czasWierzcholkuSkoku * czasWierzcholkuSkoku); 
        
        //Obliczenie sily grawitacji uzywajac wbudowanej grawitacji Unity
        //skalaGrawitacji = silaGrawitacji / Physics2D.gravity.y; 
        
        silaAkceleracjiBiegania = (50 * akceleracjaBiegania) / maksPredBiegania;
        silaDeakceleracjiBiegania = (50 * deakceleracjaBiegania) / maksPredBiegania;

        silaSkoku = Mathf.Abs(silaGrawitacji) * czasWierzcholkuSkoku;

        //Obliczenie akceleracji i deakceleracji na podstawie "zacisniecia" wartosci pomiedzy 0.01f a 
        // maksymalna predkoscia, w ten sposob gracz nie bedzie szybszy niz zaasygnowana predkosc
        akceleracjaBiegania = Mathf.Clamp(akceleracjaBiegania, 0.01f, maksPredBiegania);
        deakceleracjaBiegania = Mathf.Clamp(deakceleracjaBiegania, 0.01f, maksPredBiegania);
    }
}
