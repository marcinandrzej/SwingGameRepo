using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : CollisionScript
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public override void OnCollision(PlayerScript player)
    {
        if (!player.dead)
        {
            player.DamageHeal(player.maxHp);
        }
        player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}
