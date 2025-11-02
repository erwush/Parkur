using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public PlayerHealth healthScript;
    public Collider2D triggerArea;
    public Animator anim;
    public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        healthScript = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        triggerArea = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerArea.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            healthScript.checkPoint = transform;
            anim.SetBool("isCheck", true);
            triggerArea.enabled = false;
        }
    }
}
