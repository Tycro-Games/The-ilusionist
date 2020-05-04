using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource play;

    [SerializeField]
    private AudioClip[] clips;

    private int index = 0;

    void Start ()
    {
        play = GetComponent<AudioSource> ();

        DontDestroyOnLoad (gameObject);

        play.Play ();
    }
    private void Update ()
    {
        if (!play.isPlaying)
        {        

            play.clip = clips[index];
            index = (index + 1) % clips.Length;
            play.Play ();
        }
    }
}
