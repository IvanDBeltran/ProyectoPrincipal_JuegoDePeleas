using UnityEngine;
using System.Collections;
using System;

public class EstadoDefender : BaseMaquinaEstadosFinita
{
    public override void Start()
    {
        base.Start();
        componenteDeAnimacion.SetBool("defender", true);
    }
    public override void Salir()
    {
        //Cuando salga del estado
        componenteDeAnimacion.SetBool("defender", false);
        IsGolpeado = false;
    }

    public override void Update()
    {

        VerificarCambios();
    }

    public override Type VerficarTransiciones()
    {
        if(!EstaEchandoParaAtras() || !ElOponenteEstaAtacando())
        {
            return typeof(EstadoEstar);
        }
        return GetType();
    }
}
