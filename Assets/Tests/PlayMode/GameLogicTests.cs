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
        
        [UnityTest]
        public IEnumerator DeleteNoneTest()
        {
            SubPiece[] onePieceTetrominoSubPieces = new SubPiece[]
            {
                new SubPiece() { center: (20, 10, 10)}
            };

            Arena onePieceArena = new Arena()
            {
                Floors = new ArenaFloors[]{new ArenaFloor()
                {
                    Rows= new ArenaRow[]
                    {
                        new ArenaRow()
                        {
                            pieces = new ArenaSubPiece[]{ new ArenaSubPiece(){center: (0, 0, 0)}
                        };
                };
                };
            };

            GameLogic.updateArenaSubPieces(onePieceArena, onePieceTetrominoSubPieces);

            Assert.Equals(GameLogic.floorsDestroyed, 0);
        }

        [UnityTest]
        public IEnumerator DeleteOneTest()
        {
            SubPiece[] onePieceTetrominoSubPieces = new SubPiece[] { new SubPiece() { center: (0, 0, 0)}};
            Arena onePieceArena = new Arena()
            {
                Floors = new ArenaFloors[]
                {
                    new ArenaFloor()
                    {
                        Rows= new ArenaRow[]
                        {
                            new ArenaRow()
                            {
                                pieces = new ArenaSubPiece[]{ new ArenaSubPiece(){center: (0, 0, 0)}};
                            };
                        };
                    };
                };
        };

            GameLogic.updateArenaSubPieces(onePieceArena, onePieceTetrominoSubPieces);

            Assert.Equals(GameLogic.floorsDestroyed, 1);
        }

        [UnityTest]
        public IEnumerator DeleteTwoTest()
        {
            SubPiece[] onePieceTetrominoSubPieces = new SubPiece[]
            {
                new SubPiece(){center: (0, 0, 0)},
        new SubPiece() { center: (0, 1, 0)},
        };

            Arena onePieceArena = new Arena()
            {
                Floors = new ArenaFloors[]
                {
                    new ArenaFloor()
                    {
                        Rows= new ArenaRow[]
                        {
                            new ArenaRow()
                            {
                                pieces = new ArenaSubPiece[]{ new ArenaSubPiece(){center: (0, 0, 0)}}
                            };
                        };
            },
            new ArenaFloor()
                    {
                        Rows = new ArenaRow[]
                            {
                                new ArenaRow()
                                {
                                    pieces = new ArenaSubPiece[]{ new ArenaSubPiece(){center: (0,1,0)} }
                                };
                            };
                    };
                };
            };
            Assert.Equals(GameLogic.floorsDestroyed, 2);
        }

        [UnityTest]
        public IEnumerator RegisterOneMatching()
        {
            SubPiece[] onePieceTetrominoSubPieces = new SubPiece[]
            {
                    new SubPiece(){center: (0, 0, 0)},
            new SubPiece() { center: (0, 3, 0) },
        };

            Arena onePieceArena = new Arena()
            {
                Floors = new ArenaFloors[]
                {
                    new ArenaFloor()
                    {
                        Rows= new ArenaRow[]
                        {
                            new ArenaRow()
                            {
                                pieces = new ArenaSubPiece[]{ new ArenaSubPiece(){center: (0, 0, 0)}}
                            };
                        };
            },
            new ArenaFloor()
                    {
                        Rows = new ArenaRow[]
                        {
                            new ArenaRow()
                            {
                                pieces = new ArenaSubPiece[]{ new ArenaSubPiece(){center: (0,1,0)} }
                            };
                        };
                    };
                };
            };
            Assert.Equals(GameLogic.findMatchingArenaSubPieces().Length, 1);
        }

        [UnityTest]
        public IEnumerator RegisterNoMatching()
        {
            SubPiece[] onePieceTetrominoSubPieces = new SubPiece[]
            {
                        new SubPiece(){center: (0, 2, 0)},
                new SubPiece() { center: (0, 3, 0) },
        };

            Arena onePieceArena = new Arena()
            {
                Floors = new ArenaFloors[]
                {
                    new ArenaFloor()
                    {
                        Rows= new ArenaRow[]
                        {
                            new ArenaRow()
                            {
                                pieces = new ArenaSubPiece[]{ new ArenaSubPiece(){center: (0, 0, 0)}}
                            };
                        };
            },
            new ArenaFloor()
                    {
                        Rows = new ArenaRow[]
                        {
                            new ArenaRow()
                            {
                                pieces = new ArenaSubPiece[]{ new ArenaSubPiece(){center: (0,1,0)} }
                            };
                        };
                    };
                };
            };
            Assert.Equals(GameLogic.findMatchingArenaSubPieces().Length, 0);
        }

        #endregion

    }

}
