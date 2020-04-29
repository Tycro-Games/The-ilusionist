using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDies : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Enemy"))
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
        }
    }
}
