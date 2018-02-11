using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Linq;

public class ExpandedNetworkManager : NetworkManager {
    /*
    public class connectedPlayer: MonoBehaviour
    {
        public GameObject Player;
        public NetworkConnection conn;
        public connectedPlayer(GameObject _player, NetworkConnection _conn)
        {
            this.Player = _player;
            this.conn = _conn;
        }
    }
    
    public static List<connectedPlayer> connectedPlayers = new List<connectedPlayer>();
    */
    // Maybe?
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
        List<connectedPlayer> MyDisconnectedPlayer = connectedPlayers.Where(p => p.conn.connectionId == conn.connectionId).ToList<connectedPlayer>();
        Debug.Log(MyDisconnectedPlayer[0].conn);
        Debug.Log(connectedPlayers.Count);
        connectedPlayers.Remove(MyDisconnectedPlayer[0]);
        Debug.Log(connectedPlayers.Count);
        NetworkServer.DestroyPlayersForConnection(conn);
    }
    */
}
