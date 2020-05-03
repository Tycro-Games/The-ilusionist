using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioPlay play;

    void Start ()
    {
        play = GetComponent<AudioPlay> ();

        DontDestroyOnLoad (gameObject);
    }

    void Update ()
    {
        if (!play.source.isPlaying)
            play.PlayASound ();
    }
}
