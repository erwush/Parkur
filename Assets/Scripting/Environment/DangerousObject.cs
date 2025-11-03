using UnityEngine;

public class DangerousObject : MonoBehaviour
{

    public Collider2D triggerArea;
    public PlayerHealth healthScript;
    public enum ObjectType { Spike, Lava, Fire, Acid };
    public ObjectType objectType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerArea = GetComponent<Collider2D>();
        healthScript = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        if (triggerArea.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            DealDamage();
        }

    }

    void DealDamage()
    {
        if (objectType == ObjectType.Spike)
        {
            healthScript.HealthChange(-10f);
            if (healthScript.health <= 0f)
            {
                healthScript.ToCheckpoint();
            }
            else
            {
                healthScript.ToSafe();
            }
        }
        else if (objectType == ObjectType.Lava)
        {

        }


    }
}
