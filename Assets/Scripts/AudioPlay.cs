using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (AudioSource))]
public class AudioPlay : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] walkSound;
   
    [HideInInspector]
    public AudioSource source = null;


    private void Start ()
    {
        source = GetComponent<AudioSource> ();
    }
    public void PlayASound ()
    {

        source.clip = walkSound[Random.Range (0, walkSound.Length)];

        source.Play ();
    }
    
}
