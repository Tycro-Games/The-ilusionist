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

    Animator anim;


    //Input
    private Vector2 movement;
    private Vector2 move;
    void Start ()
    {
        anim = GetComponentInChildren<Animator> ();

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
          
        if(!Input.GetKey(KeyCode.A)&& !Input.GetKey (KeyCode.W)&& !Input.GetKey (KeyCode.S)&& !Input.GetKey (KeyCode.D))
            anim.SetBool ("Walk", false);
        if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D))
            anim.SetBool ("Walk", false);
        if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.S))
            anim.SetBool ("Walk", false);
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
     
        if (move != Vector2.zero)
        {
            anim.SetBool ("Walk", true);
            move *= Time.fixedDeltaTime * speedMovement;
            
            rb.MovePosition ((Vector2)rb.position + move);
        }
        
        
        
    }



}
