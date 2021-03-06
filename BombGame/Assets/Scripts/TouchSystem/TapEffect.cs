using StateManager;
using UnityEngine;

public class TapEffect : MonoBehaviour
{
    //[SerializeField] ParticleSystem tapEffect = default;              // タップエフェクト
    [SerializeField] Camera _camera = default;                        // カメラの座標
    TouchManager _touchManager;
    TouchManager old_phase;
    public bool isMove;
    BombBase bombBase;
    Vector3 oldPos;

    float gameTopRight;
    float gameBottomLeft;
    float gameTop;
    float gameButtom;

    public bool isCatch;

    private void Start()
    {
        _touchManager = new TouchManager();
        old_phase = new TouchManager();
        isMove = false;
        gameTopRight = 1920 - 1920 / 20;
        gameBottomLeft = 1920f / 20f;
        gameTop = 1080 - 1080 / 15;
        gameButtom = 0;
        isCatch = false;
        bombBase = default;
    }

    void Update()
    {
        //タッチ情報更新
        _touchManager.update();
        _touchManager.GetTouch();
        if (_touchManager._touch_flag)
        {
            if (Application.isEditor)
            {
                // 座標系変換
                Vector3 pos = default;
                if (old_phase._touch_phase == TouchPhase.Began)
                {
                    // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
                    pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
                }
                else if (_touchManager._touch_phase == TouchPhase.Moved)
                {
                    float distance = 100; // 飛ばす&表示するRayの長さ
                    float duration = 3;   // 表示期間（秒）
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
                    RaycastHit hit = new RaycastHit();
                    //ヒットボックスの存在するオブジェクトにRayが衝突したときの処理
                    if (Physics.Raycast(ray, out hit, distance))
                    {
                        isMove = true;
                        bombBase = hit.collider.gameObject.GetComponent<BombBase>();
                    }
                    //クリックし続けてるなら
                    if (isMove)
                    {
                        //pos                :unity.transform.positionの座標
                        //Input.mousePosition:1920x1080のクリックした座標
                        pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
                        if (_touchManager._touch_position.x < gameBottomLeft || _touchManager._touch_position.x > gameTopRight)
                        {
                            Debug.Log("範囲外 : " + "," + _touchManager._touch_position + "," + pos);
                            pos = new Vector3(oldPos.x, pos.y, pos.z);
                            //return;
                        }
                        if (_touchManager._touch_position.y > gameTop || _touchManager._touch_position.y < gameButtom)
                        {
                            Debug.Log("範囲外 : " + "," + _touchManager._touch_position + "," + pos);
                            pos = new Vector3(pos.x, oldPos.y, pos.z);
                            // return;
                        }
                        bombBase.transform.position = pos;
                    }
                }
                else if (_touchManager._touch_phase == TouchPhase.Ended)
                {
                    isMove = false;
                }
                oldPos = pos;
            }
            else
            {
                // 座標系変換
                Vector3 vPoint = _touchManager._touch_position;

                Vector3 pos = default;

                //クリックした瞬間
                if (_touchManager._touch_phase == TouchPhase.Began)
                {
                    // タップのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
                    pos = _camera.ScreenToWorldPoint(vPoint + _camera.transform.forward * 10);
                }
                //クリック長押し中（クリックした瞬間の次フレームから離すまでの間）
                else if (_touchManager._touch_phase == TouchPhase.Moved)
                {
                    float distance = 100; // 飛ばす&表示するRayの長さ
                    Ray ray = Camera.main.ScreenPointToRay(vPoint);
                    RaycastHit hit = new RaycastHit();
                    //ヒットボックス(HitBox3Dのみ、2Dには対応してないので別の書き方をする)の存在するオブジェクトにRayが衝突したときの処理^0^
                    if (Physics.Raycast(ray, out hit, distance))
                    {
                        isMove = true;
                        bombBase = hit.collider.gameObject.GetComponent<BombBase>();
                    }
                    //クリックし続けてるなら
                    if (isMove)
                    {
                        pos = _camera.ScreenToWorldPoint(vPoint + _camera.transform.forward * 10);
                        if (vPoint.x < gameBottomLeft || vPoint.x > gameTopRight)
                        {
                            pos = new Vector3(oldPos.x, pos.y, pos.z);
                        }
                        if (vPoint.y > gameTop || vPoint.y < gameButtom)
                        {
                            pos = new Vector3(pos.x, oldPos.y, pos.z);
                        }
                        bombBase.transform.position = pos;
                    }
                }
                else if (_touchManager._touch_phase == TouchPhase.Ended)
                {
                    isMove = false;
                }
                oldPos = pos;
            }
            //oldの更新
            old_phase._touch_phase = _touchManager._touch_phase;

            if (bombBase != default)
            {
                bombBase.Catch(isMove);
            }
        }
    }
}