using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public float enemyHealth = 50f;
    public float enemyMoveSpeed = 5f;
    public float enemyAttack = 10f;
    public float wanderRange = 5f;
    public GameObject ePlayer;


	// Use this for initialization
	public virtual void Start () {
        ePlayer = GameObject.Find("Player");
	}

    // Update is called once per frame
    public virtual void Update()
    {
        EnemyHealthCheck();
    }

    //Check for attack triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Swipe") //Get hit by player
        {
            StartCoroutine(Ouch(Player.playerAttack));
        }
    }

    //Get hit by something
    public IEnumerator Ouch(float dmg)
    {
        GetComponent<Wander>().Off();
        enemyHealth -= dmg;
        yield return new WaitForSeconds(.5f);
        GetComponent<Wander>().On();
    }


    //Monitor enemy health and destroy if needed
    public void EnemyHealthCheck()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
