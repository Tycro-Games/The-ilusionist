using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorSettings : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    private float Multiplier = 2.0f;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start ()
    {
        anim = GetComponent<Animator> ();

        agent = GetComponentInParent<NavMeshAgent> ();

        anim.SetFloat ("Multiplier", Multiplier);
    }

    // Update is called once per frame
    void Update ()
    {
        if (agent == null)
            return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            anim.SetBool ("ReachedDest", true);
        }
        else
            anim.SetBool ("ReachedDest", false); ;
    }
    public void DestruyThis ()
    {
        anim.SetBool ("ReachedDest", true);
        Destroy (this);
    }

}
