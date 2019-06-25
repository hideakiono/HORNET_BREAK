using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hornet.Stage
{
    
    public class MainCamera : MonoBehaviour
    {
        // プレイヤーを指定します。
        public GameObject player;
        // メインカメラを指定します。
        public GameObject maincamera;
        // ボスを指定します。
        public GameObject bigenemy;

        // メインカメラの動き
        void Update()
        {
            // カメラのX座標を強制的に進める
            //プレイヤーがデストロイされてない時のみ
            if (player && bigenemy != null)
            {
                // カメラの動くスピード
                Vector3 cameraPos = transform.position;
                transform.position = new Vector3(cameraPos.x + 2.5f * Time.deltaTime, cameraPos.y, cameraPos.z);
            }
        }
    }
}
