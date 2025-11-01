using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float health = 100f;
    private float facingDirection = 1;
    [SerializeField] private bool iFrame = false;
    public float staminaRate = 3f;
    public Rigidbody2D rb;
    public float speed = 20f;
    public float defaultSpd = 20f;
    public float maxStamina = 100f;
    public StaminaBar staminaBar;
    public float jumpStrength = 10f;
    private Collider2D plCollider;
    public float stamina = 200f;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;
    [SerializeField] private bool doubleJumped;
    public bool isGrounded;
    [SerializeField] private bool justJumped;
    public float dashStrength;
    public ForceMode2D forceMode2D = ForceMode2D.Force;
    [SerializeField] private bool flashHolding;
    [SerializeField] private float flashAmount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        plCollider = GetComponent<Collider2D>();
    }



    void Update()
    {
        staminaBar.UpdateBar(stamina, 100f);
        // Cek tanah
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);


        // Lompat hanya kalau menyentuh tanah
        if (Input.GetButtonDown("Jump") && isGrounded && doubleJumped == false || Input.GetButtonDown("Jump") && justJumped && doubleJumped == false)
        {
            rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            if (isGrounded)
            {
                justJumped = true;
            }
            else if (justJumped == true && doubleJumped == false)
            {
                justJumped = false;
            }
            else if (justJumped == true && doubleJumped == true)
            {
                if (stamina > 15f)
                {
                    StaminaChange(-15f);
                    Debug.Log("DoubleJumped");
                    justJumped = false;
                    doubleJumped = true;
                }

            }
        }

        if (Input.GetButtonDown("Dash"))
        {
            Dash();

        }


        if (Input.GetButtonDown("Reset"))
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }

        if (Input.GetButtonDown("Flash"))
        {
            flashHolding = true;

        }

        if (Input.GetButton("Flash") && flashHolding)
        {
            StaminaChange(-1f);
            flashAmount += 1f;
        }

        if (flashHolding && Input.GetButtonUp("Flash") || flashHolding && stamina <= 0)
        {

            flashHolding = false;
            FlashReleased(flashAmount);
        }

        if (Input.GetButton("Run"))
        {
            if (stamina > 0f)
            {

                speed = defaultSpd + 5f;
                StaminaChange(-1f);
            }
        }
        else if (Input.GetButton("Sprint"))
        {
            if (stamina >= 3f)
            {
                speed = defaultSpd + 10f;
                StaminaChange(-3f);
            }
        }

        if (Input.GetButtonUp("Run") || Input.GetButtonUp("Sprint"))
        {
            speedReset();
            StartCoroutine(StaminaRecharge());
        }

    }
    public void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

    }

    public void speedReset()
    {
        speed = defaultSpd;
    }

    public void FlashReleased(float consumedAmount)
    {
        StartCoroutine(FlashIFrame());
        float direction = transform.localScale.x;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2((consumedAmount * 500) * direction, 0f), forceMode2D);
        flashAmount = 0f;
        if (stamina < 0f)
        {
            stamina = 0f;
        }
        StartCoroutine(StaminaRecharge());

    }

    public IEnumerator FlashIFrame()
    {
        iFrame = true;
        yield return new WaitForSeconds(0.2f);
        iFrame = false;
    }

    public void Dash()
    {
        if (stamina > 25f)
        {

            StaminaChange(-25f);
            float direction = transform.localScale.x;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2((dashStrength * 100) * direction, 0f), forceMode2D);
        }
    }



    void FixedUpdate()
    {

        // Gerakan kiri-kanan
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveHorizontal * speed, rb.linearVelocity.y);

        //flip
        if (moveHorizontal > 0 && transform.localScale.x < 0 || moveHorizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    //coroutine for recharging stamina


    // public IEnumerator Dash()
    // {
    //     speed *= 2;
    //     iFrame = true;

    //     StaminaChange(-15f);
    //     yield return new WaitForSeconds(0.1f);
    //     speed /= 2;


    //     iFrame = false;
    // }

    public void StaminaChange(float amount)
    {
        stamina += amount;

        if (amount < 0)
        {
            StartCoroutine(StaminaRecharge());
        }

    }

    public IEnumerator StaminaRecharge()
    {
        while (stamina < maxStamina && !flashHolding && !Input.GetButton("Run") && !Input.GetButton("Sprint"))
        {
            StaminaChange(staminaRate);
            yield return new WaitForSeconds(1f);
        }
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        if(stamina < 0f)
        {
            stamina = 0f;
        }
    }





    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
