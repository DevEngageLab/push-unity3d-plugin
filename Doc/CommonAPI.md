# API description

## Listening event callback

### onMTReceiver(string messageJson) （Both android/ios support）

Listening event callback

```
{
"event_name": "",//event type
"event_data": ""//event content
}
```

#### Parameter Description
- messageJson:Returned event data
  - "event_name": event type
    - android:
      - "onNotificationStatus":Application notification switch status callback, the content type is boolean, true means open, false means closed
      - "onConnectStatus":tcp connection status callback, content type is boolean, true means connected.
      - "onNotificationArrived":Notification arrival event callback, the content is the notification message body
      - "onNotificationClicked":Notification click event callback, the content is the notification message body
      - "onNotificationDeleted":Notification deletion event callback, the content is the notification message body
      - "onCustomMessage":Custom message callback, the content is the message body of the custom message
      - "onPlatformToken":Manufacturer token message callback, the content is the manufacturer token message body
      - "OnTagOperateResult":Callback for TagOperate
      - "OnAliasOperateResult":Callback for AliasOperate
    - ios:
      - "willPresentNotification":Notification arrival event callback, the content is the notification message body
      - "didReceiveNotificationResponse":Notification click event callback, the content is the notification message body
      - "networkDidReceiveMessage":Custom message callback, the content is the message body of the custom message
      - "networkDidLogin":login successful
      - "OnTagOperateResult":Callback for TagOperate
      - "OnAliasOperateResult":Callback for AliasOperate
      - "OnNotificationAuthorizationResult":Status callback for notification permissions
  - "event_data": content


#### Example

```js
void onMTReceiver(string jsonStr)
{
  Debug.Log("recv----onMTReceiver-----" + jsonStr);
  str_unity = jsonStr;
}
```

## Setup

### initAndroid （android）
### initIos （ios）

Initialize sdk

#### Interface definition

```js
#if UNITY_ANDROID
MTPushBinding.InitMTPushAndroid(gameObject.name);
#endif

#if UNITY_IOS
MTPushBinding.InitMTPushIos(gameObject.name,"您的appkey",false,"demo",false);
#endif
```

### setSiteName

Set the name of the data center. Android also needs to be set in manifestplaceholder in launcherTemplate.gradle.
//Data center name, when filling in the blank "", the default is "Singapore" data center
ENGAGELAB_PRIVATES_SITE_NAME: "xxx",

#### Interface definition

```js
MTPushBinding.setSiteName('xxx')
```

#### return value

none

#### code example

```js
MTPushBinding.setSiteName('xxx');
```

## Turn on Debug mode

### configDebugMode （Both android/ios support）

Set whether to debug mode. Debug mode will print detailed logs.

#### Interface definition

```js
MTPushBinding.ConfigDebugMode(enable)
```

#### Parameter Description

- enable: Whether to debug mode, true means debugging mode, false does not

#### code example

```js
MTPushBinding.ConfigDebugMode(true);//Delete before publishing
```

## Get RegistrationID （Both android/ios support）

### getRegistrationId

RegistrationID :
Get the registrationId of the current device, which can be pushed through the registrationId

#### Interface definition

```js
MTPushBinding.GetRegistrationId()
```

#### return value

Call this API to get the RegistrationID. Only when the registration is successful, the EngageLab server returns the corresponding value, otherwise it returns an empty string.

#### code example

```js
string registrationId = MTPushBinding.GetRegistrationId();
```


## Tags and aliases

### SetTags(int sequence, List<string> tags)

Set a label for the current device

Note that this operation is an overwriting operation, that is, each call will overwrite the label that has been set before.

#### Parameter Description

- sequence: As the unique identifier of an operation, it will be returned in the `OnTagOperateResult` callback.
- tags: tag list.
  - Valid tags consist of: letters (case-sensitive), numbers, underscores, Chinese characters, special characters (@!#$&*+=.|).
  - Restrictions: The length of each tag is limited to 40 bytes. A single device supports setting up to 1,000 tags, and the total length of a single operation must not exceed 5,000 bytes (UTF-8 encoding is required to determine the length).

### AddTags(int sequence, List<string> tags)

Add a new label to the current device based on the existing one.

#### Parameter Description

- sequence: As the unique identifier of an operation, it will be returned in the `OnTagOperateResult` callback.
- tags: tag list。

### DeleteTags(int sequence, List<string> tags)

DeleteTags。

#### Parameter Description

- sequence: As the unique identifier of an operation, it will be returned in the `OnTagOperateResult` callback.
- tags: tag list。

### CleanTags(int sequence)

CleanTags。

#### Parameter Description

- sequence: As the unique identifier of an operation, it will be returned in the `OnTagOperateResult` callback.

### GetAllTags(int sequence)

Get all tags of the current device.

#### Parameter Description

- sequence: As the unique identifier of an operation, it will be returned in the `OnTagOperateResult` callback.

### CheckTagBindState(int sequence, string tag)

Check whether the specified tag has been bound.

The `OnTagOperateResult` callback will be accompanied by the `isBind` attribute.

#### Parameter Description

- sequence: As the unique identifier of an operation, it will be returned in the `OnTagOperateResult` callback.
- tag: The label to be queried.

### SetAlias(int sequence, string alias)

Set an alias. Each device will have only one alias.

Note: This interface is an overwrite operation, that is, new calls will overwrite previous settings.


#### Parameter Description

- sequence: As the unique identifier of an operation, it will be returned in the `OnAliasOperateResult` callback.
- alias: Alias ​​to set。
  - Valid aliases consist of: letters (case-sensitive), numbers, underscores, Chinese characters, special characters (@!#$&*+=.|).
  - Restrictions: The alias name length is limited to 40 bytes (UTF-8 encoding is required to determine the length).
  
### DeleteAlias(int sequence)

Delete the alias currently set on the device.

#### Parameter Description

- sequence: As the unique identifier of an operation, it will be returned in the `OnAliasOperateResult` callback.

### GetAlias(int sequence)

Get the alias for the current device settings.

#### Parameter Description

- sequence: As the unique identifier of an operation, it will be returned in the `OnAliasOperateResult` callback.


### FilterValidTags(List<string> tags)  // only ios

Filter illegal tags

#### Parameter Description

- tags: tag list



## Open the notification settings interface

### GoToAppNotificationSettingsAndroid （android）
### OpenSettingsForNotificationIOS （ios）


#### Interface definition

```js
#if UNITY_ANDROID
MTPushBinding.GoToAppNotificationSettingsAndroid();
#endif

#if UNITY_IOS
MTPushBinding.OpenSettingsForNotificationIOS();
#endif
```
```
