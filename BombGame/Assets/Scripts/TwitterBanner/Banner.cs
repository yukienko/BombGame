using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Banner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickTwitterButton()
    {
        //urlの作成
        string esctext = UnityWebRequest.EscapeURL("テスト　Unity\nhttps://play.google.com/store/apps/details?id=com.aniplex.magireco&hl=ja//");
        string esctag = UnityWebRequest.EscapeURL("最近やってないなぁ");
        string url = "https://twitter.com/intent/tweet?text=" + esctext + "\n&hashtags=" + esctag;

        //Twitter投稿画面の起動
        Application.OpenURL(url);
    }
}
