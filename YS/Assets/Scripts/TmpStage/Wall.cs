using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hornet.Stage
{
    // 壁のクラス
    public class Wall : MonoBehaviour
    {
        // プレイヤーを指定します。
        public GameObject player;
        // ボスを指定します。
        public GameObject bigenemy;

        // 壁の処理     
        void Update()
        {
            // プレイヤーが消えたら壁が止まる
            if (player && bigenemy != null) {
                // 壁が動くスピード
                float moveSpeedx = 0;
                var position = transform.position;
                moveSpeedx = 2.5f;
                position.x += moveSpeedx * Time.deltaTime;
                transform.position = position;
            }
        }
    }
}
