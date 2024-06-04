package com.engagelab.privates.unity.push;

import android.app.Application;
import android.content.Context;

import com.engagelab.privates.core.api.MTCorePrivatesApi;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.InputStreamReader;

public class MTPushApplication extends Application {
    private static final String TAG = "MTPushApplication";
    private static Context mtContext;

    @Override
    public void onCreate() {
        super.onCreate();
        mtContext = getApplicationContext();
        init(mtContext);
    }

    public static void init(Context context) {
        try {
            mtContext = context;
            String mtpush_config = getFromAssets("mt_engagelab_push_config");
            if (null != mtpush_config) {
                MTPushBridge.logD(TAG, "mt_engagelab_push_config:" + mtpush_config);
                JSONObject jsonObject = new JSONObject(mtpush_config);
                setDebug(jsonObject);
                setTcpSSL(jsonObject);
                testGoogle(jsonObject);
            } else {
                MTPushBridge.logD(TAG, "mt_engagelab_push_config is null");
            }
        } catch (Throwable e) {
            e.printStackTrace();
            MTPushBridge.logE(TAG, "init is e:" + e);
        }
    }

    private static void setDebug(JSONObject jsonObject) {
        if (jsonObject.has("debug")){
            boolean debug = jsonObject.optBoolean("debug", false);
            MTCorePrivatesApi.configDebugMode(mtContext,debug);
            MTPushBridge.DEBUG = debug;
        }
    }
    private static void setTcpSSL(JSONObject jsonObject) {
        if (jsonObject.has("tcp_ssl")){
            boolean tcp_ssl = jsonObject.optBoolean("tcp_ssl", false);
            MTCorePrivatesApi.setTcpSSl(tcp_ssl);
        }
    }

    private static void testGoogle(JSONObject jsonObject) {
        if (jsonObject.has("testGoogle")) {
            boolean ret = jsonObject.optBoolean("testGoogle", false);
            if (ret == true) {
                MTCorePrivatesApi.testConfigGoogle(mtContext, true);
            }
        }
    }


    public static String getFromAssets(String fileName) {
        try {
            InputStreamReader inputReader = new InputStreamReader(mtContext.getResources().getAssets().open(fileName));
            BufferedReader bufReader = new BufferedReader(inputReader);
            String line = "";
            String Result = "";
            while ((line = bufReader.readLine()) != null)
                Result += line;
            return Result;
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

}
