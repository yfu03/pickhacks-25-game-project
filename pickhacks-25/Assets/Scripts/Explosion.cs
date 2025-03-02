using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] CircleCollider2D circleCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Destructable")
        {
            UnityEngine.Debug.Log("destroy these walls");
        }

        if (collision.tag == "Sand")
        {
            UnityEngine.Debug.Log("rsdzgdfhdfhwalls");
        }
    }
}
