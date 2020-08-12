using UnityEngine;
using System.Collections;

public class EstadisticasBase : MonoBehaviour, InterfazDeMetodosGenericosParaAcciones
{
    private float vida;
    private float fuerza;
    private float patada, punio;
    private bool estaEnElpiso, isPunio, isPatada;
    private float speedJump;
    private float speed;
    private float tiempoVulnerable;

    public bool IsPunioActivo
    {
        set { isPunio = value; }
        get { return isPunio; }
    }
    public bool IsPatadaActivo
    {
        set { isPatada = value; }
        get { return isPatada; }
    }
    public float SpeedJump
    {
        set { speedJump = value; }
        get { return speedJump; }
    }

    public float Speed
    {
        set { speed = value; }
        get { return speed; }
    }
    public bool EstaEnElPiso
    {
        set { estaEnElpiso = value; }
        get { return estaEnElpiso; }
    }
    public float Patada
    {
        get { return fuerza * 2; }
        set { patada += value; }
    }
    public float Punio
    {
        get { return fuerza * 1; }
        set { punio += value; }
    }
    public float Vida{
        get { return vida; }
        set { vida = value; }
    }

    public float Fuerza
    {
        get { return fuerza; }
        set { fuerza += value; }
    }

    public float TiempoVulnerable { get => tiempoVulnerable; set => tiempoVulnerable = value; }

    public virtual void Awake()
    {
        vida = 100;
        fuerza = 10;
        tiempoVulnerable = 0.5f;
    }

    public void QuitarVida(float vidaRestar)
    {
        Vida -= vidaRestar;
    }
}
