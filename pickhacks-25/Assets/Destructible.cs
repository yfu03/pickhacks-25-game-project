using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Explosion")
        {
            Destroy(gameObject);
        }

    }
}
