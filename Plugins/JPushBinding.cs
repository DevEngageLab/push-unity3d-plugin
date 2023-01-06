using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if UNITY_IPHONE
using LitJson;
#endif

// @version v3.1.0
namespace MTPush
{
    public class JPushBinding : MonoBehaviour
    {
        #if UNITY_ANDROID

        private static AndroidJavaObject _plugin;

        static JPushBinding()
        {
            using (AndroidJavaClass jpushClass = new AndroidJavaClass("com.engagelab.privates.unity.push.MTPushBridge"))
            {
                _plugin = jpushClass.CallStatic<AndroidJavaObject>("getInstance");
            }
        }

        #endif

        /// <summary>
        /// 初始化 JPush。
        /// </summary>
        /// <param name="gameObject">游戏对象名。</param>
        public static void InitMTPush(string gameObject)
        {
            #if UNITY_ANDROID
            _plugin.Call("initMTPush", gameObject);

            #elif UNITY_IOS
            _initMTPush(gameObject);

            #endif
        }

        /// <summary>
        /// 设置是否开启 Debug 模式。
        /// <para>Debug 模式将会输出更多的日志信息，建议在发布时关闭。</para>
        /// </summary>
        /// <param name="enable">true: 开启；false: 关闭。</param>
        public static void ConfigDebugMode(bool enable)
        {
            #if UNITY_ANDROID
            _plugin.Call("configDebugMode", enable);

            #elif UNITY_IOS
            _configDebugMode(enable);

            #endif
        }

        /// <summary>
        /// 获取当前设备的 Registration Id。
        /// </summary>
        /// <returns>设备的 Registration Id。</returns>
        public static string GetRegistrationId()
        {
            #if UNITY_ANDROID
            return _plugin.Call<string>("getRegistrationId");

            #elif UNITY_IOS
            return _getRegistrationId();

            #else
            return "";

            #endif
        }


  /**
   * 设置应用角标数量，默认0（仅华为/荣耀/ios生效）
   *
   * @param context 不为空
   * @param badge   应用角标数量
   */
  public static void SetNotificationBadge(int badge) {
   #if UNITY_ANDROID
            _plugin.Call("setNotificationBadge",badge);

            #elif UNITY_IOS
            _setNotificationBadge(badge);

            #else
            

            #endif
  }

  /**
   * 重置应用角标数量，默认0（仅华为/荣耀生效/ios）
   *
   * @param context 不为空
   */
  public static void ResetNotificationBadge() {
            #if UNITY_ANDROID
             _plugin.Call("resetNotificationBadge");

            #elif UNITY_IOS
             _resetNotificationBadge();

            #else

            #endif
  }





#if UNITY_ANDROID

/**
   * 设置心跳时间间隔
   * <p>
   * 需要在Application.onCreate()方法中调用
   *
   * @param heartbeatInterval 时间单位为毫秒、必须大于0、默认值是4分50秒\
   */
  public static void ConfigHeartbeatIntervalAndroid(long heartbeatInterval) {
    _plugin.Call("configHeartbeatInterval", heartbeatInterval);
  }

  /**
   * 设置长连接重试次数
   * <p>
   * 需要在Application.onCreate()方法中调用
   * @param connectRetryCount 重试的次数、默认值为3、最少3次
   */
  public static void ConfigConnectRetryCountAndroid(int connectRetryCount) {
    _plugin.Call("configConnectRetryCount", connectRetryCount);
  }

  /**
   * 配置使用国密加密
   *
   * @param context 不为空
   */
  public static void ConfigSM4Android() {
    _plugin.Call("configSM4");
  }

  /**
   * 获取当前设备的userId，Engagelab私有云唯一标识，可同于推送
   *
   * @param context 不为空
   * @return userId
   */
  public static long GetUserIdAndroid() {
    return _plugin.Call<long>("getUserId");
  }

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
  public static void GoToAppNotificationSettingsAndroid() {
    _plugin.Call("goToAppNotificationSettings");
  }


  /**
   * 开启 Push 推送，并持久化存储开关状态为true，默认是true
   *
   * @param context 不能为空
   */
  public static void TurnOnPushAndroid() {
    _plugin.Call("turnOnPush");
  }

/**
   * 关闭 push 推送，并持久化存储开关状态为false，默认是true
   *
   * @param context 不能为空
   */
  public static void TurnOffPushAndroid() {
    _plugin.Call("turnOffPush");
  }

  /**
   * 设置通知展示时间，默认任何时间都展示
   *
   * @param context   不为空
   * @param beginHour 允许通知展示的开始时间（ 24 小时制，范围为 0 到 23 ）
   * @param endHour   允许通知展示的结束时间（ 24 小时制，范围为 0 到 23 ），beginHour不能大于等于endHour
   * @param weekDays  允许通知展示的星期数组（ 7 日制，范围为 1 到 7），空数组代表任何时候都不展示通知
   */
  public static void SetNotificationShowTimeAndroid(
     int beginHour,int endHour, int[] weekDays) {
    _plugin.Call(
        "setNotificationShowTime", beginHour, endHour, weekDays);
  }

  /**
   * 重置通知展示时间，默认任何时间都展示
   *
   * @param context 不为空
   */
  public static void ResetNotificationShowTimeAndroid() {
    _plugin.Call("resetNotificationShowTime");
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
  public static void SetNotificationSilenceTimeAndroid(
     int beginHour,int beginMinute,int endHour,int endMinute) {
    _plugin.Call("setNotificationSilenceTime",
        beginHour, beginMinute, endHour, endMinute);
  }

  /**
   * 重置通知静默时间，默认任何时间都不静默
   *
   * @param context 不为空
   */
  public static void ResetNotificationSilenceTimeAndroid() {
    _plugin.Call("resetNotificationSilenceTime");
  }

  /**
   * 设置通知栏的通知数量，默认数量为5
   *
   * @param context 不为空
   * @param count   限制通知栏的通知数量，超出限制数量则移除最老通知，不能小于等于0
   */
  public static void SetNotificationCountAndroid(int count) {
    _plugin.Call("setNotificationCount", count);
  }

  /**
   * 重置通知栏的通知数量，默认数量为5
   *
   * @param context 不为空
   */
  public static void ResetNotificationCountAndroid() {
    _plugin.Call("resetNotificationCount");
  }

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
  public static void ReportNotificationArrivedAndroid(
      string messageId,byte platform, string platformMessageId) {
    _plugin.Call(
        "reportNotificationArrived", messageId, platform, platformMessageId);
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
  public static void ReportNotificationClickedAndroid(
    string  messageId,byte platform,string platformMessageId) {
    _plugin.Call(
        "reportNotificationClicked", messageId, platform, platformMessageId);
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
  public static void ReportNotificationDeletedAndroid(
     string messageId,byte platform,string platformMessageId) {
    _plugin.Call(
        "reportNotificationDeleted", messageId, platform, platformMessageId);
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
  public static void ReportNotificationOpenedAndroid(
    string  messageId,byte platform,string platformMessageId) {
    _plugin.Call(
        "reportNotificationOpened", messageId, platform, platformMessageId);
  }

 /**
   * 上传厂商token
   * <p>
   * 走tcp上传
   *
   * @param context  不为空
   * @param platform 厂商，取值范围（1:mi、2:huawei、3:meizu、4:oppo、5:vivo、8:google）
   * @param token    厂商返回的token，不为空
   * @param region    //目前只有小米、OPPO才区分国内和国际版，其他厂商不区分;没有不用传
   */
  public static void UploadPlatformTokenAndroid(byte platform,string token,string region) {
    _plugin.Call("uploadPlatformToken", platform, token, region);
  }

#endif

#if UNITY_IOS

        
        [DllImport("__Internal")]
        private static extern void _initMTPush(string gameObject);

        [DllImport("__Internal")]
        private static extern void _configDebugMode(bool enable);

        [DllImport("__Internal")]
        private static extern string _getRegistrationId();

        [DllImport("__Internal")]
        private static extern void _setNotificationBadge(int badge);

        [DllImport("__Internal")]
        private static extern void _resetNotificationBadge(int sequence, string tags);

#endif
    }
}
