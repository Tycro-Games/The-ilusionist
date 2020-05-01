using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NExtLevel : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField]
    Sprite Change;
    private bool active = false;
    private void Start ()
    {
        sprite = GetComponent<SpriteRenderer> ();
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            StartCoroutine (ChangeLook ());
        }
    }
    IEnumerator ChangeLook ()
    {
        sprite.sprite = Change;
        yield return new WaitForSeconds (2);
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }
}
