package com.engagelab.privates.unity.push;

import android.content.Context;
import android.util.Log;

import com.engagelab.privates.core.api.MTCorePrivatesApi;
import com.engagelab.privates.push.api.MTPushPrivatesApi;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;


public class MTPushBridge {
    private static final String TAG = "MTPushBridge";

    private static MTPushBridge mtPushBridge;
    private static List<CommonReceiverCache> onCommonReceiverCache = new ArrayList<CommonReceiverCache>();

    private Context mContext;
    static String gameObject;

    public static MTPushBridge getInstance() {
        if (mtPushBridge == null) {
            mtPushBridge = new MTPushBridge();
        }
        return mtPushBridge;
    }


    public static synchronized void onCommonReceiver(String name, String data) {
        logD(TAG, "onCommonReceiver name =" + name);
        logD(TAG, "onCommonReceiver data =" + data);
        logD(TAG, "onCommonReceiver gameObject =" + gameObject);
        try {
            if (null != gameObject) {
                JSONObject dataJosn = new JSONObject();
                dataJosn.put("event_name", name);
                dataJosn.put("event_data", data);
                UnityPlayer.UnitySendMessage(gameObject, "onMTReceiver", dataJosn.toString());
            } else {
                onCommonReceiverCache.add(new CommonReceiverCache(name, data));
            }
        } catch (Throwable e) {
            e.printStackTrace();
        }
    }

    private static synchronized void sendCommonReceiverCache() {
        if (!onCommonReceiverCache.isEmpty()) {
            logD(TAG, "sendCommonReceiverCache:" + onCommonReceiverCache.size());
            for (CommonReceiverCache c : onCommonReceiverCache) {
                onCommonReceiver(c.getName(), c.getData());
            }
            onCommonReceiverCache.clear();
        }
    }


    private static class CommonReceiverCache {
        private String name;
        private String data;

        public CommonReceiverCache(String name, String data) {
            this.name = name;
            this.data = data;
        }

        public String getName() {
            return name;
        }

        public String getData() {
            return data;
        }
    }


    public void initMTPush(String gameObject) {
        logD(TAG, "initMTPush:" + gameObject);
        MTPushBridge.gameObject = gameObject;
        MTPushPrivatesApi.init(getApplicationContext());
        sendCommonReceiverCache();
    }


    private Context getApplicationContext() {
        if (null == mContext) {
            mContext = UnityPlayer.currentActivity.getApplicationContext();
        }
        return mContext;
    }

    /**
     * 设置心跳时间间隔
     * <p>
     * 需要在Application.onCreate()方法中调用
     *
     * @param context           不为空
     * @param heartbeatInterval 时间单位为毫秒、必须大于0、默认值是4分50秒\
     */
    void configHeartbeatInterval(long heartbeatInterval) {
        Context context = getApplicationContext();
        MTCorePrivatesApi.configHeartbeatInterval(context, heartbeatInterval);
    }


    /**
     * 设置长连接重试次数
     * <p>
     * 需要在Application.onCreate()方法中调用
     *
     * @param context           不为空
     * @param connectRetryCount 重试的次数、默认值为3、最少3次
     */
    void configConnectRetryCount(int connectRetryCount) {
        Context context = getApplicationContext();
        MTCorePrivatesApi.configConnectRetryCount(context, connectRetryCount);
    }

    /**
     * 设置是否debug模式，debug模式会打印更对详细日志
     * <p>
     * 需要在Application.onCreate()方法中调用
     *
     * @param context 不为空
     * @param enable  是否调试模式，true为调试模式，false不是
     */
    void configDebugMode(boolean enable) {
        DEBUG = enable;
        Context context = getApplicationContext();
        logD(TAG, "configDebugMode enable:" + enable);
        MTCorePrivatesApi.configDebugMode(context, enable);
    }

    /**
     * 配置使用国密加密
     *
     * @param context 不为空
     */
    void configSM4() {
        try {
            Context context = getApplicationContext();
            MTCorePrivatesApi.configSM4(context);
        } catch (Throwable e) {
            e.printStackTrace();
        }
    }

    /**
     * 获取当前设备的userId，Engagelab私有云唯一标识，可同于推送
     *
     * @param context 不为空
     * @return userId
     */
    long getUserId() {
        Context context = getApplicationContext();
        long userId = MTCorePrivatesApi.getUserId(context);
        return userId;
    }

    /**
     * 获取当前设备的registrationId，Engagelab私有云唯一标识，可同于推送
     *
     * @param context 不为空
     * @return registrationId
     */
    String getRegistrationId() {
        logD(TAG, "getRegistrationId");
        Context context = getApplicationContext();
        String registrationId = MTCorePrivatesApi.getRegistrationId(context);
        return registrationId;
    }

//    /**
//     * 配置push版本号为3.9.X
//     *
//     * @param context 不为空
//     */
//    void configOldPushVersion( ) {
//        try {
//            Context context = getApplicationContext();
//            MTPushPrivatesApi.configOldPushVersion(context);
//        } catch (Throwable e) {
//            e.printStackTrace();
//        }
//    }


//    // 继承MTCommonReceiver后，复写onNotificationStatus方法，获取通知开关状态，如果enable为true说明已经开启成功
//    @Override
//    public void onNotificationStatus(Context context, boolean enable) {
//        if(enable){
//            // 已设置通知开关为打开
//        }
//    }
//    启动sdk后可根据onNotificationStatus回调结果，再决定是否需要调用此借口

    /**
     * 前往通知开关设置页面
     *
     * @param context 不为空 //TODO weiry
     */
    void goToAppNotificationSettings() {
        Context context = getApplicationContext();
        MTPushPrivatesApi.goToAppNotificationSettings(context);
    }

//    // 继承MTCommonReceiver后，复写onConnectStatus方法，获取长连接的连接状态，如果enable为true说明已经开启成功
//    @Override
//    public void onConnectStatus(Context context, boolean enable){
//        if(enable){
//            // 开启 push 推送成功
//        }
//    }

    /**
     * 开启 Push 推送，并持久化存储开关状态为true，默认是true
     *
     * @param context 不能为空  //TODO weiry
     */
    void turnOnPush() {
        Context context = getApplicationContext();
        MTPushPrivatesApi.turnOnPush(context);
    }

    /**
     * 关闭 push 推送，并持久化存储开关状态为false，默认是true
     *
     * @param context 不能为空 //TODO weiry
     */
    void turnOffPush() {
        Context context = getApplicationContext();
        MTPushPrivatesApi.turnOffPush(context);
    }

    /**
     * 设置通知展示时间，默认任何时间都展示
     *
     * @param context   不为空
     * @param beginHour 允许通知展示的开始时间（ 24 小时制，范围为 0 到 23 ）
     * @param endHour   允许通知展示的结束时间（ 24 小时制，范围为 0 到 23 ），beginHour不能大于等于endHour
     * @param weekDays  允许通知展示的星期数组（ 7 日制，范围为 1 到 7），空数组代表任何时候都不展示通知
     */
    void setNotificationShowTime(int beginHour, int endHour, int[] weekDays) {
        Context context = getApplicationContext();
        MTPushPrivatesApi.setNotificationShowTime(context, beginHour, endHour, weekDays);
    }

    /**
     * 重置通知展示时间，默认任何时间都展示
     *
     * @param context 不为空
     */
    void resetNotificationShowTime() {
        Context context = getApplicationContext();
        MTPushPrivatesApi.resetNotificationShowTime(context);

    }

    /**
     * 设置通知静默时间，默认任何时间都不静默
     *
     * @param context     不为空
     * @param beginHour   允许通知静默的开始时间，单位小时（ 24 小时制，范围为 0 到 23 ）
     * @param beginMinute 允许通知静默的开始时间，单位分钟（ 60 分钟制，范围为 0 到 59 ）
     * @param endHour     允许通知静默的结束时间，单位小时（ 24 小时制，范围为 0 到 23 ）
     * @param endMinute   允许通知静默的结束时间，单位分钟（ 60 分钟制，范围为 0 到 59 ）
     */
    void setNotificationSilenceTime(int beginHour, int beginMinute, int endHour, int endMinute) {
        Context context = getApplicationContext();
        MTPushPrivatesApi.setNotificationSilenceTime(context, beginHour, beginMinute, endHour, endMinute);
    }

    /**
     * 重置通知静默时间，默认任何时间都不静默
     *
     * @param context 不为空
     */
    void resetNotificationSilenceTime() {
        Context context = getApplicationContext();
        MTPushPrivatesApi.resetNotificationSilenceTime(context);
    }

//    /**
//     * 设置自定义通知布局，默认使用系统通知布局
//     *
//     * @param context            不为空
//     * @param builderId          构建id
//     * @param notificationLayout 自定义通知布局的对象，不为空
//     */
//    public static void setNotificationLayout(Context context, int builderId, NotificationLayout notificationLayout) {
////        // 这里定义一个常量，代表构建id
////        private static final int BUILDER_ID = 11;
////
////        // 构建出一个notificationLayout
////        NotificationLayout notificationLayout = new NotificationLayout()
////                .setLayoutId(R.layout.custom_notification_layout)        // 布局layout_id
////                .setIconViewId(R.id.iv_notification_icon)                // 通知图标view_id
////                .setIconResourceId(R.drawable.mtpush_notification_icon)   // 通知图标source_id
////                .setTitleViewId(R.id.tv_notification_title)              // 通知标题view_id
////                .setContentViewId(R.id.tv_notification_content)          // 通知内容view_id
////                .setTimeViewId(R.id.tv_notification_time);               // 通知时间view_id
////
////        // 设置构建id为BUILDER_ID的自定义布局，下发通知时指定builderId为BUILDER_ID，即可改变通知布局
////        MTPushPrivatesApi.setNotificationLayout(this.getApplicationContext(), BUILDER_ID, notificationLayout);
//    }

//    /**
//     * 重置自定义通知布局，默认使用系统通知布局
//     *
//     * @param context   不为空
//     * @param builderId 自定义通知布局的id
//     */
//    public static void resetNotificationLayout(Context context, int builderId) {
////        // 这里定义一个常量，代表构建id
////        private static final int BUILDER_ID = 11;
////
////        // 重置构建id为BUILDER_ID的自定义布局，下发通知时指定builderId为BUILDER_ID，将使用系统默认布局
////        MTPushPrivatesApi.resetNotificationLayout(context,BUILDER_ID);
//    }

    /**
     * 设置通知栏的通知数量，默认数量为5
     *
     * @param context 不为空
     * @param count   限制通知栏的通知数量，超出限制数量则移除最老通知，不能小于等于0
     */
    void setNotificationCount(int count) {
        Context context = getApplicationContext();
        MTPushPrivatesApi.setNotificationCount(context, count);
    }

    /**
     * 重置通知栏的通知数量，默认数量为5
     *
     * @param context 不为空
     */
    void resetNotificationCount() {
        Context context = getApplicationContext();
        MTPushPrivatesApi.resetNotificationCount(context);
    }

    /**
     * 设置应用角标数量，默认0（仅华为/荣耀生效）
     *
     * @param context 不为空
     * @param badge   应用角标数量
     */
    void setNotificationBadge(int badge) {
        Context context = getApplicationContext();
        MTPushPrivatesApi.setNotificationBadge(context, badge);

    }

    /**
     * 重置应用角标数量，默认0（仅华为/荣耀生效）
     *
     * @param context 不为空
     */
    void resetNotificationBadge() {
        Context context = getApplicationContext();
        MTPushPrivatesApi.resetNotificationBadge(context);

    }

//    /**
//     * 展示通知
//     *
//     * @param context             不为空
//     * @param notificationMessage 构建的通知对象，不为空
//     */
//    public static void showNotification(Context context, NotificationMessage notificationMessage) {
////        // 构建一个基础的通知，其中messageId和content是必须，否则通知无法展示
////        NotificationMessage notificationMessage = new NotificationMessage()
////                .setMessageId("12345")
////                .setNotificationId(12345)
////                .setTitle("custom_notification_title")
////                .setContent("custom_notification_content");
////        // 展示通知
////        MTPushPrivatesApi.showNotification(context,notificationMessage);
//    }
//
//    /**
//     * 清除指定notifyId的通知
//     *
//     * @param context  不为空
//     * @param notifyId 通知id
//     */
//    public static void clearNotification(Context context, int notifyId) {
//
//    }

    /**
     * 上报厂商通道通知到达
     * <p>
     * 走http/https上报
     *
     * @param context           不为空
     * @param messageId         Engagelab消息id，不为空
     * @param platform          厂商，取值范围（1:mi、2:huawei、3:meizu、4:oppo、5:vivo、8:google）
     * @param platformMessageId 厂商消息id，可为空
     */
    void reportNotificationArrived(String messageId, byte platform, String platformMessageId) {
//        // 上报厂商通知到达，messageId为“12345”，厂商为小米，厂商messageId为“MI-6476RHT25”
//        MTPushPrivatesApi.reportNotificationArrived(context,“12345”,MTPushPrivatesApi.PLATFORM_XIAOMI,"MI-6476RHT25");
        Context context = getApplicationContext();
        MTPushPrivatesApi.reportNotificationArrived(context, messageId, platform, platformMessageId);
    }

    /**
     * 上报厂商通道通知点击
     * <p>
     * 走http/https上报
     *
     * @param context           不为空
     * @param messageId         Engagelab消息id，不为空
     * @param platform          厂商，取值范围（1:mi、2:huawei、3:meizu、4:oppo、5:vivo、8:google）
     * @param platformMessageId 厂商消息id，可为空
     */
    void reportNotificationClicked(String messageId, byte platform, String platformMessageId) {
//        // 上报厂商通知点击，messageId为“12345”，厂商为小米，厂商messageId为“MI-6476RHT25”
//        MTPushPrivatesApi.reportNotificationClicked(context,“12345”,MTPushPrivatesApi.PLATFORM_XIAOMI,"MI-6476RHT25");
        Context context = getApplicationContext();
        MTPushPrivatesApi.reportNotificationClicked(context, messageId, platform, platformMessageId);
    }

    /**
     * 上报厂商通道通知删除
     * <p>
     * 走http/https上报
     *
     * @param context           不为空
     * @param messageId         Engagelab消息id，不为空
     * @param platform          厂商，取值范围（1:mi、2:huawei、3:meizu、4:oppo、5:vivo、8:google）
     * @param platformMessageId 厂商消息id，可为空
     */
    void reportNotificationDeleted(String messageId, byte platform, String platformMessageId) {
//// 上报厂商通知删除，messageId为“12345”，厂商为mi，厂商messageId为“MI-6476RHT25”
//        MTPushPrivatesApi.reportNotificationDeleted(context,“12345”,MTPushPrivatesApi.PLATFORM_XIAOMI,"MI-6476RHT25");
        Context context = getApplicationContext();
        MTPushPrivatesApi.reportNotificationDeleted(context, messageId, platform, platformMessageId);

    }

    /**
     * 上报厂商通道通知打开
     * <p>
     * 走http/https上报
     *
     * @param context           不为空
     * @param messageId         Engagelab消息id，不为空
     * @param platform          厂商，取值范围（1:mi、2:huawei、3:meizu、4:oppo、5:vivo、8:google）
     * @param platformMessageId 厂商消息id，可为空
     */
    void reportNotificationOpened(String messageId, byte platform, String platformMessageId) {
        Context context = getApplicationContext();
        MTPushPrivatesApi.reportNotificationOpened(context, messageId, platform, platformMessageId);


//        Bundle bundle = intent.getExtras();
//        String platformMessage = "";
//        // huawei
//        if (intent.getData() != null) {
//            platformMessage = intent.getData().toString();
//        }
//        // 其他厂商
//        if (TextUtils.isEmpty(platformMessage) && intent.getExtras() != null) {
//            if (MTGlobal.IS_FOR_JIGUANG) {
//                platformMessage = bundle.getString("JMessageExtra");
//            }else {
//                platformMessage = bundle.getString("MTMessageExtra");
//            }
//        }
//        if (TextUtils.isEmpty(platformMessage)) {
//            return;
//        }
//        JSONObject messageJson = new JSONObject(platformMessage);
//        tvMessage.setText(toLogString(messageJson));
//        // 解析
//        String messageId = messageJson.optString("msg_id");
//        byte platform = (byte) messageJson.optInt("rom_type");
//        String title = messageJson.optString("n_title");
//        String content = messageJson.optString("n_content");
//        // 上报通知点击activity打开，建议所有厂商跳转都加上，仅MTPush4.0.0以下版本需要
//        MTPushPrivatesApi.reportNotificationOpened(this, messageId, platform, "");
    }

    /**
     * 上传厂商token
     * <p>
     * 走tcp上传
     *
     * @param context  不为空
     * @param platform 厂商，取值范围（1:mi、2:huawei、3:meizu、4:oppo、5:vivo、8:google）
     * @param token    厂商返回的token，不为空
     */
    void uploadPlatformToken(byte platform, String token, String region) {
//        // 上报厂商token，厂商为mi，厂商token为“MI-6476s-afs-afs-afaR-HT25”
//        MTPushPrivatesApi.uploadPlatformToken(context,MTPushPrivatesApi.PLATFORM_XIAOMI,"MI-6476s-afs-afs-afaR-HT25");

        Context context = getApplicationContext();
        MTPushPrivatesApi.uploadPlatformToken(context, platform, token, region);

    }

    public static boolean DEBUG = false;

    public static void logD(String tag, String msg) {
        if (DEBUG) {
            Log.d("ENGAGELAB-PRIVATES-Uni", "[" + tag + "]" + msg);
        }
    }

    public static void logE(String tag, String msg) {
        Log.e("ENGAGELAB-PRIVATES-Uni", "[" + tag + "]" + msg);
    }


    public static void logW(String tag, String msg) {
        Log.w("ENGAGELAB-PRIVATES-Uni", "[" + tag + "]" + msg);
    }


}
