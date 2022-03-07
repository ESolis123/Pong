using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Modos modoDeJuego;
    public static int totalDeGoles = 5, totalDeMinutos = 180;
    public static bool juegoEnProceso = false;
}

public enum Modos
{
    Temporizador,
    Goles
}
