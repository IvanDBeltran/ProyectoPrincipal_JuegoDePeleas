using System.Collections.Generic;
using UnityEngine;

public class EstadisticasBase : MonoBehaviour, InterfazDeMetodosGenericosParaAcciones
{
    [SerializeField]
    private float vida;
    private float fuerza;
    private float patada, punio;
    private bool estaEnElpiso, isPunio, isPatada, isFireBall, isDragonPunch;
    private float speedJump;
    [SerializeField]
    private float speed;
    [Header("Tiempos de estados")]
    [Tooltip("Tiempo en volver al combate")]
    [Range(0f, 1f)]
    [SerializeField]
    private float tiempoRecuperacion;
    [Tooltip("Tiempo donde no podra ser golpeado de nuevo")]
    [Range(0f, 1f)]
    [SerializeField]
    private float tiempoGolpeado;
    //desplazamientos por golpe (fuerzas)
    [SerializeField]
    private float fuerzaGolpeDebil, fuerzaGolpeFuerte, fuerzaDragonPunch, fuerzaEmpujeDragonPunch;
    private float speedFireBal;
    [SerializeField]
    private GameObject fireBallPrefab;

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

    public float TiempoRecuperacion { get => tiempoRecuperacion; set => tiempoRecuperacion = value; }
    public float TiempoGolpeado { get => tiempoGolpeado; set => tiempoGolpeado = value; }
    public float FuerzaGolpeDebil { get => fuerzaGolpeDebil; set => fuerzaGolpeDebil = value; }
    public float FuerzaGolpeFuerte { get => fuerzaGolpeFuerte; set => fuerzaGolpeFuerte = value; }
    public float FuerzaDragonPunch { get => fuerzaDragonPunch; set => fuerzaDragonPunch = value; }
    
    public bool IsDragonPunch { get => isDragonPunch; set => isDragonPunch = value; }
    

    public Dictionary<string, Queue<string>> ListadoDeSecuencias { get; private set; }
    public float SpeedFireBall { get => speedFireBal; set => speedFireBal = value; }

    public GameObject FireBallPrefab => fireBallPrefab;

    public bool IsFireBall { get => isFireBall; set => isFireBall = value; }
    public float FuerzaEmpujeDragonPunch { get => fuerzaEmpujeDragonPunch; set => fuerzaEmpujeDragonPunch = value; }

    public virtual void Awake()
    {
        vida = 100;
        fuerza = 10;
        fuerzaGolpeDebil = 800;
        fuerzaGolpeFuerte = 1000;
        fuerzaEmpujeDragonPunch = 4000;
        fuerzaDragonPunch = fuerza * 0.5f;


        //agregamos la secuencia de fireball
        ListadoDeSecuencias = new Dictionary<string, Queue<string>>
        {
            { SecuenciasPermitidas.FIREBALL, CrearSecuenciaFireBall() },
            { SecuenciasPermitidas.CORRER, CrearSecuenciaDeCorrer() },
            { SecuenciasPermitidas.DRAGONPUNCH, CrearSecuenciaDragonPunch() },
            { SecuenciasPermitidas.SALTARHACIAATRAS , CrearSecuenciaDeSaltarHaciaAtras() }
        };

    }

    private Queue<string> CrearSecuenciaFireBall()
    {
        Queue<string> fireBall = new Queue<string>();
        fireBall.Enqueue(SecuenciasPermitidas.ABAJO);
        fireBall.Enqueue(SecuenciasPermitidas.DIAGONALDELANTE);
        fireBall.Enqueue(SecuenciasPermitidas.DELANTE);
        fireBall.Enqueue(SecuenciasPermitidas.PUNIODEBIL);
        return fireBall;
    }

    private Queue<string> CrearSecuenciaDeCorrer()
    {
        Queue<string> correr = new Queue<string>();
        correr.Enqueue(SecuenciasPermitidas.DELANTE);
        correr.Enqueue(SecuenciasPermitidas.VACIO);
        correr.Enqueue(SecuenciasPermitidas.DELANTE);
        return correr;
    }

    private Queue<string> CrearSecuenciaDeSaltarHaciaAtras()
    {
        Queue<string> correr = new Queue<string>();
        correr.Enqueue(SecuenciasPermitidas.ATRAS);
        correr.Enqueue(SecuenciasPermitidas.VACIO);
        correr.Enqueue(SecuenciasPermitidas.ATRAS);
        return correr;
    }

    private Queue<string> CrearSecuenciaDragonPunch()
    {
        Queue<string> dragonPunch = new Queue<string>();
        dragonPunch.Enqueue(SecuenciasPermitidas.DELANTE);
        dragonPunch.Enqueue(SecuenciasPermitidas.DIAGONALDELANTE);
        dragonPunch.Enqueue(SecuenciasPermitidas.ABAJO);
        dragonPunch.Enqueue(SecuenciasPermitidas.DIAGONALDELANTE);
        dragonPunch.Enqueue(SecuenciasPermitidas.PUNIOFUERTE);
        return dragonPunch;
    }


    public void QuitarVida(float vidaRestar)
    {
        Vida -= vidaRestar;
        //Despues de restar vida, le cambiamos el estado, en este caso de quien tenga la transicion hacia golpeado
        BaseMaquinaEstadosFinita estados = GetComponent<BaseMaquinaEstadosFinita>();
        estados.IsGolpeado = true;
    }
}
