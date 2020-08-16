using UnityEngine;
using System.Collections;

public static class SecuenciasPermitidas
{
    //Lo unico que debe estar aqui, son los botones permitidos para las secuencias
    private static string atras = "atras";
    private static string diagonalAtras = "atrasabajo";
    private static string delante = "delante";
    private static string diagonalDelante = "delanteabajo";
    private static string abajo = "abajo";
    private static string arriba = "arriba";
    private static string punioFuerte = "punioFuerte";
    private static string punioDebil = "punioDebil";
    private static string patadaFuerte = "patadaFuerte";
    private static string patadaDebil = "patadaDebil";
    private static string vacio = string.Empty;
    //Todos los poderes
    private static string fireball = "fireball!!!!!!";

    public static string ATRAS { get => atras; }
    public static string DIAGONALATRAS { get => diagonalAtras; }
    public static string DELANTE { get => delante; }
    public static string DIAGONALDELANTE { get => diagonalDelante; }
    public static string ABAJO { get => abajo; }
    public static string ARRIBA { get => arriba; }
    public static string PUNIOFUERTE { get => punioFuerte; }
    public static string PUNIODEBIL{ get => punioDebil; }
    public static string PATADAFUERTE { get => patadaFuerte; }
    public static string PATADADEBIL { get => patadaDebil; }
    public static string VACIO { get => vacio; }
    public static string FIREBALL { get => fireball; }

}
