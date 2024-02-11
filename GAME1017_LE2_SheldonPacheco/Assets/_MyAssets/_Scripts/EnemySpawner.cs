using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnInterval;
    [SerializeField] private TMP_Text killValue;
    private List<GameObject> enemies;
    private int kills = 0;
    private int killMax = 999;

    public static EnemySpawner Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Initialize()
    {
        enemies = new List<GameObject>();
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void Update()
    {
        if (enemies.Count > 0 && enemies[0].transform.position.y <= -5.5f)
        {
            DeleteEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = new Vector3(Random.Range(-6f, 6f), 6f, 0f);
        enemies.Add(newEnemy);
    }

    public void DeleteEnemy(int position = 0)
    {
        Destroy(enemies[position]);
        enemies.RemoveAt(position);
        kills = Mathf.Clamp(kills += 1, 0, killMax);
        killValue.text = kills.ToString();
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

    public void SetKillCount(int count)
    {
        kills = Mathf.Clamp(count, 0, killMax);
        killValue.text = kills.ToString();
    }

    public int GetKillCount()
    {
        return kills;
    }
}