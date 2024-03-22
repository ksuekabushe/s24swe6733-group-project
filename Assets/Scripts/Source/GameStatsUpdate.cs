using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tetris3D
{
    public class GameStatsUpdate : MonoBehaviour
    {
        public TMP_Text textMesh;
        // Start is called before the first frame update
        void Start()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if(!GameLogic.nextTetromino){

                textMesh.text = $@"Level: {GameLogic.currentLevel}
    Score/HighScore: {GameLogic.currentScore}/{GameLogic.highScore}
    Floors cleared in this level: {GameLogic.floorsDestroyed}";

            } else {

                textMesh.text = $@"Level: {GameLogic.currentLevel}
    Score/HighScore: {GameLogic.currentScore}/{GameLogic.highScore}
    Floors cleared in this level: {GameLogic.floorsDestroyed}
    Next Tetromino: {GameLogic.nextTetromino.tag}";

            }
        }
    }
}
