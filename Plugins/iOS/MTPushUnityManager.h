//
//  JPushUnityAnalytics.h
//  test_certifacate
//
//  Created by qinghe on 14-4-15.
//  Copyright (c) 2014年 jpush. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UserNotifications/UserNotifications.h>
#import "MTPushService.h"
@interface MTPushUnityManager : NSObject

@end
//extern id APNativeJSONObject(NSData *data);
//extern NSData *APNativeJSONData(id obj);


@interface MTPushUnityInstnce : NSObject<MTPushRegisterDelegate>
+ (MTPushUnityInstnce*)sharedInstance;
- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions advertisingId:(NSString *) advertisingId;

//ok
- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken;

@end
