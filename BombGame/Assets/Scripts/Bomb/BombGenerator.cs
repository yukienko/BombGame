using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGenerator : MonoBehaviour
{
    [SerializeField] BombBase[] bombs = default;
    [SerializeField] Transform bombParent = default;

    private const float GenerateSpan = 2.0f;

    private float time = 0;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > GenerateSpan)
        {
            GenerateEnemy();
            time = 0;
        }
    }

    private void GenerateEnemy()
    {
        var ramdom = Random.Range(0, bombs.Length);
        Instantiate(bombs[ramdom], bombParent).transform.position = Vector3.zero;

    }
}
