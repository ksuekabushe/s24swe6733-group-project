using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Tetris3D.Logic;

namespace Tetris3D.Logic
{
    PlayerMovementRestrictions _playerMovementRestrictions = new PlayerMovementRestrictions(-10f, 10f, -10f, 10f, -10f, 10f);
    Vector3 _maxVector = new Vector3(10f, 10f, 10f);
    Vector3 _minVector = new Vector3(-10f, -10f, -10f);

    #region Move Tests

    [UnityTest]
    public IEnumerator MoveUpTest()
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
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_UP, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f, 0f + GameLogicTests.upwardDisplacement, 0f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveDownTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_DOWN, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f, 0f - GameLogicTests.upwardDisplacement, 0f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveRightTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_RIGHT, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f + GameLogicTests.sidewaysDisplacement, 0f, 0f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveLeftTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_LEFT, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f - GameLogicTests.sidewaysDisplacement, 0f, 0f), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveForwardTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_FRONT, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f, 0f, 0f + GameLogicTests.forwardDisplacement), rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveBackTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_BACK, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(new Vector3(0f, 0f, 0f - GameLogicTests.forwardDisplacement), rigidBody.position);
        }
        #endregion

        #region Limit Tests
        [UnityTest]
        public IEnumerator MoveUpLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _maxVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_UP, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_maxVector, rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveDownLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _minVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_DOWN, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_minVector, rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveRightLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _maxVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_RIGHT, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_maxVector, rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveLeftLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _minVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_LEFT, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_minVector, rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveForwardLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _maxVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_FRONT, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_maxVector, rigidBody.position);
        }

        [UnityTest]
        public IEnumerator MoveBackLimitTest()
        {
            var gameObject = new GameObject();
            gameObject.AddComponent(typeof(RigidBody)) as RigidBody;
            rigidBody = gameObject.GetComponent<Rigidbody>();

            rigidBody.position = _minVector;

            GameLogic.movePlayer(rigidBody, RelativeMovementDirection.MOVE_BACK, _playerMovementRestrictions);

            yield return new WaitForSeconds(GameLogicTests._moveDuration);

            Assert.Equals(_minVector, rigidBody.position);
        }
        #endregion

    }

}
