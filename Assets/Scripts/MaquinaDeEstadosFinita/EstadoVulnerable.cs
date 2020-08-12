using UnityEngine;
using System.Collections;
using System;

public class EstadoVulnerable : BaseMaquinaEstadosFinita
{
    private float deltaTimeLocal;
    public override void Start()
    {
        base.Start();
        deltaTimeLocal = 0;
    }
    public override void Salir()
    {
        //
    }

    public override void Update()
    {
        deltaTimeLocal += Time.deltaTime;
        //Siempre hay que verificar los cambios
        VerificarCambios();
    }

    public override Type VerficarTransiciones()
    {
        if( deltaTimeLocal >= accionesDelPersonaje.TiempoVulnerable)
        {
            return typeof(EstadoEstar);
        }
        return GetType();
    }
}
