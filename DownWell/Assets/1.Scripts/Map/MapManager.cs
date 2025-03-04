using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
public class MapManager : MonoBehaviour
{
    #region Singleton
    public static MapManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    MapGenerator mapGen;
    MapDisplay mapDisplay;
    LevelGenerator lg;

    public int width = 10;
    public int height = 100;

    // Start is called before the first frame update
    void Start()
    {
        mapGen = GetComponent<MapGenerator>();
        mapDisplay = GetComponent<MapDisplay>();
        lg = GetComponent<LevelGenerator>();

        StartCoroutine(FirstGenerateMap());
    }

    IEnumerator FirstGenerateMap()
    {
        yield return null;

        //Tile[,] genMap = mapGen.GenerateMap();
        //mapDisplay.Display(genMap);

        int[,] genLev = lg.GenerateLevel();
        int[,] genSgr = lg.GenerateStageGround();
        mapDisplay.Display(genLev, genSgr);
    }
}
