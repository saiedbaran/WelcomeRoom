using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace WelcomeRoom.Build
{
    public class BuildPlayer
    {
        private const string ScenePath = "Assets/Scenes/WelcomeRoom.unity";

        [MenuItem(("Build/Build VR"))]
        public static void BuildVR()
        {
            Build();
        }


        public static void JenkinsBuild()
        {
            var args = Environment.GetCommandLineArgs();

            var executeMethodIndex = Array.IndexOf(args, "-executeMethod");
            if (executeMethodIndex + 2 >= args.Length)
            {
                Log("[JenkinsBuild] Incorrect Parameters for -executeMethod Format: -executeMethod <output dir>");
                return;
            }

            //  args[executeMethodIndex + 1] = JenkinsBuild.Build
            var buildPath = args[executeMethodIndex + 2];

            BuildPlayer.Build(buildPath);
        }

        private static void Build(string buildPath = null)
        {
            if (string.IsNullOrEmpty(buildPath))
            {
                if (!UnityEditorInternal.InternalEditorUtility.isHumanControllingUs)
                    return;

                buildPath = EditorUtility.SaveFolderPanel("Choose Build Location", "Build", "WelcomeRoom");
                if (buildPath.Length == 0)
                    return;
            }

            if (UnityEditorInternal.InternalEditorUtility.isHumanControllingUs)
                Debug.ClearDeveloperConsole();

            Log($"Start building ...");

            PlayerSettings.bundleVersion = DateTime.UtcNow.Date.ToString("yyyyMMdd");

            var buildPlayerOptions = new BuildPlayerOptions
            {
                target = BuildTarget.StandaloneWindows,
                options = BuildOptions.None,
                locationPathName = $"{buildPath}/WelcomeRoom.exe",
                scenes = new []{ScenePath}
            };

            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            HandleBuildResult(report.summary);
        }

        private static void HandleBuildResult(BuildSummary summary)
        {
            switch (summary.result)
            {
                case BuildResult.Succeeded:
                    Log($"Build succeeded: {summary.totalSize} bytes");
                    break;
                case BuildResult.Failed:
                    Log("Build failed!");
                    break;
                case BuildResult.Unknown:
                    Log("Unknown Build result.");
                    break;
                case BuildResult.Cancelled:
                    Debug.Log("Build cancelled.");
                    break;
                default:
                    throw new Exception("BuildPlayer: Unable to handle build result.");
            }
        }

        private static void Log(string message)
        {
            if (UnityEditorInternal.InternalEditorUtility.isHumanControllingUs)
                Debug.Log(message);
            else
                Console.WriteLine(message);
        }
    }
}
