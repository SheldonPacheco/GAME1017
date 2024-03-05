using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public Color Tint;
    public Vector2 Direction;
    public float Rotation;
    [SerializeField] float minRotation;
    [SerializeField] float maxRotation;
    [SerializeField] float tintAmount;
    [SerializeField] AsteroidData data;
    private GameObject child;
    public int currentPhase = 0;
    public int CurrentPhase
    {
        get { return currentPhase; }
        set { currentPhase = AsteroidManager.Instance.phase; }
    }

    public AsteroidData AsteroidData
    {
        get { return data; }
        set { data = value; }
    }

    void Start()
    {
        data.ApplyToAsteroid(this);
        child = transform.Find("Asteroid").gameObject;
       
    }

    void Update()
    {
        transform.Translate(Direction * Time.deltaTime);
        child.transform.Rotate(0f, 0f, Rotation * Time.deltaTime);

        if (transform.position.x < -8.5f)
        {
            transform.position = new Vector2(8.5f, transform.position.y);
        }
        else if (transform.position.x > 8.5f)
        {
            transform.position = new Vector2(-8.5f, transform.position.y);
        }
        if (transform.position.y < -6.5f)
        {
            transform.position = new Vector2(transform.position.x, 6.5f);
        }
        else if (transform.position.y > 6.5f)
        {
            transform.position = new Vector2(transform.position.x, -6.5f);
        }
    }

    public void InitializeAsteroid(AsteroidData data)
    {
        this.data = data;
        data.ApplyToAsteroid(this);
    }

    public void SetAsteroidTintColor(GameObject chunk, Color tint)
    {
        this.Tint = tint;
        child.GetComponent<SpriteRenderer>().color = tint;
    }
}