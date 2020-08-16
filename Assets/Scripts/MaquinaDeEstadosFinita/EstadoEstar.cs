using UnityEngine;
using System.Collections;
using System;
using Cinemachine;

public class EstadoEstar : BaseMaquinaEstadosFinita
{
    public override void Start()
    {
        base.Start();
    }
    public override void Salir()
    {
        try
        {
            //consulto un componente de tipo atacar y le coloco la variable de lo que se precion para entrar al estado sea el caso de que ataque
            if (Input.GetKeyDown(DeterminacionDeQueBotonSePreciono()))
            {
                GetComponent<EstadoAtacar>().botonPrecionado = DeterminacionDeQueBotonSePreciono();
            }
        }
        catch(BotonesException e)
        {
            //no pasa nada
        }catch(Exception e)
        {
            //nos liamos
            Debug.LogError(e.Message);
        }

    }

    public override void Update()
    {
        //Siempre hay que verificar los cambios
        VerificarCambios();
    }

    public override Type VerficarTransiciones()
    {
        if (Input.GetKeyDown(patadaDebil) || Input.GetKeyDown(patadaFuerte) || Input.GetKeyDown(punioDebil) || Input.GetKeyDown(punioFuerte))
        {
            MandarleAlLogLoQueSePreciono();
            return typeof(EstadoAtacar);
        }
        if(ElOponenteEstaAtacando() && EstaEchandoParaAtras())
        {
            return typeof(EstadoDefender);
        }
        if (player.FueGolpeado)
        {
            return typeof(EstadoGolpeado);
        }
        return GetType();
    }

    private void MandarleAlLogLoQueSePreciono()
    {
        KeyCode LoQueSePreciono = KeyCode.None;
        if (Input.GetKeyDown(PatadaDebil))
        {
            LoQueSePreciono = PatadaDebil;
        }
        else if (Input.GetKeyDown(PatadaFuerte))
        {
            LoQueSePreciono = PatadaFuerte;
        }
        else if (Input.GetKeyDown(PunioDebil))
        {
            LoQueSePreciono = PunioDebil;
        }
        else if (Input.GetKeyDown(PunioFuerte))
        {
            LoQueSePreciono = PunioFuerte;
        }

        movimientoDelObjeto.LogearComandosIngresados(LoQueSePreciono);
    }

    private KeyCode DeterminacionDeQueBotonSePreciono()
    {
        if (Input.GetKeyDown(patadaDebil))
        {
            return patadaDebil;
        }
        else if (Input.GetKeyDown(patadaFuerte))
        {
            return patadaFuerte;
        }
        else if (Input.GetKeyDown(punioDebil))
        {
            return punioDebil;
        }
        else if (Input.GetKeyDown(punioFuerte))
        {
            return punioFuerte;
        }
        else
        {
            throw new BotonesException("No se preciono ningun boton");
        }
    }

}
