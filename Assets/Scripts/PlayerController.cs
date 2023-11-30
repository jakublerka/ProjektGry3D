using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float grawitacja; //sila z jaka dziala grawitacja
    public float speed=10f;
    private float movementX;
    private float movementY;
    private Vector2 moveInput;
    private bool czySkacze;
    private bool czyZwrotPrawo;
    private bool czyZwrotLewo;
    private bool czySkaczeNaScianie; //For later
    public float bieganieMaksSzybkosc;
    public float bieganiePrzyspieszenie;
    public float przyszpieszenieSpadania; //Bedzie dodane pod przycisk 'S' oraz strzalke w dol zeby przyspieszyc spadanie jesli gracz chce.
    public bool podtrzymanieMomentum = true;



    //Awake wywolywane jest w momencie ladowania sceny
    void Awake()
    {
        //gameObject szuka obiekt w którym jest aktualnie dołączony, w tym przypadku Player.
        rb=gameObject.GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        SetGravityScale(grawitacja);
        czyZwrotPrawo = true; 
    }

    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Run(1);
        //Debug.Log(rb.velocity.x);
        /*
        if(moveHorizontal>0.1f || moveHorizontal < -0.1f)
        {
            
            rb.AddForce(new Vector2(moveHorizontal * speed, 0f), ForceMode2D.Impulse);
        }
        */

    }

    private void Run(float lerpAmount)
    {
        float predkoscDocelowa = movementX * bieganieMaksSzybkosc;
        predkoscDocelowa = Mathf.Lerp(rb.velocity.x, predkoscDocelowa, lerpAmount); //Interpolacja liniowa

        float predkoscAkceleracji=1;

        float roznicaPredkosci = predkoscDocelowa - rb.velocity.x;
        float movement = roznicaPredkosci * predkoscAkceleracji;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

        if(movementX!=0)
        {
            SprawdzenieZwrotu(movementX>0);
        }
    }

    //Obrot parent obiektu (player) na osi X
    private void Obrot()
    {
        Vector3 skala = transform.localScale;
        skala.x *= -1; //skala.x = skala.x * (-1)
        transform.localScale = skala;

        czyZwrotPrawo = !czyZwrotPrawo;
        Debug.Log("Zwrot Prawo wartosc: " + czyZwrotPrawo);
    }


    public void SetGravityScale(float scale)
    {
        rb.gravityScale=scale;
    }

    public void SprawdzenieZwrotu(bool czyRuszaWPrawo)
    {
        if(czyRuszaWPrawo != czyZwrotPrawo)
        {
            Obrot();
        }
    }


}
