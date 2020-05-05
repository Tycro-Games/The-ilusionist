using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    public void LoadLevel ()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }
    public void Retry ()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }
    public void Quit ()
    {
        Application.Quit ();
    }
}
