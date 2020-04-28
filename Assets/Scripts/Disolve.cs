﻿using System.Collections;
using UnityEngine;

public class Disolve : MonoBehaviour
{
    [SerializeField]
    private Material disolve = null;
    SpriteRenderer sprite;
    private void OnEnable ()
    {
        DeadOnTouch.onDestroy += FadeOut;
    }
    private void OnDisable ()
    {
        DeadOnTouch.onDestroy -= FadeOut;
    }
    private void Start ()
    {
        sprite = GetComponent<SpriteRenderer> ();
        disolve = new Material (disolve);
        sprite.material = disolve;
        StartCoroutine (FadeIn ());
    }
    private IEnumerator FadeIn ()
    {
        float fade = 0;

        disolve.SetFloat ("_Fade", fade);
        while (fade < 1)
        {
            fade += Time.deltaTime;
            disolve.SetFloat ("_Fade", fade);
            yield return null;
        }
    }
    public IEnumerator FadeOut ()
    {
        float fade = 1;

        disolve.SetFloat ("_Fade", fade);
        while (fade > 0)
        {
            fade -= Time.deltaTime;
            disolve.SetFloat ("_Fade", fade);

            yield return null;
        }
        Destroy (gameObject);
    }
}
