using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum Stage { Stage1, Stage2, Stage3, Stage4, Stage5, Blocks }

public class JsonIO : MonoBehaviour
{
    public Stage stage;
    public string fileName = " ";

    private void Start()
    {
        fileName = "";
        //tiles = GameObject.Find("LevelTiles").GetComponentsInChildren<TileInfo>();
    }

    public void SaveToJson()
    {
        var jsonStr = LevelToJson();

        File.WriteAllText(Application.dataPath + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json", jsonStr);

        Debug.Log("Saved(" + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json)");
    }

    string LevelToJson()
    {
        Level level = new Level();
        level.tiles = new int[LevelEditorManager.instance.tiles.Count];

        for (int i = 0; i < level.tiles.Length; i++)
            level.tiles[i] = (int)LevelEditorManager.instance.tiles[i].GetComponent<TileInfo>().tileCode;

        level.width = (int)LevelEditorManager.instance.getCanvasSize().x;
        level.height = (int)LevelEditorManager.instance.getCanvasSize().y;

        return JsonUtility.ToJson(level);
    }

    public void LoadJson()
    {
        string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json");

        var level = JsonToLevel<Level>(jsonStr);

        LevelEditorManager.instance.LoadLevel(level);

        Debug.Log("Loaded(" + "/Resources/Levels/" + stage.ToString() + "/" + fileName + ".json)");
    }

    T JsonToLevel<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
}
