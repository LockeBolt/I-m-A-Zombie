using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.playerState == Player.PlayerState.idleState 
            || Player.playerState == Player.PlayerState.movingState 
            || Player.playerState == Player.PlayerState.attackState)
        {
            MoveInput();//Allow movement

            if(Player.playerState != Player.PlayerState.attackState)
            {
                ActionInput();//Allow actions
            }
        }
        CheckEdge();
	}

    //Take input and move player
    void MoveInput()
    {
        if (Input.GetKey("up"))
        {
            transform.Translate(Vector2.up * Player.moveSpeed * Time.deltaTime);
            Player.SetState(4);
        }
        if (Input.GetKey("down"))
        {
            transform.Translate(Vector2.down * Player.moveSpeed * Time.deltaTime);
            Player.SetState(4);
        }
        if (Input.GetKey("left"))
        {
            transform.Translate(Vector2.left * Player.moveSpeed * Time.deltaTime);
            if (!Player.terrainCollision)
            {
                Camera.main.GetComponent<ScreenScroller>().PlayerFollow(Player.moveSpeed);
            }
            Player.SetState(4);
        }
        if (Input.GetKey("right"))
        {
            transform.Translate(Vector2.right * Player.moveSpeed * Time.deltaTime);
            if (!Player.terrainCollision)
            {
                Camera.main.GetComponent<ScreenScroller>().PlayerFollow(Player.moveSpeed);
            }
            Player.SetState(4);
        }
        
        if (!Input.GetKey("up") && !Input.GetKey("down") && !Input.GetKey("left") && !Input.GetKey("right"))
        {
            Player.SetState(0);
        }
            
    }

    //Take input for actions
    void ActionInput()
    {
        if (Input.GetKey("z")) //Attack
        {
            StartCoroutine(Swipe());
        }
        if (Input.GetKey("x")) //Join zombies
        {

        }
        if (Input.GetKey("space")) //Open for if we need it
        {

        }
    }

    IEnumerator Swipe()
    {
        Player.playerState = Player.PlayerState.attackState;
        yield return new WaitForSeconds(.5f);
        Player.playerState = Player.PlayerState.idleState;
    }

    //Check for out-of-bounds character
    void CheckEdge() 
    {
        if (transform.position.y < Camera.main.GetComponentInChildren<EdgeCollider2D>().transform.position.y)
        {
            StartCoroutine(FixEdge());
        }
    }

    //Fix out of bound character
    IEnumerator FixEdge() 
    {
        transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), transform.rotation);
        yield return new WaitForSeconds(.2f);
    }

    //On Collision checks - Enter
    private void OnCollisionEnter2D(Collision2D collision)
    {
            Player.terrainCollision = true;
    }

    //On Collision checks - Exit
    private void OnCollisionExit2D(Collision2D collision)
    {
            Player.terrainCollision = false;
    }
}
