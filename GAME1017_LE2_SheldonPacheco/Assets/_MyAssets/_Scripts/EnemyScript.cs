using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = -3f;

    void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
    }
}