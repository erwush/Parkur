using UnityEngine;

public class BorderController : MonoBehaviour
{

    public PlayerHealth healthScript;
    public Collider2D triggerArea;
    public enum BorderType { LEFT, RIGHT, TOP, BOTTOM };
    public Rigidbody2D pRb;
    [SerializeField] private BorderType borderType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthScript = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        triggerArea = GetComponent<Collider2D>();
        pRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (triggerArea.IsTouchingLayers(LayerMask.GetMask("Player")) && borderType == BorderType.BOTTOM)
        {
            pRb.linearVelocity = Vector2.zero;

            healthScript.HealthChange(-15f);
            if (healthScript.health <= 0f)
            {
                healthScript.ToCheckpoint();
            }
            else
            {
                healthScript.ToSafe();
            }
        }

    }
}
