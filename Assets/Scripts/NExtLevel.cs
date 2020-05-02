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
    private void Start ()
    {
        sprite = GetComponent<SpriteRenderer> ();
    }
    private void Update ()
    {
        if (Physics.CheckSphere (transform.position, .3f, Activate) )
        {
            if (active == false)
            StartCoroutine (ChangeLook ());
        }
        else if (changeAble && active)
        {
            active = false;
            ChangeSprite ();
            Debug.Log ("Switch off");
        }
       

    }
    void ChangeSprite ()
    {
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
