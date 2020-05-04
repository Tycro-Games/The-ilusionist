using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private UnityEvent pause;

    public static bool isPause = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape)&&!isPause)
        {
            isPause = true;
            pause.Invoke ();
            StopTime ();           
        }
        
    }
    public void StopTime ()
    {
        
        Time.timeScale = 0;
    }
    public void StartTime ()
    {
        isPause = false;
        Time.timeScale = 1;
    }
}
