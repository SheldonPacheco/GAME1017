using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;


[XmlRoot("GameData")]
public class GameData
{
    public int killCount;

    [XmlArray("TurretPositions")]
    [XmlArrayItem("Position")]
    public List<Vector3> turretPositions;

    public GameData()
    {
        killCount = 0;
        turretPositions = new List<Vector3>();
    }
}

public class ClickDragScript : MonoBehaviour
{
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private Transform spawnArea;
    
    private bool isDragging = false;
    private Vector2 offset;
    private Rigidbody2D currentlyDraggedObject;
    
    private List<GameObject> turrets;
    private GameData gameData;

    void Start()
    {
        turrets = new List<GameObject>();
        gameData = DeserializeFromXml();
        if (File.Exists("gameData.xml"))
        {
            
            gameData = DeserializeFromXml();

            
            EnemySpawner.Instance.SetKillCount(gameData.killCount);

            
            SpawnTurrets(gameData.turretPositions);
        }
    }

    void SerializeToXml(GameData data)
    {
        
        data.killCount = EnemySpawner.Instance.GetKillCount();

        
        data.turretPositions.Clear();

       
        foreach (GameObject turret in turrets)
        {
            data.turretPositions.Add(turret.transform.position);
        }

        
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));

        
        using (StreamWriter streamWriter = new StreamWriter("gameData.xml"))
        {
            serializer.Serialize(streamWriter, data);
        }
    }

    GameData DeserializeFromXml()
    {
        GameData data = new GameData();

       
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));

        
        if (File.Exists("gameData.xml"))
        {
            using (StreamReader streamReader = new StreamReader("gameData.xml"))
            {
                data = serializer.Deserialize(streamReader) as GameData;
            }
        }

        return data;
    }

    void SpawnTurrets(List<Vector3> turretPositions)
    {
        foreach (Vector3 position in turretPositions)
        {
            
            GameObject turret = Instantiate(turretPrefab, position, Quaternion.identity);
            turrets.Add(turret);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            
            GameObject turret = Instantiate(turretPrefab, spawnArea.position, Quaternion.identity);
            turrets.Add(turret);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            EnemySpawner.Instance.SetKillCount(0);
            foreach (GameObject turret in turrets)
            {
                Destroy(turret);
            }
            turrets.Clear();
            DeleteFile();
        }
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.transform.gameObject.tag == "Turret")
            {
                Rigidbody2D rb2d = hit.collider.GetComponent<Rigidbody2D>();
                if (rb2d != null)
                {
                    isDragging = true;
                    currentlyDraggedObject = rb2d;
                    offset = rb2d.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            currentlyDraggedObject = null;
        }

        if (isDragging && currentlyDraggedObject != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentlyDraggedObject.MovePosition(mousePosition + offset);
        }
    }

    private void DeleteFile()
    {
        if (File.Exists("gameData.xml"))
        {
            File.Delete("gameData.xml");
            Debug.Log("XML file deleted.");
        }
    }

    void OnApplicationQuit()
    {
        SerializeToXml(gameData);
    }
}

public class BulletScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            int index = EnemySpawner.Instance.GetEnemies().IndexOf(collision.gameObject);
            if (index != -1)
                EnemySpawner.Instance.DeleteEnemy(index);
        }
    }
}