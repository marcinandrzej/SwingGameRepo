using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int maxHp;
    public int points;
    public int hp;
    public bool dead;

    public Text scoreTxt;
    public Text healthTxt;
    public Image healthImg;

    public AudioClip[] sounds;

    private AudioSource playerAudio;
    private Coroutine damageCo;

	// Use this for initialization
	void Start ()
    {
        maxHp = 3;
        hp = 3;
        points = 0;
        dead = false;
        playerAudio = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void DamageHeal(int damage)
    {
        hp -= damage;
        hp = Mathf.Max(0, Mathf.Min(hp, maxHp));
        if (hp == 0)
        {
            playerAudio.PlayOneShot(sounds[1]);
            dead = true;
            GameManagerScript.instance.EndGame();
        }
        else
        {
            if (damage > 0)
            {
                playerAudio.PlayOneShot(sounds[0]);
                if (damageCo != null)
                    StopCoroutine(damageCo);
                damageCo = StartCoroutine(DamageCoroutine());
            }
            else
            {
                playerAudio.PlayOneShot(sounds[2]);
            }
        }
        healthTxt.text = hp.ToString() + "/" + maxHp.ToString();
        healthImg.fillAmount = (float)hp / (float)maxHp;

        if (damage > 0)
        {
            if (damageCo != null)
                StopCoroutine(damageCo);
            damageCo = StartCoroutine(DamageCoroutine());
        }
    }

    public void Collect(int point)
    {
        playerAudio.PlayOneShot(sounds[2]);
        points += point;
        scoreTxt.text = "SCORE:" + points.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<CollisionScript>().OnCollision(this);
    }

    private IEnumerator DamageCoroutine()
    {
        SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 5; i++)
        {
            spr.color = new Color(255, 0, 0,255);
            yield return new WaitForSeconds(0.1f);
            spr.color = new Color(255, 255, 255,255);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForEndOfFrame();
    }
}
