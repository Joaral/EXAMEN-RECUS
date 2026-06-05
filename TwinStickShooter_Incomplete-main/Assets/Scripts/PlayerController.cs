using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float speedRotation = 10f;

    public Transform target;
    public Transform player;

    public NewActions inputActions;

    private void Start()
    {
        inputActions = new NewActions();
        inputActions.Enable();
    }
    void Update()
    {
        MovePlayer();


        RotatePLayer();
    }

    public void RotatePLayer()
    {
        Vector3 lookAtDirection = target.position - player.position;
        lookAtDirection.y = 0;

        if (lookAtDirection != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(lookAtDirection);
            player.rotation = Quaternion.Lerp(
                player.rotation,
                rot,
                speedRotation * Time.fixedDeltaTime
            );
        }
    }

    public void MovePlayer()
    {
        Vector2 dir = inputActions.Player.Move.ReadValue<Vector2>();

        Vector3 move = player.forward * dir.y + player.right * dir.x;

        move.y = 0;

        transform.position += move.normalized * speed * Time.fixedDeltaTime;
    }
}
