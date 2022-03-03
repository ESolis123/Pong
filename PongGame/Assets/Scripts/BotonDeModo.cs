using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonDeModo : MonoBehaviour
{
    public Modos modo;

    public void SeleccionarModo()
    {
        GameManager.modoDeJuego = modo;
        SceneManager.LoadScene("Juego");
    }
}
