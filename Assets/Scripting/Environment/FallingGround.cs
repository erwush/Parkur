using System.Collections;
using UnityEngine;

public class FallingGround : MonoBehaviour
{
    public BoxCollider2D triggerArea;
    public Rigidbody2D rb;
    public float countdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerArea = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(FallCountdown());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StopCoroutine(FallCountdown());
        }
    }


    public IEnumerator FallCountdown()
    {
        yield return new WaitForSeconds(countdown);
        if (triggerArea.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Destroy(gameObject);
        }
    }
}
