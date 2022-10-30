using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    private int lives;
    private int count;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    public float speed = 5;
    public float jumpPower = 150;
    public Animator anim;
    private bool facingRight = true;
    public LayerMask groundLayer;
    public Transform groundCheckCollider;
    const float groundCheckRadius = 0.2f;
    private bool Grounded = false;
    private bool Running = false;
    bool jump = false;
    private float runSpeed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        rb = GetComponent<Rigidbody2D>();
        count = 0;

        rb = GetComponent<Rigidbody2D>();
        lives = 3;

        SetCountText();
        WinTextObject.SetActive(false);

        SetCountText();
        LoseTextObject.SetActive(false);

        Audio.instance.PlayMusic("background");
    }

    void FixedUpdate()
    {
        float hozVal = Input.GetAxis("Horizontal");
        float vertVal = Input.GetAxis("Vertical");
        Move(hozVal, jump);
        GroundCheck();

        if (facingRight == false && hozVal >0)
        {
            Flip();
        }
        else if (facingRight == true && hozVal <0)
        {
            Flip();
        }

        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }
    private void Move(float dir,bool jumpFlag)
    {
        if (Grounded && jumpFlag)
        {
            Grounded = false;
            jumpFlag = false;
            rb.AddForce(new Vector2(0f, jumpPower));
        }

        float xVal = dir * speed;
        if (Running)
            xVal *= runSpeed;
        Vector2 targetVel = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVel;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            WinTextObject.SetActive(true);
            Destroy (gameObject);
            Audio.instance.StopMusic("win");
        }
        countText.text = "Count: " + count.ToString();
        if (count == 4)
        {
            lives = 3;
            transform.position = new Vector2 (100f, 0.5f);
        }
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy (gameObject);
            Audio.instance.StopMusic("lose");
        }
    }

    private void GroundCheck()
    {
        Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            Grounded = true;
        }

        anim.SetBool("Jump", !Grounded);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            Running = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            Running = false;

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Jump", true);
            jump = true;
            Audio.instance.PlaySFX("jumping");
        }
        else if(Input.GetButtonUp("Jump"))
            jump = false;
        
        anim.SetFloat("yVelocity", rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            count += 1;
            SetCountText();
            Destroy (collision.collider.gameObject);
            Audio.instance.PlaySFX("collect");
        }

        if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
            Audio.instance.PlaySFX("hurt");
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
