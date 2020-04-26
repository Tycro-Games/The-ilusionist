using UnityEngine;

public class EnemyBrain : MonoBehaviour
{

    private SpotPlayer spot = null;
    private Chase chase = null;

    private bool StopedPatrolling = false;
    private void Awake ()
    {
        spot = GetComponent<SpotPlayer> ();
        chase = GetComponent<Chase> ();

    }
    private void OnEnable ()
    {
        chase.onPlayerFound += ChangeBehavior;

    }
    private void OnDisable ()
    {
        chase.onPlayerFound -= ChangeBehavior;

    }
    private void ChangeBehavior ()
    {

        if (chase.foundTarget)
        {
            StopedPatrolling = true;
            spot.StopAllCoroutines ();

        }
        else if (StopedPatrolling)
        {
            StopedPatrolling = false;
            
            spot.StartCoroutine (spot.FollowPointsAfterChase ());
        }
    }

}
