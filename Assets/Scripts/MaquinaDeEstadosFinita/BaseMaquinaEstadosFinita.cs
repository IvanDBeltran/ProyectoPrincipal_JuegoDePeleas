using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMaquinaEstadosFinita : MonoBehaviour
{
    protected Animator componenteDeAnimacion;
    protected InterfazDeMetodosGenericosParaAcciones accionesDelPersonaje;
    [Range(1, 2)]
    public int playerNumber = 1;
    protected KeyCode patadaDebil, patadaFuerte, punioDebil, punioFuerte;
    public abstract void Salir();
    public virtual void Start()
    {
        componenteDeAnimacion = GetComponent<Animator>();
        accionesDelPersonaje = GetComponent<InterfazDeMetodosGenericosParaAcciones>();
        if (playerNumber == 2)
        {
            punioDebil = KeyCode.Joystick2Button0;
            punioFuerte = KeyCode.Joystick2Button1;
            patadaDebil = KeyCode.Joystick2Button2;
            patadaFuerte = KeyCode.Joystick2Button3;
        }
        else if (playerNumber == 1)
        {
            punioDebil = KeyCode.Joystick1Button0;
            punioFuerte = KeyCode.Joystick1Button1;
            patadaDebil = KeyCode.Joystick1Button2;
            patadaFuerte = KeyCode.Joystick1Button3;
        }
    }

    public abstract void Update();

    public abstract Type VerficarTransiciones();

    public void VerificarCambios()
    {
        Type estadoACambiar = VerficarTransiciones();
        if (estadoACambiar != this.GetType())
        {
            CambiarEstado(estadoACambiar);
        }
    }

    public void CambiarEstado(Type nuevoEstado)
    {
        //agregamos el componente
        gameObject.AddComponent(nuevoEstado);
        //salir del estado actual
        Salir();
        //destuimos el estado viejo
        Destroy(this);
    }
}
