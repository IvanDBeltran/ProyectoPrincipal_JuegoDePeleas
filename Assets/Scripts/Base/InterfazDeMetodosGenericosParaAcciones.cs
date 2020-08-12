using System;

public interface InterfazDeMetodosGenericosParaAcciones
{
    float Fuerza { set; get; }
    float Patada { set; get; }
    float Punio { set; get; }
    float Vida { set; get; }


    float Speed { set; get; }
    float SpeedJump { set; get; }

    bool IsPunioActivo { get; set; }
    bool IsPatadaActivo { get; set; }

    float TiempoVulnerable { get; set; }

    void Awake();
    void QuitarVida(float vidaRestar);
}