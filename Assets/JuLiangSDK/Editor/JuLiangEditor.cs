using System.Collections.Generic;
using UnityEditor;
namespace JuLiang
{
    [InitializeOnLoad]
	public class JuLiangEditor
	{
        [MenuItem("SDK/应用巨量参数")]
        static void ApplyJuLiangPama()
        {
            var jl = AScriptableObject.Get<JuLiangPama>();
            if (!jl)
                jl = AssetHelper.CreateOrGetAsset<JuLiangPama>();
            var gd = GradleHelper.Open();
            var cfg = gd.Root.FindNode("android/defaultConfig");
            if (cfg != null)
            {
                cfg.AddValue($"manifestPlaceholders.put(\"APPLOG_SCHEME\",\"{jl.android.pama}\".toLowerCase())");
                gd.Save();
            }
            //string fs = $"manifestPlaceholders.put(\"APPLOG_SCHEME\",\"{jl.android.pama}\".toLowerCase())";
            //if (!gd.texts.Contains(fs))
            //{
            //    var df = "defaultConfig {";
            //    int idx = gd.texts.IndexOf("defaultConfig {") + df.Length;
            //    gd.texts = gd.texts.Insert(idx, $"\n{fs}\n");
            //    gd.Save();
            //}
        }
    }
}
