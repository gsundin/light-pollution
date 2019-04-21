using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public float speed;
    public float jumpForce;
    private float moveInput;
    private float jumps;
    [SerializeField]
    private float maxJumps;

    private int pecks;

    private float lastY;
    private float currentY;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    // For objects player comes into contact with
    private SpriteRenderer otherSprite;
    private Light otherLight;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumps = 0;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jumps < (maxJumps - 1))
        {
            rb.velocity = Vector2.up * jumpForce;
            SoundManager.Instance.Play("flap");
            jumps++;
        }
        else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jumps < maxJumps)
        {
            rb.velocity = Vector2.up * jumpForce;
            SoundManager.Instance.Play("last_flap");
            jumps++;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Peck();
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");

        lastY = rb.position.y;

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == true && moveInput > 0)
        {
            Flip();
        } else if (facingRight == false && moveInput < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.position.y <= lastY)
        {
            jumps = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SpriteRenderer>() != null)
        {
            otherSprite = other.gameObject.GetComponent<SpriteRenderer>();
        }

        if (other.GetComponentInChildren<Light>() != null)
        {
            otherLight = other.GetComponentInChildren<Light>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        otherSprite = null;
    }

    void Peck()
    {
        if (otherSprite != null)
        {
            string name = otherSprite.sprite.name;

            string[] split = name.Split('_');

            if (split[0].Contains("neon") && split[1].Equals("lit"))
            {
                SoundManager.Instance.Play("glass_break");
            }
            else if (split[0].Contains("window"))
            {
                SoundManager.Instance.Play("tap");
            }

            if (split[1].Equals("lit"))
            {
                Sprite darkSprite = Resources.Load("Sprites/" + split[0] + "_dark", typeof(Sprite)) as Sprite;
                otherSprite.sprite = darkSprite;
                //otherLight.enabled = false;
                pecks++;
            }
        }

        if (pecks == Globals.LIGHTS_PER_LEVEL[0])
        {

            Invoke("nothing", 0.3f);
            SoundManager.Instance.Play("party_whistle");
            // ADVANCE TO NEXT LEVEL
        }
    }

    void nothing()
    {

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    float RoundUp(float f)
    {
        if (f > 0) { f = 1; }
        else if (f < 0) { f = -1; }
        else { f = 0; }

        return f;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            throw new System.Exception("An instance of this singleton already exists.");
        }
        else
        {
            Instance = this;
        }
    }
}
