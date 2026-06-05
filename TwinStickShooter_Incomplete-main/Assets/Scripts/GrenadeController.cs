using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeController : MonoBehaviour
{
    Rigidbody rb;

    //public LayerMask mask;

    public float launchForce = 15f;
    public float timer = 3f;

    public float radius = 5f;
    public float explosionForce = 500f;

    public GameObject particles;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);


        StartCoroutine(Explosion());
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(timer);

        Instantiate(particles, transform.position, Quaternion.identity);

        Collider[] hitColliders =
            Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hitColliders)
        {
            Rigidbody hitRb = hit.GetComponent<Rigidbody>();

            if (hitRb != null)
            {
                hitRb.AddExplosionForce(explosionForce, transform.position, radius);
            }

            EnemyController enemy =
                hit.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.Kill();
            }
        }

        Destroy(gameObject);
    }
}
