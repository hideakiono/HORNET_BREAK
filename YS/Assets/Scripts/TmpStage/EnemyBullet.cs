using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hornet.Stage
{

    // 敵が打つ弾
    public class EnemyBullet : MonoBehaviour
    {
        new Rigidbody2D rigidbody;


        void Start()
        {

            // コンポーネントの取得
            rigidbody = GetComponent<Rigidbody2D>();

        }

        void Update()
        {
            // 弾の速さ
            rigidbody.velocity = new Vector2(-12, 0);
            

            // カメラ外にでたら弾が消える
            if (!GetComponent<Renderer>().isVisible)
            {
                Destroy(gameObject);
            }
        }
        
        // 接触相手の処理
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
    }
}

