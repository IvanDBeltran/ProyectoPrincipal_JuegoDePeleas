using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoDeSprite : MonoBehaviour
{
    public float velocidad;
    [SerializeField]
    private bool punio, patada;
    private KeyCode patadaDebil, patadaFuerte, punioDebil, punioFuerte;
    [SerializeField] private int playerNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        if(playerNumber  == 2){
            punioDebil = KeyCode.Joystick2Button0;
            punioFuerte = KeyCode.Joystick2Button1;
            patadaDebil = KeyCode.Joystick2Button2;
            patadaFuerte = KeyCode.Joystick2Button3;
        }
        else if(playerNumber == 1)
        {
            punioDebil = KeyCode.Joystick1Button0;
            punioFuerte = KeyCode.Joystick1Button1;
            patadaDebil = KeyCode.Joystick1Button2;
            patadaFuerte = KeyCode.Joystick1Button3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(patadaDebil) || Input.GetKeyDown(patadaFuerte)) && !punio && !patada)
        {
            patada = true;
            GetComponent<Animator>().SetBool("patada", true);
        }

        if ((Input.GetKeyDown(punioDebil) || Input.GetKeyDown(punioFuerte)) && !punio && !patada)
        {
            punio = true;
            GetComponent<Animator>().SetBool("punio", punio);
        }
        //mandando al animator la velocidad
        //GetComponent<Animator>().SetFloat("velocidad", velocidad);
    }

    public void FinalDePatada()
    {
        patada = false;
        GetComponent<Animator>().SetBool("patada", patada);
    }


    public void FinalDePunio()
    {
        punio = false;
        GetComponent<Animator>().SetBool("punio", punio);
    }

    public Boolean Punio
    {
        get { return punio; }
    }

    public Boolean Patada
    {
        get { return patada; }
    }

    public void finDeDash()
    {
        GetComponent<Animator>().SetBool("dash", false);
    }
}
