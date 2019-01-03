using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : CollisionScript
{
    public bool heal;

    private bool inUse;
    private CollectableSpawnScript css;

    public bool InUse
    {
        get
        {
            return inUse;
        }

        set
        {
            inUse = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Set(CollectableSpawnScript _css, bool _inUse)
    {
        css = _css;
        InUse = _inUse;
    }

    public override void OnCollision(PlayerScript player)
    {
        if (heal)
        {
            player.DamageHeal(-1);
        }
        else
        {
            player.Collect(1);
        }
        gameObject.transform.position = new Vector3(100, 0, 0);
        inUse = false;
        css.Spawn();
    }
}
