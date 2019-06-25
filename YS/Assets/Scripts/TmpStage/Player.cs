using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hornet.Stage
{
    // プレイヤーの色々な設定
    public class Player : MonoBehaviour
    {
        public enum PlayerState
        {
            None,
            NonActive,
            Active,
            Invincible,
            GameOver,
            StageClear,
        }

        // 無敵時間を指定します。
        [SerializeField]
        float invincibleTime = 2;
        // 強制スクロール速度を指定します。
        [SerializeField]
        float cameraSpeed = 2.5f;
        // 画面内での移動速度を指定します。
        [SerializeField]
        float speedInCamera = 5.0f;

        // 自機の弾を指定します。
        public GameObject bullet;
        public GameObject superbullet;
        // 自機の弾が出る部分を指定します。
        public Transform shotPos;
        public Transform shotPos2;
        // 爆発エフェクトを指定します。
        public GameObject explosion;
        // 自機の残機表示を指定します。
        public GameObject[] playerLife;
        public int lifeCount = 3;

        // シーンコントローラー
        [SerializeField]
        SceneController sceneController = null;

        // ステートの設定
        public PlayerState playerState = PlayerState.None;

        // ショットタイムを0に設定
        float shotTime = 0;

        void Start()
        {
            playerState = PlayerState.NonActive;
        }

        // Update is called once per frame
        void Update()
        {
            switch (playerState)
            {
                case PlayerState.None:
                    break;
                case PlayerState.NonActive:
                    playerState = PlayerState.Active;
                    break;
                case PlayerState.Active:
                    UpdateForActiveState();
                    break;
                case PlayerState.Invincible:
                    UpdateForInvincibleState();
                    break;
                case PlayerState.GameOver:
                    break;
                case PlayerState.StageClear:
                    break;
            }
        }


        // アクティブ時をフレーム更新です。
        private void UpdateForActiveState()
        {
            Vector3 velocity = Vector3.zero;

            // 左移動
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                velocity.x = cameraSpeed - speedInCamera;
            }
            // 右移動
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                velocity.x = cameraSpeed + speedInCamera;
            }
            else
            {
                velocity.x = cameraSpeed;
            }

            // 上移動
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                velocity.y = 5f;
            }
            // 下移動
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                velocity.y = -5f;
            }
            else
            {
                velocity.y = 0;
            }

            //発射ボタンが押されたら
            if (Input.GetButton("Jump") || Input.GetButton("Fire3"))
            {
                shotTime += Time.deltaTime;
                //弾のクールタイム
                if (shotTime > 0.1f)
                {
                    Instantiate(bullet, shotPos.transform.position + new Vector3(0, -0.25f, 0), bullet.transform.rotation);
                    Instantiate(bullet, shotPos.transform.position + new Vector3(0, +0.25f, 0), bullet.transform.rotation);
                    shotTime -= 0.1f;
                }
            }

            // ここでポジションを設定する
            var position = transform.position;
            // Time.deltaTime : 前回のUpdate関数実行からの差分時間
            position += velocity * Time.deltaTime;
            transform.position = position;
        }

        // アクティブ時をフレーム更新です。
        private void UpdateForInvincibleState()
        {
            Vector3 velocity = Vector3.zero;
            velocity.x = cameraSpeed;

            // 左移動
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                velocity.x = cameraSpeed - speedInCamera;
            }
            // 右移動
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                velocity.x = cameraSpeed + speedInCamera;
            }
            else
            {
                velocity.x = cameraSpeed;
            }

            // 上移動
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                velocity.y = 5f;
            }
            // 下移動
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                velocity.y = -5f;
            }
            else
            {
                velocity.y = 0;
            }

            // 無敵時のアルファ 
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
            // ここでポジションを設定する
            var position = transform.position;
            // Time.deltaTime : 前回のUpdate関数実行からの差分時間
            position += velocity * Time.deltaTime;
            transform.position = position;
        }

        // プレイヤーのコライダーに他のオブジェクトが接触した際に呼び出されます。
        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (playerState)
            {
                case PlayerState.Active:
                case PlayerState.None:
                case PlayerState.NonActive:
                case PlayerState.Invincible:
                case PlayerState.GameOver:
                case PlayerState.StageClear:
                default:
                    break;
            }
        }

        // トリガー内にオブジェクトが侵入した際に呼び出されます。
        private void OnTriggerEnter2D(Collider2D collider)
        {
            switch (playerState)
            {
                case PlayerState.Active:
                    {
                        // プレイヤーがダメージを受ける場合
                        if (collider.gameObject.tag == "EnemyBullet" ||
                            collider.gameObject.tag == "Enemy" ||
                            collider.gameObject.tag == "BigEnemy")
                        {
                            if (collider.gameObject.tag != "BigEnemy")
                            {
                                // 衝突相手を削除
                                Destroy(collider.gameObject);
                            }

                            lifeCount--;
                            UpdateLifeUI();

                            // 残機が残っている場合
                            if (lifeCount > 0)
                            {
                                StartCoroutine(Invincible());
                            }
                            // ゲームオーバーの場合
                            else
                            {
                                // ゲームオーバー
                                sceneController.GameOver();
                                // 自機破壊演出を開始
                                StartCoroutine(GameOver());
                            }
                        }
                    }
                    break;
                case PlayerState.None:
                case PlayerState.NonActive:
                case PlayerState.Invincible:
                case PlayerState.GameOver:
                case PlayerState.StageClear:
                    break;
                default:
                    break;
            }
        }
        // 無敵状態に設定します。
        IEnumerator Invincible()
        {
            // 無敵状態に遷移
            playerState = PlayerState.Invincible;
            yield return new WaitForSeconds(invincibleTime);
            // 無敵のアルファ
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 100);
            // 無敵状態から復帰
            playerState = PlayerState.Active;
        }

        // 『自機の残機』表示を更新します。
        void UpdateLifeUI()
        {
            for (int i = 0; i < playerLife.Length; i++)
            {
                if (i < lifeCount)
                {
                    playerLife[i].SetActive(true);
                }
                else
                {
                    playerLife[i].SetActive(false);
                }
            }
        }

        // 『ゲームオーバー』の際のプレイヤー破壊演出
        private IEnumerator GameOver()
        {
            // 爆発エフェクト
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, explosion.transform.rotation);
            }
            yield return null;

            // プレイヤーを削除
            Destroy(gameObject);
        }
    }
}
