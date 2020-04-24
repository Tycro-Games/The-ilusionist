using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimateInRIghtDir : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    Vector2 dir;
    // Start is called before the first frame update
    void Start ()
    {
        agent = GetComponentInParent<NavMeshAgent> ();
        anim = GetComponent<Animator> ();
    }
    private void Update ()
    {
        Debug.DrawLine (transform.position, transform.position + agent.desiredVelocity.normalized);


    }
    public void DirHeaded ()
    {
         dir = agent.desiredVelocity.normalized;
        anim.SetFloat ("Dirx", dir.x);
        anim.SetFloat ("Diry", dir.y);
    }
}
