using UnityEngine;
using System.Collections;

public class Ejemplo1 : EstadisticasBase, InterfazDeMetodosGenericosParaAcciones
{
    public bool EstaElPunioActivo()
    {
        return GetComponent<MovimientoDeSprite>().Punio;
    }

    public bool EstaLaPatadaActivo()
    {
        return GetComponent<MovimientoDeSprite>().Patada;
    }

    public float GetFuerza()
    {
        return Fuerza;
    }

    public float GetFuerzaPatada()
    {
        return Patada;
    }

    public float GetFuerzaPunio()
    {
        return Punio;
    }

    public float GetVida()
    {
        return Vida;
    }

    public void QuitarVida(float vidaRestar)
    {
        Vida -= vidaRestar;
    }

    private void Awake()
    {
        Vida += 50;
        Fuerza = 1;
    }

    
}
