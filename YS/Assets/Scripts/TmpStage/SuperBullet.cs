using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hornet.Stage
{
    // プレイヤーが撃つ弾
    public class SuperBullet: MonoBehaviour
    {   
        new Rigidbody2D rigidbody;
        // 爆発エフェクト
        public GameObject explosion;
        // スコア用
        public int scoreValue;
        // スコアマネージャー
        private ScoreManager sm;

        void Start()
        {
            // コンポ―ネントの取得
            rigidbody = GetComponent<Rigidbody2D>();
            sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        }
        void Update()
        {
            
            // 弾の速さ
            rigidbody.velocity = new Vector2(12, 0);
            // 画面外に弾が出たら消える
            if (!GetComponent<Renderer>().isVisible) {
                Destroy(this.gameObject);
            }
        }

        // 自機の弾の当たり判定
        public void OnHitEnemyBullet(Collider2D collider)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, explosion.transform.rotation);
            }
            sm.Addscore(scoreValue);
            Destroy(gameObject);
            Destroy(collider.gameObject);
        }

        // あたり判定
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "BigEnemy") {
                Destroy(gameObject);
            }
            else if (collider.gameObject.tag == "EnemyBullet") {
                OnHitEnemyBullet(collider);
            }
        }
      //// カメラの視界内に入った際に呼び出されます。
        //private void OnBecameVisible()
        //{

        //}

        //// カメラの視界外に出た際に呼び出されます。
        //private void OnBecameInvisible()
        //{
        //    Destroy(gameObject);
        //}
    }
}
