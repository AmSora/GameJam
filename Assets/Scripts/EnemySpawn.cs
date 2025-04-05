using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [System.Serializable]
    public class SpawnZone
    {
        public Transform spawnPoint;
        public List<GameObject> enemigos = new List<GameObject>();
    }

    public GameObject enemigoPrefab;
    public float tiempoEntreSpawns = 5f;
    public float radioSpawn = 2f; // Radio dentro del cual spawnea el enemigo

    private List<SpawnZone> zonasDeSpawn = new List<SpawnZone>();

    void Start()
    {
        // Obtener automáticamente todos los hijos como zonas de spawn
        foreach (Transform hijo in transform)
        {
            SpawnZone nuevaZona = new SpawnZone();
            nuevaZona.spawnPoint = hijo;
            zonasDeSpawn.Add(nuevaZona);
        }

        StartCoroutine(SpawnEnemigosRutina());
    }

    IEnumerator SpawnEnemigosRutina()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreSpawns);

            // Elegir la zona con menos enemigos
            SpawnZone mejorZona = null;
            int menorCantidad = int.MaxValue;

            foreach (var zona in zonasDeSpawn)
            {
                zona.enemigos.RemoveAll(e => e == null);

                if (zona.enemigos.Count < menorCantidad)
                {
                    menorCantidad = zona.enemigos.Count;
                    mejorZona = zona;
                }
            }

            if (mejorZona != null)
            {
                // Calcular posición aleatoria dentro de un círculo
                Vector2 offset = UnityEngine.Random.insideUnitCircle * radioSpawn;
                Vector3 spawnPos = mejorZona.spawnPoint.position + (Vector3)offset;

                GameObject nuevoEnemigo = Instantiate(enemigoPrefab, spawnPos, Quaternion.identity);
                mejorZona.enemigos.Add(nuevoEnemigo);
            }
        }
    }
}
