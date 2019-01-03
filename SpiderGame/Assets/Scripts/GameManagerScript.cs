using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public EnemySpawnScript spawn;
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        if (instance == null)
            instance = this;		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void EndGame()
    {
        spawn.Stop();
        player.GetComponent<HookScript>().End();
        player.GetComponent<HookScript>().enabled = false;
        player.GetComponent<PlayerScript>().enabled = false;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        player.GetComponent<HingeJoint2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 30, ForceMode2D.Impulse);
    }
}
