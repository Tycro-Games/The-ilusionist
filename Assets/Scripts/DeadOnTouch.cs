using System.Collections;
using UnityEngine;

public class DeadOnTouch : MonoBehaviour
{
    [SerializeField]
    private bool DetashChild = false;

    public delegate IEnumerator OnDestroy ();
    public static event OnDestroy onDestroy;

    private TeleportSound teleport;
    private void OnTriggerStay (Collider other)
    {
        if (other.tag == "Enemy")
        {
            Disolve ();
        }
    }
    public void inject (TeleportSound Teleport)
    {
        teleport = Teleport;
    }
    private void Update ()
    {
        if (Input.GetMouseButtonDown (1) && onDestroy != null)
        {
            Disolve ();
        }
    }
    public void Disolve ()
    {
        teleport.TeleportingSound (true);

        GetComponentInChildren<Disolve> ().StartCoroutine (onDestroy ());
        if (DetashChild)
            transform.GetChild (0).parent = null;
        Destroy (gameObject);
    }

}
