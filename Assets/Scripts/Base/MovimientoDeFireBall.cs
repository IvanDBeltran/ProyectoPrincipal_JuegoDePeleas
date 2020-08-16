using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoDeFireBall : MonoBehaviour
{
    InterfazDeMetodosGenericosParaAcciones quienLoLanza;
    BaseMaquinaEstadosFinita maquina;
    [SerializeField]
    private float speed;
    private void Start()
    {
        quienLoLanza = GetComponent<ReferenciaAlPadre>().padre.GetComponent<InterfazDeMetodosGenericosParaAcciones>();
        maquina = GetComponent<ReferenciaAlPadre>().padre.GetComponent<BaseMaquinaEstadosFinita>();
        speed += quienLoLanza.SpeedFireBall;
        Debug.Log(maquina.CardinalidadDeHaciaAtras());
        GetComponent<SpriteRenderer>().flipY = maquina.CardinalidadDeHaciaAtras() == 1 ? true : false;
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * GetComponent<ReferenciaAlPadre>().padre.GetComponent<BaseMaquinaEstadosFinita>().CardinalidadDeHaciaAtras() * -1, 0);
    }
}
