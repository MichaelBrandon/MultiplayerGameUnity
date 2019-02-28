using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class Network : MonoBehaviour {
    static SocketIOComponent socket;
    public GameObject playerPrefab;
    public Spawner spawner;

	// Use this for initialization
	void Start () {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected );
        socket.On("talkback", OnTalkBack);
        socket.On("spawn", OnSpawn);
        socket.On("move", OnMove);
        socket.On("disconnected", OnDisconnected);
        socket.On("registered", OnRegistered);
        socket.On("updatePosition", OnUpdatePosition);
    }

    private void OnUpdatePosition(SocketIOEvent obj)
    {
        Debug.Log("Updating positions " + obj.data);

        var v = float.Parse(obj.data["v"].ToString().Replace("\"", ""));
        var h = float.Parse(obj.data["h"].ToString().Replace("\"", ""));
        var player = spawner.FindPlayer(obj.data["id"].ToString());
        var playerMover = player.GetComponent<PlayerMovementNetwork>();

        playerMover.h = h;
        playerMover.v = v;
    }

    private void OnRegistered(SocketIOEvent obj)
    {
        Debug.Log("Registered Player" + obj.data);
        spawner.AddPlayer(obj.data["id"].ToString(), spawner.localPlayer);
    }

    private void OnDisconnected(SocketIOEvent obj)
    {
        Debug.Log("Player disconnected " + obj.data);

        var id = obj.data["id"].ToString();

        spawner.RemovePlayer(id);
    }

    private void OnMove(SocketIOEvent obj)
    {
        //Debug.Log("Player Moving" + obj.data);
        var id = obj.data["id"].ToString();
        //Debug.Log("Player ID: " + id);

        

        var player = spawner.FindPlayer(id);

        var playerMover = player.GetComponent<PlayerMovementNetwork>();

        
       
        
    }

    private void OnSpawn(SocketIOEvent obj)
    {
        Debug.Log("Player Spawned" + obj.data);
        var player = spawner.SpawnPlayer(obj.data["id"].ToString());

        //spawn existing players at location
    }

    private void OnTalkBack(SocketIOEvent obj)
    {
        Debug.Log("The Server says Hello Back");
    }

    private void OnConnected(SocketIOEvent obj)
    {
        Debug.Log("Connected to Server");
        socket.Emit("sayhello");
        //socket.Emit("move");
    }

    static public void Move(float currentPosV, float currentPosH)
    {
        //Debug.Log("Send Position to Server" + VectorToJson(currentPosV, currentPosH));
        socket.Emit("move", new JSONObject(VectorToJson(currentPosV, currentPosH)));
        
    }

    public static string VectorToJson(float dirV, float dirH)
    {
        return string.Format(@"{{""v"":""{0}"",""h"":""{1}""}}", dirV, dirH);
    }
}
