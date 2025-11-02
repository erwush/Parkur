using UnityEngine;

public class SafeArea : MonoBehaviour
{
    public Collider2D triggerArea;
    public PlayerHealth healthScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerArea = GetComponent<Collider2D>();
        healthScript = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            healthScript.safeArea = transform;
        }
    }
}
