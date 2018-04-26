using UnityEngine;
using System.Collections;
using Pathfinding;

public class FoodWander : MonoBehaviour
{
    //Wander range
    public float radius = 5f;
    public string facing = "";
    private float xDiff;
    private float yDiff;
    private float xPower;
    private float yPower;

    IAstarAI ai;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
    }

    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * radius;

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