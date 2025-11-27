using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class MapController : MonoBehaviour
{
    // Prefab del terra que genero constantment
    [SerializeField]
    GameObject groundPrefab;

    // Separació entre cada tros de terra
    [SerializeField]
    float espaiMapa = 24.15f;

    bool estatGenerat = false;

    // Prefabs dels objectes del joc
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject coinPrefab;

    GameObject nouMapa;
    GameObject mapaAnterior;

    // Rang horitzontal i vertical per generar objectes
    public float spawnRangeX = 10f;
    public float spawnRangeY = 1.5f;

    // Zona jugable per enemics i monedes
    public float minY = -0.2f;
    public float maxY = 1.8f;

    // Zones correctes per obstacles
    public float obstacleLowerMin = -1.4f;
    public float obstacleLowerMax = -0.7f;
    public float obstacleUpperMin = 2.2f;
    public float obstacleUpperMax = 3.0f;


    // Quan el jugador entra al trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // PRIMER cop que activo el trigger
            if (!estatGenerat)
            {
                Debug.Log("TRIGGER ACTIVAT!"); // COmprovo per consola que passo el trigger

                // Creo un nou tros de terra davant del jugador
                nouMapa = Instantiate(
                    groundPrefab,
                    new Vector3(collision.transform.position.x + espaiMapa, groundPrefab.transform.position.y, 0),
                    Quaternion.identity
                );

                // Guardo l'últim mapa per poder-lo destruir després
                mapaAnterior = nouMapa;

                // Genero enemics
                for (int i = 0; i < 5; i++)
                {
                    Vector3 pos = GetRandomEnemyCoinPos(collision.transform.position);
                    SafeInstantiate(enemyPrefab, pos, 0.5f);
                }

                // Genero monedes
                for (int i = 0; i < 5; i++)
                {
                    Vector3 pos = GetRandomEnemyCoinPos(collision.transform.position);
                    SafeInstantiate(coinPrefab, pos, 0.5f);
                }

                // Genero obstacles
                for (int i = 0; i < 5; i++)
                {
                    Vector3 pos = GetRandomObstaclePos(collision.transform.position);
                    SafeInstantiate(obstaclePrefab, pos, 1f);
                }

                estatGenerat = true;
            }
            else
            {
                // Destrueixo el mapa anterior quan passo pel següent trigger
                Destroy(mapaAnterior);
                estatGenerat = false;
            }
        }
    }

    // Comprovo si una posició està lliure
    bool IsPositionClear(Vector3 position, float minDistance)
    {
        return Physics2D.OverlapCircleAll(position, minDistance).Length == 0;
    }

    // Posició aleatòria per enemics i monedes
    Vector3 GetRandomEnemyCoinPos(Vector3 playerPosition)
    {
        float spawnX = playerPosition.x + espaiMapa + Random.Range(-spawnRangeX, spawnRangeX);
        float spawnY = Random.Range(minY, maxY);
        return new Vector3(spawnX, spawnY, 0);
    }

    // Posició aleatòria per obstacles (dalt o baix)
    Vector3 GetRandomObstaclePos(Vector3 playerPosition)
    {
        float spawnX = playerPosition.x + espaiMapa + Random.Range(-spawnRangeX, spawnRangeX);
        float spawnY = Random.value > 0.5f
            ? Random.Range(obstacleUpperMin, obstacleUpperMax)
            : Random.Range(obstacleLowerMin, obstacleLowerMax);

        return new Vector3(spawnX, spawnY, 0);
    }

    // Instancio objectes amb comprovació de col·lisions
    void SafeInstantiate(GameObject prefab, Vector3 startPos, float minDistance, int maxTries = 10)
    {
        Vector3 tryPos = startPos;

        for (int i = 0; i < maxTries; i++)
        {
            if (IsPositionClear(tryPos, minDistance))
            {
                Instantiate(prefab, tryPos, Quaternion.identity);
                return;
            }

            // Ajusto lleugerament la Y si està ocupat
            tryPos += new Vector3(0, Random.Range(-0.7f, 0.7f), 0);
        }

        // Si no trobo lloc, el poso igualment
        Instantiate(prefab, tryPos, Quaternion.identity);
    }
}
