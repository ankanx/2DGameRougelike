using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    public Transform player;

    void Awake()
    {
        this.enabled = false;
    }

    private void LateUpdate()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }else
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            newPosition.z = -30;
            transform.position = newPosition;
        }


        // Add buttons that changes projecttion size for zoom etc
    }

}
