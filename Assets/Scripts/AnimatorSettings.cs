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

    AudioPlay play;
    // Start is called before the first frame update
    void Start ()
    {
        play = GetComponent<AudioPlay> ();

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
        {
            if (!play.source.isPlaying)
                play.PlayASound ();
            anim.SetBool ("ReachedDest", false);
        }

    }
    public void DestruyThis ()
    {
        anim.SetBool ("ReachedDest", true);
        Destroy (this);
    }

}
