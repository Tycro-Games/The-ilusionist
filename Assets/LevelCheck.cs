using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCheck : MonoBehaviour
{
    public static LevelCheck level;
    public static NExtLevel[] checks;

    // Start is called before the first frame update
    void Start ()
    {
        if (level == null)
            level = this;
        else if (level != this)
            Destroy (gameObject);

        checks = FindObjectsOfType<NExtLevel> ();
    }
    public static void NextLevel ()
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
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }


}
