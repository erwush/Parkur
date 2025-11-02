using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public bool iFrame;
    public Transform checkPoint;
    public Rigidbody2D rb;
    public BarController healthBar;
    public SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.UpdateBar(health, maxHealth);

    }


    //* Function Group
    //Function for changing Health
    public void HealthChange(float amount)
    {
        if (!iFrame)
        {
            health += amount;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        //go to checkpoint
        if (health <= 0f)
        {
            // StartCoroutine(Died());
            rb.linearVelocity = Vector2.zero;
            health = maxHealth;
            transform.position = checkPoint.position;
        }


    }


    //* Coroutine

    //Coroutine for Died
    // public IEnumerator Died()
    // {

    // }
}
