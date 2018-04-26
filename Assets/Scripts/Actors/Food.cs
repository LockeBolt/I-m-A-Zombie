using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public GameObject food;
    public GameObject nom;
    public GameObject player;
    public AudioClip[] hop;
    private AudioSource speaker;
    private bool hoppin = true;

    void Start()
    {
        speaker = GetComponent<AudioSource>();
        StartCoroutine(Speak());
    }

    //Check for Player catching food
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Swipe")
        {
            GetComponent<FoodWander>().Off();
            StartCoroutine(Nom());
        }
    }

    //Handle Player eating the food
    IEnumerator Nom()
    {
        hoppin = false;
        Destroy(food);
        nom.SetActive(true);
        GetComponent<FoodWander>().enabled = false;
        Player.SetState(1);
        int ran = Random.Range(4, 6);
        speaker.PlayOneShot(hop[ran]);
        yield return new WaitForSeconds(1.0f);
        nom.SetActive(false);
        Player.AdjustHealth(20);
        Player.SetState(0);
        Destroy(gameObject);
    }

    IEnumerator Speak()
    {
        while (hoppin)
        {
            yield return new WaitForSeconds(.4f);
            int clip = Random.Range(0, 3);
            speaker.PlayOneShot(hop[clip]);
        }
    }
}
