using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private GameObject explosionObject;
    [SerializeField] private Animator animator;

    private bool exploded;
    private bool placed;
    private bool placingMode;
    private bool destroyTimer;

    private float explosionTime;
    private float explosionLength = 1f;

    private Camera Camera;
    private Vector2 mousePosition;
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        explosionObject = GameObject.Find("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        if(destroyTimer)
        {
            if (Time.time - explosionTime > explosionLength)
                Destroy(gameObject);
        }

        if (placingMode)
        {
            followCursor();
        }

        if (!placed && !placingMode && Input.GetKeyDown(KeyCode.Q))
        {
            placingMode = true;
            circleCollider.enabled = false;
        }

        if (placingMode && Input.GetMouseButton(0))
        {
            placeBomb();
        }

        if(placed && Input.GetKeyDown(KeyCode.Q))
        {
            explodeBomb();
        }
    }

    private void followCursor()
    {
        
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    private void placeBomb()
    {
        placed = true;
        placingMode = false;

        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;

        circleCollider.enabled = true;
        GameManager.gamemanager.useBomb();
    }

    private void explodeBomb()
    {
        explosionObject.GetComponent<SpriteRenderer>().enabled = true;
        explosionObject.GetComponent<CircleCollider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
        animator.SetTrigger("explosion_trigger");
        destroyTimer = true;
        explosionTime = Time.time;
    }
}
