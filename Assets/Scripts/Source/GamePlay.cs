using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris3D
{
    public class GamePlay : MonoBehaviour
    {
        public static bool playing;

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

        public float playerMinimumX = -4f;
        public float playerMaximumX = 4f;
        public float playerMinimumY =  0f;
        public float playerMaximumY =   19f;
        public float playerMinimumZ =   -4f;
        public float playerMaximumZ =  4f;

        public float tetrominoMinimumX = -0.5f;
        public float tetrominoMaximumX = 0.5f;
        public float tetrominoMinimumY = -1.15f;
        public float tetrominoMaximumY =  17.85f;
        public float tetrominoMinimumZ =  -0.5f;
        public float tetrominoMaximumZ = 0.5f;

        public Vector3 initialTetrominoPosition; // Initial player position

        // Tetromino movement
        public float sidewaysDisplacement = 10f;
        public float forwardDisplacement = 10f;
        public float upwardDisplacement = 10f;

        public static GameObject currentTetromino;
        public static GameObject nextTetromino;


        void Start()
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
            // Randomly pick tetrominoes
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

            UnityEngine.Debug.Log($"Starting with {currentTetromino.tag} tetromino");

            currentTetromino.SetActive(true);
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
            UnityEngine.Debug.Log($"Next tetromino: {nextTetromino.tag}");

            GameLogic.initialTetrominoPosition = initialTetrominoPosition;
            GameLogic.resetTetrominoTransform(currentTetromino);

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

            GameLogic.setTetrominoMaterial(currentTetromino, tetrominoMaterial);

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


        void FixedUpdate()
        {
            if (!currentTetromino) {
                currentTetromino = tesseractTetromino;
            }
            // var rigidBody = currentTetromino.GetComponent<Rigidbody>();
            var displacement = new Vector3(0, -upwardDisplacement * Time.fixedDeltaTime, 0);
            var newPosition = currentTetromino.transform.position + displacement;
            UnityEngine.Debug.Log($"Current Tetromino position ({currentTetromino.transform.position.x},{currentTetromino.transform.position.y},{currentTetromino.transform.position.z})");
            UnityEngine.Debug.Log($"Tentative new position ({newPosition.x},{newPosition.y},{newPosition.z})");
            if (newPosition.x <= tetrominoMaximumX && newPosition.x >= tetrominoMinimumX &&
                    newPosition.y <= tetrominoMaximumY && newPosition.y >= tetrominoMinimumY &&
                    newPosition.z <= tetrominoMaximumZ && newPosition.z >= tetrominoMinimumZ
                )
            {
                currentTetromino.transform.position = newPosition;
            }
        }
    }
}
