using System.Collections.Generic;
using UnityEngine;
namespace JuLiang
{
    [System.Serializable]
    public class JuLiangPamaBase
    {
        public bool debug = false;
        public string appid;
        public string channel;
        public bool enablePlay = true;
        public string pama = "rangersapplog.byAx6uYt";
    }
    public class JuLiangPama : AScriptableObject
    {
        public override string filePath => "巨量投放参数";
        //public bool debug = false;
        //public string appid;
        //public string channel;
        //public bool enablePlay = true;
        public JuLiangPamaBase android;
        public JuLiangPamaBase ios = new JuLiangPamaBase { channel = "App Store" };
    }
}
