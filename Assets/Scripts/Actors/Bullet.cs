using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Transform target;
    public float projectileSpeed = 10;
    public int bulletDamage = 10;

    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
        // rotate the projectile to aim the target:
        myTransform.LookAt(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        // distance moved since last frame:
        float amtToMove = projectileSpeed * Time.deltaTime;
        // translate projectile in its forward direction:
        myTransform.Translate(Vector3.forward * amtToMove);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemies")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), collision.collider, true);
        }

        if (collision.collider.tag == "Player")
        {
            Player.AdjustHealth(-bulletDamage);
        }
        Destroy(gameObject);
    }

}
