using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidData", menuName = "ScriptableObjects/AsteroidData", order = 1)]
public class AsteroidData : ScriptableObject
{
    [SerializeField] private float asteroidSpeed;
    [SerializeField] private float asteroidRotationSpeed;
    [SerializeField] private Color asteroidTintColor;

    public int CurrentPhase { get; private set; } = 0; 

    public float AsteroidSpeed => asteroidSpeed;
    public float AsteroidRotationSpeed => asteroidRotationSpeed;
    public Color AsteroidTintColor => asteroidTintColor;

    public void ApplyToAsteroid(AsteroidScript asteroidScript)
    {
        asteroidScript.Direction = new Vector2(RandomSign(), RandomSign()).normalized * asteroidSpeed;
        asteroidScript.Rotation = Random.Range(asteroidRotationSpeed, asteroidRotationSpeed);
        asteroidScript.Tint = asteroidTintColor;
        asteroidScript.CurrentPhase = CurrentPhase;
    }

    private int RandomSign()
    {
        return 1 - 2 * Random.Range(0, 2);
    }
}