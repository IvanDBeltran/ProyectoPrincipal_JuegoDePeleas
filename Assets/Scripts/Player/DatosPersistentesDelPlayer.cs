using UnityEngine;
using System.Collections;
using TMPro;

public class DatosPersistentesDelPlayer : MonoBehaviour
{
    [Range(1, 2)]
    public int playerNumber;
    [SerializeField]
    public bool FueGolpeado;
    //Propiedades para los combos
    [SerializeField]
    private TextMeshProUGUI letrero;
    [SerializeField]
    private int cantidadDeGolpes;

    public TextMeshProUGUI Letrero { get => letrero; set => letrero = value; }

    public int CantidadDeGolpes { get => cantidadDeGolpes; set => cantidadDeGolpes = value; }

    public void AumentarCantidadDeGolpes()
    {
        cantidadDeGolpes++;
    }

    public void MostrarLetreroDeCombos()
    {
        letrero.gameObject.SetActive(true);
        letrero.text = string.Format("Combo {0}", cantidadDeGolpes);
    }
    public void OcultarLetreroDeCombos()
    {
        letrero.gameObject.SetActive(false);
    }
}
