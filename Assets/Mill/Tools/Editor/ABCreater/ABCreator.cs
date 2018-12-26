using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class ABCreator : Editor {

    static string AssetPath = "/Mill/Assets/";
    static string extension = ".next";

    [MenuItem("资源包工具/打包")]
    public static void CreateAssetBundlesc创建资源包()
    {
        string outPath = Application.streamingAssetsPath;
        ///创建文件夹
        Directory.CreateDirectory(outPath);

        if (Directory.Exists(outPath))
        {
            Directory.Delete(outPath, true);
            Directory.CreateDirectory(outPath);
        }

        ///打包资源包API
        BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
    [MenuItem("资源包工具/批量设置资源包名")]
    public static void SetAssetsABName批量设置资源包名()
    {
        string ab包资源路径 = Application.dataPath + AssetPath;
        Debug.Log(ab包资源路径);
        FindFiles(ab包资源路径);
    }

    /// <summary>
    /// 寻找文件 深度查找
    /// </summary>
    /// <param name="path"></param>
    public static void FindFiles(string path)
    {

        string rootPath = Application.dataPath + AssetPath;

        string[] folderNames当前文件夹中的文件夹名 = Directory.GetDirectories(path);

        string[] fileNames当前文件夹中的文件名称 = Directory.GetFiles(path);
        
        for (int i = 0; i < fileNames当前文件夹中的文件名称.Length; i++)
        {
            
            string fileName = fileNames当前文件夹中的文件名称[i];
            string filePath = fileNames当前文件夹中的文件名称[i];
            if (!fileName.Contains(".meta"))
            {
                ///资源包名
                fileName = fileName.Replace(@"\", "/");
                fileName = fileName.Replace(rootPath, "");
               
                fileName = fileName.Remove(fileName.LastIndexOf("/"));
               
                //fileName = fileName.Substring(fileName.LastIndexOf(".") + 1);
                //Debug.Log(fileName);

                ///资源所在的相对路径
                filePath = filePath.Replace(@"\", "/");
                filePath = filePath.Replace(rootPath, "");
                filePath = "Assets" + AssetPath + filePath;
                Debug.Log("FilePath" + filePath);
                ///使用AssetImporter 把当前资源包名重置成指定包名
                AssetImporter assetImporter资源导入 = AssetImporter.GetAtPath(filePath);
                Debug.Log(fileName);
                assetImporter资源导入.assetBundleName = fileName + ".next";
            }
        }

        for (int i = 0; i < folderNames当前文件夹中的文件夹名.Length; i++)
        {
            FindFiles(folderNames当前文件夹中的文件夹名[i]);
        }
    }

}
