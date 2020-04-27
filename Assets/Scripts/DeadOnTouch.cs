using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadOnTouch : MonoBehaviour
{
    private void OnTriggerStay (Collider other)
    {
        if (other.tag == "Enemy")
            Destroy (gameObject);
    }
}
