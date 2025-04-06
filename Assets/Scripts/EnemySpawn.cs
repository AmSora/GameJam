using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] enemyPrefabs;
    public float tiempoEntreSpawns = 1.5f;
    public float tiempoEntreOleadas = 25f;

    private List<GameObject> enemigosVivos = new List<GameObject>();
    public int oleadaActual = 1;
    private int enemigosSpawneados = 0;
    private int enemigosMaximos = 0;
    private bool esperandoSiguienteOleada = false;

    public static EnemySpawn instance;

    private void Awake()
    {
        instance = this;
    }

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
                int index = Random.Range(0, enemyPrefabs.Length);
                GameObject selectedPrefab = enemyPrefabs[index];

                GameObject nuevoEnemigo = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);
                enemigosVivos.Add(nuevoEnemigo);
                enemigosSpawneados++;

                EnemyHealth eh = nuevoEnemigo.GetComponent<EnemyHealth>();
                if (eh != null)
                {
                    eh.Initialize(oleadaActual);
                }
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
