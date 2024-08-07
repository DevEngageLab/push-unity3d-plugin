# MTPush Unity Plugin

## Setup

Place the files in the Plugins folder into your own project's Assets/Plugins folder.

### Android

1. Generate build file:
   In Unity, Select *File---Build Settings---Player Settings*
   ---Publishing Settings ---- Build , Check the following options：
*  Custom Launcher Gradle Template
*  Custom Gradle Properties Template
*  Custom Base Gradle Template
*  Custom LauncherManifest

##### The following files will be generated
*  launcherTemplate.gradle
*  gradleTemplate.properties
*  baseProjectTemplate.gradle
*  LauncherManifest.xml

#### Modify the baseProjectTemplate.gradle file:
Add the following code in both repositories files
```
google()
jcenter()
mavenCentral()
maven { url 'https://developer.huawei.com/repo/' }//Huawei manufacturers need
```
For details, please refer to the baseProjectTemplate.gradle file under Examples

#### Modify the launcherTemplate.gradle file：
- Add the following code at the top of the file
```
apply plugin: 'com.android.application'
// google push need, if you don’t need google channel, delete it
apply plugin: 'com.google.gms.google-services'
// huawei push need, if the huawei channel is not needed, delete it
apply plugin: 'com.huawei.agconnect'
```
- Add the following code at dependencies file.
```
    
    //Required 
    implementation 'com.engagelab:engagelab:4.4.0' // Here we take version 4.4.0 as an example.
    //Optional，google manufacturer
    implementation 'com.engagelab.plugin:google:4.4.0' // Here we take version 4.4.0 as an example.
    //Optional，honor manufacturer
    implementation 'com.engagelab.plugin:honor:4.4.0' // Here we take version 4.4.0 as an example.
    implementation 'com.engagelab.plugin:honor_th_push:4.4.0' // Here we take version 4.4.0 as an example.
    //Optional，huawei manufacturer
    implementation 'com.engagelab.plugin:huawei:4.4.0' // Here we take version 4.4.0 as an example.
    //Optional，mi manufacturer
    implementation 'com.engagelab.plugin:mi_global:4.4.0' // Here we take version 4.4.0 as an example.
    //Optional，meizu manufacturer
    implementation 'com.engagelab.plugin:meizu:4.4.0' // Here we take version 4.4.0 as an example.
    //Optional，oppo manufacturer
    implementation 'com.engagelab.plugin:oppo:4.4.0' // Here we take version 4.4.0 as an example.
    implementation 'com.engagelab.plugin:oppo_th_push:4.4.0' // Here we take version 4.4.0 as an example.
    //Optional，vivo manufacturer
    implementation 'com.engagelab.plugin:vivo:4.4.0' // Here we take version 4.4.0 as an example.

    // google push need, if you don’t need google channel, delete it
    implementation 'com.google.firebase:firebase-messaging:23.2.0'

    // huawei push need, if the huawei channel is not needed, delete it
    implementation 'com.huawei.hms:push:6.11.0.300'
    // The following dependencies of oppo need to be added. If the oppo channel is not required, delete it.
    implementation 'com.google.code.gson:gson:2.8.9'
    implementation 'commons-codec:commons-codec:1.13'
    implementation 'androidx.annotation:annotation:1.1.0'
    
```
- Add it to defaultConfig and fill in the corresponding information
```
manifestPlaceholders = [
                ENGAGELAB_PRIVATES_APPKEY : "your appkey",
                ENGAGELAB_PRIVATES_CHANNEL: "developer",
                ENGAGELAB_PRIVATES_PROCESS: ":remote",
                // The following manufacturers are available
                // Xiaomi manufacturer information
                XIAOMI_APPID            : "",
                XIAOMI_APPKEY           : "",
                // MEIZU manufacturer information
                MEIZU_APPID            : "",
                MEIZU_APPKEY           : "",
                // OPPO manufacturer information
                OPPO_APPID             : "",
                OPPO_APPKEY            : "",
                OPPO_APPSECRET         : "",
                // VIVO manufacturer information
                VIVO_APPID             : "",
                VIVO_APPKEY            : "",
                // Honor manufacturer information
                HONOR_APPID            : ""
        ]
```
- Add before code SPLITS_VERSION_CODE LAUNCHER_SOURCE_BUILD_SETUP
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
        from('Your agconnect-services.json file path')
        into('./')
        include("agconnect-services.json")
    }

    copy {
        delete("src/main/assets/mt_engagelab_push_config")
        from('Your mt_engagelab_push_config file path')
        into('src/main/assets/')
        include("mt_engagelab_push_config")
    }
}
preBuild.dependsOn copyJsonFile
```
For details, please refer to the launcherTemplate.gradle file under Examples



#### Modify the gradleTemplate.properties file:

Add the following Code

```
// If you don’t use GOOLE manufacturer, you don’t need to add it.
android.useAndroidX=true
```
For details, please refer to the gradleTemplate.properties file under Examples

#### Modify the LauncherManifest.xml file：

Add it under the application tag

```
android:name="com.engagelab.privates.unity.push.MTPushApplication"
```

For details, please refer to the LauncherManifest.xml file under Examples

#### Configure the mt_engagelab_push_config file:

Add the following Code
```
{
	"tcp_ssl": true, // Fill in the true flag to use ssl encryption for tcp
	"debug":true,  // debug mode, true means printing debug logs
    "testGoogle": true // Fill in true to test fcm, it is only used for testing. In a production environment, please set it to false or delete this item.
}
```
For details, please refer to the mt_engagelab_push_config file under Examples.


### iOS
##  When copying the plugin, directly keep the corresponding debugging package.
## In the plugin iOS directory, there is the SDK package mtpush-ios-x.x.x.xcframework. There are two folders in the package.
 - "ios-arm64" is a real machine architecture, used for real machine running, debugging and release. Please keep this folder and delete `ios-arm64_×86_64-simulator` floder when you need to run it on a real machine.
 - "ios-arm64_×86_64-simulator" is the simulator architecture, used for simulator running and debugging. If you need to run the iOS simulator, please keep this folder and delete the `ios-arm64` folder. (that is, when the project configuration iOS Target SDK is specified as Simulator SDK).

1. Generate an iOS project and open the project.
2. Add necessary frameworks:

   - CFNetwork.framework
   - CoreFoundation.framework
   - CoreTelephony.framework
   - SystemConfiguration.framework
   - CoreGraphics.framework
   - Foundation.framework
   - UIKit.framework
   - Security.framework
   - libz.tbd（(Xcode 7 and below versions are libz.dylib)
   - AdSupport.framework (Required to get IDFA; don't add if you don't use IDFA)
   - UserNotifications.framework (Xcode 8 and above)
   - libresolv.tbd (Required for JPush 2.2.0 and above, libresolv.dylib for Xcode 7 and below)
   - StoreKit.framework 
   - libsqlite3.tbd 
   
      ​

3. Add the header file `MTPushUnityManager.h` in UnityAppController.mm.

    ```Objective-C
   #include <MTPushUnityManager.h>
   #import <AdSupport/AdSupport.h>// If you need to use the advertising identifier IDFA, add this header file, otherwise do not add it.
   #import <AppTrackingTransparency/AppTrackingTransparency.h>// If you need to use the advertising identifier IDFA, add this header file, otherwise do not add it.
    ```

4. Add the following code in the following methods of UnityAppController.mm:

    ```Objective-C
    - (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    
   
   __block NSString *advertisingId = nil;
   //    if (@available(iOS 14, *)) {
   //        //设置Info.plist中 NSUserTrackingUsageDescription, Requires ad tracking permissions to target unique user identifiers
   //        [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
   //            if (status == ATTrackingManagerAuthorizationStatusAuthorized) {
   //                advertisingId = [[ASIdentifierManager sharedManager] advertisingIdentifier].UUIDString;
   //            }
   //        }];
   //    } else {
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
    
5.Some Unity versions need to be modified after exporting the Xcode project:

```Objective-C
    In Preprocessor.h file,

    #define UNITY_USES_REMOTE_NOTIFICATIONS 0
    change to
    #define UNITY_USES_REMOTE_NOTIFICATIONS 1
```

## API description

[API](/Doc/CommonAPI.md)。

