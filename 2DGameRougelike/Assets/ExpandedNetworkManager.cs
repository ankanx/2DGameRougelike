using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Linq;

public class ExpandedNetworkManager : NetworkManager {

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var player = (GameObject)GameObject.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        //connectedPlayers.Add(new connectedPlayer(player,conn));
        //Debug.Log("Connected: " + player.GetComponent<PlayerConnectionObject>().PlayerName);
        //StringMessage SpawningPlayersName = new StringMessage();
        //SpawningPlayersName.value = player.GetComponent<PlayerConnectionObject>().PlayerName + " has joined.";
        //NetworkServer.SendToAll(131, SpawningPlayersName);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        
    }
    
    /*
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        
        NetworkServer.DestroyPlayersForConnection(conn);
    }*/
    
}
