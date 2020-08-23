using UnityEngine;
using System.Collections;
using System;

public class EstadoAtacar : BaseMaquinaEstadosFinita
{
    public KeyCode botonPrecionado;
    public override void Start()
    {
        base.Start();
        if ((botonPrecionado == patadaDebil || botonPrecionado == patadaFuerte) && !accionesDelPersonaje.IsPunioActivo && !accionesDelPersonaje.IsPatadaActivo && !accionesDelPersonaje.IsDragonPunch /*&& !accionesDelPersonaje.IsFireBall*/)
        {
            accionesDelPersonaje.IsPatadaActivo = true;
            componenteDeAnimacion.SetBool("patada", true);
        }

        if ((botonPrecionado == punioDebil || botonPrecionado == punioFuerte) && !accionesDelPersonaje.IsPunioActivo && !accionesDelPersonaje.IsPatadaActivo && !accionesDelPersonaje.IsDragonPunch/* && !accionesDelPersonaje.IsFireBall*/)
        {
            accionesDelPersonaje.IsPunioActivo = true;
            componenteDeAnimacion.SetBool("punio", true);
        }
        if (!accionesDelPersonaje.IsPunioActivo && !accionesDelPersonaje.IsPatadaActivo && accionesDelPersonaje.IsDragonPunch)
        {
            componenteDeAnimacion.SetBool("dragonPunch", true);
            movimientoDelObjeto.ComenzarContar = true;
            movimientoDelObjeto.X = 0;
        }
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
            return typeof(EstadoEstar);
        }
        if (player.FueGolpeado)
        {
            return typeof(EstadoGolpeado);
        }
        return GetType();
    }

}
