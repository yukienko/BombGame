using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using conv;

public class BombGenerator : MonoBehaviour
{
    [SerializeField] BombBase[] bombs = default;
    [SerializeField] Transform bombParent = default;

    private const float GenerateSpan = 2.0f;
    private const int GeneratePosVolume = 5;

    private float time = 0;

    //初期各色生成数
    private const int DefaultGenerateBombValue = 100;
    [SerializeField] List<BombBase> BombsList = default;
    int activeEnemyCount = 0;

    private void Awake()
    {
        GenerateEnemy();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > GenerateSpan)
        {
            //GenerateEnemy();
            SpawnEnemy();
            time = 0;
        }
    }

    //初期生成
    private void GenerateEnemy()
    {
        for (int i = 0; i < DefaultGenerateBombValue; i++)
        {
            var random = Random.Range(0, bombs.Length);
            BombsList.Add(Instantiate(bombs[random], bombParent));
            BombsList[i].gameObject.SetActive(false);
            Debug.Log(i);
        }
    }

    //ボム表示
    private void SpawnEnemy()
    {
        BombsList[activeEnemyCount].gameObject.SetActive(true);
        BombsList[activeEnemyCount].gameObject.transform.position = RandSpawnPos();
        activeEnemyCount++;
    }

    private Vector3 RandSpawnPos()
    {
        Vector3 center = new Vector3(0, -0.5f, 0);
        Vector3 ret = center;
        float widthY = 3.5f;
        float widthX = 6.0f;

        //番号振り分け
        //UIUIUIUIUIUIUIUIUIUIUIUIUIUI
        //UI                        UI
        //UI           1            UI
        //UI                        UI
        //UI                        UI
        //UI   2       0        3   UI
        //UI                        UI
        //UI                        UI
        //UI           4            UI
        var rand = ConvenientAssets.RandomInt(0, GeneratePosVolume);
        switch (rand)
        {
            case 0:
                ret = center;
                break;
            case 1:
                ret = center + ConvenientAssets.v2Tov3(0, widthY);
                break;
            case 2:
                ret = center + ConvenientAssets.v2Tov3(-widthX, 0);
                break;
            case 3:
                ret = center + ConvenientAssets.v2Tov3(widthX, 0);
                break;
            case 4:
                ret = center + ConvenientAssets.v2Tov3(0, -widthY);
                break;
        }
        return ret;
    }
}
