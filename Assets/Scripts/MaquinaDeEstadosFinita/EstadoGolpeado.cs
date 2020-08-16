using UnityEngine;
using System.Collections;
using System;

public class EstadoGolpeado : BaseMaquinaEstadosFinita
{
    private float deltaTimeLocal = 0;
    private bool termino;
    public override void Start()
    {
        base.Start();
        componenteDeAnimacion.SetBool("golpeado", true);
        oponente.GetComponent<DatosPersistentesDelPlayer>().AumentarCantidadDeGolpes();
        if(oponente.GetComponent<DatosPersistentesDelPlayer>().CantidadDeGolpes > 1)
        {
            //Activamos el letrero, y seteamos sus golpes
            oponente.GetComponent<DatosPersistentesDelPlayer>().MostrarLetreroDeCombos();
        }
    }
    public override void Salir()
    {
        //aSidoGolpeado = false;
        componenteDeAnimacion.SetBool("golpeado", false);
        IsGolpeado = false;
    }

    public override void Update()
    {
        deltaTimeLocal += Time.deltaTime;
        termino = deltaTimeLocal >= accionesDelPersonaje.TiempoGolpeado;
        VerificarCambios();
    }

    public override Type VerficarTransiciones()
    {
        if (termino)
        {
            //Debug.Log("Paso a recuperacion");
            return typeof(EstadoRecuperacion);
        }
        return GetType();
    }
}
