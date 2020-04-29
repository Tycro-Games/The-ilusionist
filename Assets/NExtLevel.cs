﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NExtLevel : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
        }
    }
}
