# API 说明

## 监听，回调的消息

### onMTReceiver(string messageJson) （android/ios都支持）

集成了 sdk 回调的事件
```
{
"event_name": "",//事件类型
"event_data": ""//事件内容
}
```

#### 参数说明
- messageJson:反回的事件数据
  - "event_name": 为事件类型
    - android:
      - "onNotificationStatus":应用通知开关状态回调,内容类型为boolean，true为打开，false为关闭
      - "onConnectStatus":长连接状态回调,内容类型为boolean，true为连接
      - "onNotificationArrived":通知消息到达回调，内容为通知消息体
      - "onNotificationClicked":通知消息点击回调，内容为通知消息体
      - "onNotificationDeleted":通知消息删除回调，内容为通知消息体
      - "onCustomMessage":自定义消息回调，内容为通知消息体
      - "onPlatformToken":厂商token消息回调，内容为厂商token消息体
      - "OnTagOperateResult":标签操作消息回调
      - "OnAliasOperateResult":别名操作消息回调
    - ios:
      - "willPresentNotification":通知消息到达回调，内容为通知消息体
      - "didReceiveNotificationResponse":通知消息点击回调，内容为通知消息体
      - "networkDidReceiveMessage":自定义消息回调，内容为通知消息体
      - "networkDidLogin":登陆成功
      - "OnTagOperateResult":标签操作消息回调
      - "OnAliasOperateResult":别名操作消息回调
  - "event_data": 为对应内容


#### 代码示例

```js
void onMTReceiver(string jsonStr)
{
  Debug.Log("recv----onMTReceiver-----" + jsonStr);
  str_unity = jsonStr;
}
```

## 初始化

### initAndroid （android）
### initIos （ios）

初始化sdk

#### 接口定义

```js
#if UNITY_ANDROID
MTPushBinding.InitMTPushAndroid(gameObject.name);
#endif

#if UNITY_IOS
MTPushBinding.InitMTPushIos(gameObject.name,"您的appkey",false,"demo",false);
#endif
```

### setSiteName

设置数据中心的名字，安卓也需要在launcherTemplate.gradle中manifestplaceholder 中设置
//数据中心名称，填空""时，默认"Singapore"数据中心
ENGAGELAB_PRIVATES_SITE_NAME: "xxx",

#### 接口定义

```js
MTPushBinding.setSiteName('xxx')
```

#### 返回值

无

#### 代码示例

```js
MTPushBinding.setSiteName('xxx');
```

## 开启 Debug 模式

### configDebugMode （android/ios都支持）

设置是否debug模式，debug模式会打印更对详细日志

#### 接口定义

```js
MTPushBinding.ConfigDebugMode(enable)
```

#### 参数说明

- enable: 是否调试模式，true为调试模式，false不是

#### 代码示例

```js
MTPushBinding.ConfigDebugMode(true);//发布前要删除掉
```

## 获取 RegistrationID （android/ios都支持）

### getRegistrationId

RegistrationID 定义:
获取当前设备的registrationId，Engagelab私有云唯一标识，可同于推送

#### 接口定义

```js
MTPushBinding.GetRegistrationId()
```

#### 返回值

调用此 API 来取得应用程序对应的 RegistrationID。 只有当应用程序成功注册到 JPush 的服务器时才返回对应的值，否则返回空字符串。

#### 代码示例

```js
string registrationId = MTPushBinding.GetRegistrationId();
```


## 标签与别名

### SetTags(int sequence, List<string> tags)

给当前设备设置标签。

注意该操作是覆盖逻辑，即每次调用会覆盖之前已经设置的标签。

#### 参数说明

- sequence: 作为一次操作的唯一标识，会在 `OnTagOperateResult` 回调中一并返回。
- tags: 标签列表。
  - 有效的标签组成：字母（区分大小写）、数字、下划线、汉字、特殊字符（@!#$&*+=.|）。
  - 限制：每个 tag 命名长度限制为 40 字节，单个设备最多支持设置 1000 个 tag，且单次操作总长度不得超过 5000 字节（判断长度需采用 UTF-8 编码）。

### AddTags(int sequence, List<string> tags)

给当前设备在已有的基础上新增标签。

#### 参数说明

- sequence: 作为一次操作的唯一标识，会在 `OnTagOperateResult` 回调中一并返回。
- tags: 标签列表。

### DeleteTags(int sequence, List<string> tags)

删除标签。

#### 参数说明

- sequence: 作为一次操作的唯一标识，会在 `OnTagOperateResult` 回调中一并返回。
- tags: 标签列表。

### CleanTags(int sequence)

清空标签。

#### 参数说明

- sequence: 作为一次操作的唯一标识，会在 `OnTagOperateResult` 回调中一并返回。

### GetAllTags(int sequence)

获取当前设备的所有标签。

#### 参数说明

- sequence: 作为一次操作的唯一标识，会在 `OnTagOperateResult` 回调中一并返回。

### CheckTagBindState(int sequence, string tag)

检查指定标签是否已经绑定。

`OnTagOperateResult` 回调中会附带 `isBind` 属性。

#### 参数说明

- sequence: 作为一次操作的唯一标识，会在 `OnTagOperateResult` 回调中一并返回。
- tag: 待查询的标签。

### SetAlias(int sequence, string alias)

设置别名，每个设备只会有一个别名。

注意：该接口是覆盖逻辑，即新的调用会覆盖之前的设置。

#### 参数说明

- sequence: 作为一次操作的唯一标识，会在 `OnAliasOperateResult` 回调中一并返回。
- alias: 要设置的别名。
  - 有效的别名组成：字母（区分大小写）、数字、下划线、汉字、特殊字符（@!#$&*+=.|）。
  - 限制：alias 命名长度限制为 40 字节（判断长度需采用 UTF-8 编码）。

### DeleteAlias(int sequence)

删除当前设备设置的别名。

#### 参数说明

- sequence: 作为一次操作的唯一标识，会在 `OnAliasOperateResult` 回调中一并返回。

### GetAlias(int sequence)

获取当前设备设置的别名。

#### 参数说明

- sequence: 作为一次操作的唯一标识，会在 `OnAliasOperateResult` 回调中一并返回。


### FilterValidTags(List<string> tags)  // only ios

过滤非法tag

#### 参数说明

- tags: tag列表