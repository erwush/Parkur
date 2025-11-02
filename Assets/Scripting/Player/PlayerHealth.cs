using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public bool iFrame;
    public Transform checkPoint;
    public BarController healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            health = maxHealth;
            transform.position = checkPoint.position;
        }


    }
}
