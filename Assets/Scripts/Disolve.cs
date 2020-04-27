using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disolve : MonoBehaviour
{
    [SerializeField]
    private Material disolve=null;
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
