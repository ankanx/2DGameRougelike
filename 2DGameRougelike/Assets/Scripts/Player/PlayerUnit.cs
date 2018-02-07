using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// A PlayerUnit is a unit controlled by a player
// This could be a character in an FPS, a zergling in a RTS
// Or a scout in a TBS

public class PlayerUnit : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    Vector3 velocity;

    // The position we think is most correct for this player.
    //   NOTE: if we are the authority, then this will be
    //   exactly the same as transform.position
    Vector3 bestGuessPosition;

    // This is a constantly updated value about our latency to the server
    // i.e. how many second it takes for us to receive a one-way message
    // TODO: This should probably be something we get from the PlayerConnectionObject
    float ourLatency;   

    // This higher this value, the faster our local position will match the best guess position
    float latencySmoothingFactor = 10;

	// Update is called once per frame
	void Update () {

        // How do I verify that I am allowed to mess around with this object?
        if (hasAuthority == false)
        {

            return;
        }



    }


}
