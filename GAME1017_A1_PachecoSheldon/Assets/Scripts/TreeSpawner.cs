using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> activeTrees = new List<GameObject>();
    [SerializeField] private List<GameObject> poolOfTrees = new List<GameObject>();
    public GameObject christmasTree;
    private float spawnInterval = 1f;
    public float treeSpeed = 6f;
    public int maxTrees = 3;

    void Start()
    {
        InvokeRepeating("SpawnTree", 0f, spawnInterval);
    }

    void SpawnTree()
    {
        if (activeTrees.Count < maxTrees && (activeTrees.Count == 0 || activeTrees[activeTrees.Count - 1].transform.position.x < 7.68f))
        {
            GameObject tree = GetTreeFromPool();

            if (tree == null)
            {
                tree = Instantiate(christmasTree, GetRandomSpawnPosition(), Quaternion.identity);
                tree.SetActive(true);
            }

            tree.SetActive(true);

            Rigidbody2D treeRb = tree.GetComponent<Rigidbody2D>();
            if (treeRb != null)
            {
                treeRb.velocity = Vector2.left * treeSpeed;
            }

            activeTrees.Add(tree);
        }
    }

    GameObject GetTreeFromPool()
    {
        if (poolOfTrees.Count > 0)
        {
            GameObject tree = poolOfTrees[0];
            poolOfTrees.RemoveAt(0);
            return tree;
        }

        return null;
    }

    public void MoveToPool(GameObject tree)
    {
        if (activeTrees.Contains(tree))
        {
            activeTrees.Remove(tree);

            if (activeTrees.Count + poolOfTrees.Count < maxTrees)
            {
                tree.transform.position = GetRandomSpawnPosition();

                Rigidbody2D treeRb = tree.GetComponent<Rigidbody2D>();
                if (treeRb != null)
                {
                    treeRb.velocity = Vector2.left * treeSpeed;
                }

                tree.SetActive(false);
                poolOfTrees.Add(tree);
            }
        }
        else
        {
            if (poolOfTrees.Count < maxTrees)
            {
                tree.SetActive(false);
                poolOfTrees.Add(tree);
            }
        }
    }

    public Vector3 GetRandomSpawnPosition()
    {
        float randomY = Random.Range(-3.83f, 4.15f);
        float spawnX = 10.0f;
        return new Vector3(spawnX, randomY, 0f);
    }

    
    void Update()
    {
        // checks if trees are off-screen and move them to the pool
        float minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        for (int i = activeTrees.Count - 1; i >= 0; i--)
        {
            if (activeTrees[i].transform.position.x < minX)
            {
                MoveToPool(activeTrees[i]);
            }
        }
    }
}