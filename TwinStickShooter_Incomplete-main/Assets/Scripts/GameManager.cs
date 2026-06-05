using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform[] spawnLocations;
    public GameObject enemyPrefab;




    public int enemiesPerRound = 10;
    public float spawnDelay = 0.2f;

    private int enemiesLeft;

    void Start()
    {
        StartCoroutine(SpawnEnemys());
    }

    IEnumerator SpawnEnemys()
    {

        for (int i = 0; i < enemiesPerRound; i++)
        {
            int pos = Random.Range(0, spawnLocations.Length);

            GameObject enemy = Instantiate(
                enemyPrefab,
                spawnLocations[pos].position,
                spawnLocations[pos].rotation
            );

            EnemyController controller = enemy.GetComponent<EnemyController>();

            controller.gameManager = this;
            enemiesLeft++;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void EnemyKilled()
    {
        enemiesLeft--;
        Debug.Log(enemiesLeft);
        if (enemiesLeft <= 0)
        {
            StartCoroutine(SpawnEnemys());
        }
    }
}
