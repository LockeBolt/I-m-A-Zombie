using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public float enemyHealth = 50f;
    public float enemyMoveSpeed = 5f;
    public float enemyAttack = 10f;
    public float wanderRange = 5f;
    public float enemyRange = 6f;
    public GameObject ePlayer;
    public GameObject image;
    public AudioClip[] clips;
    public AudioSource speaker;
    public bool movin = true;


	// Use this for initialization
	public virtual void Start () {
        ePlayer = GameObject.Find("Player");
        StartCoroutine(Step());
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
        if (gameObject.tag == "Zombie")
        {
            GetComponent<Wander>().Off();
        } else
        {
            GetComponent<HunterWander>().Off();
        }
        
        enemyHealth -= dmg;
        int ran = Random.Range(5, 7);
        speaker.PlayOneShot(clips[ran]);
        yield return new WaitForSeconds(.5f);

        if (gameObject.tag == "Zombie")
        {
            GetComponent<Wander>().On();
        }
        else
        {
            GetComponent<HunterWander>().On();
        }
    }

    public IEnumerator Death()
    {
        speaker.PlayOneShot(clips[0]);
        yield return new WaitForSeconds(.2f);
    }


    //Monitor enemy health and destroy if needed
    public void EnemyHealthCheck()
    {
        if (enemyHealth <= 0)
        {
            movin = false;
            StartCoroutine(Death());
            Destroy(image);
            Destroy(gameObject);
        } else
        {
            int ran = Random.Range(0, 100);
            if (ran == 50 && gameObject.name == "Zombie"){
                speaker.PlayOneShot(clips[8]);
            }
           
        }
    }

    IEnumerator Step()
    {
        while (movin && gameObject.name == "Zombie")
        {
            yield return new WaitForSeconds(.4f);
            int clip = Random.Range(1, 4);
            speaker.PlayOneShot(clips[clip],.25f);
        }
    }

}
