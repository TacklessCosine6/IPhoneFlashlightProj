using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using Vuforia;

public class LightBulb : MonoBehaviour
{
    NetworkClient client;
    public Light light;
    public bool LightOn;
    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        light = GetComponent<Light>();
        client = new NetworkClient();
        client.RegisterHandler(942, UpdateVars);
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.Connect("127.0.0.1", 4444);
    }
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
    void UpdateVars(NetworkMessage netMsg)
    {
        int Msg = netMsg.ReadMessage<IntegerMessage>().value;
        bool mode = false;
        switch (Msg)
        {
            case 1:
                mode = true;
                break;
            default:
                mode = false;
                break;
        }
        lightMode(mode);
    }

    void lightMode(bool isOn)
    {
        if (isOn)
        {
            CameraDevice.Instance.SetFlashTorchMode(true);
            light.enabled = true;
        }
        else
        {
            CameraDevice.Instance.SetFlashTorchMode(false);
            light.enabled = false;
        }
    }
    
}
