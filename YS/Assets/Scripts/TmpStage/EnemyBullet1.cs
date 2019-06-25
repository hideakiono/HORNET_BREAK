using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hornet.Stage
{

    // 敵が打つ弾
    public class EnemyBullet1 : MonoBehaviour
    {
        new Rigidbody2D rigidbody;

        public GameObject enemyFierMissilPrefab;

        public int wayNumber;

         

        public void Start()
        {

            for(int i = 0; i < wayNumber; i++) {

               var bullet = Instantiate(enemyFierMissilPrefab, transform.position, Quaternion.Euler(0, -30 + (15 * i), 0));

            }
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

