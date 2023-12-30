using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    static public Animator animatorControl;
    //PlayerController playerController = new PlayerController();

    //[SerializeField] sprawia ze zmienna nadal jest widoczna w edytorze i wciaz pozostaje prywatna
    //[SerializeField] private float moveSpeed;
    //[SerializeField] private float jump;
    //private float movementX;

    //Na potrzeby "pseudo-delayu"
    private float delayTimer = 2f; // Set the delay time in seconds
    private bool isDelaying = true;
    
    void Start()
    {
        animatorControl = GetComponent<Animator>();
    }

    
    void Update()
    {
        //movementX = Input.GetAxisRaw("Horizontal") * PlayerController.speed;
        //animatorControl.SetFloat("moveSpeed", Mathf.Abs(PlayerController.movementX*PlayerController.speed)); //Mathf.Abs() daje wartosc absolutna, aby uniknac sytuacji gdzie moveSpeed < 0
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animatorControl.SetBool("jump",true);
            //Task.Delay(2000);
            //yield return new WaitForSeconds(2f);
            if(isDelaying)
            {
                delayTimer -= Time.deltaTime;
                if(delayTimer <= 0f){
                    animatorControl.SetBool("jump",false);
                }
            }
            //OnLanding();
        }
        //playerController.Jump();
        //OnLanding();
    }

    /*public void OnLanding()
    {
        animatorControl.SetBool("jump", playerController.czySkacze);
    }*/
}
