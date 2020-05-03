using System.Collections;
using UnityEngine;

public class Disolve : MonoBehaviour
{
    [SerializeField]
    private Material disolve = null;
    SpriteRenderer sprite;
    AnimatorSettings animSet;
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
        animSet = GetComponent<AnimatorSettings> ();

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
        if (animSet == null)
            yield break;
        float fade = 1;

        disolve.SetFloat ("_Fade", fade);
        animSet.DestruyThis ();
        while (fade > 0)
        {
            fade -= Time.deltaTime;
            disolve.SetFloat ("_Fade", fade);
            
            yield return null;
        }
        Destroy (gameObject);
    }
}
