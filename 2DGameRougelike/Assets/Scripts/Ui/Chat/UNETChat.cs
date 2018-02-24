using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class UNETChat : Chat
{
	//just a random number
	private const short chatMessage = 131;
    private InputField inputfield;
    public Rigidbody2D playerRigidbody;


    private void Update()
    {
        if (!inputfield.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                inputfield.Select();
                playerRigidbody.bodyType = RigidbodyType2D.Static;
            }else
            {
                playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
            }
            
        }

        if (inputfield.isFocused)
        {
            playerRigidbody.bodyType = RigidbodyType2D.Static;
        }

        // Maybe Change
        if (Input.GetKeyDown(KeyCode.Return) && inputfield.text.Length > 0)
        {
            SendMessage(inputfield);
            EventSystem.current.SetSelectedGameObject(null);
        }
        

    }

    private void Start()
	{

        inputfield = gameObject.GetComponentInChildren<InputField>();

        //if the client is also the server
        if (NetworkServer.active) 
		{
			//registering the server handler
			NetworkServer.RegisterHandler(chatMessage, ServerReceiveMessage);
            

        }

		//registering the client handler
		NetworkManager.singleton.client.RegisterHandler (chatMessage, ReceiveMessage);
	}


    private void ReceiveMessage(NetworkMessage message)
	{
		//reading message
		string text = message.ReadMessage<StringMessage> ().value;

		AddMessage(text);
	}

	private void ServerReceiveMessage(NetworkMessage message)
	{
		StringMessage myMessage = new StringMessage ();
        ChatMessage deserialised = JsonConvert.DeserializeObject<ChatMessage> (message.ReadMessage<StringMessage>().value);
        if (deserialised.IsBroadCast)
        {
            myMessage.value = deserialised.SenderName + " has connected.";
        }else
        {
            myMessage.value = "[" + deserialised.SenderName + "]: " + deserialised.Message;
        }
        //we are using the connectionId as player name only to exemplify
        //myMessage.value = message.conn.connectionId + ": " + message.ReadMessage<StringMessage> ().value;

        //sending to all connected clients
        NetworkServer.SendToAll (chatMessage, myMessage);
	}

	public override void SendMessage (UnityEngine.UI.InputField input)
	{
        if (input.text.Length > 0)
        {
            StringMessage myMessage = new StringMessage();
            //getting the value of the input
            ChatMessage newMessage = new ChatMessage(Network.player.externalIP,GameObject.FindGameObjectWithTag("NetworkSpawning").GetComponent<PlayerConnectionObject>().PlayerName, input.text,false);
            myMessage.value = JsonConvert.SerializeObject(newMessage);

            //sending to server
            NetworkManager.singleton.client.Send(chatMessage, myMessage);
            input.text = "";
        }
	}
}