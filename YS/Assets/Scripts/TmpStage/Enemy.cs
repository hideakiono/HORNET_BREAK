using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hornet.Stage
{
    // 敵機の動きを表します。
    public class Enemy : MonoBehaviour
    {
        // 敵機の状態を表します。
        public enum EnemyState
        {
            None,
            NonActive,
            Active,
            GameOver,
        }
        // エネミーが撃つ弾の数をコントロールします。
        // public int bc;
        // public int bc2;
        // 現在の状態を表します。
        public EnemyState enemyState = EnemyState.None;
        // ゲームオーバー画面を表示
        //public GameObject gameOver;
        // エネミーが撃つ弾を指定します。
        public GameObject enemybullet;
        //public GameObject enemybullet1;
        // 弾の出る所を指定します。
        public Transform shotPos;
        // 爆発エフェクトを指定します。
        public GameObject explosion;
        // スコアを指定します。
        public int scoreValue;
        // スコアマネージャーを指定します。
        private ScoreManager sm;

        void Start()
        {
            enemyState = EnemyState.NonActive;
            sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        }

        // 弾の出る初期値
        float shotTime = 0;

        public void OnHitBullet(Collider2D collision)
        {
            // 弾を削除
            Destroy(collision.gameObject);
            // 爆発エフェクトを生成
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, explosion.transform.rotation);
            }
            // スコア
            sm.Addscore(scoreValue);
            // 敵機を削除
            Destroy(gameObject);
        }
        void Update()
        {
            switch (enemyState)
            {
                case EnemyState.None:
                    break;
                case EnemyState.NonActive:
                    if (GetComponentInChildren<ActiveArea>().isActive)
                    {
                        enemyState = EnemyState.Active;
                    }
                    break;
                case EnemyState.Active:
                    {
                        // 敵の動くスピード
                        float moveSpeedx = 0;
                        //float moveSpeedy = 0;
                        var position = transform.position;
                        moveSpeedx = -1.0f;
                        //moveSpeedy =  1.0f;
                        position.x += moveSpeedx * Time.deltaTime;
                        //position.y += moveSpeedy * Time.deltaTime;
                        transform.position = position;

                        shotTime += Time.deltaTime;
                        //弾のクールタイム
                        if (shotTime > 1.5f)
                        {
                            // 弾がでるポジション
                            Vector3 shotPosition;
                            shotPosition = shotPos.transform.position;
                            shotPosition.y += 0.2f * -1;
                            
                            for (shotPosition.y = -1; shotPosition.y < 2; shotPosition.y++)
                            {
                                Instantiate(enemybullet, shotPosition, shotPos.transform.rotation);
                            }

                            shotTime = 0;
                        }

                        // カメラの範囲外に消えた場合、この敵機を削除
                        if (transform.position.x < Camera.main.transform.position.x - 6.6f)
                        {
                            Destroy(gameObject);
                        }
                    }
                    break;
                case EnemyState.GameOver:
                    break;
                default:
                    break;
            }
        }

        // この敵機のコライダーに他のオブジェクトが接触した際に呼び出されます。
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 接触相手の処理
            if (collision.gameObject.tag == "Bullet")
            {
                OnHitBullet(collision);
            }
        }
    }
}
