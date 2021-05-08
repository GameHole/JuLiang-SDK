using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniGameSDK;
namespace Default
{
	public class Test:MonoBehaviour
	{
        IAnalyzeEvent analyze;

        public void Send0()
        {
            Debug.Log("send 有参");
            analyze.SetEvent("test_event", new KVPair() { key = "test", value = "aa" });
        }
        public void Send1()
        {
            Debug.Log("send");
            analyze.SetEvent("test_event");
        }
    }
}
