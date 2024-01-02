using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.UIElements;


// !!!HEADS UP!!!
//TEN KOD DZIALAL, ALE POSTANOWILEM GO PRZEPISAC ZEBY POPRAWIC PRZEJRZYSTOC I CZYTELNOSC KODU
//NAPISALEM TO JESZCZE RAZ PONIZEJ
//POPRZEDNIA WERSJA ZNAJDUJE SIE W PLIKU PlayerControllerOLD.cs


public class PlayerController : MonoBehaviour 
{

    public Rigidbody2D rb {get; private set;} //public read, private write
    public bool zwrotPrawo {get; private set;}
    static public bool czySkacze {get; private set;}



    static public Vector2 moveInput; //static poniewaz inaczej nie dziala animacja, nie jestem pewien dlaczego.
    private bool doubleJumpAvailable = false;



    //Checkery polozenia postaci, beda potrzebne do np. skoku od sciany.
    [Header("Checkery")]
    [SerializeField] private Transform punktUziemieniaChecker; //Sprawia ze prywatna zmienna pokazuje sie w Inspektorze
    [SerializeField] private Vector2 wielkoscPunktuUziemienia = new Vector2(0.50f, 0.03f);


    [SerializeField] private LayerMask podlogaLayer;

    
    // <!-- PRZENIESIONE Z PLIKU PLAYER DATA --!>

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
    //public float predkoscBiegania;
    public float maksPredBiegania;
    public float akceleracjaBiegania; //predkosc z jaka gracz osiaga maksymanlna predkosc
    // public float tlumienieAkceleracji;
    [HideInInspector] public float silaAkceleracjiBiegania; //Ta siła jest dodawana do gracza
    public float deakceleracjaBiegania;
    [HideInInspector] public float silaDeakceleracjiBiegania; //Ta siła jest dodawana do gracza
    
    // [Space(5)]
    // [Range(0f, 1)] public float akceleracjaWPowietrzu;
    // [Range(0f, 1)] public float deakceleracjaWPowietrzu;
    //[Range(0f,1)] sprawia ze zmienne sa w zakresie od 0 do 1 float, jako slider w inspektorze

    [Header("Skakanie")]
    public float wysokoscSkoku;
    public float czasWierzcholkuSkoku; //Czas z jakim gracz ma osiagnac najwyzszy punkt skoku
    public float silaSkoku;
    private int coyoteTime=0; //Czas kiedy gracz moze skoczyc po 'spadnieciu' z podloza
    private float jumpBufferTime = 0.2f; //Bufor kiedy gracz moze zkolejkowac
    private float jumpBufferCounter;




    public void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UstawienieGrawitacji(skalaGrawitacji);
        zwrotPrawo = true;
    }


    private void Update()
    {
        // <!-- INPUT HANDLERS --!>
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        Obrot();
        Jump();

        //Sprawdzenie czy gracz skacze czy jest na podlodze
		if (Physics2D.OverlapBox(punktUziemieniaChecker.position, wielkoscPunktuUziemienia, 0, podlogaLayer))
		{
            czySkacze=false;
            coyoteTime = 0;
        } else {
            czySkacze = true;
        }

        //Coyote time zaczyna sie naliczac kiedy nie jestem na podlodze i wynosi 0 lub wiecej, nalicza sie w petli do momentu jak dobije do limitu. Twardy reset jest poprzez odpadniecie na platforme. 
        // Po dobiciu do limitu wartosc coyoteTime zmieniona jest na -1 w celu unikniecia nieskonczonej petli 0->limit->0->limit->... 

        if(coyoteTime > 200)
        {
            coyoteTime = -1;
        }

        if(czySkacze && coyoteTime>=0)
        {
            coyoteTime++;
        }
    }

    private void FixedUpdate()
    {
        Run(1.5f);
    }

    //Wowylywane w inskeptorze
    private void OnValidate()
    {
        
        silaGrawitacji = -(2*wysokoscSkoku) / (czasWierzcholkuSkoku * czasWierzcholkuSkoku); 
        
        //Obliczenie sily grawitacji uzywajac wbudowanej grawitacji Unity
        skalaGrawitacji = silaGrawitacji / Physics2D.gravity.y; 
        
        silaSkoku = Mathf.Abs(silaGrawitacji) * czasWierzcholkuSkoku;

        silaAkceleracjiBiegania = (50 * akceleracjaBiegania) / maksPredBiegania;
        silaDeakceleracjiBiegania = (50 * deakceleracjaBiegania) / maksPredBiegania;

        //Obliczenie akceleracji i deakceleracji na podstawie "zacisniecia" wartosci pomiedzy 0.01f a 
        // maksymalna predkoscia, w ten sposob gracz nie bedzie szybszy niz zaasygnowana predkosc
        akceleracjaBiegania = Mathf.Clamp(akceleracjaBiegania, 0.01f, maksPredBiegania);
        deakceleracjaBiegania = Mathf.Clamp(deakceleracjaBiegania, 0.01f, maksPredBiegania);
        
    }

    // <!-- Metody poruszania gracza --!>
    
    private void Run(float lerpAmount)
    {

        float predkoscDocelowa = moveInput.x * maksPredBiegania;
        predkoscDocelowa = Mathf.Lerp(rb.velocity.x, predkoscDocelowa, lerpAmount);

        float wspolAkceleracji;
        //Jeśli predkoscDocelowa wynosi wiecej niz 0.01f to wspolAkceleracji przyjmuje wartosc silaAkceleracjiBiegania, jesli wartosc jest mniejsza to przyjmuje wartosc silaDeakceleracjiBiegania.
        wspolAkceleracji = (Mathf.Abs(predkoscDocelowa) > 0.01f) ? silaAkceleracjiBiegania : silaDeakceleracjiBiegania;

        float roznicaPredkosci = predkoscDocelowa - rb.velocity.x;
        float movement = roznicaPredkosci * wspolAkceleracji;

        //Debug.Log("Movespeed gracza: "+movement);

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

        //float horizontalVelocity = rb.velocity.x;
        //horizontalVelocity += moveInput.x;
        //horizontalVelocity *= Mathf.Pow(1f - tlumienieAkceleracji, Time.deltaTime*5f);

        //rb.velocity = new Vector2(horizontalVelocity, 0f);

        // if(moveInput.x != 0)
        // {
        //     SprawdzenieZwrotu(moveInput.x>0);
        // }
    }
    

    private void Jump()
    {
        /*if(czySkacze && rb.velocity.y < 0)
        {
            rb.gravityScale = silaGrawitacji * 1.5f; //Zwiekszenie grawitacji podczas spadania
        } else 
        {
            rb.gravityScale = silaGrawitacji;
        }
        */
        //TO TEST
        //rb.velocity.y = 0;
        
        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            // Debug.Log(coyoteTime);
            //Jesli gracz jest na podlodze LUB spadl z podlogi, to moze skoczyc
            if(!czySkacze)
            {
                // Debug.Log("Zwykly skok + coyoteTime = "+coyoteTime);
                rb.AddForce(Vector2.up * silaSkoku, ForceMode2D.Impulse);
                doubleJumpAvailable = true;
                jumpBufferCounter = 0;
            }

            if (coyoteTime > 0 && !doubleJumpAvailable) {
                // Debug.Log("Coyote skok + coyoteTime = "+coyoteTime);
                rb.AddForce(Vector2.up * silaSkoku, ForceMode2D.Impulse);
                doubleJumpAvailable = false;
                coyoteTime = -1; //limit do pojedynczego skoku coyote, twardy reset przez stycznosc z podloga
                jumpBufferCounter = 0;
            }

            if(czySkacze && doubleJumpAvailable)
            {
                if(Input.GetButtonDown("Jump"))
                {
                    // Debug.Log("Double skok + coyoteTime = "+coyoteTime);
                    rb.AddForce(Vector2.up * silaSkoku * 0.75f, ForceMode2D.Impulse);
                    doubleJumpAvailable = false;
                    jumpBufferCounter = 0;
                }
            }          
        } else {
            jumpBufferCounter -= Time.deltaTime;
        }
        
        if(!czySkacze && jumpBufferCounter > 0f)
        {
            // Debug.Log("Bufor skoku");
            rb.AddForce(Vector2.up * silaSkoku, ForceMode2D.Impulse);
            doubleJumpAvailable = false; //wartosc true powoduje petle skoku regular->double->buffer->double->buffer->...
            jumpBufferCounter=0;
        }
    }


    private void UstawienieGrawitacji(float skala)
    {
        rb.gravityScale = skala;
    }

    private void Obrot()
    {
        if(zwrotPrawo && moveInput.x < 0f || !zwrotPrawo && moveInput.x > 0f)
        {
            zwrotPrawo = !zwrotPrawo;
            Vector3 skala = transform.localScale;
            skala.x *= -1f; //skala.x = skala.x * (-1)
            transform.localScale = skala;
            //Debug.Log("Zwrot Prawo wartosc: " + zwrotPrawo);
        }
        
    }

    /*
    public void SprawdzenieZwrotu(bool czyRuszaWPrawo)
    {
        if(czyRuszaWPrawo != zwrotPrawo)
        {
            Obrot();
        }
    }
    */
}