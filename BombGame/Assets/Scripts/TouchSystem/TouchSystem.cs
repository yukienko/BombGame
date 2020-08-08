using StateManager;
using StaticManager;
using UnityEditor;
using UnityEngine;

public class TouchSystem : MonoBehaviour
{
    [SerializeField] Camera _camera = default;
    TouchManager _touchManager;
    TouchManager old_phase;
    BombBase bombBase;
    Vector3 oldPos;
    bool isMove;

    // Start is called before the first frame update
    void Start()
    {
        _touchManager = new TouchManager();
        old_phase = new TouchManager();
        isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        //タッチ情報更新
        TouchManagerUpdate();
        if (_touchManager._touch_flag)
        {
            Vector3 pos = default;
            if (old_phase._touch_phase == TouchPhase.Began)
            {
                // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
                pos = _camera.ScreenToWorldPoint(AlmightyTapPosition() + _camera.transform.forward * 10);
            }
            else if (_touchManager._touch_phase == TouchPhase.Moved)
            {
                float distance = 100; // 飛ばす&表示するRayの長さ
                float duration = 3;   // 表示期間（秒）
                Ray ray = Camera.main.ScreenPointToRay(AlmightyTapPosition());
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
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
                    pos = _camera.ScreenToWorldPoint(AlmightyTapPosition() + _camera.transform.forward * 10);
                    if (AlmightyTapPosition().x < StaticManager.GameRect.gameLeft || AlmightyTapPosition().x > StaticManager.GameRect.gameRight)
                    {
                        pos = new Vector3(oldPos.x, pos.y, pos.z);
                    }
                    if (AlmightyTapPosition().y > StaticManager.GameRect.gameTop || AlmightyTapPosition().y < StaticManager.GameRect.gameButtom)
                    {
                        pos = new Vector3(pos.x, oldPos.y, pos.z);
                    }
                    bombBase.transform.position = pos;
                    Debug.Log(Input.mousePosition + "," + pos + "," + _camera.ScreenToWorldPoint(AlmightyTapPosition() + _camera.transform.forward * 10));
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
}
