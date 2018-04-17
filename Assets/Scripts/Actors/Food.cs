using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public GameObject food;
    public GameObject nom;

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
        Destroy(food);
        nom.SetActive(true);
        GetComponent<FoodWander>().enabled = false;
        Player.SetState(1);
        yield return new WaitForSeconds(1.0f);
        nom.SetActive(false);
        Player.AdjustHealth(20);
        Player.SetState(0);
        Destroy(gameObject);
    }
}
