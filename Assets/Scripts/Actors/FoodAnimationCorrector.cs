using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAnimationCorrector : MonoBehaviour 
{

    public GameObject target;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z * -1);
        Animate();
    }

    //Animates the attached gameobject
    void Animate()
    {
        if (target.GetComponent<FoodWander>().facing == "right")
        {
            anim.SetTrigger("moveRight");
        }
        if (target.GetComponent<FoodWander>().facing == "left")
        {
            anim.SetTrigger("moveLeft");
        }
        if (target.GetComponent<FoodWander>().facing == "up")
        {
            anim.SetTrigger("moveUp");
        }
        if (target.GetComponent<FoodWander>().facing == "down")
        {
            anim.SetTrigger("moveDown");
        }
    }
}

