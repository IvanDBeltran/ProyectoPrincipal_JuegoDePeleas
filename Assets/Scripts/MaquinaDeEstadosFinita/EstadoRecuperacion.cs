using UnityEngine;
using System.Collections;
using System;

public class EstadoRecuperacion : BaseMaquinaEstadosFinita
{
    private float deltaTimeLocal = 0;
    private bool termino;
    public override void Start()
    {
        base.Start();
    }
    public override void Salir()
    {
        
    }

    public override void Update()
    {
        deltaTimeLocal += Time.deltaTime;
        termino = deltaTimeLocal >= accionesDelPersonaje.TiempoRecuperacion;
        VerificarCambios();
    }

    public override Type VerficarTransiciones()
    {
        if (termino)
        {
            oponente.GetComponent<DatosPersistentesDelPlayer>().CantidadDeGolpes = 0;
            //desactivamos el letrero
            oponente.GetComponent<DatosPersistentesDelPlayer>().OcultarLetreroDeCombos();
            return typeof(EstadoEstar);
        }
        if (player.FueGolpeado)
        {
            //Debug.Log("Paso a Golpeado");
            return typeof(EstadoGolpeado);
        }
        return GetType();
    }
}
