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
    private LayerMask Activate;

    [SerializeField]
    private AudioClip reversed;
    private AudioSource play;

    private void Start ()
    {
        play = GetComponent<AudioSource> ();
        sprite = GetComponent<SpriteRenderer> ();
    }
    private void Update ()
    {
        if (Physics.CheckSphere (transform.position, .3f, Activate))
        {
            if (active == false)
                StartCoroutine (ChangeLook ());
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

    IEnumerator ChangeLook ()
    {
        ChangeSprite ();
        active = true;
        yield return new WaitForSeconds (2);

        LevelCheck.NextLevel ();
    }
}
