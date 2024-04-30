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
        MTPushBinding.ConfigDebugMode(true);
        #if UNITY_ANDROID
            MTPushBinding.InitMTPushAndroid(gameObject.name);
        #endif

        #if UNITY_IOS
            MTPushBinding.SetSiteName("Singapore");
            MTPushBinding.InitMTPushIos(gameObject.name,"fcc545917674d6f06c141704",false,"demo",false);
            // MTPushBinding.InitMTPushIos(gameObject.name,"您的appkey",false,"demo",false);
        #endif
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
        
         if (GUILayout.Button(" ", GUILayout.Height(80)))
        {
            Debug.Log("------>Button: ");
        }

        str_unity = GUILayout.TextField(str_unity, GUILayout.Width(Screen.width),
        GUILayout.Height(200));


        if (GUILayout.Button("getRegistrationId", GUILayout.Height(80)))
        {
            string registrationId = MTPushBinding.GetRegistrationId();
            Debug.Log("------>registrationId: " + registrationId);
            str_unity = "registrationId: "+registrationId;
        }

        if (GUILayout.Button("setTags", GUILayout.Height(80)))
        {
            List<string> tags = new List<string> ();
            tags.Add("111");
            tags.Add("222");
			MTPushBinding.SetTags(callbackId++, tags);
        }

        if (GUILayout.Button("setAlias", GUILayout.Height(80)))
        {
            MTPushBinding.SetAlias(2, "replaceYourAlias");
        }

        if (GUILayout.Button("addTags", GUILayout.Height(80)))
        {
            List<string> tags = new List<string>(){"addtag1", "addtag2"};
            MTPushBinding.AddTags(callbackId++, tags);
        }

        if (GUILayout.Button("deleteTags", GUILayout.Height(80)))
        {
            List<string> tags = new List<string>();
            tags.Add("addtag1");
            tags.Add("addtag2");

            MTPushBinding.DeleteTags(callbackId++, tags);
        }

        if (GUILayout.Button("cleanTags", GUILayout.Height(80)))
        {
            MTPushBinding.CleanTags(callbackId++);
        }

        if (GUILayout.Button("get all tags", GUILayout.Height(80)))
        {
            MTPushBinding.GetAllTags(callbackId++);
        }

        if (GUILayout.Button("getAlias", GUILayout.Height(80)))
        {
            MTPushBinding.GetAlias(callbackId++);
            Debug.Log("Alias 将在 onMTReceiver 中回调");
        }

        if (GUILayout.Button("check tag is binding", GUILayout.Height(80)))
        {
            MTPushBinding.CheckTagBindState(callbackId++,"addtag1");
            Debug.Log("Alias 将在 onMTReceiver 中回调");
        }

        if (GUILayout.Button("OpenSettingsForNotification", GUILayout.Height(80)))
        {
            #if UNITY_ANDROID
                MTPushBinding.GoToAppNotificationSettingsAndroid();
            #endif

            #if UNITY_IOS
                MTPushBinding.OpenSettingsForNotificationIOS();
            #endif
        }

        if (GUILayout.Button("set badge : 5", GUILayout.Height(80)))
        {
            MTPushBinding.SetNotificationBadge(5);
        }

        if (GUILayout.Button("reset badge", GUILayout.Height(80)))
        {
            MTPushBinding.ResetNotificationBadge();
        }

        if (GUILayout.Button("get notification status - IOS", GUILayout.Height(80)))
        {
            #if UNITY_IOS
            MTPushBinding.GetNotificationAuthorizationIOS();
            #endif
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
