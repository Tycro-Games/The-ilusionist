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
    private float firerate = .5f;
    private float currentTime = 0;

    [Header ("Raycast")]
    [SerializeField]
    private float maxDist = 100.0f;
    [SerializeField]
    private LayerMask Ground = new LayerMask ();

    private TeleportSound teleport;

    private Animator anim;
    private void Start ()
    {
        anim = GetComponentInChildren<Animator> ();
        teleport = GetComponentInChildren<TeleportSound> ();
    }

    public void Update ()
    {
        if (currentTime > 0)
            currentTime -= Time.deltaTime;


        if (currentInstance == null)
        {
            anim.SetBool ("AbilityReady", true);
        }
        else
        {
            Queue ();
            anim.SetBool ("AbilityReady", false);
        }

    }
    public void Spawn (InputAction.CallbackContext ctx)
    {
        if (SceneManager.GetActiveScene ().buildIndex == 1)
            return;

        if (ctx.ReadValueAsButton () && currentInstance == null && currentTime <= 0)
        {
            currentTime = firerate;

            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray, out RaycastHit hit, maxDist, Ground))
                if (hit.transform.tag == "Ground")
                {
                    teleport.TeleportingSound (false);
                    currentInstance = Instantiate (Illusion, transform.position, transform.rotation);

                    currentInstance.GetComponent<DeadOnTouch> ().inject (teleport);

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
