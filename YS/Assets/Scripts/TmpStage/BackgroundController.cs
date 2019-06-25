using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hornet.Stage {
    // 背景をループさせるため
    public class BackgroundController : MonoBehaviour
    {
        // 背景表示に使うパネル
        Transform[] backgrounds;

        // 指定する場所　 
        void Start()
        {
            // 子オブジェクトを検索してパネル配列に設定
            backgrounds = new Transform[transform.childCount];
            for (int i = 0; i < backgrounds.Length; i++) {
                backgrounds[i] = transform.GetChild(i);

            }
            // すべてのパネルの位置を取得
            UpdatePanels();
        }
        void Update()
        {
            UpdatePanels();
        }
 
        // 全てのパネル位置を更新して動かす
        void UpdatePanels()
        {
            // 現在のカメラ位置を取得
            var cameraPosition = Camera.main.transform.position;
            // カメラ座標からグリット位置を計算
            var gridX = Mathf.FloorToInt(cameraPosition.x * 3 / 40);

            // 全てのパネルの位置を設定
            for (int i = 0; i < backgrounds.Length; i++) {
                var position = backgrounds[i].position;
                position.x = 40.0f / 3 * (gridX + i);
                backgrounds[i].position = position;
            }
        }
    }
}