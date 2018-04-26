using UnityEngine;
using System.Collections;
using Pathfinding;

public class HunterWander : MonoBehaviour
{
    public string facing = "";
    public bool chase = false;
    private float xDiff;
    private float yDiff;
    private float xPower;
    private float yPower;

    IAstarAI ai;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
        ai.maxSpeed = GetComponent<Hunter>().enemyMoveSpeed;
    }

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * GetComponent<Hunter>().wanderRange;

        //point.y = 0; - JKG commented this out
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
            if (!chase)
            {
                ai.destination = PickRandomPoint();
            }
            else
            {
                ai.destination = GetComponent<Hunter>().ePlayer.transform.position;
            }
            ai.SearchPath();
            CheckDirection();
        }
    }

    void CheckDirection()
    {
        xDiff = ai.destination.x - transform.position.x;
        if (xDiff < 0) xPower = xDiff * -1;
        yDiff = ai.destination.y - transform.position.y;
        if (yDiff < 0) yPower = yDiff * -1;

        if (xPower > yPower)
        {
            if (xDiff < 0)
            {
                facing = "left";

            }
            else if (xDiff >= 0)
            {
                facing = "right";
            }
        }
        else if (yPower >= xPower)
        {
            if (yDiff >= 0)
            {
                facing = "up";
            }
            else if (yDiff < 0)
            {
                facing = "down";
            }
        }
        //Debug.Log(facing);
        //Debug.Log(xDiff);
        //Debug.Log(ai.destination);
        //Debug.Log(transform.position);
        //Debug.Log(yDiff);
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
