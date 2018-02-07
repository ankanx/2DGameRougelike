using UnityEngine;
using System.Collections;

public class PlayerScriptCameraFollow : MonoBehaviour
{

    public GameObject Player;
    public float SmothingSpeed = 0.125f;
    public Vector3 offset;  
    private Camera camera;

    private void Awake()
    {
        this.enabled = false;
    }

    void Start()
    {
        camera = GetComponent<Camera>();
        offset = transform.position;// - Player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void FixedUpdate()
    {


            camera.orthographicSize = (Screen.height / 50f) / 4f;
            Vector3 desiredPosition = Player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmothingSpeed);
            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = smoothedPosition;

            // transform.LookAt(Player.transform); activate for 3D
        

    }
}