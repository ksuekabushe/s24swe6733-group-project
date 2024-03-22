using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tetris3D
{
    public class GameStatsUpdate : MonoBehaviour
    {
        public TMP_Text textMesh;
        private int level = 1;
        private int floorsDestroyed = 0;
        private int score = 0;
        private int highScore = 0;
        private string nextTetromino = "3D Hammer";
        // Start is called before the first frame update
        void Start()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            textMesh.text = $@"Level: {level}
    Score/HighScore: {score}/{highScore}
    Floors cleared in this level: {floorsDestroyed}
    Next Tetromino: {nextTetromino}";
        }
    }
}
