using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Juegoganado : MonoBehaviour
{
    [SerializeField]
    private string enemyTag = "enemigo para terminar el juego";

    [SerializeField]
    private float tiempoAntesDeCambiar = 3f;

    [SerializeField]
    private int escenaDestino = 1;

    private GameObject[] enemigos;
    private bool nivelCompletado = false;

    void Start()
    {
        // Buscar todos los enemigos al comenzar la escena
        enemigos = GameObject.FindGameObjectsWithTag(enemyTag);

        Debug.Log("Enemigos encontrados: " + enemigos.Length);
    }

    void Update()
    {
        if (nivelCompletado)
            return;

        int enemigosActivos = 0;

        // Revisar cuáles enemigos siguen activos
        foreach (GameObject enemigo in enemigos)
        {
            if (enemigo != null && enemigo.activeInHierarchy)
            {
                enemigosActivos++;
            }
        }

        Debug.Log("Enemigos activos: " + enemigosActivos);

        // Si no queda ningún enemigo activo
        if (enemigosActivos == 0)
        {
            nivelCompletado = true;
            StartCoroutine(CambiarEscena());
        }
    }

    IEnumerator CambiarEscena()
    {
        Debug.Log("¡Todos los enemigos fueron derrotados!");

        // Esperar 3 segundos
        yield return new WaitForSeconds(tiempoAntesDeCambiar);

        // Cambiar de escena
        SceneManager.LoadScene(escenaDestino);
    }
}