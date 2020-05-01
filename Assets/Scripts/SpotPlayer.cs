using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SpotPlayer : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private Transform pathHolder = null;

    [Header ("Movement")]
    [SerializeField]
    private float moveSpeed = 10.0f;
    private Vector2 currentPoint = Vector2.zero;



    private int index = 0;

    [SerializeField]
    private float timeToWait = 2.0f;

    [Header ("Rotation")]
    [SerializeField]
    private float turnSpeed = 10.0f;

    public Vector2[] Points;

    private void Start ()
    {
        anim = GetComponentInChildren<Animator> ();

        injectPoints ();
    }
    private void injectPoints ()
    {
        int count = pathHolder.childCount;
        if (count == 0 )
            return;
        Points = new Vector2[count];


        for (int i = 0; i < count; i++)
        {
            Points[i] = pathHolder.GetChild (i).position;
        }

        index = 1;
        if (Points.Length == 1)
            return;
        currentPoint = Points[index];

        transform.position = Points[0];

        transform.rotation = Quaternion.LookRotation (transform.forward, GetDir (currentPoint));

        StartCoroutine (FollowPoints ());
    }
    public Vector2 ReturnCurrentPoint ()
    {
        return Points[index];
    }
    public IEnumerator FollowPointsAfterChase ()
    {
        yield return StartCoroutine (RotateToPoint (currentPoint));
        while (true)
        {
            anim.SetBool ("Walk", true);
            Move (Points[index], moveSpeed);
            if ((Vector2)transform.position == Points[index])
            {
                anim.SetBool ("Walk", false);
                index = (index + 1) % Points.Length;
                currentPoint = Points[index];
                yield return new WaitForSeconds (timeToWait);
                yield return StartCoroutine (RotateToPoint (currentPoint));
            }
            yield return null;
        }
    }

    public IEnumerator FollowPoints ()
    {
        while (true)
        {
            anim.SetBool ("Walk", true);
            Move (Points[index], moveSpeed);
            if ((Vector2)transform.position == Points[index])
            {
                anim.SetBool ("Walk", false);
                index = (index + 1) % Points.Length;
                currentPoint = Points[index];
                yield return new WaitForSeconds (timeToWait);
                yield return StartCoroutine (RotateToPoint (currentPoint));
            }
            yield return null;
        }
    }
    public IEnumerator RotateToPoint (Vector2 target)
    {

        Quaternion newRot = Quaternion.LookRotation (transform.forward, GetDir (target));

        while (transform.rotation != newRot)
        {
            transform.rotation = Quaternion.RotateTowards (transform.rotation, newRot, turnSpeed * Time.deltaTime);
            yield return null;
        }
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
    void OnDrawGizmos ()
    {
        if (pathHolder.childCount == 0)
            return;
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
    }
}
