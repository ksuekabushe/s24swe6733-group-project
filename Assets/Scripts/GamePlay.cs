using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris3D.Logic
{

    public class GamePlay : MonoBehaviour
    {
        public Material material1;
        public Material material2;
        public Material material3;
        public Material material4;
        public Material material5;
        public Material material6;
        public Material material7;
        public Material material8;
        public Material material9;
        public GameObject BirdTetromino;
        public GameObject ButterflyTetromino;
        public GameObject CaneTetromino;
        public GameObject CubeTetromino;
        public GameObject HammerTetromino;
        public GameObject RodTetromino;
        public GameObject SlabTetromino;
        public GameObject SnakeTetromino;
        public GameObject TesseractTetromino;
        private GameObject _currentTetromino;

        public float minimumX = -4f;
        public float maximumX = 4f;
        public float minimumY = 0f;
        public float maximumY = 19f;
        public float minimumZ = -4f;
        public float maximumZ = 4f;

        public float sidewaysDisplacement = 10f;
        public float forwardDisplacement = 10f;
        public float upwardDisplacement = 10f;



        void Start()
        {
            // TODO: implement (sends the components to GameLogic)
            BirdTetromino.SetActive(false);
            ButterflyTetromino.SetActive(false);
            CaneTetromino.SetActive(false);
            CubeTetromino.SetActive(false);
            HammerTetromino.SetActive(false);
            RodTetromino.SetActive(false);
            SlabTetromino.SetActive(false);
            SnakeTetromino.SetActive(false);
            TesseractTetromino.SetActive(true);
            _currentTetromino = TesseractTetromino;
        }


        void FixedUpdate()
        {
            _currentTetromino = TesseractTetromino;
            // var rigidBody = _currentTetromino.GetComponent<Rigidbody>();
            var displacement = new Vector3(0, -upwardDisplacement * Time.fixedDeltaTime, 0);
            var newPosition = _currentTetromino.transform.position + displacement;
            UnityEngine.Debug.Log($"Current Tetromino position ({_currentTetromino.transform.position.x},{_currentTetromino.transform.position.y},{_currentTetromino.transform.position.z})");
            UnityEngine.Debug.Log($"Tentative new position ({newPosition.x},{newPosition.y},{newPosition.z})");
            if (newPosition.x <= maximumX && newPosition.x >= minimumX &&
                    newPosition.y <= maximumY && newPosition.y >= minimumY &&
                    newPosition.z <= maximumZ && newPosition.z >= minimumZ
                )
            {
                _currentTetromino.transform.position = newPosition;
            }
        }
    }
}