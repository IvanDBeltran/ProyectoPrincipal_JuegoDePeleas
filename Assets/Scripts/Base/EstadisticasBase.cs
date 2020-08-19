using System.Collections.Generic;
using UnityEngine;

public class EstadisticasBase : MonoBehaviour, InterfazDeMetodosGenericosParaAcciones
{
    [SerializeField]
    private float vida;
    private float fuerza;
    private float patada, punio;
    private bool estaEnElpiso, isPunio, isPatada, isFireBall;
    private float speedJump;
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
    private float fuerzaGolpeDebil, fuerzaGolpeFuerte;
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

    public Dictionary<string, Queue<string>> ListadoDeSecuencias { get; private set; }
    public float SpeedFireBall { get => speedFireBal; set => speedFireBal = value; }

    public GameObject FireBallPrefab => fireBallPrefab;

    public bool IsFireBall { get => isFireBall; set => isFireBall = value; }

    public virtual void Awake()
    {
        vida = 100;
        fuerza = 10;
        fuerzaGolpeDebil = 800;
        fuerzaGolpeFuerte = 1000;

        //agregamos la secuencia de fireball
        ListadoDeSecuencias = new Dictionary<string, Queue<string>>
        {
            { SecuenciasPermitidas.FIREBALL, CrearSecuenciaFireBall() }
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

    public void QuitarVida(float vidaRestar)
    {
        Vida -= vidaRestar;
        //Despues de restar vida, le cambiamos el estado, en este caso de quien tenga la transicion hacia golpeado
        BaseMaquinaEstadosFinita estados = GetComponent<BaseMaquinaEstadosFinita>();
        //TODO esto no se para que era pero lo dejo comentado
        /*if (estados.GetType() == typeof(EstadoEstar))
        {
            estados.IsGolpeado = true;
        }*/
        estados.IsGolpeado = true;
    }
}
