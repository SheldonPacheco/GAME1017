using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBranch : MonoBehaviour
{
    private void Start()
    {
        
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        PolygonCollider2D PolygonCollider = GetComponentInParent<PolygonCollider2D>();
        if (collision.CompareTag("Pillar"))
        {

            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -2;
            if (PolygonCollider != null)
                 PolygonCollider.isTrigger = true;    
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        PolygonCollider2D PolygonCollider = GetComponentInParent<PolygonCollider2D>();
        if (collision.CompareTag("Pillar"))
        {

            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            if (PolygonCollider != null)
                PolygonCollider.isTrigger = false;
        }
    }
}
