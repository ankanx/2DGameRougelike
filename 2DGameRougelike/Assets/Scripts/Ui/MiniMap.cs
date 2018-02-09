using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    public Transform Player;

    void Awake()
    {
        this.enabled = false;
    }

    private void LateUpdate()
    {

            Vector3 newPosition = Player.position;
            newPosition.z = -30;
            transform.position = newPosition;
        


        // Add buttons that changes projecttion size for zoom etc
    }

}
