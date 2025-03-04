using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    MapManager mapManager;
    LoadLevel loadLevel;

    public Stage currentStage = Stage.Stage1;
    [Range(0, 100)]
    public int wallRatio = 15;
    [Range(0, 100)]
    public int blockRatio = 15;
    [Range(0, 100)]
    public int enemyRatio = 15;

    int[,] map;

    #region BothSideWall
    List<int[,]> wallList = new List<int[,]>();

    int[,] wall_0 = new int[11, 11] {
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
                                   };

    int[,] wall_1 = new int[11, 11] {
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1 },
                                        { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                        { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                        { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                        { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                        { 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1 },
                                        { 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1 },
                                        { 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
                                   };

    int[,] wall_2 = new int[11, 11] {
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                                        { 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1 },
                                        { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                        { 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1 },
                                        { 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
                                   };

    int[,] wall_3 = new int[11, 11] {
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                                        { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
                                   };

    int[,] wall_4 = new int[11, 11] {
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
                                   };

    int[,] wall_5 = new int[11, 11] {
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
                                   };

    int[,] wall_6 = new int[11, 11] {
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 9, 9, 9, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 9, 9, 9, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 9, 9, 9, 0, 0, 0, 1 },
                                        { 1, 1, 0, 0, 9, 9, 9, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 9, 9, 9, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
                                   };

    int[,] wall_7 = new int[11, 11] {
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 9, 9, 1, 1, 1, 0, 0, 0, 0, 1 },
                                        { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1 },
                                        { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
                                   };

    int[,] wall_8 = new int[11, 11] {
                                        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 9, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 9, 1, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 9, 1, 1, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
                                        { 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 }
                                   };


    #endregion

    #region LeftWall
    List<int[,]> wallListLeft = new List<int[,]>();

    int[,] wall_l_0 = new int[4, 4] { { 1, 0, 0, 0 },
                                      { 1, 1, 1, 1 },
                                      { 1, 1, 1, 0 },
                                      { 1, 0, 0, 0 } };

    int[,] wall_l_1 = new int[6, 6] { { 1, 0, 0, 0, 0, 0 },
                                      { 1, 1, 1, 1, 0, 0 },
                                      { 1, 1, 1, 1, 1, 0 },
                                      { 1, 1, 1, 1, 1, 0 },
                                      { 1, 0, 0, 1, 1, 0 },
                                      { 1, 0, 0, 0, 1, 0 } };

    int[,] wall_l_2 = new int[6, 2] { { 1, 1 },
                                    { 1, 1 },
                                    { 1, 1 },
                                    { 1, 1 },
                                    { 1, 1 },
                                    { 1, 1 } };

    int[,] wall_l_3 = new int[4, 4] { { 1, 0, 0, 0 },
                                    { 1, 1, 0, 0 },
                                    { 1, 1, 0, 0 },
                                    { 1, 0, 0, 0 } };

    int[,] wall_l_4 = new int[6, 6] { { 1, 0, 0, 1, 1, 0 },
                                      { 1, 1, 1, 1, 1, 1 },
                                      { 1, 1, 1, 1, 1, 1 },
                                      { 1, 1, 1, 1, 1, 0 },
                                      { 1, 1, 1, 1, 0, 0 },
                                      { 1, 0, 1, 1, 0, 0 } };
    #endregion

    #region RightWall
    List<int[,]> wallListRight = new List<int[,]>();

    int[,] R_wall_2_2 = new int[4, 4] { { 0, 0, 0, 1 },
                                        { 0, 0, 1, 1 },
                                        { 0, 0, 1, 1 },
                                        { 0, 0, 0, 1 } };

    int[,] R_wall_2_3 = new int[6, 6] { { 0, 1, 1, 0, 0, 1 },
                                        { 0, 1, 1, 1, 1, 1 },
                                        { 0, 0, 1, 1, 1, 1 },
                                        { 0, 0, 0, 1, 1, 1 },
                                        { 0, 0, 0, 0, 0, 1 },
                                        { 0, 0, 0, 0, 0, 1 } };
    #endregion

    #region Block
    List<int[,]> blockList = new List<int[,]>();

    int[,] block_0 = new int[1, 1] { { 1 } };

    int[,] block_1 = new int[1, 2] { { 1, 1 } };

    int[,] block_2 = new int[1, 3] { { 1, 1, 1 } };

    int[,] block_3 = new int[2, 2] { { 1, 1 },
                                     { 1, 1 } };

    int[,] block_4 = new int[3, 3] { { 1, 1, 1 },
                                     { 1, 1, 1 },
                                     { 1, 1, 1 } };

    #endregion

    #region StageGround

    int[,] stageGround = new int[11, 11] { { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                          { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                          { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                          { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                          { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                          { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                          { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                          { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                          { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                          { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                                          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        mapManager = MapManager.instance;
        loadLevel = LoadLevel.instance;
    }

    public int[,] GenerateLevel()
    {
        InitMap();

        InitwallList();
        InitwallListLeft();
        InitwallListRight();

        RandomLevelWall();
        //RandomWall();
        //RandomPlaceWall();

        InitBlockList();
        RandomLevelBlock();

        RandomLevelEnemy();

        return map;
    }

    void InitMap()
    {
        map = new int[mapManager.width, mapManager.height];

        for(int y = 0; y < mapManager.height; y++)
        {
            for(int x = 0; x < mapManager.width; x++)
            {
                map[x, y] = 0;
            }
        }

        InitWall();
    }

    void InitWall()
    {
        for (int y = 0; y < mapManager.height; y++)
        {
            map[0, y] = 1;
            map[mapManager.width - 1, y] = 1;
        }
    }

    void InitwallList()
    {
        wallList.Add(wall_0);
        wallList.Add(wall_1);
        wallList.Add(wall_2);
        wallList.Add(wall_3);
        wallList.Add(wall_4);
        wallList.Add(wall_5);
        wallList.Add(wall_6);
        wallList.Add(wall_7);
        wallList.Add(wall_8);
    }

    void InitwallListLeft()
    {
        wallListLeft.Add(wall_l_0);
        wallListLeft.Add(wall_l_1);
        wallListLeft.Add(wall_l_2);
        wallListLeft.Add(wall_l_3);
        wallListLeft.Add(wall_l_4);
    }

    void InitwallListRight()
    {
        wallListRight.Add(R_wall_2_2);
        wallListRight.Add(R_wall_2_3);
    }

    void InitBlockList()
    {
        blockList.Add(block_0);
        blockList.Add(block_1);
        blockList.Add(block_2);
        blockList.Add(block_3);
        blockList.Add(block_4);
    }

    void RandomLevelWall()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for (int y = 0; y < mapManager.height;)
        {
            if (rand.Next(0, 100) < wallRatio)
            {
                y += FillLevelWall(0, y);
            }
            else
            {
                y++;
            }
        }
    }

    void RandomWall()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for (int y = 0; y < mapManager.height; )
        {
            if(rand.Next(0, 100) < wallRatio)
            {
                y += FillWall(0, y);
            }
            else
            {
                y++;
            }
        }
    }

    void RandomPlaceWall()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for (int y = 0; y < mapManager.height; y++)
        {
            if (rand.Next(0, 100) < wallRatio)
                y += FillWallLeft(0, y);
            else
                y++;
        }

        for(int y = 0; y < mapManager.height; y++)
        {
            if (rand.Next(0, 100) < wallRatio)
                y += FillWallRight(mapManager.width, y);
            else
                y++;
        }
    }

    int FillLevelWall(int _x, int _y)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        List<Level> levels = loadLevel.GetLevels(currentStage);
        //Debug.Log(levels.Count);
        Level randomWall = levels[rand.Next(0, levels.Count)];

        int[,] wallRandom = new int[randomWall.height, randomWall.width];

        for (int y = 0; y < randomWall.height; y++)
        {
            for(int x = 0; x < randomWall.width; x++)
            {
                //Debug.Log(new Vector2(x, y));
                wallRandom[y, x] = randomWall.tiles[y * randomWall.width + x];
            }
        }

        //int[,] wallRandom = wallList[rand.Next(0, wallList.Count)];

        int wallWidth = wallRandom.GetLength(1);
        int wallHeight = wallRandom.GetLength(0);

        for (int y = _y; y < _y + wallHeight; y++)
        {
            for (int x = _x; x < _x + wallWidth; x++)
            {
                if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
                {
                    //Debug.Log(new Vector2(x, y));
                    map[x, y] = wallRandom[y - _y, x - _x];
                }
            }
        }

        return wallHeight;
    }

    int FillWall(int _x, int _y)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        int[,] wallRandom = wallList[rand.Next(0, wallList.Count)];

        int wallWidth = wallRandom.GetLength(1);
        int wallHeight = wallRandom.GetLength(0);

        for (int y = _y; y < _y + wallHeight; y++)
        {
            for (int x = _x; x < _x + wallWidth; x++)
            {
                if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
                {
                    map[x, y] = wallRandom[y - _y, x - _x];
                }
            }
        }

        return wallHeight;
    }

    int FillWallLeft(int _x, int _y)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        int[,] wallRandom = wallListLeft[rand.Next(0, wallListLeft.Count)];

        int wallWidth = wallRandom.GetLength(1);
        int wallHeight = wallRandom.GetLength(0);

        for (int y = _y; y < _y + wallHeight; y++)
        {
            for (int x = _x; x < _x + wallWidth; x++)
            {
                if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
                {
                    map[x, y] = wallRandom[y - _y, x - _x];
                }
            }
        }

        return wallHeight;
    }

    int FillWallRight(int _x, int _y)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        int[,] wallRandom = wallListRight[rand.Next(0, wallListRight.Count)];

        int wallWidth = wallRandom.GetLength(0);
        int wallHeight = wallRandom.GetLength(1);

        for (int y = _y; y < _y + wallHeight; y++)
        {
            for (int x = _x - wallWidth; x < _x; x++)
            {
                if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
                {
                    map[x, y] = wallRandom[y - _y, x - (_x - wallWidth)];
                }
            }
        }

        return wallHeight;
    }

    void RandomLevelBlock()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for (int y = 0; y < mapManager.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                if (map[x, y] == 2)
                {
                    if (rand.Next(0, 100) < blockRatio)
                        FillLevelBlock(x, y);
                    else
                        map[x, y] = 0;
                }
            }
        }
    }

    void RandomBlock()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for (int y = 0; y < mapManager.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                if (map[x, y] != 1 && rand.Next(0, 100) < blockRatio)
                    FillBlock(x, y);
            }
        }
    }

    void FillLevelBlock(int _x, int _y)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        int[,] blockRandom = blockList[rand.Next(0, blockList.Count)];

        int blockWidth = blockRandom.GetLength(1);
        int blockHeight = blockRandom.GetLength(0);

        for (int y = _y; y < _y + blockHeight; y++)
        {
            for (int x = _x; x < _x + blockWidth; x++)
            {
                if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
                {
                    if (map[x, y] != 1 && map[x, y] != 2)
                        map[x, y] = blockRandom[y - _y, x - _x] == 1 ? 2 : 0;
                }
            }
        }
    }

    void FillBlock(int _x, int _y)
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        int[,] blockRandom = blockList[rand.Next(0, blockList.Count)];

        int blockWidth = blockRandom.GetLength(1);
        int blockHeight = blockRandom.GetLength(0);

        for (int y = _y; y < _y + blockHeight; y++)
        {
            for (int x = _x; x < _x + blockWidth; x++)
            {
                if (x >= 0 && x < mapManager.width && y >= 0 && y < mapManager.height)
                {
                    if (map[x, y] != 1 && map[x, y] != 2 && map[x, y] == 9)
                        map[x, y] = blockRandom[y - _y, x - _x] == 1 ? 2 : 0;
                }
            }
        }
    }

    void RandomLevelEnemy()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for (int y = 0; y < mapManager.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                if (map[x, y] > 10)
                {
                    if (rand.Next(0, 100) > enemyRatio)
                        map[x, y] = 0;
                }
            }
        }
    }

    void RandomEnemy()
    {
        string seed = (Time.time + Random.value).ToString();
        System.Random rand = new System.Random(seed.GetHashCode());

        for (int y = 0; y < mapManager.height; y++)
        {
            for (int x = 0; x < mapManager.width; x++)
            {
                if (map[x, y] == 0 && rand.Next(0, 100) < enemyRatio)
                    map[x, y] = 3;
            }
        }
    }

    public int[,] GenerateStageGround()
    {
        return stageGround;
    }
}
