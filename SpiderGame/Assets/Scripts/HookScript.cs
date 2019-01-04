using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    public float nodeDistance;
    public float lineWidth;
    public Material lineMaterial;

    private LineRenderer lineRenderer;
    private List<GameObject> nodes;
    private GameObject hook;
    private Coroutine shot;

    // Use this for initialization
    void Start ()
    {
        nodes = new List<GameObject>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = lineMaterial;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CreateHook();
            shot = StartCoroutine(Shoot(hook, pos));
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.touchCount == 0)
        {
            if (shot != null)
            {
                StopCoroutine(shot);
            }
            ClearLine();
        }

        if (nodes.Count > 0)
        {
            lineRenderer.positionCount = nodes.Count + 1;
            for (int i = 0; i < nodes.Count; i++)
            {
                lineRenderer.SetPosition(i, nodes[i].transform.position);
            }
            lineRenderer.SetPosition(nodes.Count, gameObject.transform.position);
        }
    }

    private void CreateHook()
    {
        hook = new GameObject("Hook");
        hook.transform.position = transform.position;

        hook.AddComponent<Rigidbody2D>();

        hook.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

        nodes.Add(hook);
        lineRenderer.enabled = true;
    }

    private IEnumerator Shoot(GameObject hook, Vector2 destiny)
    {
        //shoot
        while ((Vector2)hook.transform.position != destiny)
        {
            hook.transform.position = Vector2.MoveTowards((Vector2)hook.transform.position, destiny, 50f * Time.deltaTime);
            CheckNodes();
            yield return new WaitForEndOfFrame();
        }
        ConnectNodes();
        yield return new WaitForEndOfFrame();
        //climb
        while (gameObject.transform.position != nodes[nodes.Count - 1].transform.position && nodes.Count > 1)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                nodes[nodes.Count - 1].transform.position, 10f * Time.deltaTime);
            if (gameObject.transform.position == nodes[nodes.Count - 1].transform.position)
                DeleteNode();
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

    private void CreateNode()
    {
        GameObject node = new GameObject("node");
        node.transform.position = transform.position;
        node.transform.SetParent(hook.transform);
        node.AddComponent<Rigidbody2D>();
        node.AddComponent<HingeJoint2D>();
        nodes.Add(node);
    }

    private void CheckNodes()
    {
        if (Vector2.Distance(transform.position, nodes[nodes.Count - 1].transform.position) > nodeDistance)
        {
            CreateNode();
        }
    }

    private void ConnectNodes()
    {
        gameObject.GetComponent<HingeJoint2D>().enabled = true;
        gameObject.GetComponent<HingeJoint2D>().connectedBody = nodes[nodes.Count - 1].GetComponent<Rigidbody2D>();
        int count = nodes.Count;
        for (int i = 1; i < count; i++)
        {
            nodes[i].GetComponent<HingeJoint2D>().connectedBody = nodes[i - 1].GetComponent<Rigidbody2D>();
        }
    }

    public void End()
    {
        if (shot != null)
        {
            StopCoroutine(shot);
        }
        ClearLine();
    }

    private void ClearLine()
    {
        gameObject.GetComponent<HingeJoint2D>().enabled = false;
        gameObject.GetComponent<HingeJoint2D>().connectedBody = null;
        lineRenderer.enabled = false;

        if (nodes.Count > 0)
        {
            foreach (GameObject node in nodes)
            {
                Destroy(node);
            }
            nodes = new List<GameObject>();
        }
    }

    private void DeleteNode()
    {
        Destroy(nodes[nodes.Count - 1]);
        nodes.RemoveAt(nodes.Count - 1);
        gameObject.GetComponent<HingeJoint2D>().connectedBody = nodes[nodes.Count - 1].GetComponent<Rigidbody2D>();        
    }
}
