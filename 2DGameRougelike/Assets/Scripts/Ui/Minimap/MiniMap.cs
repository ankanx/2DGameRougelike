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

            Vector3 newPosition = new Vector3(Player.position.x, Player.position.y, transform.position.z);
            
            transform.position = newPosition;

    }

    public void zoomIn()
    {
        if (transform.position.z < (-5))
        {
            Vector3 newPosition = transform.position;
            newPosition.z = transform.position.z + 5;
            transform.position = newPosition;
        }
        

    }

    public void zoomOut()
    {
        if(transform.position.z > (-40))
        {
            Vector3 newPosition = transform.position;
            newPosition.z = transform.position.z - 5;
            transform.position = newPosition;
        }
        
    }
 }
    
