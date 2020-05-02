using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IllusionSpawner : MonoBehaviour
{
    [SerializeField]
    private float normalSpeed = 2.0f;

    private NavMeshAgent agent = null;
    private GameObject currentInstance = null;

    [Header ("Illusion")]
    [SerializeField]
    private GameObject Illusion = null;
    [SerializeField]
    private float firerate;

    [Header ("Raycast")]
    [SerializeField]
    private float maxDist = 100.0f;
    [SerializeField]
    private LayerMask Ground = new LayerMask ();

    private Animator anim;
    private void Start ()
    {
        anim = GetComponentInChildren<Animator> ();
    }

    public void Update ()
    {

        if (currentInstance == null)
        {
            anim.SetBool ("AbilityReady", true);
        }
        else
        {
            anim.SetBool ("AbilityReady", false);
        }
        Queue ();
    }
    public void Spawn (InputAction.CallbackContext ctx)
    {
        if (SceneManager.GetActiveScene ().buildIndex == 0)
            return;



        if (ctx.ReadValueAsButton () && currentInstance == null)
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray, out RaycastHit hit, maxDist, Ground))
                if (hit.transform.tag == "Ground")
                {
                    currentInstance = Instantiate (Illusion, transform.position, transform.rotation);



                    agent = currentInstance.GetComponent<NavMeshAgent> ();


                    agent.SetDestination ((Vector2)hit.point);



                }

        }
    }
    public void Queue ()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            if (currentInstance != null)
            {
                agent.speed = normalSpeed;
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                if (Physics.Raycast (ray, out RaycastHit hit, maxDist, Ground))
                    if (hit.transform.tag == "Ground")
                        agent.destination = (Vector2)hit.point;

            }
    }
}
