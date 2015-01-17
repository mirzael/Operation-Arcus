using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class FindUnusedAssets 
{
    [MenuItem("Assets/Select Unused Assets")]
    static void Init()
    {
		List<string> usedAssets = FindUsedAssets();
		
		if(usedAssets != null)
		{
			List<Object> unusedAssets = FindUnusedObjects(FindAllAssets(), usedAssets);
			SelectObjects(unusedAssets);
		}
		else //unable to read EditorLog
		{
			EditorUtility.DisplayDialog("Editor log not found!", "You need to build your project before doing this!", "Ok");
		}
    }

    static void SelectObjects(List<Object> selectList)
    {
		if(selectList != null && selectList.Count > 0)
		{
			Object[] objArray = new Object[selectList.Count];

			int i=0;
			foreach(Object o in selectList)
			{
				objArray[i++] = o;
			}

			Selection.objects = objArray;
		}
    }

    private static List<Object> FindUnusedObjects(string[] assetList, List<string> usedAssets)
    {
        List<Object> unusedAssets = new List<Object>();

        for (int i = 0; i < assetList.Length; i++)
        {
            if (!usedAssets.Contains(assetList[i]))
            {
                Object objToFind = AssetDatabase.LoadAssetAtPath(assetList[i], typeof(Object));

				string path = AssetDatabase.GetAssetPath(objToFind).ToLower();  
    
				if (!path.Contains("/editor/"))
				{
					unusedAssets.Add(objToFind);
				}
            }
        }

		return unusedAssets;
    }

	public static string[] FindAllAssets()
    {
        string[] tmpAssets1 = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories);
        string[] tmpAssets2 = System.Array.FindAll(tmpAssets1, name => !name.EndsWith(".meta"));
        string[] allAssets;

        allAssets = System.Array.FindAll(tmpAssets2, name => !name.EndsWith(".unity"));

        for (int i = 0; i < allAssets.Length; i++)
        {
            allAssets[i] = allAssets[i].Substring(allAssets[i].IndexOf("/Assets") + 1);
            allAssets[i] = allAssets[i].Replace(@"\", "/");
        }

        return allAssets;
    }

    public static List<string> FindUsedAssets()
    {
		List<string> usedAssets = new List<string>();

        string UnityEditorLogfile = string.Empty;

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            UnityEditorLogfile = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\Unity\\Editor\\Editor.log";
        }
        else if (Application.platform == RuntimePlatform.OSXEditor)
        {
            UnityEditorLogfile = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/Library/Logs/Unity/Editor.log";
        }

        try
        {
            FileStream FS = new FileStream(UnityEditorLogfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader SR = new StreamReader(FS);

            string line;
            while (!SR.EndOfStream && !(line = SR.ReadLine()).Contains("Used Assets,")) ;
            while (!SR.EndOfStream && (line = SR.ReadLine()) != "")
            {
                line = line.Substring(line.IndexOf("% ") + 2);
                usedAssets.Add(line);
            }
        }
        catch (System.Exception E)
        {
            Debug.LogError("Error: " + E);
        }

		if(usedAssets.Count == 0)
			return null;

		return usedAssets;
    }
}