using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public AudioClip[] clips;
    public AudioSource speaker;

	// Use this for initialization
	void Start () {
        StartCoroutine(Footsteps());
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
        if (Input.GetKeyDown("space") && Player.playerState == Player.PlayerState.hitState) //Open for if we need it
        {
            if (Player.playerState == Player.PlayerState.hitState)
            {
                Player.escapeCharge += 1;
            }
        }
    }

    //Take input and move player
    void MoveInput()
    {
        if (Input.GetKey("up"))
        {
            transform.Translate(Vector2.up * Player.moveSpeed * Time.deltaTime);
            Player.anim.SetTrigger("moveUp");
            Player.SetState(4);
        }
        if (Input.GetKey("down"))
        {
            transform.Translate(Vector2.down * Player.moveSpeed * Time.deltaTime);
            Player.anim.SetTrigger("moveDown");
            Player.SetState(4);
        }
        if (Input.GetKey("left"))
        {
            transform.Translate(Vector2.left * Player.moveSpeed * Time.deltaTime);
            Player.anim.SetTrigger("moveLeft");
            Player.SetState(4);
        }
        if (Input.GetKey("right"))
        {
            transform.Translate(Vector2.right * Player.moveSpeed * Time.deltaTime);
            Player.anim.SetTrigger("moveRight");
            Player.SetState(4);
        }
        
        if (!Input.GetKey("up") && !Input.GetKey("down") && !Input.GetKey("left") && !Input.GetKey("right"))
        {
            Player.SetState(0);
            Player.anim.SetTrigger("idle");
        }
            
    }

    //Take input for actions
    void ActionInput()
    {
        if (Input.GetKeyDown("z")) //Attack
        {
            StartCoroutine(Swipe());
        }
        if (Input.GetKeyDown("x")) //Join zombies
        {

        }
    }

    //Attack 
    IEnumerator Swipe()
    {
        Player.playerState = Player.PlayerState.attackState;
        int ran = Random.Range(5, 7);
        speaker.PlayOneShot(clips[ran]);
        yield return new WaitForSeconds(.5f);
        Player.playerState = Player.PlayerState.idleState;
    }

    //Play footsteps
    IEnumerator Footsteps()
    {
        while (Player.playerState != Player.PlayerState.deadState)
        {
            while (Player.playerState == Player.PlayerState.movingState) // walking sounds
            {
                int ran = Random.Range(1, 4);
                speaker.PlayOneShot(clips[ran]);
                yield return new WaitForSeconds(.3f);
            }
            yield return new WaitForSeconds(.1f);
        }
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
