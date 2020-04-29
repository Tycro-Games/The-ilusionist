﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DissolveThoughWalls : MonoBehaviour
{
    [SerializeField]
    private Material disolve = null;
    SpriteRenderer sprite;

    [SerializeField]
    private float multiplier = 1.0f;

    private Light2D light;
    private void Start ()
    {
        light = GetComponentInParent<Light2D> ();
        sprite = GetComponent<SpriteRenderer> ();
        disolve = new Material (disolve);
        sprite.material = disolve;
        disolve.SetFloat ("_Fade", 1);
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Enviroment")
        {
            light.enabled = false;
            StartCoroutine (FadeOut ());
        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.tag == "Enviroment")
        {
            light.enabled = true;
            StartCoroutine (FadeIn ());
        }
    }

    private IEnumerator FadeIn ()
    {
        float fade = 0;

        disolve.SetFloat ("_Fade", fade);
        while (fade < 1)
        {
            fade += Time.deltaTime*multiplier;
           
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
            fade -= Time.deltaTime * multiplier;
            
            disolve.SetFloat ("_Fade", fade);
            yield return null;
        }
       
    }
}
