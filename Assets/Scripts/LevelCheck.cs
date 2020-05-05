using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCheck : MonoBehaviour
{
    public static LevelCheck level;
    public static NExtLevel[] checks;

    private static AudioSource play;

    // Start is called before the first frame update
    void Start ()
    {
        play = GetComponent<AudioSource> ();
        if (level == null)
            level = this;
        else if (level != this)
            Destroy (gameObject);

        checks = FindObjectsOfType<NExtLevel> ();
    }
    public static IEnumerator NextLevel (float waitTime)
    {
        bool allChecks = true;
        for (int i = 0; i < checks.Length; i++)
        {
            if (!checks[i].active)
            {
                allChecks = false;
                break;
            }
        }

        if (allChecks)
        {
            play.Play ();
            yield return new WaitForSeconds (waitTime);
            int index = SceneManager.GetActiveScene ().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings > index)
                SceneManager.LoadScene (index);
            else
            {
                //endGame
                Application.Quit ();
            }
        }
    }


}
