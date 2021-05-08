#import "BDAutoTrack.h"
//#import“BDAutoTrack+SharedInstance.h"
#import "BDAutoTrackConfig.h"
#import "BDAutoTrack+Game.h"
extern  void _Init(char*appid,bool enablePlay,bool debug,char* channal,char* appname){
 /* 初始化开始 */
 BDAutoTrackConfig *config = [BDAutoTrackConfig new];
 /* 域名默认国内: BDAutoTrackServiceVendorCN,
 新加坡: BDAutoTrackServiceVendorSG,
 美东:BDAutoTrackServiceVendorVA, 
 注意：国内外不同vendor服务注册的did不⼀样。由CN切换到SG或者VA，会发⽣变化，切回来也
会发⽣变化。因此vendor的切换⼀定要慎重，随意切换导致⽤户新增和统计的问题，需要⾃⾏评估。*/
 config.serviceVendor = BDAutoTrackServiceVendorCN;
 config.appID =[NSString stringWithUTF8String:appid]; // 如不清楚请联系专属客户成功经理
 config.appName = [NSString stringWithUTF8String:appname]; // 与您申请APPID时的app_name⼀致
 config.channel = [NSString stringWithUTF8String:channal]; // iOS⼀般默认App Store
 /*5.配置⼼跳事件（时⻓统计）
需在初始化是⼿动配置打开⼼跳事件play_session上报，⽤于游戏时⻓统计。
6.上报⾏为⽇数据
必传埋点
投放深度转化须上报register：注册、purchase：⽀付两个埋点事件，由SDK预定义，调⽤如下接
⼝即可：*/
 config.showDebugLog = debug; // 是否在控制台输出⽇志，仅调试使⽤。release版本请设置为NO
 config.logNeedEncrypt = debug; // 是否加密⽇志，默认加密。release版本请设置为 YES

config.gameModeEnable = enablePlay;
 
 [BDAutoTrack startTrackWithConfig:config];

// 内置事件: “注册” ， 属性：注册⽅式，是否成功，属性值为：wechat ，YES
[BDAutoTrack registerEventByMethod:@"wechat" isSuccess:YES];
// 内置事件 “购买道具”，属性：道具类型，道具名称，道具ID， 道具数量，⽀付渠道，币种，⾦额（必传），是否成功（必传）
[BDAutoTrack purchaseEventWithContentType:@"gift"
 contentName:@"flower"
 contentID:@"008"
 contentNumber:1
 paymentChannel:@"wechat"
 currency:@"￥"
 currency_amount:1
 isSuccess:YES];
}
extern void _SetEvent(char* key,char*values){
NSMutableDictionary *dictionary = [NSMutableDictionary dictionaryWithCapacity:1];
NSString *vs=[NSString stringWithUTF8String:values];
NSArray *arr = [vs componentsSeparatedByString:@","];
 for (int i=0; i<arr.count; i++) {
      NSString* kv= arr[i];
      NSArray * kvsp= [kv componentsSeparatedByString:@":"];
[dictionary setObject:kvsp[1] forKey:kvsp[0]];
    }
[BDAutoTrack eventV3:[NSString stringWithUTF8String:key]
params:dictionary];
}