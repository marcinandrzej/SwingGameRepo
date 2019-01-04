using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawnScript : MonoBehaviour
{
    private const float MAX_X = 10.0f;
    private const float MAX_Y = 5.0f;

    public GameObject heartPrefab;
    public GameObject gemPrefab;
    public Transform spawnPoint;

    private List<CollectableScript> collectables;

    // Use this for initialization
    void Start ()
    {
       SetUp();
    }

    private void SetUp()
    {
        collectables = new List<CollectableScript>();
        GameObject g = Instantiate(heartPrefab, spawnPoint);
        g.GetComponent<CollectableScript>().Set(this, false);
        collectables.Add(g.GetComponent<CollectableScript>());
        for (int i = 0; i < 6; i++)
        {
            g = Instantiate(gemPrefab, spawnPoint);
            g.GetComponent<CollectableScript>().Set(this, false);
            collectables.Add(g.GetComponent<CollectableScript>());
        }
    }

    private List<CollectableScript> Available()
    {
        List<CollectableScript> l = new List<CollectableScript>();
        foreach (CollectableScript clsc in collectables)
        {
            if (!clsc.InUse)
                l.Add(clsc);
        }
        return l;
    }

    public void Spawn()
    {
        List<CollectableScript> list = Available();
        int i = Random.Range(0, list.Count - 1);
        list[i].InUse = true;
        list[i].gameObject.transform.position = new Vector3(Random.Range(-MAX_X, MAX_X), Random.Range(-MAX_Y, MAX_Y), 0);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
