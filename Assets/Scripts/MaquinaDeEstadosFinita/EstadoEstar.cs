using UnityEngine;
using System.Collections;
using System;

public class EstadoEstar : BaseMaquinaEstadosFinita
{
    private string baseNombreHorizontal = "Horizontal_Player";
    private string baseNombreVertical = "Vertical_Player";
    [SerializeField]
    private float min, max, x, y, deltaTimeLocal, alturamax;
    private bool comenzarContar;
    private Rigidbody2D rb;
    private float movimientoInyectado = 0;
    public override void Start()
    {
        base.Start();
        //Todo el codigo aqui
        min = -1f;
        max = 1f;
        rb = GetComponent<Rigidbody2D>();
        deltaTimeLocal = min;
        baseNombreHorizontal += playerNumber.ToString();
        baseNombreVertical += playerNumber.ToString();
    }
    public override void Salir()
    {
        //consulto un componente de tipo atacar y le coloco la variable de lo que se precion para entrar al estado sea el caso de que ataque
        if (Input.GetKeyDown(DeterminacionDeQueBotonSePreciono()))
        {
            GetComponent<EstadoAtacar>().botonPrecionado = DeterminacionDeQueBotonSePreciono();
        }
    }

    public override void Update()
    {
        if (!comenzarContar)
        {
            if (GetMovimientoDelObjeto() > 0)
            {
                x = 1;
                //derecha pero ya no, porque ahora debe mirar siempre al otro player
                //transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            if (GetMovimientoDelObjeto() < 0)
            {
                x = -1;
                //izqueirda pero ya no, porque ahora debe mirar siempre al otro player
                //transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            if (GetMovimientoDelObjeto() == 0)
            {
                x = 0;
            }
            if (Input.GetAxis(baseNombreVertical) > 0)
            {
                GetComponent<Animator>().SetTrigger("saltar");
                GetComponent<Animator>().SetBool("tocarPiso", false);
                comenzarContar = true;
            }
        }

        if (comenzarContar)
        {

            deltaTimeLocal += (Time.deltaTime * 2);
            //ir modificando a una funcion matematica mas suave en su movimiento
            y = Mathf.Cos(deltaTimeLocal) * accionesDelPersonaje.SpeedJump;
            //Como por ejemplo
            //2x^(3)+2
            //y = ((2 * Mathf.Pow(deltaTimeLocal, 3)) + 2) * speedJump;
            if (y > alturamax)
            {
                alturamax = y;
            }
            else
            {
                y *= -1;
            }
            if (deltaTimeLocal >= max)
            {
                ResetObject();
            }
        }
        else
        {
            y = rb.velocity.y;
        }
        rb.velocity = new Vector2(x * accionesDelPersonaje.Speed, y);
        //rb.velocity = new Vector2(x * speed, rb.velocity.y);
        GetComponent<Animator>().SetFloat("velocidad", Mathf.Abs(rb.velocity.x));


        //Siempre hay que verificar los cambios
        VerificarCambios();
    }

    public override Type VerficarTransiciones()
    {
        if (Input.GetKeyDown(patadaDebil) || Input.GetKeyDown(patadaFuerte) || Input.GetKeyDown(punioDebil) || Input.GetKeyDown(punioFuerte))
        {
            return typeof(EstadoAtacar);
        }
        return GetType();
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
            throw new Exception("No se preciono ningun boton");
        }
    }


    private float GetMovimientoDelObjeto()
    {
        if (movimientoInyectado != 0)
        {
            return movimientoInyectado;
        }
        else
        {
            return Input.GetAxis(baseNombreHorizontal);
        }
    }

    public float MovimientoInyectado
    {
        set { movimientoInyectado = value; }
    }

    public void ResetObject()
    {
        comenzarContar = false;
        deltaTimeLocal = min;
        y = 0;
        alturamax = -1;
        GetComponent<Animator>().SetBool("tocarPiso", true);
    }

}
