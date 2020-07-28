using StateManager;
using UnityEngine;

public class TapEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem tapEffect;              // タップエフェクト
    [SerializeField] Camera _camera;                        // カメラの座標
    TouchManager _touchManager;
    TouchManager old_phase;

    private void Start()
    {
        _touchManager = new TouchManager();
        old_phase = new TouchManager();
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
                Vector3 pos;
                Vector3 world_point = Camera.main.ScreenToWorldPoint(_touchManager._touch_position);
                if (old_phase._touch_phase == TouchPhase.Began)
                {
                    // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
                    pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
                    tapEffect.transform.position = pos;
                    tapEffect.Emit(1);
                }
                else if (_touchManager._touch_phase == TouchPhase.Moved)
                {
                    float distance = 100; // 飛ばす&表示するRayの長さ
                    float duration = 3;   // 表示期間（秒）
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
                    RaycastHit hit = new RaycastHit();
                    //ヒットボックスの存在するオブジェクトにRayが衝突したときの処理
                    if(Physics.Raycast(ray, out hit, distance))
                    {
                        GameObject hitobject = hit.collider.gameObject;
                        pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 20);
                        hitobject.transform.position = pos;
                    }
                }
            }
            else
            {
                // 座標系変換
                Vector3 world_point = Camera.main.ScreenToWorldPoint(_touchManager._touch_position);
                Vector3 vPoint = _touchManager._touch_position;

                Vector3 pos;

                //クリックした瞬間
                if (_touchManager._touch_phase == TouchPhase.Began)
                {
                    // タップのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
                    pos = _camera.ScreenToWorldPoint(vPoint + _camera.transform.forward * 10);
                    tapEffect.transform.position = pos;
                    tapEffect.Emit(1);


                    float distance = 100; // 飛ばす&表示するRayの長さ
                    Ray ray = Camera.main.ScreenPointToRay(vPoint);
                    RaycastHit hit = new RaycastHit();
                    //ヒットボックス(HitBox3Dのみ、2Dには対応してないので別の書き方をする)の存在するオブジェクトにRayが衝突したときの処理^0^
                    if (Physics.Raycast(ray, out hit, distance))
                    {
                        GameObject hitobject = hit.collider.gameObject;
                        pos = _camera.ScreenToWorldPoint(vPoint + _camera.transform.forward * 20);
                        hitobject.transform.position = pos;
                    }
                }
                //クリック長押し中（クリックした瞬間の次フレームから離すまでの間）
                else if(_touchManager._touch_phase == TouchPhase.Moved)
                {
                    float distance = 100; // 飛ばす&表示するRayの長さ
                    Ray ray = Camera.main.ScreenPointToRay(vPoint);
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit, distance))
                    {
                        GameObject hitobject = hit.collider.gameObject;
                        pos = _camera.ScreenToWorldPoint(vPoint + _camera.transform.forward * 20);
                        hitobject.transform.position = pos;
                    }
                }
            }
        }
        //oldの更新
        old_phase._touch_phase = _touchManager._touch_phase;
    }
}