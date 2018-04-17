using UnityEngine;
using System.Collections;
using Pathfinding;

public class Wander : MonoBehaviour
{

    IAstarAI ai;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
        ai.maxSpeed = GetComponent<AllEnemies>().enemyMoveSpeed;
    }

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * GetComponent<AllEnemies>().wanderRange;

        point.y = 0;
        point += ai.position;
        return point;
    }

    void Update()
    {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
    }

    //Turn AI off
    public void Off()
    {
        ai.canMove = false;
    }

    //Turn AI on
    public void On()
    {
        ai.canMove = true;
    }
}
