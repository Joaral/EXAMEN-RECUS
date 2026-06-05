using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : MonoBehaviour
{
    public LineRenderer line;
    public float lineFadeSpeed = 5f;

    public LayerMask objectMask;
    public LayerMask enemyMask;

    public float knockbackForce = 10;
    public float shootDistance = 500f;
    void Update()
    {
        FadeLine();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FadeLine()
    {
        Color start = line.startColor;
        Color end = line.endColor;

        start.a -= Time.deltaTime * lineFadeSpeed;
        end.a -= Time.deltaTime * lineFadeSpeed;

        line.startColor = start;
        line.endColor = end;
    }

    void Shoot()
    {
        line.startColor = new Color(1, 1, 1, 1);
        line.endColor = new Color(1, 1, 1, 1);

        Vector3 startPos = transform.position;
        Vector3 direction = transform.forward;

        line.SetPosition(0, startPos);
        line.SetPosition(1, startPos + direction * shootDistance);

        RaycastHit hit;


        if (Physics.Raycast(startPos, direction, out hit, shootDistance, enemyMask))
        {
            EnemyController enemy = hit.collider.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.Kill();
            }
        }


        if (Physics.Raycast(startPos, direction, out hit, shootDistance, objectMask))
        {
            Rigidbody rb = hit.rigidbody;

            if (rb != null)
            {
                rb.AddForceAtPosition(direction * knockbackForce, hit.point, ForceMode.Impulse);
            }
        }
    }
}
