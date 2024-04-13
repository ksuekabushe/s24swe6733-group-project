using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Tetris3D;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tetris3D
{
    public class PieceMovementTests
    {
        MovementRestrictions movementRestrictions = new MovementRestrictions( -5f, 5f, 0f, 20f, - 5f,5);

        #region Movement Tests

        [UnityTest]
        public IEnumerator MoveInitialTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.RESET_TO_INITIAL_POSITION, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(GameLogic.initialPosition, rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MovePieceDownTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            gameObject.transform.position = new Vector3(0,5,0);

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.MOVE_DOWN, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(new Vector3(0f, 4f, 0f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MovePieceUpTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.MOVE_UP, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(new Vector3(0f, 1f, 0f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MovePieceForwardTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.MOVE_FORWARD, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(new Vector3(0f, 0f, 1f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MovePieceBackwardTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.MOVE_BACKWARD, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(new Vector3(0f, 0f, -1f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MovePieceRightTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.MOVE_RIGHT, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(new Vector3(1f, 0f, 0f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MovePieceLeftTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.MOVE_LEFT, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(new Vector3(-1f, 0f, 0f), rigidBody.position);
        }

        #endregion

        #region Rotation Test

        [UnityTest]
        public IEnumerator RotateForwardTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.TURN_FORWARD, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(90f, rigidBody.rotation.eulerAngles.x);
        }

        [UnityTest]
        public IEnumerator RotateBackwardTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.TURN_BACKWARD, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(-90f, rigidBody.rotation.eulerAngles.x);
        }

        [UnityTest]
        public IEnumerator RotateLeftTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.TURN_LEFT, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(-90f, rigidBody.rotation.eulerAngles.y);
        }

        [UnityTest]
        public IEnumerator RotateRightTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.TURN_RIGHT, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(90f, rigidBody.rotation.eulerAngles.y);
        }

        [UnityTest]
        public IEnumerator RotateUpTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.TURN_UP, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(90f, rigidBody.rotation.eulerAngles.z);
        }

        [UnityTest]
        public IEnumerator RotateDownTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Rigidbody>();
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.moveTetromino(rigidBody, Tetris3D.RelativeMovementDirection.TURN_DOWN, movementRestrictions);

            yield return new WaitForSeconds(Time.deltaTime);

            Assert.Equals(-90f, rigidBody.rotation.eulerAngles.z);
        }
        #endregion
    }
}
