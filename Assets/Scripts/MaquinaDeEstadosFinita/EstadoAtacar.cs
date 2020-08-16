using UnityEngine;
using System.Collections;
using System;

public class EstadoAtacar : BaseMaquinaEstadosFinita
{
    private bool terminoLaAnimacion = false;
    public KeyCode botonPrecionado;
    public override void Start()
    {
        base.Start();
        if ((botonPrecionado == patadaDebil || botonPrecionado == patadaFuerte) && !accionesDelPersonaje.IsPunioActivo && !accionesDelPersonaje.IsPatadaActivo /*&& !accionesDelPersonaje.IsFireBall*/)
        {
            accionesDelPersonaje.IsPatadaActivo = true;
            componenteDeAnimacion.SetBool("patada", true);
        }

        if ((botonPrecionado == punioDebil || botonPrecionado == punioFuerte) && !accionesDelPersonaje.IsPunioActivo && !accionesDelPersonaje.IsPatadaActivo/* && !accionesDelPersonaje.IsFireBall*/)
        {
            accionesDelPersonaje.IsPunioActivo = true;
            componenteDeAnimacion.SetBool("punio", true);
        }

        /*if (!accionesDelPersonaje.IsPunioActivo && !accionesDelPersonaje.IsPatadaActivo && accionesDelPersonaje.IsFireBall)
        {
            componenteDeAnimacion.SetTrigger("fireball");
        }*/
    }
    public override void Salir()
    {
        //Codigo para cuando salga del estado
    }

    public override void Update()
    {
        //Siempre hay que verificar los cambios
        VerificarCambios();
    }

    public override Type VerficarTransiciones()
    {
        if (terminoLaAnimacion)
        {
            return typeof(EstadoVulnerable);
        }
        return GetType();
    }
    public void FinalDePatada()
    {
        accionesDelPersonaje.IsPatadaActivo = false;
        GetComponent<Animator>().SetBool("patada", false);
        terminoLaAnimacion = true;
    }
    public void FinalDePunio()
    {
        accionesDelPersonaje.IsPunioActivo = false;
        GetComponent<Animator>().SetBool("punio", false);
        terminoLaAnimacion = true;
    }
}
