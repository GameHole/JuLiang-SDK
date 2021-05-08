using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System.Text;
using System.Runtime.InteropServices;

namespace JuLiang
{
    public class JuLiangEvent : IAnalyzeEvent
    {
#if UNITY_ANDROID
        AndroidJavaClass ene;
#endif
#if UNITY_IOS
        [DllImport("__Internal")]
        static extern void m_Init(string appid, bool play, bool debug, string channal,string appname);
        [DllImport("__Internal")]
        static extern void m_SetEvent(string appid, string values);
#endif
        public JuLiangEvent()
        {
            var jl = AScriptableObject.Get<JuLiangPama>();
#if UNITY_ANDROID
            var activity = ActivityGeter.GetActivity();
#if !UNITY_EDITOR
            if (activity == null)
            {
                Debug.LogError("获取 unity Activity 失败");
                return;
            }
#endif
            ///DynamicPermission.GetPermission("android.permission.READ_PHONE_STATE");
            ene = new AndroidJavaClass("com.myunity.juliang.EventAna");
            ene?.CallStatic("Init", "mask", activity, jl.android.appid, jl.android.enablePlay, jl.android.debug, jl.android.channel);
#endif
#if UNITY_IOS
#if !UNITY_EDITOR
            _Init(jl.ios.appid, jl.ios.enablePlay, jl.ios.debug, jl.ios.channel, Application.productName);
#endif
#endif
        }
        public void SetEvent(string key)
        {
#if UNITY_ANDROID
            ene?.CallStatic("SendEvent", "mask", key, "");
#endif
        }

        public void SetEvent(string key, Dictionary<string, string> values)
        {

            StringBuilder builder = new StringBuilder();
            foreach (var item in values)
            {
                builder.Append(item.Key);
                builder.Append(":");
                builder.Append(item.Value);
                builder.Append(",");
            }
            if (builder.Length > 0)
                builder.Remove(builder.Length - 1, 1);
#if UNITY_ANDROID
            ene?.CallStatic("SendEvent", "mark", key, builder.ToString());
#endif
#if UNITY_IOS
#if !UNITY_EDITOR
            _SetEvent(key, builder.ToString());
#endif
#endif
        }

        public void SetEvent(string key, params KVPair[] pairs)
        {
            var dc = new Dictionary<string, string>();
            foreach (var item in pairs)
            {
                dc.Add(item.key, item.value);
            }
            SetEvent(key, dc);
        }
    }
}
