using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris3D
{
    public class GamePlay : MonoBehaviour
    {
        public Material skyMaterial1;
        public Material skyMaterial2;
        public Material skyMaterial3;
        public Material skyMaterial4;
        public Material skyMaterial5;
        public Material skyMaterial6;
        public Material skyMaterial7;
        public Material skyMaterial8;
        public Material skyMaterial9;

        public Material groundMaterial1;
        public Material groundMaterial2;
        public Material groundMaterial3;
        public Material groundMaterial4;
        public Material groundMaterial5;
        public Material groundMaterial6;
        public Material groundMaterial7;
        public Material groundMaterial8;
        public Material groundMaterial9;

        public Material tetrominoMaterial1;
        public Material tetrominoMaterial2;
        public Material tetrominoMaterial3;
        public Material tetrominoMaterial4;
        public Material tetrominoMaterial5;
        public Material tetrominoMaterial6;
        public Material tetrominoMaterial7;
        public Material tetrominoMaterial8;
        public Material tetrominoMaterial9;

        public GameObject birdTetromino;
        public GameObject butterflyTetromino;
        public GameObject caneTetromino;
        public GameObject cubeTetromino;
        public GameObject hammerTetromino;
        public GameObject rodTetromino;
        public GameObject slabTetromino;
        public GameObject snakeTetromino;
        public GameObject tesseractTetromino;

        public GameObject ground;
        public GameObject arena;

        public float playerMinimumX = -4f;
        public float playerMaximumX = 4f;
        public float playerMinimumY = 0f;
        public float playerMaximumY = 19f;
        public float playerMinimumZ = -4f;
        public float playerMaximumZ = 4f;

        public float tetrominoMinimumX = -0.5f;
        public float tetrominoMaximumX = 0.5f;
        public float tetrominoMinimumY = -1.15f;
        public float tetrominoMaximumY = 17.85f;
        public float tetrominoMinimumZ = -0.5f;
        public float tetrominoMaximumZ = 2.5f;

        public Vector3 initialPosition; // Initial player position
        public Vector3 initialTetrominoPosition; // Initial player position

        // Tetromino movement
        public float playerSidewaysDisplacement = 10f;
        public float playerForwardDisplacement = 10f;
        public float playerUpwardDisplacement = 10f;
        public float tetrominoSidewaysDisplacement = 10f;
        public float tetrominoForwardDisplacement = 10f;
        public float tetrominoUpwardDisplacement = 5f;

        public static GameObject currentTetromino;
        public static GameObject nextTetromino;


        void Start()
        {
            GameLogic.initialPosition = initialPosition;
            GameLogic.initialTetrominoPosition = initialTetrominoPosition;
            GameLogic.playerSidewaysDisplacement = playerSidewaysDisplacement;
            GameLogic.playerForwardDisplacement = playerForwardDisplacement;
            GameLogic.playerUpwardDisplacement = playerUpwardDisplacement;
            GameLogic.tetrominoSidewaysDisplacement = tetrominoSidewaysDisplacement;
            GameLogic.tetrominoForwardDisplacement = tetrominoForwardDisplacement;
            GameLogic.tetrominoUpwardDisplacement = tetrominoUpwardDisplacement;
            GameLogic.playerMovementRestrictions = new MovementRestrictions(
                playerMinimumX,
                playerMaximumX,
                playerMinimumY,
                playerMaximumY,
                playerMinimumZ,
                playerMaximumZ
            );
            GameLogic.tetrominoMovementRestrictions = new MovementRestrictions(
                tetrominoMinimumX,
                tetrominoMaximumX,
                tetrominoMinimumY,
                tetrominoMaximumY,
                tetrominoMinimumZ,
                tetrominoMaximumZ
            );
            GameLogic.arenaObject = arena;
            GameLogic.gamePlay = this;
            StartNewGame();
        }

        void StartNewGame()
        {
            GameLogic.gameIsPaused = true;
            GameLogic.hideArena();
            GameLogic.floorsDestroyed = 0;
            ResetTetrominoes();
            PickTetrominoes();
            UnityEngine.Debug.Log($"Starting with {currentTetromino.tag}");
            UnityEngine.Debug.Log($"Next tetromino: {nextTetromino.tag}");
            PickRandomSkyAndGround();
            GameLogic.tetrominoReady = true;
            GameLogic.gameIsPaused = false;
        }
        void PickRandomSkyAndGround()
        {
            // Set random materials for the sky and ground
            var tetrominoMaterial = GameLogic.getRandomMaterial(new Material[]{
                tetrominoMaterial1,
                tetrominoMaterial2,
                tetrominoMaterial3,
                tetrominoMaterial4,
                tetrominoMaterial5,
                tetrominoMaterial6,
                tetrominoMaterial7,
                tetrominoMaterial8,
                tetrominoMaterial9
            });

            GameLogic.setGameObjectMaterial(currentTetromino, tetrominoMaterial);

            var skyMaterial = GameLogic.getRandomMaterial(new Material[]{
                skyMaterial1,
                skyMaterial2,
                skyMaterial3,
                skyMaterial4,
                skyMaterial5,
                skyMaterial6,
                skyMaterial7,
                skyMaterial8,
                skyMaterial9
            });
            GameLogic.setSkyMaterial(skyMaterial);

            var groundMaterial = GameLogic.getRandomMaterial(new Material[]{
                groundMaterial1,
                groundMaterial2,
                groundMaterial3,
                groundMaterial4,
                groundMaterial5,
                groundMaterial6,
                groundMaterial7,
                groundMaterial8,
                groundMaterial9
            });
            GameLogic.setGameObjectMaterial(ground, groundMaterial);
        }
        public void PickNextTetrominoes()
        {
            // Randomly pick tetrominoes
            currentTetromino = nextTetromino;
            nextTetromino = GameLogic.getRandomGameObject(new GameObject[]{
                birdTetromino,
                butterflyTetromino,
                caneTetromino,
                cubeTetromino,
                hammerTetromino,
                rodTetromino,
                slabTetromino,
                snakeTetromino,
                tesseractTetromino
            });
            GameLogic.resetTetrominoTransform(currentTetromino);
            currentTetromino.SetActive(true);
            GameLogic.currentTetromino = currentTetromino;
            nextTetromino = GameLogic.getRandomGameObject(new GameObject[]{
                birdTetromino,
                butterflyTetromino,
                caneTetromino,
                cubeTetromino,
                hammerTetromino,
                rodTetromino,
                slabTetromino,
                snakeTetromino,
                tesseractTetromino
            });
            GameLogic.nextTetromino = nextTetromino;
        }
        public void PickTetrominoes()
        {

            currentTetromino = GameLogic.getRandomGameObject(new GameObject[]{
                birdTetromino,
                butterflyTetromino,
                caneTetromino,
                cubeTetromino,
                hammerTetromino,
                rodTetromino,
                slabTetromino,
                snakeTetromino,
                tesseractTetromino
            });
            GameLogic.resetTetrominoTransform(currentTetromino);
            currentTetromino.SetActive(true);
            GameLogic.currentTetromino = currentTetromino;
            nextTetromino = GameLogic.getRandomGameObject(new GameObject[]{
                birdTetromino,
                butterflyTetromino,
                caneTetromino,
                cubeTetromino,
                hammerTetromino,
                rodTetromino,
                slabTetromino,
                snakeTetromino,
                tesseractTetromino
            });
            GameLogic.nextTetromino = nextTetromino;
        }

        void ResetTetrominoes()
        {
            // Hide all tetrominoes
            birdTetromino.SetActive(false);
            butterflyTetromino.SetActive(false);
            caneTetromino.SetActive(false);
            cubeTetromino.SetActive(false);
            hammerTetromino.SetActive(false);
            rodTetromino.SetActive(false);
            slabTetromino.SetActive(false);
            snakeTetromino.SetActive(false);
            tesseractTetromino.SetActive(false);

        }

        void Update()
        {
            if (!GameLogic.gameIsPaused &&
                !GameLogic.playerMovingTetromino &&
                GameLogic.tetrominoReady &&
                !GameLogic.tetrominoHasCollided &&
                GameLogic.currentTetromino &&
                GameLogic.currentTetromino.activeSelf
            )
            {
                UnityEngine.Debug.Log($"Tetromino moving down on its own");
                var body = GameLogic.currentTetromino.GetComponent<Rigidbody>();
                GameLogic.moveTetromino(body, RelativeMovementDirection.MOVE_DOWN, new MovementRestrictions(
                    tetrominoMinimumX,
                    tetrominoMaximumX,
                    tetrominoMinimumY,
                    tetrominoMaximumY,
                    tetrominoMinimumZ,
                    tetrominoMaximumZ
                ));
            }
            if (GameLogic.tetrominoesUsed >= GameLogic.tetrominoesNeededToWin && !GameLogic.gameIsLost)
            {
                StartNewGame();
                GameLogic.currentScore += 100 + GameLogic.floorsDestroyed;
                GameLogic.currentLevel++;
                if (GameLogic.highScore < GameLogic.currentScore)
                {
                    GameLogic.highScore = GameLogic.currentScore;
                }
            }
            else if (GameLogic.gameIsLost)
            {
                StartNewGame();
            }
        }
    }
}
