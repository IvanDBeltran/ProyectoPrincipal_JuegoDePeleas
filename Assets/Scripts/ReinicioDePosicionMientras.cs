using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinicioDePosicionMientras : MonoBehaviour
{
    public GameObject retorno;
    // Update is called once per frame
    void Update()
    {
        //negativo para ir a la izqueirda
        //positivo para ir a la derecha
        if((retorno.transform.position - transform.position).x < -1)
        {
            GetComponent<BaseMaquinaEstadosFinita>().MovimientoInyectado = -1;
        }else if ((retorno.transform.position - transform.position).x > 1)
        {
            GetComponent<BaseMaquinaEstadosFinita>().MovimientoInyectado = 1;
        }
        else
        {
            GetComponent<BaseMaquinaEstadosFinita>().MovimientoInyectado = 0;
        }
    }
}
