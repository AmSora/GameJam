using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject enemigoPrefab;
    public float tiempoEntreSpawns = 1.5f;
    public float tiempoEntreOleadas = 25f;

    private List<GameObject> enemigosVivos = new List<GameObject>();
    private int oleadaActual = 1;
    private int enemigosSpawneados = 0;
    private int enemigosMaximos = 0;
    private bool esperandoSiguienteOleada = false;

    void Start()
    {
        IniciarNuevaOleada();
        StartCoroutine(SpawnEnemigosRutina());
    }

    void IniciarNuevaOleada()
    {
        Debug.Log($"Iniciando Oleada {oleadaActual}");
        enemigosMaximos = 20 + (oleadaActual * 3);
        enemigosSpawneados = 0;
        enemigosVivos.Clear();
        esperandoSiguienteOleada = false;
    }

    IEnumerator SpawnEnemigosRutina()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreSpawns);

            if (esperandoSiguienteOleada) continue;

            enemigosVivos.RemoveAll(e => e == null);

            if (enemigosSpawneados < enemigosMaximos)
            {
                GameObject nuevoEnemigo = Instantiate(enemigoPrefab, spawnPoint.position, Quaternion.identity);
                enemigosVivos.Add(nuevoEnemigo);
                enemigosSpawneados++;
            }
            else if (enemigosVivos.Count == 0)
            {
                esperandoSiguienteOleada = true;
                StartCoroutine(EsperarYSiguienteOleada());
            }
        }
    }

    IEnumerator EsperarYSiguienteOleada()
    {
        Debug.Log("Todos los enemigos eliminados. Próxima oleada en " + tiempoEntreOleadas + " segundos...");
        yield return new WaitForSeconds(tiempoEntreOleadas);
        oleadaActual++;
        IniciarNuevaOleada();
    }
}
