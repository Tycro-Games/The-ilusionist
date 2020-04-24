using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class IllusionSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Illusion = null;

    private NavMeshAgent agent = null;
    private GameObject currentInstance = null;
    [Header ("Raycast")]
    [SerializeField]
    private float maxDist = 100.0f;
    [SerializeField]
    private LayerMask Ground = new LayerMask ();

    private bool isUsingIlusion = false;

    public void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Tab))
        {
            isUsingIlusion = !isUsingIlusion;
        }
    }
    public void Spawn (InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton () && isUsingIlusion)
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray, out RaycastHit hit, maxDist, Ground))
                if (hit.transform.tag == "Ground")
                {
                    currentInstance = Instantiate (Illusion, transform.position, transform.rotation);
                    agent = currentInstance.GetComponent<NavMeshAgent> ();


                    agent.SetDestination ((Vector2)hit.point);
                    isUsingIlusion = false;
                }
            
        }
    }
}
