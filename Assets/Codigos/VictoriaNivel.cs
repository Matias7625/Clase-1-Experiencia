using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gamekit3D;

public class VictoriaNivel : MonoBehaviour
{
    [Header("Configuración de enemigos")]

    [Tooltip("Tag de los enemigos que deben ser derrotados.")]
    [SerializeField]
    private string tagEnemigo = "enemigo para terminar el juego";


    [Header("Configuración de Victoria")]

    [Tooltip("Tiempo que espera después de derrotar al último enemigo.")]
    [SerializeField]
    private float tiempoAntesDelFade = 3f;

    [Tooltip("Duración del Fade cuando se gana.")]
    [SerializeField]
    private float tiempoFadeVictoria = 1f;

    [Tooltip("Escena a la que se va cuando todos los enemigos son derrotados.")]
    [SerializeField]
    private int escenaVictoria = 1;


    [Header("Configuración de Tiempo Límite")]

    [Tooltip("Tiempo máximo para derrotar a todos los enemigos.")]
    [SerializeField]
    private float tiempoLimite = 120f;

    [Tooltip("Duración del Fade cuando se acaba el tiempo.")]
    [SerializeField]
    private float tiempoFadeDerrota = 1f;

    [Tooltip("Escena a la que se va cuando se acaba el tiempo.")]
    [SerializeField]
    private int escenaDerrota = 2;


    // ---------------------------------------------------------
    // VARIABLES INTERNAS
    // ---------------------------------------------------------

    private int enemigosTotales;
    private int enemigosDerrotados;

    private bool nivelCompletado = false;

    private float tiempoTranscurrido;


    // ---------------------------------------------------------
    // START
    // ---------------------------------------------------------

    private void Start()
    {
        // Buscar todos los enemigos al comenzar el nivel
        GameObject[] enemigos =
            GameObject.FindGameObjectsWithTag(tagEnemigo);

        enemigosTotales = enemigos.Length;

        enemigosDerrotados = 0;

        tiempoTranscurrido = 0f;

        Debug.Log(
            "Enemigos encontrados: " +
            enemigosTotales
        );

        if (enemigosTotales == 0)
        {
            Debug.LogWarning(
                "No se encontraron enemigos con la tag: " +
                tagEnemigo
            );
        }
    }


    // ---------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------

    private void Update()
    {
        // Si el nivel ya terminó, no hacer nada
        if (nivelCompletado)
            return;

        // Contar el tiempo
        tiempoTranscurrido += Time.deltaTime;

        // Comprobar si se acabó el tiempo
        if (tiempoTranscurrido >= tiempoLimite)
        {
            nivelCompletado = true;

            Debug.Log("¡SE ACABÓ EL TIEMPO!");

            StartCoroutine(TiempoAgotado());
        }
    }


    // ---------------------------------------------------------
    // ENEMIGO DERROTADO
    // ---------------------------------------------------------

    public void EnemigoDerrotado()
    {
        // Si el nivel ya terminó, ignorar
        if (nivelCompletado)
            return;

        enemigosDerrotados++;

        Debug.Log(
            "Enemigo derrotado: " +
            enemigosDerrotados +
            " / " +
            enemigosTotales
        );

        // Comprobar si todos los enemigos fueron derrotados
        if (
            enemigosTotales > 0 &&
            enemigosDerrotados >= enemigosTotales
        )
        {
            nivelCompletado = true;

            Debug.Log(
                "¡TODOS LOS ENEMIGOS HAN SIDO DERROTADOS!"
            );

            StartCoroutine(Victoria());
        }
    }


    // ---------------------------------------------------------
    // VICTORIA
    // ---------------------------------------------------------

    private IEnumerator Victoria()
    {
        // Esperar después de derrotar al último enemigo
        yield return new WaitForSeconds(
            tiempoAntesDelFade
        );

        Debug.Log("Iniciando Fade de Victoria");

        // Configurar duración del Fade
        ScreenFader.Instance.fadeDuration =
            tiempoFadeVictoria;

        // Hacer Fade de Game Over
        yield return StartCoroutine(
            ScreenFader.FadeSceneOut(
                ScreenFader.FadeType.GameOver
            )
        );

        Debug.Log(
            "Cambiando a escena de victoria: " +
            escenaVictoria
        );

        // Cambiar de escena
        SceneManager.LoadScene(
            escenaVictoria
        );
    }


    // ---------------------------------------------------------
    // TIEMPO AGOTADO
    // ---------------------------------------------------------

    private IEnumerator TiempoAgotado()
    {
        Debug.Log(
            "Tiempo agotado. Cambiando a escena de derrota."
        );

        // Fade
        ScreenFader.Instance.fadeDuration =
            tiempoFadeDerrota;

        yield return StartCoroutine(
            ScreenFader.FadeSceneOut(
                ScreenFader.FadeType.GameOver
            )
        );

        Debug.Log(
            "Cambiando a escena de derrota: " +
            escenaDerrota
        );

        // Cambiar de escena
        SceneManager.LoadScene(
            escenaDerrota
        );
    }
}