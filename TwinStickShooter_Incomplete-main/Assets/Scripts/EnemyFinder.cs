using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{

    public Transform targetVisual;
    public Transform closestEnemy;
    void Update()
    {
        FindEnemy();
        MoveVisual();
    }


    void FindEnemy()
    {
        EnemyController[] enemies = FindObjectsByType<EnemyController>( FindObjectsSortMode.None);

        float closestDistance = Mathf.Infinity;

        closestEnemy = null;

        foreach (EnemyController enemy in enemies)
        {
            if (enemy.isDead) continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
    }

    void MoveVisual()
    {
        if (closestEnemy == null) return;

        Vector3 pos = closestEnemy.position;

        pos.y = 0;

        targetVisual.position = pos;
    }
}
