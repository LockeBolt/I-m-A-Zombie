using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    //Class for player states and some other basic stuff (might move the movement into its own script later)
    public static PlayerState playerState;
    public static float moveSpeed = 5f;
    public static bool terrainCollision = false;
    public static int playerHealth = 50;
    public static int playerAttack = 20;
    public Slider healthBar;
    public GameObject swipeRadius;

    //Player state for management of player character
    public enum PlayerState
    {
        idleState, // nothing is happeneing to player
        eatingState, //player is nomnoming something
        hidingState, //player is hiding in a bush
        hordeState, //player is in a horde
        movingState, //player is moving normally
        hitState, //player has just been hit (temporary)
        deadState, //player is dead...again 
        attackState //player is attacking
    }

	// Use this for initialization
	void Start () {
        playerState = PlayerState.idleState;
	}
	
	// Update is called once per frame
	void Update () {
        CheckState();
        CheckHealth();
	}

    //Used to change the player's state
    public static void SetState(int x)
    {
        switch(x)
        {
            case 0:
                playerState = PlayerState.idleState;
                break;
            case 1:
                playerState = PlayerState.eatingState;
                break;
            case 2:
                playerState = PlayerState.hidingState;
                break;
            case 3:
                playerState = PlayerState.hordeState;
                break;
            case 4:
                playerState = PlayerState.movingState;
                break;
            case 5:
                playerState = PlayerState.hitState;
                break;
            case 6:
                playerState = PlayerState.deadState;
                break;
            case 7:
                playerState = PlayerState.attackState;
                break;
            default:
                break;
        }
    }

    // Global settings based on player's state
    void CheckState() 
    {
        Debug.Log(playerState);
        switch (playerState)
        {
            case PlayerState.idleState:
                Camera.main.GetComponent<ScreenScroller>().enabled = true;
                swipeRadius.SetActive(false);
                break;
            case PlayerState.eatingState:
                Camera.main.GetComponent<ScreenScroller>().enabled = true;
                break;
            case PlayerState.hidingState:
                Camera.main.GetComponent<ScreenScroller>().enabled = false; //stop camera scrolling
                break;
            case PlayerState.hordeState:
                Camera.main.GetComponent<ScreenScroller>().enabled = true;
                break;
            case PlayerState.movingState:
                Camera.main.GetComponent<ScreenScroller>().enabled = true;
                break;
            case PlayerState.hitState:
                Camera.main.GetComponent<ScreenScroller>().enabled = true;
                break;
            case PlayerState.deadState:
                Camera.main.GetComponent<ScreenScroller>().enabled = true;
                break;
            case PlayerState.attackState:
                Camera.main.GetComponent<ScreenScroller>().enabled = true;
                swipeRadius.SetActive(true);
                break;
            default:

                break;
        }
    }

    //Used to adjust the Player's health. Use negative numbers to subtract
    public static void AdjustHealth(int heart)
    {
        playerHealth += heart;
    }

    //make dead if too low
    void CheckHealth() 
    {
        healthBar.value = playerHealth;
        if(playerHealth <= 0)
        {
            playerState = PlayerState.deadState;
        }
    }
}
