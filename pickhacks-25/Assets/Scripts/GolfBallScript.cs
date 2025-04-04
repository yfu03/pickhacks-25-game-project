using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GolfBallScript : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject confetti;
    [SerializeField] private TilemapCollider2D wall;

    [SerializeField] public float maxPower = 15f;
    [SerializeField] public float putPower = 7f;
    [SerializeField] public float explosionPower = 4f;
    [SerializeField] public float goalSpeed = 6f;


    private bool dragClicking;
    private bool inHole;

    private float defaultDrag = 1.2f;

    private Camera Camera;
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isMoving())
        {
            lr.positionCount = 0;
            return;
        }
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        float distanceBetween = Vector2.Distance(transform.position, mousePosition);
        Vector3 direction = transform.position - mousePosition;

        if (distanceBetween <= 0.3f && Input.GetMouseButtonDown(0))
        {
            dragClicking = true;
            lr.positionCount = 2;
        }

        if (dragClicking && Input.GetMouseButton(0))
        {
            drawLine(mousePosition);
        }

        if (dragClicking && Input.GetMouseButtonUp(0))
        {
            float distance = Vector3.Distance(transform.position, mousePosition);
            dragClicking = false;

            if (distance < 0.8f)
                return;

            GameManager.gamemanager.increaseStrokes();

            rb.velocity = Vector3.ClampMagnitude((direction * putPower), maxPower);
            GameManager.gamemanager.playPuttSound();
        }

        //Rotates the ball to face the direction it is moving
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, rb.velocity);
        transform.rotation = toRotation;
    }

    private bool isMoving()
    {
        return rb.velocity.magnitude > 0.2f;
    }

    private void drawLine(Vector3 mousePosition)
    {
        Vector3 direction = mousePosition - transform.position;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + Vector3.ClampMagnitude((direction * putPower), maxPower));

        Vector3 lineLength = Vector3.ClampMagnitude((direction * putPower), maxPower) - transform.position;
        float lineLengthC = getLineLength(transform.position, transform.position + Vector3.ClampMagnitude((direction * putPower), maxPower));
        //UnityEngine.Debug.Log(transform.position);
        //UnityEngine.Debug.Log("start: " + transform.position + " end: " + (transform.position + Vector3.ClampMagnitude((direction * putPower), maxPower)));
        //UnityEngine.Debug.Log("line length: " + lineLengthC);
        changeLineColor(lineLengthC);
    }

    private float getLineLength(Vector3 origin, Vector3 point)
    {
        float xValue = Mathf.Abs(origin.x) - Mathf.Abs(point.x);
        float yValue = Mathf.Abs(origin.y) - Mathf.Abs(point.y);
        //UnityEngine.Debug.Log(Mathf.Abs(origin.y) + " - " + Mathf.Abs(point.y));
        return Mathf.Sqrt(Mathf.Pow(xValue, 2) + Mathf.Pow(yValue, 2));
    }

    private void changeLineColor(float length)
    {
        if (length <= 2.5f)
        {
            lr.startColor = UnityEngine.Color.red;
            lr.endColor = UnityEngine.Color.red;
        }

        else if (length > 2.5f && length < 6.0f)
        {
            lr.startColor = UnityEngine.Color.blue;
            lr.endColor = UnityEngine.Color.blue;
        }
        else
        {
            lr.startColor = UnityEngine.Color.green;
            lr.endColor = UnityEngine.Color.green;
        }

        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Goal")
        {
            if (inHole)
                return;
            if (rb.velocity.magnitude <= goalSpeed)
            {
                inHole = true;
                rb.velocity = Vector3.zero;
                gameObject.SetActive(false);

                GameObject winConfetti = Instantiate(confetti, transform.position, Quaternion.identity);
                Destroy(winConfetti, 3f);

                GameManager.gamemanager.completeHole();
                GameManager.gamemanager.playHoleSound();
            }
        }

        if (collision.tag == "Explosion")
        {
            //UnityEngine.Debug.Log("EXPLOSION TOUCHED BALL");
            Vector3 explosionPosition = collision.transform.position;
            UnityEngine.Debug.Log(explosionPosition);
            UnityEngine.Debug.Log(transform.position);

            explosionVelocity(explosionPosition);
            collision.enabled = false;
        }

        if (collision.tag == "Spring")
        {
            UnityEngine.Debug.Log("spring");
            float angleRadians = collision.transform.rotation.eulerAngles[2] * Mathf.PI / 180f;
            Vector2 bounceForce = new Vector2(maxPower * Mathf.Sin(-angleRadians), maxPower * Mathf.Cos(-angleRadians));
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + bounceForce, maxPower);
            GameManager.gamemanager.playSpringSound();
        }
    }

    private void explosionVelocity(Vector3 explosionPosition)
    {
        //UnityEngine.Debug.Log
        Vector3 direction = transform.position - explosionPosition;
        float distanceFromExplosionOrigin = getLineLength(transform.position, explosionPosition);
        UnityEngine.Debug.Log("distance: " + distanceFromExplosionOrigin);
        if (distanceFromExplosionOrigin < 0.2f) { distanceFromExplosionOrigin = 0.2f; }
        rb.velocity = Vector3.ClampMagnitude((direction * putPower), maxPower);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Sand")
        {
            rb.drag = 6.5f;
        }

        if (collision.tag == "Pad")
        {
            rb.drag = 0.5f;
            float angleRadians = collision.transform.rotation.eulerAngles[2] * Mathf.PI / 180f;
            Vector2 accForce = new Vector2((0.4f) * Mathf.Sin(-angleRadians), (0.4f) * Mathf.Cos(-angleRadians));
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + accForce, (maxPower / 2f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Sand")
        {
            //UnityEngine.Debug.Log("leaving sand!!!!!!!");
            rb.drag = defaultDrag;
        }

        if (collision.tag == "Pad")
        {
            rb.drag = defaultDrag;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Spring")
            GameManager.gamemanager.playBounceSound();

    }
}
