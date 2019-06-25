using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hornet.Stage
{
    // スコアを増加させるクラス
    public class ScoreManager : MonoBehaviour
    {
        private int score = 0;
        private Text scoreLabel;

        void Start()
        {
            scoreLabel = GameObject.Find("ScoreLabel").GetComponent<Text>();
            scoreLabel.text = "SCORE:" + score;
        }

        public void Addscore (int amount)
        {
            score += amount;
            scoreLabel.text = "SCORE:" + score;
        }
    }
}
