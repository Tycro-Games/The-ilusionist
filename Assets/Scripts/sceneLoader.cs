using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    public void LoadLevel ()
    {
        SceneManager.LoadScene (0);
    }
    public void Quit ()
    {
        Application.Quit ();
    }
}
