using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    public Animator animatorControl;
    //PlayerController playerController = new PlayerController();

    //[SerializeField] sprawia ze zmienna nadal jest widoczna w edytorze i wciaz pozostaje prywatna
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jump;

    void Start()
    {
        animatorControl = GetComponent<Animator>();
    }

    
    void Update()
    {
        animatorControl.SetFloat("moveSpeed", Mathf.Abs(PlayerController.moveInput.x)); //Mathf.Abs() daje wartosc absolutna, aby uniknac sytuacji gdzie moveSpeed < 0
        animatorControl.SetBool("jump", PlayerController.czySkacze);
    }
}
