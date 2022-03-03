using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostJuegoManager : MonoBehaviour
{
    public void VolverAJugar()
    {
        SceneManager.LoadScene("Juego");
    }

    public void Salir()
    {
        SceneManager.LoadScene("Inicio");
    }
}
