using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Chase : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private Light2D lighting;

    [SerializeField]
    private LayerMask atackLayer = new LayerMask ();
    [SerializeField]
    private LayerMask viewLayer = new LayerMask ();

    [SerializeField]
    private Color patrolColor = new Color ();
    [SerializeField]
    private Color chaseColor = new Color ();

    [SerializeField]
    private float chaseSpeed = 20.0f;

    [HideInInspector]
    public bool foundTarget = false;

    [SerializeField]
    private float timeToSpotPlayer = 0.0f;
    //private staff
    private float playerVisibleTimer = 0.0f;

    [SerializeField]
    private float chaseTurnSpeed = 150.0f;

    private Collider[] Hostiles = new Collider[5];

    private Transform currentTarget = null;

    public delegate void OnPlayerFound ();
    public event OnPlayerFound onPlayerFound;
    private void Start ()
    {
        anim = GetComponentInChildren<Animator> ();

        lighting = GetComponent<Light2D> ();
    }
    private void Update ()
    {
        if (foundTarget == false)
            CheckSoroundings ();
    }
    private IEnumerator ColorTransition (bool Atack)
    {
        if (Atack)
        {
            while (playerVisibleTimer < timeToSpotPlayer)
            {
                playerVisibleTimer += Time.deltaTime;
                playerVisibleTimer = Mathf.Clamp (playerVisibleTimer, 0, timeToSpotPlayer);
                lighting.color = Color.Lerp (patrolColor, chaseColor, playerVisibleTimer / timeToSpotPlayer);
                yield return null;
            }
        }
        else
        {
            while (playerVisibleTimer > 0)
            {
                playerVisibleTimer -= Time.deltaTime;
                playerVisibleTimer = Mathf.Clamp (playerVisibleTimer, 0, timeToSpotPlayer);
                lighting.color = Color.Lerp (patrolColor, chaseColor, playerVisibleTimer / timeToSpotPlayer);
                yield return null;
            }
        }
    }
    private void CheckSoroundings ()
    {

        if (CanSeePlayer ())
        {

            foundTarget = true;

            onPlayerFound?.Invoke ();

            StartCoroutine (chase ());

        }
    }
    public bool CanSeePlayer ()
    {
        int hostileNumber = Physics.OverlapSphereNonAlloc (transform.position, lighting.pointLightOuterRadius, Hostiles, atackLayer);

        if (hostileNumber != 0)
        {
            Transform target = CanSeeHostile ();
            if (target != null)
            {
                currentTarget = target;
                return true;
            }
        }
        return false;
    }
    private Transform CanSeeHostile ()
    {
        foreach (Collider col in Hostiles)
        {
            if (col == null)
                continue;

            Vector2 playerPos = col.transform.position;
            Vector2 myPos = transform.position;
            if (Vector2.Distance (myPos, playerPos) <= lighting.pointLightOuterRadius)
            {
                Vector2 dir = (playerPos - myPos).normalized;
                if (Vector2.Angle (transform.up, dir) <= lighting.pointLightOuterAngle / 2)
                    if (Physics.Raycast (myPos, dir, out RaycastHit hit, lighting.pointLightOuterRadius, viewLayer))
                    {
                        if (hit.collider.tag == "Player")
                            return col.transform;
                    }
            }
        }
        return null;
    }

    private IEnumerator chase ()
    {
        yield return StartCoroutine (ColorTransition (true));
        while (currentTarget != null)
        {
            Vector2 target = currentTarget.position;
            anim.SetBool ("Walk", true);
            RotateToTarget (target);
            Move (target, chaseSpeed);

            yield return null;
        }
        yield return StartCoroutine (ColorTransition (false));
        anim.SetBool ("Walk", false);
        foundTarget = false;

        onPlayerFound?.Invoke ();
    }
    private void RotateToTarget (Vector2 target)
    {
        Quaternion newRot = Quaternion.LookRotation (transform.forward, GetDir (target));
        transform.rotation = Quaternion.RotateTowards (transform.rotation, newRot, chaseTurnSpeed * Time.deltaTime);
    }
    private void Move (Vector2 pos, float speed)
    {
        transform.position = Vector2.MoveTowards (transform.position, pos, Time.deltaTime * speed);
    }
    private Vector2 GetDir (Vector2 target)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        return dir;
    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay (transform.position, transform.up * lighting.pointLightOuterRadius);
    }

}

