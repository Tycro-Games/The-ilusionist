using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NExtLevel : MonoBehaviour
{
    SpriteRenderer sprite;

    [SerializeField]
    Sprite Change;
    public bool active = false;

    [SerializeField]
    private bool changeAble = false;

    [SerializeField]
    private float TimeToWait = 2f;

    [SerializeField]
    private LayerMask Activate;

    [SerializeField]
    private AudioClip reversed;
    private AudioSource play;


    private void Start ()
    {
        play = GetComponent<AudioSource> ();
        sprite = GetComponent<SpriteRenderer> ();
        if (!changeAble)
        {
            AudioClip Switc = play.clip;
            play.clip = reversed;
            reversed = play.clip;
        }
    }
    private void Update ()
    {
        if (Physics.CheckSphere (transform.position, .3f, Activate))
        {
            if (active == false)
                ChangeLook ();
        }
        else if (changeAble && active)
        {
            active = false;
            ChangeSprite ();
        }


    }
    void ChangeSprite ()
    {
        if (changeAble)
        {
            AudioClip Switc = play.clip;
            play.clip = reversed;
            reversed = play.clip;
        }

        play.Play ();



        Sprite sp = sprite.sprite;
        sprite.sprite = Change;
        Change = sp;
    }

    void ChangeLook ()
    {
        ChangeSprite ();
        active = true;

        StartCoroutine (LevelCheck.NextLevel (TimeToWait));
    }
}
