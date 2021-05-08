using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;
namespace JuLiang
{
	public class JuLiangBuildPostProcess
	{
        [PostProcessBuildAttribute]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target == BuildTarget.iOS)
            {
                // Read.
                string projectPath = PBXProject.GetPBXProjectPath(path);
                PBXProject project = new PBXProject();
                project.ReadFromString(File.ReadAllText(projectPath));
                string targetName = PBXProject.GetUnityTargetName();
                string targetGUID = project.TargetGuidByName(targetName);

                AddFrameworks(project, targetGUID);

                // Write.
                project.WriteToFile(projectPath);// File.WriteAllText(projectPath, project.WriteToString());
            }
        }

        static void AddFrameworks(PBXProject project, string targetGUID)
        {
            // Frameworks 

            project.AddFrameworkToProject(targetGUID, "libz.tbd", false);
            project.AddFrameworkToProject(targetGUID, "libsqlite3.tbd", false);
            project.AddFrameworkToProject(targetGUID, "JavaScriptCore.framework", false);
            project.AddFrameworkToProject(targetGUID, "WebKit.framework", false);
            project.AddFrameworkToProject(targetGUID, "CoreFoundation.framework", false);
            project.AddFrameworkToProject(targetGUID, "CoreTelephony.framework", false);
            project.AddFrameworkToProject(targetGUID, "Security.framework", false);
            project.AddFrameworkToProject(targetGUID, "SystemConfiguration.framework", false);
            project.AddFrameworkToProject(targetGUID, "AdSupport.framework", false);
            project.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-ObjC");
        }
    }
}
