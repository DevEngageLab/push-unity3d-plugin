using UnityEngine;
using System.Collections;
using MTPush;
using System.Collections.Generic;
using System;

#if UNITY_IPHONE
using LitJson;
#endif

public class MTPluginsDemo : MonoBehaviour
{
    string str_unity = "";
	  int callbackId = 0;

    // Use this for initialization
    void Start()
    {
        gameObject.name = "Main Camera";
        JPushBinding.ConfigDebugMode(true);
        JPushBinding.InitMTPush(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Home))
        {
            Application.Quit();
        }
    }

    void OnGUI()
    {
        str_unity = GUILayout.TextField(str_unity, GUILayout.Width(Screen.width - 80),
        GUILayout.Height(200));


        if (GUILayout.Button("getRegistrationId", GUILayout.Height(80)))
        {
            string registrationId = JPushBinding.GetRegistrationId();
            Debug.Log("------>registrationId: " + registrationId);
            
        }
    }

    /* data format
     {
        "event_name": "",//事件类型
        "event_data": ""//事件内容
     }
     */
    // 回调的消息。
    void onMTReceiver(string jsonStr)
    {
        Debug.Log("recv----onMTReceiver-----" + jsonStr);
        str_unity = jsonStr;
    }
}
