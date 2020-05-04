using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip teleportSound;
    [SerializeField]
    private AudioClip teleportSoundReversed;
    [HideInInspector]
    public AudioSource source = null;
    private void Start ()
    {
        source = GetComponent<AudioSource> ();
    }
    public void TeleportingSound (bool reversed)
    {
        if (!reversed)
            source.clip = teleportSound;
        else
            source.clip = teleportSoundReversed;

        source.Play ();
    }
}
