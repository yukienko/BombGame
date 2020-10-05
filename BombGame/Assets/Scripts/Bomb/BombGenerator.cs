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
    static Vector3 center = new Vector3(0, -0.5f, 0);
    private const float widthY = 3.5f;
    private const float widthX = 6.0f;
    public bool isExplosion;
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
    private Vector3[] RandSpawnPos =
        { center,
        center + ConvenientAssets.v2Tov3(0, widthY),
        center + ConvenientAssets.v2Tov3(-widthX, 0),
        center + ConvenientAssets.v2Tov3(widthX, 0),
        center + ConvenientAssets.v2Tov3(0, -widthY)};

    private float time = 0;

    //初期各色生成数
    private const int DefaultGenerateBombValue = 10;
    [SerializeField] List<BombBase> BombsList = default;
    int activeEnemyCount = 0;

    private void Awake()
    {
        init();
    }

    private void init()
    {
        isExplosion = false;
        ReleaseListBombs();
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

        CheckExplosion();
    }

    //初期生成
    private void GenerateEnemy()
    {
        for (int i = 0; i < DefaultGenerateBombValue; i++)
        {
            var random = Random.Range(0, bombs.Length);
            BombsList.Add(Instantiate(bombs[random], bombParent));
            BombsList[i].gameObject.SetActive(false);
        }
    }

    //ボム表示
    private void SpawnEnemy()
    {
        BombsList[activeEnemyCount].gameObject.SetActive(true);
        var rand = ConvenientAssets.RandomInt(0, GeneratePosVolume);
        BombsList[activeEnemyCount].gameObject.transform.position = RandSpawnPos[rand];
        activeEnemyCount++;
    }

    //ボムのリリース
    private void ReleaseListBombs()
    {
        BombsList.RemoveAll(BombsList.Contains);
        BombsList.Clear();
    }

    private void CheckExplosion()
    {
        if (isExplosion)
        {
            foreach(Transform anim in bombParent.transform)
            {
                anim.gameObject.GetComponent<Animator>().SetBool("Explosion", true);
            }
        }
    }
}
