using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gamekit3D;

public class SceneChange : MonoBehaviour
{
    [Header("Configuración del cambio de escena")]

    [Tooltip("Tiempo que espera antes de comenzar el Fade.")]
    [SerializeField]
    private float tiempoEspera = 8f;

    [Tooltip("Duración del Fade de Game Over.")]
    [SerializeField]
    private float tiempoFade = 1f;

    [Tooltip("Índice de la escena a la que se cambiará.")]
    [SerializeField]
    private int escenaDestino = 1;

    private bool cambiandoEscena = false;

    private void Start()
    {
        StartCoroutine(CambiarEscena());
    }

    private IEnumerator CambiarEscena()
    {
        // Evitar que el cambio se ejecute más de una vez
        if (cambiandoEscena)
            yield break;

        cambiandoEscena = true;

        // -----------------------------------------
        // ESPERAR
        // -----------------------------------------

        yield return new WaitForSeconds(tiempoEspera);

        // -----------------------------------------
        // GAME OVER FADE
        // -----------------------------------------

        Debug.Log("Iniciando Game Over Fade");

        ScreenFader.Instance.fadeDuration = tiempoFade;

        yield return StartCoroutine(
            ScreenFader.FadeSceneOut(
                ScreenFader.FadeType.GameOver
            )
        );

        // -----------------------------------------
        // CAMBIAR DE ESCENA
        // -----------------------------------------

        Debug.Log(
            "Cambiando a la escena: " +
            escenaDestino
        );

        SceneManager.LoadScene(escenaDestino);
    }
}