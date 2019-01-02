using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    private const float MIN_SECONDS = 0.5f;
    private const float MAX_SECONDS = 1.2f;

    public Sprite enemySprite;
    public RuntimeAnimatorController enemyAnimator;
    public List<GameObject> spawnPoints;
    public GameObject player;

    private List<EnemyScript> enemies;

    // Use this for initialization
    void Start ()
    {
        enemies = new List<EnemyScript>();
        StartCoroutine(SpawnEnemies());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void CreateEnemy()
    {
        GameObject enemy = new GameObject("Enemy");
        enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].transform.position;

        enemy.AddComponent<SpriteRenderer>();
        enemy.AddComponent<EnemyScript>();
        enemy.AddComponent<CircleCollider2D>();

        enemy.GetComponent<SpriteRenderer>().sprite = enemySprite;
        enemy.GetComponent<SpriteRenderer>().sortingOrder = 1;
        enemy.GetComponent<EnemyScript>().Move(player.transform.position);
        enemy.GetComponent<EnemyScript>().SetAnimator(enemyAnimator);
        enemy.GetComponent<CircleCollider2D>().isTrigger = true;
        enemy.GetComponent<CircleCollider2D>().radius = 1.0f;

        enemies.Add(enemy.GetComponent<EnemyScript>());
    }

    private int EnemiesInMotion()
    {
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].InMotion)
                    return i;
            }
        }
        return -1;
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MIN_SECONDS, MAX_SECONDS));
            int index = EnemiesInMotion();
            if (index == -1)
            {
                CreateEnemy();
            }
            else
            {
                enemies[index].transform.position = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].transform.position;
                enemies[index].GetComponent<EnemyScript>().Move(player.transform.position);
            }
        }
    }
}
