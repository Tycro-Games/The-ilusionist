using System.Collections;
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
    [SerializeField]
    private float maxIn;
    [SerializeField]
    private float minOut;
    private void Start ()
    {
        light = GetComponentInParent<Light2D> ();
        sprite = GetComponent<SpriteRenderer> ();
        disolve = new Material (disolve);
        sprite.material = disolve;
        disolve.SetFloat ("_Fade", maxIn);
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
        float fade = minOut;

        disolve.SetFloat ("_Fade", fade);
        while (fade < maxIn)
        {
            fade += Time.deltaTime*multiplier;
           
            disolve.SetFloat ("_Fade", fade);
            yield return null;
        }
    }
    public IEnumerator FadeOut ()
    {
        float fade = maxIn;

        disolve.SetFloat ("_Fade", fade);
        while (fade > minOut)
        {
            fade -= Time.deltaTime * multiplier;
            
            disolve.SetFloat ("_Fade", fade);
            yield return null;
        }
       
    }
}
