using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemies : Enemy
{

    public GameObject scuffle;
    bool scufflin = false;
    float distance;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
        FindPlayer();
	}

    void FindPlayer()
    {
        distance = Vector2.Distance(transform.position, ePlayer.transform.position);
        if (distance < enemyRange)
        {
            GetComponent<Wander>().chase = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && !scufflin)
        {
            scufflin = true;
            StartCoroutine(Kerfluffle());
        }
    }

    IEnumerator Kerfluffle()
    {
        scuffle.transform.position = new Vector2(image.transform.position.x, image.transform.position.y);
        GetComponent<Wander>().Off();
        image.SetActive(false);
        Player.scuffler = gameObject;
        Player.playerState = Player.PlayerState.hitState;
        scuffle.SetActive(true);
        while(Player.escapeCharge < 10)
        {
            yield return new WaitForSeconds(.1f);
        }
        scuffle.SetActive(false);
        Player.escapeCharge = 0;
        Player.playerState = Player.PlayerState.idleState;
        image.SetActive(true);
        GetComponent<Wander>().On();
        scufflin = false;
    }
}
