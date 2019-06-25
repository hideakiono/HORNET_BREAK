using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hornet.Stage
{
    // プレイヤーが撃つ弾
    public class Bullet : MonoBehaviour
    {   
        new Rigidbody2D rigidbody;
        // 爆発エフェクトを指定します。
        public GameObject explosion;
        // スコア用を指定します。
        public int scoreValue;
        // スコアマネージャーを指定します。
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
            // 爆発エフェクトの処理
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, explosion.transform.rotation);
            }
            // スコア
            sm.Addscore(scoreValue);
            // プレイヤーの弾
            Destroy(gameObject);
            // 敵の弾 
            Destroy(collider.gameObject);
        }

        // 接触相手の処理
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "BigEnemy") {
                Destroy(gameObject);
            }
            else if (collider.gameObject.tag == "EnemyBullet") {
                OnHitEnemyBullet(collider);
            }
        }
    }
}
