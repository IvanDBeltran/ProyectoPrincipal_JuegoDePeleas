using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionesDePlayer : MonoBehaviour
{
    private bool YaPego;
    private InterfazDeMetodosGenericosParaAcciones instanciaAComponenteDeDanioDelOtro;
    private BaseMaquinaEstadosFinita maquina;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("golpe")) {
            if (collision.gameObject.GetComponent<ReferenciaAlPadre>().padre.GetComponent<InterfazDeMetodosGenericosParaAcciones>() != null)
            {
                instanciaAComponenteDeDanioDelOtro = collision.gameObject.GetComponent<ReferenciaAlPadre>().padre.GetComponent<InterfazDeMetodosGenericosParaAcciones>();
            }

            if (!GetComponent<BaseMaquinaEstadosFinita>().IsGolpeado && GetComponent<BaseMaquinaEstadosFinita>().GetType() != typeof(EstadoDefender) && collision.gameObject.GetComponent<ReferenciaAlPadre>().padre != gameObject)
            {
                GetComponent<InterfazDeMetodosGenericosParaAcciones>().QuitarVida(LoQueTieneQueQuitarDependiendoDeTipoDeGolpe(instanciaAComponenteDeDanioDelOtro));
                if(collision.gameObject.GetComponent<ComportamientoFireBall>() != null)
                {
                    collision.gameObject.GetComponent<Animator>().SetTrigger("colisiono");
                }
                //aplicamos la fuerza del oponente, y se la aplicamos en direccion hacia atras de este personaje
                //fuerza a aplicar del oponente
                GetComponent<MovimientoGenerico>().AplicarFuerzaPersonaje(FuerzaAplicarParaDesplazamientoDelObjeto(instanciaAComponenteDeDanioDelOtro));
            }
        }
    }

    private float LoQueTieneQueQuitarDependiendoDeTipoDeGolpe(InterfazDeMetodosGenericosParaAcciones instanciaAComponenteDeDanioMetodo)
    {
        float fuerzaAQuitar = 0;
        //Sacamos del objecto de colision su padre
        if (instanciaAComponenteDeDanioMetodo.IsPunioActivo)
        {
            fuerzaAQuitar = instanciaAComponenteDeDanioMetodo.Punio;

        }
        else if (instanciaAComponenteDeDanioMetodo.IsPatadaActivo)
        {
            fuerzaAQuitar = instanciaAComponenteDeDanioMetodo.Patada;
        }else if (instanciaAComponenteDeDanioMetodo.IsFireBall)
        {
            fuerzaAQuitar = 100;
        }
        //Debug.Log("Fuerza a quitar " + fuerzaAQuitar);
        return fuerzaAQuitar;
    }

    private float FuerzaAplicarParaDesplazamientoDelObjeto(InterfazDeMetodosGenericosParaAcciones instanciaAComponenteDeOponente)
    {
        float fuerzaAplicar = 0;
        //Sacamos del objecto de colision su padre
        if (instanciaAComponenteDeOponente.IsPunioActivo)
        {
            fuerzaAplicar = instanciaAComponenteDeOponente.FuerzaGolpeDebil;

        }
        else if (instanciaAComponenteDeOponente.IsPatadaActivo)
        {
            fuerzaAplicar = instanciaAComponenteDeOponente.FuerzaGolpeFuerte;
        }
        //ahora que sabemos cual es la fuerza, debemos buscar la cardinalidad de el
        fuerzaAplicar *= GetComponent<BaseMaquinaEstadosFinita>().CardinalidadDeHaciaAtras();
        return fuerzaAplicar;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("golpe"))
        {
            YaPego = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("piso"))
        {
            GetComponent<Animator>().SetBool("tocarPiso", true);
            GetComponent<InterfazDeMetodosGenericosParaAcciones>().EstaEnElPiso = true;
        }
    }

}
