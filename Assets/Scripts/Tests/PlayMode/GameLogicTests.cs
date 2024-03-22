using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tetris3D
{
    public class GameLogicTests
    {
        private MovementRestrictions _playerMovementRestrictions = new MovementRestrictions(-10f, 10f, -10f, 10f, -10f, 10f);
        private Vector3 _maxVector = new Vector3(10f, 10f, 10f);
        private Vector3 _minVector = new Vector3(-10f, -10f, -10f);
        private static float _moveDuration = 0.012f;

        #region Move Tests

        [UnityTest]
        public IEnumerator MoveUpTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_UP, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f, 0f + GameLogic.playerUpwardDisplacement, 0f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveDownTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_DOWN, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f, 0f - GameLogic.playerUpwardDisplacement, 0f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveRightTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_RIGHT, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f + GameLogic.playerSidewaysDisplacement, 0f, 0f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveLeftTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_LEFT, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f - GameLogic.playerSidewaysDisplacement, 0f, 0f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveForwardTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_FORWARD, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f, 0f, 0f + GameLogic.playerForwardDisplacement), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveBackTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_BACKWARD, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f, 0f, 0f - GameLogic.playerForwardDisplacement), rigidBody.position);
        }
        #endregion

        #region Limit Tests
        [UnityTest]
        public IEnumerator MoveUpLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _maxVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_UP, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_maxVector, rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveDownLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _minVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_DOWN, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_minVector, rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveRightLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _maxVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_RIGHT, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_maxVector, rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveLeftLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _minVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_LEFT, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_minVector, rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveForwardLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _maxVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_FORWARD, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_maxVector, rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveBackLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(Rigidbody));
            var rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _minVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_BACKWARD, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_minVector, rigidBody.position);
        }
        #endregion
    }
}
