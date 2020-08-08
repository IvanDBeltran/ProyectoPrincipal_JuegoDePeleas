using UnityEngine;
using System.Collections;

public class EstadisticasBase : MonoBehaviour
{
    private float vida = 100;
    private float fuerza = 10;
    private float patada, punio;

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
}
