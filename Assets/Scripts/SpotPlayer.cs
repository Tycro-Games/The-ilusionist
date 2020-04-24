using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SpotPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform pathHolder = null;

    [SerializeField]
    private Light2D lighting;

    [SerializeField]
    private LayerMask viewLayer = new LayerMask ();

    [Header ("Movement")]
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float timeToWait = 2.0f;
    [Header ("Rotation")]
    [SerializeField]
    private float turnSpeed = 10.0f;
    [Header ("Player Related")]

    [SerializeField]
    private float timeToSpotPlayer;
    //private staff
    private float playerVisibleTimer = 0.0f;
    private void Start ()
    {
        lighting = GetComponent<Light2D> ();

        int count = pathHolder.childCount;
        Vector2[] Points = new Vector2[count];


        for (int i = 0; i < count; i++)
        {
            Points[i] = pathHolder.GetChild (i).position;
        }

        StartCoroutine (FollowPoints (Points));
    }
    public bool CanSeePlayer ()
    {
        Vector2 playerPos = StaticInfo.PlayerInfo.position;
        Vector2 myPos = transform.position;
        if (Vector2.Distance (myPos, playerPos) <= lighting.pointLightOuterRadius)
        {
            Vector2 dir = (playerPos - myPos).normalized;
            if (Vector2.Angle (transform.up, dir) <= lighting.pointLightOuterAngle / 2)
                if (Physics.Raycast (myPos, dir, lighting.pointLightOuterRadius, viewLayer))
                {
                    return true;
                }
        }

        return false;
    }
    private void Update ()
    {
        #region see player
        if (CanSeePlayer ())
        {
            playerVisibleTimer += Time.deltaTime;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp (playerVisibleTimer, 0, timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
            Debug.Log ("atackTheFucker");
        #endregion

    }

    private IEnumerator FollowPoints (Vector2[] Points)
    {
        int index = 1;
        Vector2 currentPoint = Points[index];

        transform.position = Points[0];


        transform.rotation = Quaternion.LookRotation (transform.forward, GetDir (currentPoint));

        while (true)
        {
            transform.position = Vector2.MoveTowards (transform.position, Points[index], Time.deltaTime * moveSpeed);
            if ((Vector2)transform.position == Points[index])
            {
                index = (index + 1) % Points.Length;
                currentPoint = Points[index];
                yield return new WaitForSeconds (timeToWait);
                yield return StartCoroutine (RotateToPoint (currentPoint));
            }
            yield return null;
        }
    }
    private IEnumerator RotateToPoint (Vector2 target)
    {

        Quaternion newRot = Quaternion.LookRotation (transform.forward, GetDir(target));

        while (transform.rotation != newRot)
        {
            transform.rotation = Quaternion.RotateTowards (transform.rotation, newRot, turnSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private Vector2 GetDir (Vector2 target)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        return dir;
    }
    void OnDrawGizmos ()
    {

        Vector2 startPosition = pathHolder.GetChild (0).position;
        Vector2 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            if (waypoint == pathHolder.GetChild (0))
                Gizmos.color = Color.cyan;
            else
                Gizmos.color = Color.white;

            Gizmos.DrawWireSphere (waypoint.position, .3f);
            Gizmos.DrawLine (previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine (previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay (transform.position, transform.up * lighting.pointLightOuterRadius);
    }
}
