using System;

public interface InterfazDeMetodosGenericosParaAcciones
{
    float GetFuerza();
    float GetFuerzaPatada();
    float GetFuerzaPunio();
    float GetVida();
    Boolean EstaElPunioActivo();
    Boolean EstaLaPatadaActivo();

    void QuitarVida(float vidaRestar);
}