using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class InputScript : NetworkBehaviour
{

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public Canvas menu;

    private bool grounded = true;
    private Animator animator;
    private Rigidbody2D rigidbody2d;

    public AudioClip audio_jump;
    public AudioClip audio_walk;
    public AudioSource audio_source;



    void Start()
    {

        if (!hasAuthority)
        {
            Debug.Log("No Authority" + hasAuthority);
            transform.gameObject.tag = "Untagged";
        }

        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public override void OnStartAuthority()
    {
        GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>().enabled = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScriptCameraFollow>().enabled = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScriptCameraFollow>().Player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

        if(Input.GetButtonDown("Cancel") && !menu.gameObject.active)
        {
            menu.gameObject.SetActive(true);
        }else if(Input.GetButtonDown("Cancel") && menu.gameObject.active)
        {
            menu.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis ("Vertical");

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("some");
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().AddItem("sword");
        }



        //animator.SetFloat("Speed", Mathf.Abs(h));
        /*
        if ((Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)  && grounded)
          {
          rb2d.velocity = new Vector2(0, 0);
          
        }*/


        if (h * rigidbody2d.velocity.x < maxSpeed)
            rigidbody2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rigidbody2d.velocity.x) > maxSpeed)
            rigidbody2d.velocity = new Vector2(Mathf.Sign(rigidbody2d.velocity.x) * maxSpeed, rigidbody2d.velocity.y);

        if (v * rigidbody2d.velocity.y < maxSpeed)
            rigidbody2d.AddForce(Vector2.up * v * moveForce);

        if (Mathf.Abs(rigidbody2d.velocity.y) > maxSpeed)
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, Mathf.Sign(rigidbody2d.velocity.y) * maxSpeed);

        // Remove when octagonal fix
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}