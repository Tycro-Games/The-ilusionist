using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDies : MonoBehaviour
{
    AudioSource play;
    private void Start ()
    {
        play = GetComponent<AudioSource> ();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Enemy"))
        {
            StartCoroutine (PlayerDie ());

        }
    }

   public IEnumerator PlayerDie ()
    {
        play.Play ();

        yield return new WaitWhile (() => play.isPlaying);
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }
}
