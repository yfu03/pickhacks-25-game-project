using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallScript : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject confetti;

    [SerializeField] public float maxPower = 10f;
    [SerializeField] public float putPower = 5f;
    [SerializeField] public float goalSpeed = 6f;

    private bool dragClicking;
    private bool inHole;

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
            
            rb.velocity = Vector3.ClampMagnitude((direction * putPower), maxPower);
        }

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
        UnityEngine.Debug.Log("start: " + transform.position + " end: " + (transform.position + Vector3.ClampMagnitude((direction * putPower), maxPower)));
        UnityEngine.Debug.Log("line length: " + lineLengthC);
        changeLineColor(lineLengthC);
    }

    private float getLineLength(Vector3 origin, Vector3 point)
    {
        float xValue = Mathf.Abs(origin.x) - Mathf.Abs(point.x);
        float yValue = Mathf.Abs(origin.y) - Mathf.Abs(point.y);
        UnityEngine.Debug.Log(Mathf.Abs(origin.y) + " - " + Mathf.Abs(point.y));
        return Mathf.Sqrt(Mathf.Pow(xValue, 2) + Mathf.Pow(yValue, 2));
    }

    private void changeLineColor(float length)
    {
        if (length <= 1.25f)
        {
            lr.startColor = UnityEngine.Color.red;
            lr.endColor = UnityEngine.Color.red;
        }
        else if(length > 1.25f && length < 3.0f)
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
        if(collision.tag == "Goal")
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
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Goal")
        {

        }
    }
}
