using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hornet.Stage
{
    // コライダーの範囲内に侵入した際にアクティブに設定します。
    public class ActiveArea : MonoBehaviour
    {
        // アクティブフラグ
        public bool isActive = false;
        // 「Player」レイヤー
        int playerLayer;

        private void Awake()
        {
            // 「playerLayer」をプレイヤー指定
            playerLayer = LayerMask.NameToLayer("Player");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // エリアに入ったら動く
            if (playerLayer == collision.gameObject.layer) {
                isActive = true;
            }
        }
    }
}
