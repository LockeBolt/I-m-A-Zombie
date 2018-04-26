using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Enemy
{
    public GameObject bullet;
    bool shootin = false;
    float distance;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        FindPlayer();
    }

    void FindPlayer()
    {
        distance = Vector2.Distance(transform.position, ePlayer.transform.position);
        if (distance < enemyRange && !shootin)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        shootin = true;
        GetComponent<HunterWander>().Off();
        while(distance < enemyRange)
        {
            GetComponent<AudioSource>().Play();
            Instantiate(bullet,new Vector3(transform.position.x,transform.position.y, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(.1f);
        GetComponent<HunterWander>().On();
        shootin = false;
    }

}
