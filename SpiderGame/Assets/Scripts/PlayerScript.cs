using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int maxHp;
    public int points;
    public int hp;

	// Use this for initialization
	void Start ()
    {
        maxHp = 3;
        hp = 3;
        points = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DamageHeal(int damage)
    {
        hp -= damage;
        hp = Mathf.Max(0, Mathf.Min(hp, maxHp));
        if (hp == 0)
        {
            GameManagerScript.instance.EndGame();
        }
    }

    public void Collect(int point)
    {
        points += point;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<CollisionScript>().OnCollision(this);
    }
}
