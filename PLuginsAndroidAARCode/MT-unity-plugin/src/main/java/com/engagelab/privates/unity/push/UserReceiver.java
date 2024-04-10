package com.engagelab.privates.unity.push;

import android.content.Context;

import com.engagelab.privates.common.component.MTCommonReceiver;
import com.engagelab.privates.push.api.AliasMessage;
import com.engagelab.privates.push.api.CustomMessage;
import com.engagelab.privates.push.api.NotificationMessage;
import com.engagelab.privates.push.api.PlatformTokenMessage;
import com.engagelab.privates.push.api.TagMessage;

/**
 * This class echoes a string called from JavaScript.
 */
public class UserReceiver extends MTCommonReceiver {

    private static final String TAG = "UserReceiver";

    /**
     * 应用通知开关状态回调
     *
     * @param context 不为空
     * @param enable  通知开关是否开，true为打开，false为关闭
     */
    @Override
    public void onNotificationStatus(Context context, boolean enable) {
        MTPushBridge.logD(TAG, "onNotificationStatus:" + enable);
        MTPushBridge.onCommonReceiver("onNotificationStatus", MsgToJson.booleanToJson(enable));
    }

    /**
     * 长连接状态回调
     *
     * @param context 不为空
     * @param enable  是否连接
     */
    @Override
    public void onConnectStatus(Context context, boolean enable) {
        MTPushBridge.logD(TAG, "onConnectState:" + enable);
        MTPushBridge.onCommonReceiver("onConnectStatus", MsgToJson.booleanToJson(enable));
    }

    /**
     * 通知消息到达回调
     *
     * @param context             不为空
     * @param notificationMessage 通知消息
     */
    @Override
    public void onNotificationArrived(Context context, NotificationMessage notificationMessage) {
        MTPushBridge.logD(TAG, "onNotificationArrived:" + notificationMessage.toString());
        MTPushBridge.onCommonReceiver("onNotificationArrived", MsgToJson.notificationMessageToJson(notificationMessage));
    }

    /**
     * 通知消息点击回调
     *
     * @param context             不为空
     * @param notificationMessage 通知消息
     */
    @Override
    public void onNotificationClicked(Context context, NotificationMessage notificationMessage) {
        MTPushBridge.logD(TAG, "onNotificationClicked:" + notificationMessage.toString());
        MTPushBridge.onCommonReceiver("onNotificationClicked", MsgToJson.notificationMessageToJson(notificationMessage));
    }

    /**
     * 通知消息删除回调
     *
     * @param context             不为空
     * @param notificationMessage 通知消息
     */
    @Override
    public void onNotificationDeleted(Context context, NotificationMessage notificationMessage) {
        MTPushBridge.logD(TAG, "onNotificationDeleted:" + notificationMessage.toString());
        MTPushBridge.onCommonReceiver("onNotificationDeleted", MsgToJson.notificationMessageToJson(notificationMessage));
    }

    /**
     * 自定义消息回调
     *
     * @param context       不为空
     * @param customMessage 自定义消息
     */
    @Override
    public void onCustomMessage(Context context, CustomMessage customMessage) {
        MTPushBridge.logD(TAG, "onCustomMessage:" + customMessage.toString());
        MTPushBridge.onCommonReceiver("onCustomMessage", MsgToJson.customMessageToJson(customMessage));
    }

    /**
     * 厂商token消息回调
     *
     * @param context              不为空
     * @param platformTokenMessage 厂商token消息
     */
    @Override
    public void onPlatformToken(Context context, PlatformTokenMessage platformTokenMessage) {
        MTPushBridge.logD(TAG, "onPlatformToken:" + platformTokenMessage.toString());
        MTPushBridge.onCommonReceiver("onPlatformToken", MsgToJson.platformTokenMessageToJson(platformTokenMessage));
    }


    @Override
    public void onTagMessage(Context context, TagMessage tagMessage) {
        MTPushBridge.logD(TAG,"onTagMessage:" + tagMessage.toString());
        MTPushBridge.onCommonReceiver("OnTagOperateResult", MsgToJson.tagMessageToJson(tagMessage));
    }

    @Override
    public void onAliasMessage(Context context, AliasMessage aliasMessage) {
        MTPushBridge.logD(TAG,"onAliasMessage:" + aliasMessage.toString());
        MTPushBridge.onCommonReceiver("OnAliasOperateResult", MsgToJson.aliasMessageToJson(aliasMessage));
    }

}

