using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class LightSwitch : NetworkBehaviour
{
    const short MyBeginMsg = 1002;
    NetworkClient m_client;
    public bool isOn;
    public bool dirty;
    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        NetworkServer.Listen(4444);
    }

    public void FlipSwitch()
    {
        isOn = !isOn;
        dirty = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (dirty)
        {

            if (isOn)
            {
                IntegerMessage msg = new IntegerMessage(1); //turn on
                NetworkServer.SendToAll(942, msg);
            }
            else
            {
                IntegerMessage msg = new IntegerMessage(0);//turn off
                NetworkServer.SendToAll(942, msg);
            }

            dirty = false;
        }
    }
    public void SendReadyToBeginMessage(int myId)
    {
        IntegerMessage msg = new IntegerMessage(myId);
        m_client.Send(MyBeginMsg, msg);
    }

    public void Init(NetworkClient client)
    {
        m_client = client;
        NetworkServer.RegisterHandler(MyBeginMsg, OnServerReadyToBeginMessage);
    }

    void OnServerReadyToBeginMessage(NetworkMessage netMsg)
    {
        IntegerMessage beginMessage = netMsg.ReadMessage<IntegerMessage>();
        Debug.Log("received OnServerReadyToBeginMessage " + beginMessage.value);
    }

}
