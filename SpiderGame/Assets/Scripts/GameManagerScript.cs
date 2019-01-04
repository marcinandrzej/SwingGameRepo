using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public GameObject menu;
    public EnemySpawnScript spawnE;
    public CollectableSpawnScript spawnC;
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        if (instance == null)
            instance = this;

        Invoke("StartGame", 1);	
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void StartGame()
    {
        spawnE.StartSpawn();
        spawnC.Spawn();
        spawnC.Spawn();
        player.GetComponent<HookScript>().enabled = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public void EndGame()
    {
        spawnE.Stop();
        player.GetComponent<HookScript>().End();
        player.GetComponent<HookScript>().enabled = false;
        player.GetComponent<HingeJoint2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 30, ForceMode2D.Impulse);
        StartCoroutine(MenuShow());
    }

    private IEnumerator MenuShow()
    {
        menu.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        menu.SetActive(true);
        for (int i = 1; i < 50; i++)
        {
            menu.transform.localScale = new Vector3(0.02f * i, 0.02f * i, 0.02f * i);
            yield return new WaitForEndOfFrame();
        }
        Button[] buttons = menu.GetComponentsInChildren<Button>();
        foreach (Button but in buttons)
        {
            but.interactable = true;
        }
    }
}
