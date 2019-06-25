using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hornet.Stage
{

    // デットエリアに触れた時の判定
    public class MoobGround : MonoBehaviour
    {
       void Update()
        {
            // 壁が動くスピード
            float moveSpeedx = 0;
            var position = transform.position;
            moveSpeedx = 2.5f;
            position.x += moveSpeedx * Time.deltaTime;
            transform.position = position;

        }
    }
}