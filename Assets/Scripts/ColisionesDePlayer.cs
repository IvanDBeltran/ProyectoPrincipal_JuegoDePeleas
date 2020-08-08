using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionesDePlayer : MonoBehaviour
{
    private bool YaPego;
    private InterfazDeMetodosGenericosParaAcciones instanciaAComponenteDeDanioDelOtro;
    private ReferenciaAlPadre referenciaAlPadre;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("golpe")) {
            if (collision.gameObject.GetComponent<ReferenciaAlPadre>().padre.GetComponent<InterfazDeMetodosGenericosParaAcciones>() != null)
            {
                instanciaAComponenteDeDanioDelOtro = collision.gameObject.GetComponent<ReferenciaAlPadre>().padre.GetComponent<InterfazDeMetodosGenericosParaAcciones>();
            }
            
            if (!YaPego)
            {
                GetComponent<InterfazDeMetodosGenericosParaAcciones>().QuitarVida(LoQueTieneQueQuitarDependiendoDeTipoDeGolpe(instanciaAComponenteDeDanioDelOtro));
                YaPego = true;
            }
        }
    }

    private float LoQueTieneQueQuitarDependiendoDeTipoDeGolpe(InterfazDeMetodosGenericosParaAcciones instanciaAComponenteDeDanioMetodo)
    {
        float fuerzaAQuitar = 0;
        //Sacamos del objecto de colision su padre
        if (instanciaAComponenteDeDanioMetodo.EstaElPunioActivo())
        {
            fuerzaAQuitar = instanciaAComponenteDeDanioMetodo.GetFuerzaPunio();

        }
        else if (instanciaAComponenteDeDanioMetodo.EstaLaPatadaActivo())
        {
            fuerzaAQuitar = instanciaAComponenteDeDanioMetodo.GetFuerzaPatada();
        }
        Debug.Log("Fuerza a quitar " + fuerzaAQuitar);
        return fuerzaAQuitar;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("golpe"))
        {
            YaPego = false;
        }
    }

}
