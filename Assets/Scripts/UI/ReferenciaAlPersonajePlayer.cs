using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenciaAlPersonajePlayer : MonoBehaviour
{
    public GameObject personajeSeleccionado;
    public Image barraDeVida;
    private float cienPorcientoDeVidaDePlayer;

    private void Update()
    {
        //vamos estar siempre mirando al personaje elegido para tomar como 100% su vida y como va a ir bajando
        barraDeVida.fillAmount = PorcentajeDeVidaRestanteDelPersonaje();
    }

    private float PorcentajeDeVidaRestanteDelPersonaje()
    {
        float valor100porciento = cienPorcientoDeVidaDePlayer;
        float vidaRestante = personajeSeleccionado.GetComponent<InterfazDeMetodosGenericosParaAcciones>().Vida;
        //Debug.Log("100% " + valor100porciento + " vida restante " + vidaRestante+" divicion "+ (vidaRestante / valor100porciento));
        return vidaRestante / valor100porciento;
    }

    public void AgregarPersonajeAlUI(GameObject personaje)
    {
        cienPorcientoDeVidaDePlayer = personaje.GetComponent<InterfazDeMetodosGenericosParaAcciones>().Vida;
        personajeSeleccionado = personaje;
    }
}
