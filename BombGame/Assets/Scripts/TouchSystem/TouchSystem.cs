using StateManager;
using StaticManager;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TouchSystem : MonoBehaviour
{
    [SerializeField] Camera _camera = default;
    TouchManager _touchManager;
    TouchManager old_phase;
    BombBase bombBase;
    Vector3 oldPos;
    bool isMove;
    Vector2 GameFieldPixel;
    Vector3 pos = default;

    // Start is called before the first frame update
    void Start()
    {
        _touchManager = new TouchManager();
        old_phase = new TouchManager();
        isMove = false;
        GameFieldPixel = new Vector2(HorizontalPixelCalc(), VerticalPixelCalc());
        Debug.LogWarning("GameFieldPixel:" + GameFieldPixel);
    }

    // Update is called once per frame
    void Update()
    {
        //タッチ情報更新
        TouchManagerUpdate();
        //タッチされているオブジェクトなどの更新
        TouchSystemUpdate();
    }

    void TouchSystemUpdate()
    {
        if (_touchManager._touch_flag)
        {
            //クリックし始めの処理
            if (old_phase._touch_phase == TouchPhase.Began)
            {
                TouchPhaseBegan();
            }
            else if (_touchManager._touch_phase == TouchPhase.Moved)
            {
                TouchPhaseMoved();
            }
            else if (_touchManager._touch_phase == TouchPhase.Ended)
            {
                TouchPhaseEnded();
            }
            oldPos = pos;
        }
        //oldの更新
        old_phase._touch_phase = _touchManager._touch_phase;
    }

    void TouchPhaseBegan()
    {
        // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
        pos = _camera.ScreenToWorldPoint(AlmightyTapPosition() + _camera.transform.forward * 10);
    }

    void TouchPhaseMoved()
    {
        //ボムをつかむまで
        if (bombBase == default)
        {
            float distance = 100; // 飛ばす&表示するRayの長さ
            float duration = 3;   // 表示期間（秒）
            Ray ray = Camera.main.ScreenPointToRay(AlmightyTapPosition());
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
            RaycastHit hit = new RaycastHit();
            //ボムをつかんだ
            if (Physics.Raycast(ray, out hit, distance))
            {
                if (hit.collider.tag == "EnemyBomb")
                {
                    isMove = true;
                    bombBase = hit.collider.gameObject.GetComponent<BombBase>();
                    BombAnimationChatch();
                }
            }
        }
        //クリックし続けてるなら
        if (isMove)
        {
            pos = _camera.ScreenToWorldPoint(AlmightyTapPosition() + _camera.transform.forward * 10);
            Vector3 fixPos = AlmightyTapPosition();
            if (CheckUIRect(0) == 1)
                fixPos.x = EachPixel(1) + EachPixel(5);
            else if (CheckUIRect(0) == 2)
                fixPos.x = EachPixel(2) - EachPixel(5);
            if (CheckUIRect(1) == 3)
                fixPos.y = EachPixel(3) + EachPixel(6);
            else if (CheckUIRect(1) == 4)
                fixPos.y = EachPixel(4) - EachPixel(6);
            fixPos.z = 0;
            pos = _camera.ScreenToWorldPoint(fixPos + _camera.transform.forward * 10);
            bombBase.transform.position = pos;
            bombBase.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //Debug.Log(Input.mousePosition + "," + pos + "," + _camera.ScreenToWorldPoint(AlmightyTapPosition() + _camera.transform.forward * 10));
        }
    }

    void TouchPhaseEnded()
    {
        isMove = false;
        bombBase.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        if (bombBase != default)
            BombAnimationChatch();
        bombBase = default;
    }

    float EachPixel(int mode)
    {
        float sideUIZone = GameFieldPixel.x * ResolutionData.SideUIRect;
        float upperUIZone = GameFieldPixel.y * ResolutionData.UpperUIRect;

        //! GameFieldPixel <= ResolutionData.GameSceneResolution.x
        float gameLeft = (ResolutionData.GameSceneResolution.x - GameFieldPixel.x) / 2;
        float gameRight = GameFieldPixel.x + ((ResolutionData.GameSceneResolution.x - GameFieldPixel.x) / 2);
        float gameTop = GameFieldPixel.y + ((ResolutionData.GameSceneResolution.y - GameFieldPixel.y) / 2);
        float gameButtom = upperUIZone + ((ResolutionData.GameSceneResolution.y - GameFieldPixel.y) / 2);

        switch (mode)
        {
            case 1:
                return gameLeft;
            case 2:
                return gameRight;
            case 3:
                return gameButtom;
            case 4:
                return gameTop;
            case 5:
                return sideUIZone;
            case 6:
                return upperUIZone;
        }
        return 0;
    }

    int CheckUIRect(int mode)
    {
        //Debug.Log(EachPixel(1) + "," + EachPixel(2) + "," + EachPixel(5));
        //Debug.Log(GameFieldPixel.x + "," + ResolutionData.GameSceneResolution.x + "," + EachPixel(5));
        //text.text = ResolutionData.GameSceneResolution.x + "," + ResolutionData.GameSceneResolution.y;
        //横
        if (mode == 0)
        {
            if (AlmightyTapPosition().x < EachPixel(1) + EachPixel(5))
                return 1;
            if (AlmightyTapPosition().x > EachPixel(2) - EachPixel(5))
                return 2;
        }
        //縦
        else
        {
            if (AlmightyTapPosition().y < EachPixel(3))
                return 3;
            if (AlmightyTapPosition().y > EachPixel(4) - EachPixel(6))
                return 4;
        }
        return 0;
    }

    //
    float HorizontalPixelCalc()
    {
        //モニター,画面解像度
        Vector2 OverallResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        float ret = 0;
        //横が長い
        if (OverallResolution.x / ResolutionData.ResolutionRect.x > OverallResolution.y / ResolutionData.ResolutionRect.y)
        {
            //16:9=？:Y
            float xRect = OverallResolution.y * ResolutionData.ResolutionRect.x / ResolutionData.ResolutionRect.y;
            ret = xRect;
        }
        else
        {
            ret = OverallResolution.x;
        }
        return ret;
        //カメラサイズ（ゲーム画面）が調整されたあとの画面からはみ出している部分を除く横、縦のピクセル数
    }

    float VerticalPixelCalc()
    {
        //画面解像度
        Vector2 OverallResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        float ret = 0;
        //横が長い
        if (OverallResolution.x / ResolutionData.ResolutionRect.x < OverallResolution.y / ResolutionData.ResolutionRect.y)
        {
            //16:9=X:？
            float yRect = OverallResolution.x * ResolutionData.ResolutionRect.y / ResolutionData.ResolutionRect.x;
            ret = yRect;
        }
        else
        {
            ret = OverallResolution.y;
        }
        return ret;
    }

    void TouchManagerUpdate()
    {
        _touchManager.update();
        _touchManager.GetTouch();
    }

    Vector3 AlmightyTapPosition()
    {
        Vector3 vPoint = _touchManager._touch_position;
        return Application.isEditor ? Input.mousePosition : vPoint;
    }

    void BombAnimationChatch()
    {
        bombBase.Catch(isMove);
    }


    ////画面外の黒いとこを機能させない
    //bool CheckGameRect(bool mode)
    //{
    //    //ゲームに関係ない黒い部分の大きさ
    //    Vector2 sideEdge = new Vector2((ResolutionData.GameSceneResolution.x - GameFieldPixel.x) / 2, (ResolutionData.GameSceneResolution.y - GameFieldPixel.y) / 2);

    //    if (AlmightyTapPosition().x < sideEdge.x || AlmightyTapPosition().x > (GameFieldPixel.x + sideEdge.x))
    //    {
    //        return false;
    //    }

    //    if (AlmightyTapPosition().y < sideEdge.y || AlmightyTapPosition().y > (VerticalPixelCalc() + sideEdge.y))
    //    {
    //        return false;
    //    }
    //    return true;
    //}
}
