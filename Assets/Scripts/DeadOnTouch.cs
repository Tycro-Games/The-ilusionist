using System.Collections;
using UnityEngine;

public class DeadOnTouch : MonoBehaviour
{
    [SerializeField]
    private bool DetashChild = false;

    public delegate IEnumerator OnDestroy ();
    public static event OnDestroy onDestroy;
    private void OnTriggerStay (Collider other)
    {
        if (other.tag == "Enemy")
        {
            Disolve ();
        }
    }
    private void Update ()
    {
        if (Input.GetMouseButtonDown (1) && onDestroy != null)
        {
            Disolve ();
        }
    }
    void Disolve ()
    {
        GetComponentInChildren<Disolve> ().StartCoroutine (onDestroy ());
        if (DetashChild)
            transform.GetChild (0).parent = null;
        Destroy (gameObject);
    }

}
