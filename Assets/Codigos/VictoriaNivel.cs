using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoriaNivel : MonoBehaviour
{
    [Header("Configuración")]
    public string tagEnemigo = "enemigo para terminar juego";

    public float tiempoAntesDeCambiar = 3f;

    public int escenaDestino = 1;

    private int enemigosTotales;
    private int enemigosDerrotados;

    private bool nivelCompletado = false;

    void Start()
    {
        // Contar todos los enemigos que existen al comenzar el nivel
        enemigosTotales = GameObject.FindGameObjectsWithTag(tagEnemigo).Length;

        enemigosDerrotados = 0;

        Debug.Log("Enemigos encontrados: " + enemigosTotales);

        // Si no hay enemigos, NO termina automáticamente.
        if (enemigosTotales == 0)
        {
            Debug.LogWarning("No se encontraron enemigos con la tag: " + tagEnemigo);
        }
    }

    public void EnemigoDerrotado()
    {
        // Evitar que se ejecute después de completar el nivel
        if (nivelCompletado)
            return;

        enemigosDerrotados++;

        Debug.Log(
            "Enemigo derrotado: " +
            enemigosDerrotados +
            " / " +
            enemigosTotales
        );

        // SOLO cambiar cuando TODOS los enemigos hayan muerto
        if (enemigosTotales > 0 &&
            enemigosDerrotados >= enemigosTotales)
        {
            nivelCompletado = true;

            StartCoroutine(CambiarEscena());
        }
    }

    private IEnumerator CambiarEscena()
    {
        Debug.Log("TODOS LOS ENEMIGOS HAN SIDO DERROTADOS");

        // Esperar 3 segundos
        yield return new WaitForSeconds(tiempoAntesDeCambiar);

        // Cambiar de escena
        SceneManager.LoadScene(escenaDestino);
    }
}