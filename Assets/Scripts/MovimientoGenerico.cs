using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoGenerico : MonoBehaviour
{
    public float speed = 2;
    public float speedJump, direccion;
    private Rigidbody2D rb;
    [SerializeField]
    private float min, max, x,y, deltaTimeLocal, alturamax;
    [SerializeField]
    private bool comenzarContar, ordencreciente, punio, patada, estaEnPiso;
    public int playerNumber = 1;
    private string player;
    private string baseNombreHorizontal = "Horizontal_Player";
    private string baseNombreVertical = "Vertical_Player";
    public float movimientoInyectado = 0;
    //Horizontal_Player1
    private void Start()
    {
        min = -1f;
        max = 1f;
        estaEnPiso = true;
        ordencreciente = true;
        rb = GetComponent<Rigidbody2D>();
        deltaTimeLocal = min;
        if(playerNumber == 1)
        {
            player = "1";
        }
        else if (playerNumber == 2)
        {
            player = "2";
        }
        baseNombreHorizontal += player;
        baseNombreVertical += player;
    }
    // Update is called once per frame
    void Update()
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
                estaEnPiso = false;
                comenzarContar = true;
            }
        }


        if (comenzarContar)
        {
            direccion = x;
            deltaTimeLocal += (Time.deltaTime*2);
            //ir modificando a una funcion matematica mas suave en su movimiento
            y = Mathf.Cos(deltaTimeLocal) * speedJump;
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

        rb.velocity = new Vector2(x * speed, y);
        //rb.velocity = new Vector2(x * speed, rb.velocity.y);
        GetComponent<Animator>().SetFloat("velocidad", Mathf.Abs(rb.velocity.x));
    }

    private float GetMovimientoDelObjeto()
    {
        if(movimientoInyectado != 0)
        {
            return movimientoInyectado;
        }
        else
        {
            return Input.GetAxis(baseNombreHorizontal);
        }
    }

    public void ResetObject()
    {
        comenzarContar = false;
        deltaTimeLocal = min;
        y = 0;
        alturamax = -1;
        estaEnPiso = true;
        GetComponent<Animator>().SetTrigger("tocarPiso");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (comenzarContar)
        {
            ResetObject();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (comenzarContar)
        {
            ResetObject();
        }
    }

}
