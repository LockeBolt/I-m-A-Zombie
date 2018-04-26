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
    public static Animator anim;
    public Slider healthBar;
    public GameObject swipeRadius;
    public AudioClip[] clips;
    public static AudioClip[] sClips;
    public AudioSource speaker;
    public static AudioSource sSpeaker;
    public static GameObject scuffler;
    public static int escapeCharge = 0;
    public static Rigidbody2D rb;

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
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sClips = clips;
        sSpeaker = speaker;
        StartCoroutine(Decay());
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
                rb.WakeUp();
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
                rb.Sleep();
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
                swipeRadius.SetActive(false);
                GetComponent<SpriteRenderer>().enabled = true;
                break;
            case PlayerState.eatingState:
                break;
            case PlayerState.hidingState:
                break;
            case PlayerState.hordeState:
                break;
            case PlayerState.movingState:
                break;
            case PlayerState.hitState:
                GetComponent<SpriteRenderer>().enabled = false;
                break;
            case PlayerState.deadState:
                StartCoroutine(Die());
                break;
            case PlayerState.attackState:
                swipeRadius.SetActive(true);
                break;
            default:

                break;
        }
    }

    //Used to adjust the Player's health. Use negative numbers to subtract
    public static void AdjustHealth(int heart)
    {
        int old = playerHealth;
        playerHealth += heart;
        int ran = Random.Range(0, 2);
        if (old > playerHealth)
        {
            sSpeaker.PlayOneShot(sClips[ran]);
        } else
        {
            sSpeaker.PlayOneShot(sClips[3]);
        }
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

    IEnumerator Decay()
    {
        yield return new WaitForSeconds(1f);
        playerHealth -= 2;
    }

    IEnumerator Die()
    {
        sSpeaker.PlayOneShot(sClips[0]);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
