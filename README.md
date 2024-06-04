# MTPush Unity Plugin

## 集成

把Plugins文件夹里的文件合并到您自己的项目Assets/Plugins文件夹下面

### Android

1. 生成build文件：
   在 Unity 中选择 *File---Build Settings---Player Settings*
   ---Publishing Settings ---- Build 勾上选以下选项下：
*  Custom Launcher Gradle Template
*  Custom Gradle Properties Template
*  Custom Base Gradle Template
*  Custom LauncherManifest

##### 会生成以下文件
*  launcherTemplate.gradle文件
*  gradleTemplate.properties文件
*  baseProjectTemplate.gradle文件
*  LauncherManifest.xml文件

#### 修改baseProjectTemplate.gradle文件：
在两个repositories中添加
```
google()
jcenter()
mavenCentral()
maven { url 'https://developer.huawei.com/repo/' }//华为厂商需要
```
具体可参考Examples下 baseProjectTemplate.gradle文件

#### 修改launcherTemplate.gradle文件：
- 在最上面加
```
apply plugin: 'com.android.application'
// google push need，不需要 google 通道，则删除
apply plugin: 'com.google.gms.google-services'
// huawei push need，不需要 huawei 通道，则删除
apply plugin: 'com.huawei.agconnect'
```
- 在dependencies里面加
```
    
    //必须 主包
    implementation 'com.engagelab:engagelab:3.4.0' // 此处以3.4.0 版本为例。
    //可选，google厂商
    implementation 'com.engagelab.plugin:google:3.4.0' // 此处以3.4.0 版本为例。
    //可选，honor厂商
    implementation 'com.engagelab.plugin:honor:3.4.0' // 此处以3.4.0 版本为例。
    implementation 'com.engagelab.plugin:honor_th_push:3.4.0' // 此处以3.4.0 版本为例。
    //可选，huawei厂商
    implementation 'com.engagelab.plugin:huawei:3.4.0' // 此处以3.4.0 版本为例。
    //可选，mi厂商，海外版
    implementation 'com.engagelab.plugin:mi_global:3.4.0' // 此处以3.4.0 版本为例。
    //可选，meizu厂商
    implementation 'com.engagelab.plugin:meizu:3.4.0' // 此处以3.4.0 版本为例。
    //可选，oppo厂商
    implementation 'com.engagelab.plugin:oppo:3.4.0' // 此处以3.4.0 版本为例。
    implementation 'com.engagelab.plugin:oppo_th_push:3.4.0' // 此处以3.4.0 版本为例。
    //可选，vivo厂商
    implementation 'com.engagelab.plugin:vivo:3.4.0' // 此处以3.4.0 版本为例。

    // google push need，不需要 google 通道，则删除
    implementation 'com.google.firebase:firebase-messaging:23.1.1'

    // huawei push need，不需要 huawei 通道，则删除
    implementation 'com.huawei.hms:push:6.7.0.300'
    //oppo以下依赖都需要添加，不需要 oppo 通道，则删除
    implementation 'com.google.code.gson:gson:2.6.2'
    implementation 'commons-codec:commons-codec:1.6'
    implementation 'androidx.annotation:annotation:1.1.0'
    
```
- 在defaultConfig里面加,并填写对应信息
```
manifestPlaceholders = [
                ENGAGELAB_PRIVATES_APPKEY : "你的appkey",
                ENGAGELAB_PRIVATES_CHANNEL: "developer",
                ENGAGELAB_PRIVATES_PROCESS: ":remote",
                //数据中心名称，填空""时，默认"Singapore"数据中心
                ENGAGELAB_PRIVATES_SITE_NAME: "Singapore",
                //以下厂商可选
                //小米海外厂商信息
                XIAOMI_GLOBAL_APPID            : "",
                XIAOMI_GLOBAL_APPKEY           : "",
                //MEIZU厂商信息
                MEIZU_APPID            : "",
                MEIZU_APPKEY           : "",
                //OPPO厂商信息
                OPPO_APPID             : "",
                OPPO_APPKEY            : "",
                OPPO_APPSECRET         : "",
                //VIVO厂商信息
                VIVO_APPID             : "",
                VIVO_APPKEY            : "",
                HONOR_APPID            : ""
        ]
```
- 在最后面，SPLITS_VERSION_CODE LAUNCHER_SOURCE_BUILD_SETUP 前加
```
task copyJsonFile {
    copy {
        delete("google-services.json")
        from('您的google-services.json路径')
        into('./')
        include("google-services.json")
    }

    copy {
        delete("agconnect-services.json")
        from('您的agconnect-services.json路径')
        into('./')
        include("agconnect-services.json")
    }

    copy {
        delete("src/main/assets/mt_engagelab_push_config")
        from('您的mt_engagelab_push_config文件路径')
        into('src/main/assets/')
        include("mt_engagelab_push_config")
    }
}
preBuild.dependsOn copyJsonFile
```
具体可参考Examples下  launcherTemplate.gradle 文件



#### 修改gradleTemplate.properties文件：
添加以下内容
```
//没有用GOOLE厂商的不用加
android.useAndroidX=true
```
具体可参考Examples下  gradleTemplate.properties 文件

#### 修改LauncherManifest.xml文件：
在application添加以下内容
```
android:name="com.engagelab.privates.unity.push.MTPushApplication"
```
具体可参考Examples下  LauncherManifest.xml 文件

#### 配置mt_engagelab_push_config文件：
添加以下内容
```
{
	"tcp_ssl": true,//true为tcp使用ssl加密
	"debug":true, //debug 模式，true为打印debug日志
    "testGoogle": true // true可以测试fcm，只试用于测试。在正式环境时请设置为false或删除该项。
}
```
具体可参考Examples下  mt_engagelab_push_config 文件


### iOS

## ！！！ 注意，使用unity生成iOS项目后，如果提示SDK的xcframework包里面缺少 "info.plist"文件，把插件xcframework包中的info.plist文件拷贝到项目中插件xcframework包中。

## 或者，在拷贝插件时，直接保留相应的调试包。
## 插件iOS目录中，有SDK包 mtpush-ios-x.x.x.xcframework ,包里面有两个文件夹
 - "ios-arm64" 是真机架构，用于真机运行调试以及上架发布。当需要在真机上运行时请保留此文件夹并删除其他。
 - "ios-arm64_×86_64-simulator" 是模拟器架构，用于模拟器运行调试。如果需要运行 iOS 模拟器请保留此文件夹并删除其他（即当工程配置 iOS Target SDK 指定为 Simulator SDK 时）。

1. 生成 iOS 工程，并打开该工程。
2. 添加必要的框架：

   - CFNetwork.framework
   - CoreFoundation.framework
   - CoreTelephony.framework
   - SystemConfiguration.framework
   - CoreGraphics.framework
   - Foundation.framework
   - UIKit.framework
   - Security.framework
   - libz.tbd（Xcode 7 以下版本是 libz.dylib）
   - AdSupport.framework（获取 IDFA 需要；如果不使用 IDFA，请不要添加）
   - UserNotifications.framework（Xcode 8 及以上）
   - libresolv.tbd（JPush 2.2.0 及以上版本需要，Xcode 7 以下版本是 libresolv.dylib）
   - StoreKit.framework 
   - libsqlite3.tbd 
   
      ​

3. 在 UnityAppController.mm 中添加头文件 `MTPushUnityManager.h`  。

    ```Objective-C
   #include <MTPushUnityManager.h>
   #import <AdSupport/AdSupport.h>// 如需使用广告标识符 IDFA 则添加该头文件，否则不添加。
   #import <AppTrackingTransparency/AppTrackingTransparency.h>// 如需使用广告标识符 IDFA 则添加该头文件，否则不添加。
    ```

4. 在 UnityAppController.mm 的下列方法中添加以下代码：

    ```Objective-C
    - (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    
   
   __block NSString *advertisingId = nil;
   //    if (@available(iOS 14, *)) {
   //        //设置Info.plist中 NSUserTrackingUsageDescription 需要广告追踪权限，用来定位唯一用户标识
   //        [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
   //            if (status == ATTrackingManagerAuthorizationStatusAuthorized) {
   //                advertisingId = [[ASIdentifierManager sharedManager] advertisingIdentifier].UUIDString;
   //            }
   //        }];
   //    } else {
   //        // 使用原方式访问 IDFA
   //        advertisingId = [[[ASIdentifierManager sharedManager] advertisingIdentifier] UUIDString];
   //    }

    [[MTPushUnityInstnce sharedInstance] application:application didFinishLaunchingWithOptions:launchOptions advertisingId:advertisingId];
    
      return YES;
    }

    - (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken {
      // Required.
    [[MTPushUnityInstnce sharedInstance] application:application didRegisterForRemoteNotificationsWithDeviceToken:deviceToken];
    }
    ```
    
5.有些Unity版本在导出Xcode项目之后，需要修改：

```Objective-C
    Preprocessor.h 文件中

    #define UNITY_USES_REMOTE_NOTIFICATIONS 0
    更改为
    #define UNITY_USES_REMOTE_NOTIFICATIONS 1

    否则

    didRegisterForRemoteNotificationsWithDeviceToken

    都将无法使用
```
## API 说明

[API](/Doc/CommonAPI.md)。

