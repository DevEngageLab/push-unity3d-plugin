package com.engagelab.privates.unity.push;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.LinkedHashSet;
import java.util.Set;

import com.google.gson.Gson;

public class JsonUtil {
    public static Set<String> jsonToSet(String tagsJsonStr) {
        Set<String> tagSet = new LinkedHashSet<String>();

        try {
            JSONObject itemsJsonObj = new JSONObject(tagsJsonStr);
            JSONArray tagsJsonArr = itemsJsonObj.getJSONArray("Items");

            for (int i = 0; i < tagsJsonArr.length(); i++) {
                tagSet.add(tagsJsonArr.getString(i));
            }
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return tagSet;
    }


//    {"Items":["111","222"]}

    public static String setToJson(Set<String> tagSet) {
        if (null == tagSet || tagSet.size() == 0) {
            return null;
        }

        JSONObject itemsJsonObj = new JSONObject();
        JSONArray tagsJsonArr = new JSONArray();

        for (String tag : tagSet
        ) {
            tagsJsonArr.put(tag);
        }
        try {
            itemsJsonObj.put("Items", tagsJsonArr);
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return itemsJsonObj.toString();
    }

    public static String[] jsonToArr(String jsonString) {
        Gson gson = new Gson();
        String[] stringArray = gson.fromJson(jsonString, String[].class);
        return stringArray;
    }

}
