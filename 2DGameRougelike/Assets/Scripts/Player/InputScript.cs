using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class InputScript : NetworkBehaviour
{

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public GameObject menu;

    private Animator animator;
    private Rigidbody2D rigidbody2d;

    public AudioClip audio_jump;
    public AudioClip audio_walk;
    public AudioSource audio_source;


    void Start()
    {

        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public override void OnStartAuthority()
    {
        gameObject.GetComponent<InputScript>().enabled = true;
        transform.gameObject.tag = "Player";
        menu = GameObject.FindGameObjectWithTag("PlayerMenu");
        GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>().enabled = true;
        GameObject.FindGameObjectWithTag("MiniMap").GetComponent<MiniMap>().Player = gameObject.transform;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScriptCameraFollow>().enabled = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScriptCameraFollow>().Player = gameObject;
        GameObject.FindGameObjectWithTag("Chat").GetComponent<UNETChat>().playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && menu.transform.localScale != new Vector3(1, 1, 1))
        {
            menu.transform.localScale = new Vector3(1, 1,1);
        }
        else if(Input.GetButtonDown("Cancel") && menu.transform.localScale != new Vector3(0, 0, 0))
        {
            menu.transform.localScale = new Vector3(0, 0,0);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

        Vector2 current = new Vector2(h, v);

        if(current != Vector2.zero && rigidbody2d.bodyType == RigidbodyType2D.Dynamic)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("input_x", current.x);
            animator.SetFloat("input_y", current.y);

        }
        else
        {
            animator.SetBool("isWalking", false);
        }

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

    }
}