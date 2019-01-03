using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : CollisionScript
{
    private const float MIN_SPEED = 10.0f;
    private const float MAX_SPEED = 15.0f;
    private const float dist = 50.0f;

    private bool inMotion;

    private Coroutine movement;

    public bool InMotion
    {
        get
        {
            return inMotion;
        }

        set
        {
            inMotion = value;
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

    public override void OnCollision(PlayerScript player)
    {
        player.DamageHeal(1);
        gameObject.transform.position = new Vector3(dist, 0, 0);
    }

    public void SetAnimator(RuntimeAnimatorController animator)
    {
        gameObject.AddComponent<Animator>();
        gameObject.GetComponent<Animator>().runtimeAnimatorController = animator;
    }

    public void Move(Vector3 destiny)
    {
        StartCoroutine(MoveCoroutine(destiny));
    }

    private IEnumerator MoveCoroutine(Vector3 destiny)
    {
        InMotion = true;
        float speed = Random.Range(MIN_SPEED, MAX_SPEED);
        Vector3 direction = (destiny - transform.position).normalized;
        
        float heading = Mathf.Atan2(-direction.x, direction.y);
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, heading * Mathf.Rad2Deg + 90);

        while (Vector2.Distance(destiny, transform.position) < dist)
        {
            transform.position += (direction * speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        InMotion = false;
        yield return new WaitForEndOfFrame();
    }
}
