using UnityEngine;
using System.Collections;

public class InputScript : MonoBehaviour
{

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public Canvas menu;

	public bool onLadder;
	public float climbSpeed;
	private float climbVelocity;
	private float gravityStore;


    private bool grounded = true;
    private Animator anim;
    private Rigidbody2D rb2d;

    //public PlayerHealth playerHp;

    public AudioClip audio_jump;
    public AudioClip audio_walk;
    public AudioClip audio_ladder;

    public AudioSource audio_source;


    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
		gravityStore = rb2d.gravityScale;
       // playerHp = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));


        if (grounded)
        {
            
            anim.SetBool("jumpcomplete", true);
        }
        else
        {
            anim.SetBool("jumpcomplete", false);
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

        if(Input.GetButtonDown("Menu") && !menu.gameObject.active)
        {
            menu.gameObject.SetActive(true);
        }else if(Input.GetButtonDown("Menu") && menu.gameObject.active)
        {
            menu.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis ("Vertical");

        anim.SetFloat("Speed", Mathf.Abs(h));
        /*
        if ((Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)  && grounded)
          {
          rb2d.velocity = new Vector2(0, 0);
          
        }*/


        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (v * rb2d.velocity.y < maxSpeed)
            rb2d.AddForce(Vector2.up * v * moveForce);

        if (Mathf.Abs(rb2d.velocity.y) > maxSpeed)
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Sign(rb2d.velocity.y) * maxSpeed);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            //audio_source.PlayOneShot(audio_jump, 0.5f);
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

		if (onLadder) {

            climbVelocity = climbSpeed * v;
			if (h != 0) {
				rb2d.gravityScale = gravityStore;
			} else {
				if (climbVelocity != 0) {
                    //audio_source.PlayOneShot(audio_ladder, 0.1f);
                    rb2d.gravityScale = 0f;
					rb2d.velocity = new Vector2 (rb2d.velocity.x, climbVelocity);
				}
			}
		}
		if (!onLadder) {
			rb2d.gravityScale = gravityStore;
		}
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cookie")){
            Destroy(collision.gameObject);
            //playerHp.GainLife(15);
        }
    }
}