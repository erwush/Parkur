using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public float windForce;
    public float windDuration;
    public Collider2D triggerArea;
    public float maxForce;
    public float forceStrength;
    public float Damping;
    public float windDelay;
    public PlayerMovement movementScript;
    public enum WindType { Continous, Single, Rising }
    public enum WindDirection { Left, Up, Right, Down }
    public WindType windType;
    public WindDirection windDirection;
    public Rigidbody2D pRb;
    public bool isBlowing = true;
    public Vector2 windVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Damping = windForce * 1.5f;
        forceStrength = windForce;
        triggerArea = GetComponent<Collider2D>();
        movementScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        pRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        if (windDirection == WindDirection.Left)
        {
            windVector = Vector2.left;
        }
        else if (windDirection == WindDirection.Up)
        {
            windVector = Vector2.up;
        }
        else if (windDirection == WindDirection.Right)
        {
            windVector = Vector2.right;
        }
        else if (windDirection == WindDirection.Down)
        {
            windVector = Vector2.down;
        }
    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && windType == WindType.Continous)
        {
            Debug.Log("Continous");
            movementScript.speed = movementScript.defaultSpd * 0.5f;
            pRb.AddForce(windVector * (forceStrength * 10), ForceMode2D.Force);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isBlowing = true;
        if(collision.tag == "Player" && windType == WindType.Single)
        {
            StartCoroutine(WindBlow());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pRb.linearDamping = 0f;
            movementScript.speedReset();
            StopCoroutine(WindBlow());
            isBlowing = false;
            Debug.Log("Exit");
        }
    }

    public IEnumerator WindBlow()
    {

        while (isBlowing)
        {
            yield return new WaitForSeconds(windDelay);
            Debug.Log("wuuush");
            pRb.AddForce(windVector * (forceStrength * 1000), ForceMode2D.Force);
        }

    }
}
