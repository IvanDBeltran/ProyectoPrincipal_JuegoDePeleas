using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface InterfazDeMetodosGenericosParaAcciones
{
    float Fuerza { set; get; }
    float Patada { set; get; }
    float Punio { set; get; }
    float Vida { set; get; }


    float Speed { set; get; }
    float SpeedJump { set; get; }
    float SpeedFireBall { set; get; }

    bool IsPunioActivo { get; set; }
    bool IsPatadaActivo { get; set; }
    bool IsFireBall { get; set; }

    float TiempoRecuperacion { get; set; }

    float TiempoGolpeado { get; set; }

    bool EstaEnElPiso { get; set; }

    void Awake();
    void QuitarVida(float vidaRestar);

    //Fuerzas por golpe, para el desplazamiento
    float FuerzaGolpeDebil { get; set; }
    float FuerzaGolpeFuerte { get; set; }
    GameObject FireBallPrefab { get; }

    Dictionary<string, Queue<string>> ListadoDeSecuencias { get; }
}