using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bola : MonoBehaviour
{
    private float velocidad = 30.0f;
    public int golesIzquierda = 0, golesDerecha = 0;
    public int GolesDerecha
    {
        set
        {
            golesDerecha = value;
            contadorDerecha.text = golesDerecha.ToString();
        }

        get => golesDerecha;
    }

     public int GolesIzquierda
    {
        set
        {
            golesIzquierda = value;
            contadorIzquierda.text = golesIzquierda.ToString();
        }

        get => golesIzquierda;
    }

    public GameObject centro, botonesDeSalida;
    public Text contadorIzquierda, contadorDerecha;
    public AudioClip  audioRaqueta, audioGol, audioRebote, audioInicio, audioFin;

    public void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
        GameManager.juegoEnProceso = true;
        PlayClip(audioInicio);
    }

    void PlayClip(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }

    void RevisarParametrosDelJuego()
    {
        if(GameManager.modoDeJuego == Modos.Goles)
        {
            if(golesDerecha >= GameManager.totalDeGoles || golesIzquierda >= GameManager.totalDeGoles)
                TerminarJuego();
        }

        else if(GameManager.modoDeJuego == Modos.Temporizador)
        {
            if(Temporizador.tiempo >= GameManager.totalDeMinutos)
                TerminarJuego();
        }
    }

    void TerminarJuego()
    {
        PlayClip(audioFin);
        GameManager.juegoEnProceso = false;
        transform.position = Vector2.zero;

        if(golesDerecha > golesIzquierda)
            contadorDerecha.text = "Ganador";
        else
            contadorIzquierda.text = "Ganador";

        centro.SetActive(false);
        botonesDeSalida.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D micolision)
    {
        if(micolision.gameObject.name.Contains("Raqueta"))
        {
            int x = micolision.gameObject.name == "RaquetaIzquierda" ? 1 : -1;
            int y = direccionY(transform.position, micolision.transform.position);
            Vector2 direccion = new Vector2(x, y);
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
            PlayClip(audioRaqueta);
        }

        else
            PlayClip(audioRebote);
    }

    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        if (posicionBola.y > posicionRaqueta.y) return 1;
        else if (posicionBola.y < posicionRaqueta.y) return -1;
        else return 0;
    }

    void OnTriggerEnter2D(Collider2D micolision)
    {
        string direccion = micolision.gameObject.name == "Izquierda" ? "Derecha" : "Izquierda";
        ReiniciarBola(direccion);
        PlayClip(audioGol);
    }

    void ReiniciarBola(string direccion)
    {
        transform.position = Vector2.zero;

        if (direccion == "Derecha")
        {
            GolesDerecha++;
            contadorDerecha.text = golesDerecha.ToString();
            GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
        }

        else if (direccion == "Izquierda")
        {
            GolesIzquierda++;
            contadorIzquierda.text = golesIzquierda.ToString();
            GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
        }

        if(velocidad<200)
            IncrementarDificultad(10);
    }

    void IncrementarDificultad(int multiplier) => velocidad += multiplier;

    void Update()
    {
        if(GameManager.juegoEnProceso)
        {
            RevisarParametrosDelJuego();
        }
    }
}
