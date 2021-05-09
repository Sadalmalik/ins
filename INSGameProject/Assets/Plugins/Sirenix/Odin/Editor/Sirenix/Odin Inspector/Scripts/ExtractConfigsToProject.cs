using Sirenix.Serialization;
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;


namespace Sirenix.Utilities
{
    internal static class ExtractConfigsToProject
    {
        [InitializeOnLoadMethod]
        private static void ExtractConfigs()
        {
            const string configPath = "Assets/Plugins/Sirenix/Odin Inspector/Config/Editor/";
            const string configResourcesPath = "Assets/Plugins/Sirenix/Odin Inspector/Config/Resources/Sirenix/";
            
            // It's important to trigger static constructor
            if (!SirenixAssetPaths.OdinEditorConfigsPath.Equals(configPath))
            {
                string projectPath = Application.dataPath;
                projectPath = projectPath.Substring(0, projectPath.Length - 7); // Trim "/Assets"
                if (!Directory.Exists($"{projectPath}/{configPath}"))
                {
                    Directory.CreateDirectory($"{projectPath}/{configPath}");
                }
                if (!Directory.Exists($"{projectPath}/{configResourcesPath}"))
                {
                    Directory.CreateDirectory($"{projectPath}/{configResourcesPath}");
                }

                SetStaticReadonlyField(typeof(SirenixAssetPaths), "OdinEditorConfigsPath", configPath);
                SetStaticReadonlyField(typeof(SirenixAssetPaths), "OdinResourcesPath", configResourcesPath);
                SetStaticReadonlyField(typeof(SirenixAssetPaths), "OdinResourcesConfigsPath", configResourcesPath);
                
                SetupGlobalSerializationAssetPath();
            }
            
            
            void SetStaticReadonlyField(Type type, string fieldName, string fieldValue)
            {
                FieldInfo fieldInfo = type.GetField(fieldName,BindingFlags.Static | BindingFlags.Public);
                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(null, fieldValue);
                }
                else
                {
                    Debug.LogWarning($"Can't file field {fieldName} in type {type}!");
                }
            }
            
            
            void SetupGlobalSerializationAssetPath()
            {
                PropertyInfo propertyInfo = typeof(GlobalConfig<GlobalSerializationConfig>).GetProperty("ConfigAttribute", BindingFlags.Static | BindingFlags.NonPublic);
                GlobalConfigAttribute attribute = propertyInfo.GetValue(null) as GlobalConfigAttribute;
                
                FieldInfo fieldInfo = typeof(GlobalConfigAttribute).GetField("assetPath",BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo.SetValue(attribute, configResourcesPath);
            }
        }
    }
}