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

    private Light2D lighting = null;
    [SerializeField]
    private float maxIn = 1.0f;
    [SerializeField]
    private float minOut = 0.0f;
    private void Start ()
    {
        lighting = GetComponentInParent<Light2D> ();
        sprite = GetComponent<SpriteRenderer> ();
        disolve = new Material (disolve);
        sprite.material = disolve;
        disolve.SetFloat ("_Fade", maxIn);
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Enviroment")
        {
            lighting.enabled = false;
            StartCoroutine (FadeOut ());
        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.tag == "Enviroment")
        {
            lighting.enabled = true;
            StartCoroutine (FadeIn ());
        }
    }

    private IEnumerator FadeIn ()
    {
        float fade = minOut;

        disolve.SetFloat ("_Fade", fade);
        while (fade < maxIn)
        {
            fade += Time.deltaTime * multiplier;

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
