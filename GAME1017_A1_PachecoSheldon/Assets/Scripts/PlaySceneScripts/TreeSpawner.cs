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

    private void Start()
    {
        InvokeRepeating("SpawnTree", 0f, spawnInterval);
    }

    private void Update()
    {
        RespawnTrees();
    }

    void SpawnTree()
    {
        if (activeTrees.Count < maxTrees && (activeTrees.Count == 0 || activeTrees[activeTrees.Count - 1].transform.position.x < 7.68f))
        {
            GameObject tree = GetTreeFromPool();

            if (tree == null)
            {
                tree = Instantiate(christmasTree, GetRandomSpawnPosition(), Quaternion.identity);
            }
            else
            {
                tree.transform.position = GetRandomSpawnPosition();
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

    public GameObject GetTreeFromPool()
    {
        if (poolOfTrees.Count > 0)
        {
            GameObject tree = poolOfTrees[0];
            poolOfTrees.RemoveAt(0);
            return tree;
        }

        return null;
    }

    void RespawnTrees()
    {
        // Remove trees that are beyond a certain position
        activeTrees.RemoveAll(tree => tree.transform.position.x < -10f);

        // Calculate how many more trees can be spawned
        int treesToSpawn = maxTrees - (activeTrees.Count + poolOfTrees.Count);

        for (int i = 0; i < treesToSpawn; i++)
        {
            GameObject tree = GetTreeFromPool();

            if (tree == null)
            {
                tree = Instantiate(christmasTree, GetRandomSpawnPosition(), Quaternion.identity);
            }
            else
            {
                tree.transform.position = GetRandomSpawnPosition();
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
}