using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hornet.Title
{
    // シーンを切り替えるためのクラス
    public class SceneController : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("StageSelect");
            }

        }
    }
}
