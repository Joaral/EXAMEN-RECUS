using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float speedRotation;

    public float stoppingDistance;

    public Transform player;

    public GameManager gameManager;

    public Rigidbody rb;
    public GameObject bones;

    public bool isDead;

    public Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player").transform;

    }
    private void Update()
    {
        if (isDead) return;

        MoveToPlayer();
    }

    public void MoveToPlayer()
    {
        if (player == null)
        {
            return;
        }

        Vector3 direction = (player.position - rb.position).normalized;

        //Debug.Log(direction);

        
        if (direction != Vector3.zero)
        {
            Quaternion rot =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, speedRotation * Time.deltaTime);
        }


        transform.position += direction * speed * Time.deltaTime;

    }

    public void Kill()
    {
        if(isDead)
        {
            return;
        }

        isDead = true;


        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;

        animator.enabled = false;

        CapsuleCollider col = GetComponent<CapsuleCollider>();
        col.enabled = false;

        Rigidbody[] ragdollBodies =
            bones.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody body in ragdollBodies)
        {
            body.isKinematic = false;
        }

        gameManager.EnemyKilled();

        enabled = false;
    }
}
