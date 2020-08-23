using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControladorDeEscenario : MonoBehaviour
{
    public ReferenciaAlPersonajePlayer uiPlayer1, uiPlayer2;
    public TextMeshProUGUI combosPlayer1, combosPlayer2;
    public GameObject player1, player2;
    // Start is called before the first frame update
    void Start()
    {
        player1 = BuscandoPJ(1);
        player2 = BuscandoPJ(2);
        uiPlayer1.AgregarPersonajeAlUI(player1);
        uiPlayer2.AgregarPersonajeAlUI(player2);
        //colocamos a cada player su target del oponente
        player1.GetComponent<ControladorDeLaOrientacionDelPJ>().otroPlayer = player2;
        player2.GetComponent<ControladorDeLaOrientacionDelPJ>().otroPlayer = player1;
        player1.GetComponent<DatosPersistentesDelPlayer>().Letrero = combosPlayer1;
        player2.GetComponent<DatosPersistentesDelPlayer>().Letrero = combosPlayer2;
    }

    public GameObject BuscandoPJ(int numberPlayer)
    {
        //con este componente
        GameObject[] listaDePlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in listaDePlayers)
        {
            if (player.GetComponent<DatosPersistentesDelPlayer>().playerNumber == numberPlayer)
            {
                return player;
            }
        }
        throw new Exception("No encontramos al player " + numberPlayer);
    }

    public GameObject BuscandoPJContrario(int numberPlayer)
    {
        //con este componente
        GameObject[] listaDePlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in listaDePlayers)
        {
            if (player.GetComponent<DatosPersistentesDelPlayer>().playerNumber != numberPlayer)
            {
                return player;
            }
        }
        throw new Exception("No encontramos al player " + numberPlayer);
    }
}
