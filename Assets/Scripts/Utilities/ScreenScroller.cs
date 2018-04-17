using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScroller : MonoBehaviour {

    public float scrollSpeed = 0.5f; //Camera scroll speed

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        SmoothMoves();
	}

    //Camera Scrolling up
    void SmoothMoves() {
        transform.Translate(Vector2.up * scrollSpeed * Time.deltaTime);
    }

    //Follows player left & right only
    public void PlayerFollow (float moveSpeed)
    {
        if (Input.GetKey("left"))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey("right"))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }
}
