using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTime : MonoBehaviour
{
    Text timeText = default;
    private float time = 0f;

    private void Start()
    {
        timeText = gameObject.GetComponent<Text>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("f3");
    }
}
