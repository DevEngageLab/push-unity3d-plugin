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
    public class MTPushBinding : MonoBehaviour
    {
#if UNITY_ANDROID

        private static AndroidJavaObject _plugin;

        static MTPushBinding()
        {
            using (AndroidJavaClass jpushClass = new AndroidJavaClass("com.engagelab.privates.unity.push.MTPushBridge"))
            {
                _plugin = jpushClass.CallStatic<AndroidJavaObject>("getInstance");
            }
        }

#endif

#if UNITY_ANDROID
        /// <summary>
        /// 初始化 JPush。
        /// </summary>
        /// <param name="gameObject">游戏对象名。</param>
        public static void InitMTPushAndroid(string gameObject)
        {
            _plugin.Call("initMTPush", gameObject);
        }

   #endif

#if UNITY_IOS
                /// <summary>
                /// 初始化 JPush。
                /// </summary>
                /// <param name="gameObject">游戏对象名。</param>
                public static void InitMTPushIos(string gameObject,string appKey,bool isProduction,string channel,bool isIdfa)
                {

                JsonData jd = new JsonData();
                            jd["appKey"] = appKey;
                            jd["isProduction"] = isProduction;
                            jd["channel"] = channel;
                            jd["isIdfa"] = isIdfa;
                            string config = JsonMapper.ToJson(jd);
                    _initMTPush(gameObject,config);
                }     
#endif

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

  /**
   * 设置siteName
   *
   * @param context 不为空
   */
  public static void SetSiteName(string siteName) {
            #if UNITY_ANDROID
             _plugin.Call("setSiteName", siteName);

            #elif UNITY_IOS
             _setSiteName(siteName);

            #else

            #endif
  }


/// <summary>
        /// 为设备设置标签（tag）。
        /// <para>注意：这个接口是覆盖逻辑，而不是增量逻辑。即新的调用会覆盖之前的设置。</para>
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        /// <param name="tags">
        ///     标签列表。
        ///     <para>每次调用至少设置一个 tag，覆盖之前的设置，不是新增。</para>
        ///     <para>有效的标签组成：字母（区分大小写）、数字、下划线、汉字、特殊字符 @!#$&*+=.|。</para>
        ///     <para>限制：每个 tag 命名长度限制为 40 字节，最多支持设置 1000 个 tag，且单次操作总长度不得超过 5000 字节（判断长度需采用 UTF-8 编码）。</para>
        /// </param>
        public static void SetTags(int sequence, List<string> tags)
        {
            string tagsJsonStr = JsonHelper.ToJson<string>(tags);

            #if UNITY_ANDROID
            _plugin.Call("setTags", sequence, tagsJsonStr);

            #elif UNITY_IOS
            _setTagsJpush(sequence, tagsJsonStr);

            #endif
        }

        /// <summary>
        /// 为设备新增标签（tag）。
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        /// <param name="tags">
        ///     标签列表。
        ///     <para>每次调用至少设置一个 tag，覆盖之前的设置，不是新增。</para>
        ///     <para>有效的标签组成：字母（区分大小写）、数字、下划线、汉字、特殊字符 @!#$&*+=.|。</para>
        ///     <para>限制：每个 tag 命名长度限制为 40 字节，最多支持设置 1000 个 tag，且单次操作总长度不得超过 5000 字节（判断长度需采用 UTF-8 编码）。</para>
        /// </param>
        public static void AddTags(int sequence, List<string> tags)
        {
            string tagsJsonStr = JsonHelper.ToJson(tags);

            #if UNITY_ANDROID
            _plugin.Call("addTags", sequence, tagsJsonStr);

            #elif UNITY_IOS
            _addTagsJpush(sequence, tagsJsonStr);

            #endif
        }

        /// <summary>
        /// 删除标签（tag）。
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        /// <param name="tags">
        ///     标签列表。
        ///     <para>每次调用至少设置一个 tag，覆盖之前的设置，不是新增。</para>
        ///     <para>有效的标签组成：字母（区分大小写）、数字、下划线、汉字、特殊字符 @!#$&*+=.|。</para>
        ///     <para>限制：每个 tag 命名长度限制为 40 字节，最多支持设置 1000 个 tag，且单次操作总长度不得超过 5000 字节（判断长度需采用 UTF-8 编码）。</para>
        /// </param>
        public static void DeleteTags(int sequence, List<string> tags)
        {
            string tagsJsonStr = JsonHelper.ToJson(tags);

            #if UNITY_ANDROID
            _plugin.Call("deleteTags", sequence, tagsJsonStr);

            #elif UNITY_IOS
            _deleteTagsJpush(sequence, tagsJsonStr);

            #endif
        }

        /// <summary>
        /// 清空当前设备设置的标签（tag）。
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        public static void CleanTags(int sequence)
        {
            #if UNITY_ANDROID
            _plugin.Call("cleanTags", sequence);

            #elif UNITY_IOS
            _cleanTagsJpush(sequence);

            #endif
        }

        /// <summary>
        /// 获取当前设备设置的所有标签（tag）。
        /// <para>需要实现 OnJPushTagOperateResult 方法获得操作结果。</para>
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        public static void GetAllTags(int sequence)
        {
            #if UNITY_ANDROID
            _plugin.Call("getAllTags", sequence);

            #elif UNITY_IOS
            _getAllTagsJpush(sequence);

            #endif
        }

        /// <summary>
        /// 查询指定标签的绑定状态。
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        /// <param name="tag">待查询的标签。</param>
        public static void CheckTagBindState(int sequence, string tag)
        {
            #if UNITY_ANDROID
            _plugin.Call("checkTagBindState", sequence, tag);

            #elif UNITY_IOS
            _checkTagBindStateJpush(sequence, tag);

            #endif
        }

        /// <summary>
        /// 设置别名。
        /// <para>注意：这个接口是覆盖逻辑，而不是增量逻辑。即新的调用会覆盖之前的设置。</para>
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        /// <param name="alias">
        ///     别名。
        ///     <para>有效的别名组成：字母（区分大小写）、数字、下划线、汉字、特殊字符@!#$&*+=.|。</para>
        ///     <para>限制：alias 命名长度限制为 40 字节（判断长度需采用 UTF-8 编码）。</para>
        /// </param>
        public static void SetAlias(int sequence, string alias)
        {
            #if UNITY_ANDROID
            _plugin.Call("setAlias", sequence, alias);

            #elif UNITY_IOS
            _setAliasJpush(sequence, alias);

            #endif
        }

        /// <summary>
        /// 删除别名。
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        public static void DeleteAlias(int sequence)
        {
            #if UNITY_ANDROID
            _plugin.Call("deleteAlias", sequence);

            #elif UNITY_IOS
            _deleteAliasJpush(sequence);

            #endif
        }

        /// <summary>
        /// 获取当前设备设置的别名。
        /// </summary>
        /// <param name="sequence">用户自定义的操作序列号。同操作结果一起返回，用来标识一次操作的唯一性。</param>
        public static void GetAlias(int sequence)
        {
            #if UNITY_ANDROID
            _plugin.Call("getAlias", sequence);

            #elif UNITY_IOS
            _getAliasJpush(sequence);

            #endif
        }

#if UNITY_IOS

        ///接口返回
        ///有效的 tag 集合。
        public static List<string> FilterValidTags(List<string> jsonTags)
        {
            string tagsJsonStr = JsonHelper.ToJson(jsonTags);
            string reJson = null;

            // #if UNITY_ANDROID
            // reJson = _plugin.Call<string>("filterValidTags", tagsJsonStr);
            // #elif UNITY_IOS
            reJson =  _filterValidTagsJpush(tagsJsonStr);
            // #endif
            if (null == reJson)
            {
                return new List<string>();
            }

            string[] reStringArray = JsonHelper.FromJson<string>(reJson);

            if (null == reStringArray)
            {
                return new List<string>();
            }

            List<string> reList = new List<string>(); ;
            for (int i = 0; i < reStringArray.Length; i++)
            {
                reList.Add(reStringArray[i]);
            }

            return reList;
        }

#endif




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
 public static void SetTcpSSL(bool enable) {
    _setTcpSSL(enable);
  }
#endif


#if UNITY_IOS


        
        [DllImport("__Internal")]
        private static extern void _initMTPush(string gameObject,string config);

        [DllImport("__Internal")]
        private static extern void _configDebugMode(bool enable);

        [DllImport("__Internal")]
        private static extern string _getRegistrationId();

        [DllImport("__Internal")]
        private static extern void _setNotificationBadge(int badge);

        [DllImport("__Internal")]
        private static extern void _resetNotificationBadge();

        [DllImport("__Internal")]
        private static extern void _setTcpSSL(bool enable);

        [DllImport("__Internal")]
        private static extern void _setTagsJpush(int sequence, string tags);

        [DllImport("__Internal")]
        private static extern void _setSiteName(string tags);

        [DllImport("__Internal")]
        private static extern void _addTagsJpush(int sequence, string tags);

        [DllImport("__Internal")]
        private static extern void _deleteTagsJpush(int sequence, string tags);

        [DllImport("__Internal")]
        private static extern void _cleanTagsJpush(int sequence);

        [DllImport("__Internal")]
        private static extern void _getAllTagsJpush(int sequence);

        [DllImport("__Internal")]
        private static extern void _checkTagBindStateJpush(int sequence, string tag);

        [DllImport("__Internal")]
        private static extern string _filterValidTagsJpush(string tags);

        [DllImport("__Internal")]
        private static extern void _setAliasJpush(int sequence, string alias);

        [DllImport("__Internal")]
        private static extern void _deleteAliasJpush(int sequence);

        [DllImport("__Internal")]
        private static extern void _getAliasJpush(int sequence);


#endif
    }
}
