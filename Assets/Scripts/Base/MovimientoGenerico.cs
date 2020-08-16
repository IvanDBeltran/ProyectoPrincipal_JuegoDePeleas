using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovimientoGenerico : MonoBehaviour
{
    private bool comenzarContar, logeoDeSalto;
    private Rigidbody2D rb;
    private float min, max, x, y, deltaTimeLocal, alturamax, deltaTimeLocalParaControl;
    private BaseMaquinaEstadosFinita maquina;
    [Range(0f, 2f)]
    [SerializeField]
    private float tiempoDeTomaDeControl;
    [SerializeField]
    private Queue<string> palancas;
    [SerializeField]
    private int stackMaximo;
    [SerializeField]
    private KeyCode botonPrecionado;

    private void Start()
    {
        //Todo el codigo aqui
        min = -1f;
        max = 1f;
        rb = GetComponent<Rigidbody2D>();
        deltaTimeLocal = min;
        maquina = GetComponent<BaseMaquinaEstadosFinita>();
        palancas = new Queue<string>();
    }
    private void Update()
    {
        if (!comenzarContar)
        {
            if (GetMovimientoDelObjeto() > 0)
            {
                x = 1;
                LogearComandosIngresados();
            }
            if (GetMovimientoDelObjeto() < 0)
            {
                x = -1;
                LogearComandosIngresados();
            }
            if (GetMovimientoDelObjeto() == 0)
            {
                x = 0;
            }
            if (Input.GetAxis(maquina.Vertical) > 0)
            {
                maquina.ComponenteAnimacion.SetTrigger("saltar");
                maquina.ComponenteAnimacion.SetBool("tocarPiso", false);
                maquina.AccionesDelPersonaje.EstaEnElPiso = false;
                comenzarContar = true;
                LogearComandosIngresados();
            }
            else if (Input.GetAxis(maquina.Vertical) < 0)
            {
                LogearComandosIngresados();
            }
            deltaTimeLocalParaControl += Time.deltaTime;
        }

        if (comenzarContar)
        {

            deltaTimeLocal += (Time.deltaTime * 2);
            //ir modificando a una funcion matematica mas suave en su movimiento
            y = Mathf.Cos(deltaTimeLocal) * maquina.AccionesDelPersonaje.SpeedJump;
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
            LogearComandosIngresados();
        }
        else
        {
            y = rb.velocity.y;
        }
        rb.velocity = new Vector2(x * maquina.AccionesDelPersonaje.Speed, y);
        //rb.velocity = new Vector2(x * speed, rb.velocity.y);
        GetComponent<Animator>().SetFloat("velocidad", Mathf.Abs(rb.velocity.x));
    }

    public string CardinalidadEscritaHorizontal()
    {
        if (GetMovimientoDelObjeto() > 0)
        {
            if (GetComponent<BaseMaquinaEstadosFinita>().CardinalidadDeHaciaAtras() > 0)
            {
                return SecuenciasPermitidas.ATRAS;
            }
            else
            {
                return SecuenciasPermitidas.DELANTE;
            }
        }else if (GetMovimientoDelObjeto() < 0)
        {
            if (GetComponent<BaseMaquinaEstadosFinita>().CardinalidadDeHaciaAtras() < 0)
            {
                return SecuenciasPermitidas.ATRAS;
            }
            else
            {
                return SecuenciasPermitidas.DELANTE;
                
            }
        }else
        {
            return SecuenciasPermitidas.VACIO;
        }
    }

    public string CardinalidadEscritaVertical()
    {
        if (Input.GetAxis(maquina.Vertical) > 0)
        {
            return SecuenciasPermitidas.ARRIBA;
        }
        else if (Input.GetAxis(maquina.Vertical) < 0)
        {
            return SecuenciasPermitidas.ABAJO;
        }
        else
        {
            return SecuenciasPermitidas.VACIO;
        }
    }

    public void LogearComandosIngresados(KeyCode control)
    {
        botonPrecionado = control;
        LogearComandosIngresados();
    }
    public void LogearComandosIngresados()
    {
        if (deltaTimeLocalParaControl < tiempoDeTomaDeControl || botonPrecionado != KeyCode.None)
        {
            string loQueVamosLogear;

            if (maquina.PatadaDebil == botonPrecionado)
            {
                loQueVamosLogear = SecuenciasPermitidas.PATADADEBIL;
            }else if (maquina.PatadaFuerte == botonPrecionado)
            {
                loQueVamosLogear = SecuenciasPermitidas.PATADAFUERTE;
            }else if (maquina.PunioDebil == botonPrecionado)
            {
                loQueVamosLogear = SecuenciasPermitidas.PUNIODEBIL;
            }else if (maquina.PunioFuerte == botonPrecionado)
            {
                loQueVamosLogear = SecuenciasPermitidas.PUNIOFUERTE;
            }
            else
            {
                loQueVamosLogear = CardinalidadEscritaHorizontal() + CardinalidadEscritaVertical();
            }

            //comprobar si el que vamos a ingresar ya esta en la ultima posicion
            if (palancas.Count >= 1)
            {
                if (!palancas.Last().Equals(loQueVamosLogear))
                {
                    palancas.Enqueue(loQueVamosLogear);
                }
            }
            else
            {
                palancas.Enqueue(loQueVamosLogear);
            }
            //seteamos el boton para que regrese a su estado original! sellado magico!!!!!
            botonPrecionado = KeyCode.None;
            if (palancas.Count > stackMaximo)
            {
                palancas.Dequeue();
            }
            //analizaremos lo que esta guardado, contra lo que tenemos almacenado en el diccionario de secuencias

            DetectorDeConvinaciones();
            //Debug.LogWarning("");
        }
        else
        {
            //Debug.LogWarning(">>>>>"+ deltaTimeLocalParaControl);
            deltaTimeLocalParaControl = 0;
        }

    }

    private void DetectorDeConvinaciones()
    {
        foreach (KeyValuePair<string, Queue<string>> result in GetComponent<InterfazDeMetodosGenericosParaAcciones>().ListadoDeSecuencias)
        {
            int i = 0;//.ToList()[i]
            foreach (string stack in palancas)
            {
                if (stack.Equals(result.Value.ToList()[i]))
                {
                    i++;
                    //continue;
                }
                else
                {
                    break;
                }
                Debug.Log(result.Value.ToList().Count +" - "+ i+ " stack "+ stack);
                if (result.Value.ToList().Count == i)
                {
                    //instaciamos el objeto
                    //llenamos los datos que le falten (padre)
                    Debug.Log(result.Key);
                    GameObject fireBall = GetComponent<InterfazDeMetodosGenericosParaAcciones>().FireBallPrefab;
                    GetComponent<InterfazDeMetodosGenericosParaAcciones>().IsFireBall = true;
                    Instantiate(fireBall, gameObject.transform.position, fireBall.transform.rotation).GetComponent<ReferenciaAlPadre>().padre = gameObject;
                }
            }
        }
    }

    /* 
       foreach(string stack in palancas)
       {
           Debug.Log(stack);
       }
*/
    protected float GetMovimientoDelObjeto()
    {
        if (maquina.MovimientoInyectado != 0)
        {
            return maquina.MovimientoInyectado;
        }
        else
        {
            return Input.GetAxis(maquina.Horizontal);
        }
    }

    public void ResetObject()
    {
        comenzarContar = false;
        deltaTimeLocal = min;
        y = 0;
        alturamax = -1;
        GetComponent<Animator>().SetBool("tocarPiso", true);
    }

    public void AplicarFuerzaPersonaje(float fuerzaEnX)
    {
        rb.AddForce(new Vector2(fuerzaEnX, 0));
    }
}
