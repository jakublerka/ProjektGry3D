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
    public PlayerData Data;

    public Rigidbody2D rb {get; private set;} //public read, private write
    public bool zwrotPrawo {get; private set;}
    public bool czySkacze {get; private set;}



    private Vector2 moveInput;



    //Checkery polozenia postaci, beda potrzebne do np. skoku od sciany.
    [Header("Checkery")]
    [SerializeField] private Transform punktUziemieniaChecker; //Sprawia ze prywatna zmienna pokazuje sie w Inspektorze
    [SerializeField] private Vector2 wielkoscPunktuUziemienia = new Vector2(0.50f, 0.03f);


    [SerializeField] private LayerMask podlogaLayer;


    public void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UstawienieGrawitacji(Data.skalaGrawitacji);
        zwrotPrawo = true;
    }


    private void Update()
    {
        //To do: TIMERY


        // <!-- INPUT HANDLERS --!>
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        Run(1);
    }


    // <!-- Metody poruszania gracza --!>
    private void Run(float lerpAmount)
    {
        float predkoscDocelowa = moveInput.x * Data.maksPredBiegania;
        predkoscDocelowa = Mathf.Lerp(rb.velocity.x, predkoscDocelowa, lerpAmount);

        float przyspieszenie = 2;

        float roznicaPredkosci = predkoscDocelowa - rb.velocity.x;
        float movement = roznicaPredkosci * przyspieszenie;

        rb.AddForce(Vector2.right * movement, ForceMode2D.Force);

        if(moveInput.x != 0)
        {
            SprawdzenieZwrotu(moveInput.x>0);
        }
    }


    private void Obrot()
    {
        Vector3 skala = transform.localScale;
        skala.x *= -1; //skala.x = skala.x * (-1)
        transform.localScale = skala;

        zwrotPrawo = !zwrotPrawo;
        Debug.Log("Zwrot Prawo wartosc: " + zwrotPrawo);
    }



    private void Jump()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = Data.silaGrawitacji * 1.5f; //Zwiekszenie grawitacji podczas spadania
        }
        //TO TEST
        //rb.velocity.y = 0;
        
        rb.AddForce(Vector2.up * Data.silaSkoku, ForceMode2D.Impulse);

    }




    private void UstawienieGrawitacji(float skala)
    {
        rb.gravityScale = skala;
    }

    public void SprawdzenieZwrotu(bool czyRuszaWPrawo)
    {
        if(czyRuszaWPrawo != zwrotPrawo)
        {
            Obrot();
        }
    }






}