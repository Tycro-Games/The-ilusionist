using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private float speedMovement = 6.0f;
    Rigidbody rb;

    //Input
    private Vector2 movement;
    private Vector2 move;
    void Start ()
    {
        rb = GetComponent<Rigidbody> ();
    }
    private void FixedUpdate ()
    {
        Move ();
        Rotate ();
    }
    private void Update ()
    {
        StaticInfo.GetPlayerPos (transform.position);
    }
    public void SetMovement (InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2> ();
    }
    private void Rotate ()
    {
        if (movement != Vector2.zero)
        {
            Quaternion newRot = Quaternion.LookRotation (transform.forward, movement);
            transform.rotation = newRot;
        }
    }
    private void Move ()
    {
        move = movement;
        move *= Time.fixedDeltaTime * speedMovement;

        rb.MovePosition ((Vector2)rb.position + move);

    }



}
