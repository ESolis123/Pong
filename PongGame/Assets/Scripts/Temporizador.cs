using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    public static float tiempo;
    int minutos, segundos;
    Text texto;

    void Start()
    {
        tiempo = 0;

        if(GameManager.modoDeJuego != Modos.Temporizador)
            gameObject.SetActive(false);

        texto = GetComponent<Text>();
    }

    void MostrarTiempo()
    {
        tiempo += Time.deltaTime;
        minutos = (int) tiempo/60;
        segundos = (int) tiempo%60;
        texto.text = $"{minutos.ToString("00")}:{segundos.ToString("00")}";
    }

    void Update()
    {
        if(GameManager.juegoEnProceso)
            MostrarTiempo();
    }
}
