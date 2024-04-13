using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Tetris3D
{
    public static class GameLogic
    {
        public const int tetrominoesNeededToWin = 10; // Set to a low number for testing the game
        public static Rigidbody body;
        public static float playerSidewaysDisplacement = 10f;
        public static float playerForwardDisplacement = 10f;
        public static float playerUpwardDisplacement = 10f;
        public static float tetrominoSidewaysDisplacement = 10f;
        public static float tetrominoForwardDisplacement = 10f;
        public static float tetrominoUpwardDisplacement = 10f;
        public static float playerUnitRotation = 500f;
        public static float tetrominoUnitRotation = 500f;
        public static Vector3 initialPosition; // Initial player position
        public static Vector3 initialTetrominoPosition; // Initial player position
        public static bool gameIsPaused = false;
        public static bool playerMovingTetromino = false;
        public static bool tetrominoReady = false;
        public static bool tetrominoHasCollided = false;
        public static int highScore = 0;
        public static int currentScore = 0;
        public static int currentLevel = 1;
        public static int tetrominoesUsed = 0;
        public static int floorsDestroyed = 0;
        public static GameObject currentTetromino;
        public static GameObject nextTetromino;
        public static MovementRestrictions playerMovementRestrictions;
        public static MovementRestrictions tetrominoMovementRestrictions;
        public static bool gameIsLost = false;
        public static GameObject arenaObject;
        public static GamePlay gamePlay;

        public static void hideArena()
        {
            if (arenaObject != null)
            {
                Transform[] arenaPieces = arenaObject.GetComponentsInChildren<Transform>();
                for (int i = 0; i < arenaPieces.Length; i++)
                {
                    if (arenaPieces[i].name != "ArenaCubes" && arenaPieces[i].name.Contains("ArenaCube"))
                    {
                        arenaPieces[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        public static Arena getArena()
        {
            if (arenaObject == null)
            {
                return null;
            }
            Transform[] arenaPieces = arenaObject.GetComponentsInChildren<Transform>();
            List<ArenaFloor> floors = new List<ArenaFloor>() { };
            for (int i = 0; i < arenaPieces.Length; i++)
            {
                List<ArenaRow> rows = new List<ArenaRow>() { };
                if (arenaPieces[i].name != "ArenaFloors" && arenaPieces[i].name.Contains("ArenaFloor"))
                {
                    Transform[] floorPieces = arenaPieces[i].GetComponentsInChildren<Transform>();
                    for (int j = 0; j < floorPieces.Length; j++)
                    {
                        if (floorPieces[j].name != "ArenaRows" && floorPieces[j].name.Contains("ArenaRow"))
                        {
                            Transform[] rowPieces = floorPieces[j].GetComponentsInChildren<Transform>();
                            List<ArenaSubPiece> pieces = new List<ArenaSubPiece>() { };
                            for (int k = 0; k < rowPieces.Length; k++)
                            {
                                if (rowPieces[k].name != "ArenaCubes" && rowPieces[k].name.Contains("ArenaCube"))
                                {
                                    pieces.Add(new ArenaSubPiece()
                                    {
                                        gameObject = rowPieces[k].gameObject
                                    });
                                }
                            }
                            rows.Add(new ArenaRow()
                            {
                                pieces = pieces.ToArray()
                            });
                        }
                    }
                }
                floors.Add(new ArenaFloor()
                {
                    rows = rows.ToArray()
                });
            }
            return new Arena()
            {
                floors = floors.ToArray()
            };
        }
        public static void updateArenaSubPieces(Arena arena, SubPiece[] tetrominoPieces)
        {
            SubPiece[] subPieces = arena.getAllSubPieces();
            for (int i = 0; i < tetrominoPieces.Length; i++)
            {
                SubPiece closestPiece = null;
                float distance = 20f;
                Material material = getGameObjectMaterial(tetrominoPieces[i].gameObject);
                for (int j = 0; j < subPieces.Length; j++)
                {
                    float newDistance = Vector3.Distance(subPieces[j].gameObject.transform.position, tetrominoPieces[i].gameObject.transform.position);
                    setGameObjectMaterial(subPieces[j].gameObject, material);
                    subPieces[j].gameObject.SetActive(true);
                    if (newDistance <= distance)
                    {
                        distance = newDistance;
                        closestPiece = subPieces[j];
                    }
                }
                if (closestPiece != null && distance < 2f)
                {
                    UnityEngine.Debug.Log("Shortest distance");
                    UnityEngine.Debug.Log(distance);
                    UnityEngine.Debug.Log("closest sub piece");
                    UnityEngine.Debug.Log(closestPiece);
                    UnityEngine.Debug.Log("material");
                    UnityEngine.Debug.Log(material);
                    UnityEngine.Debug.Log(closestPiece.gameObject);
                    setGameObjectMaterial(closestPiece.gameObject, material);
                    closestPiece.gameObject.SetActive(true);
                }
                else
                {
                    UnityEngine.Debug.Log("no closest piece found");
                }
            }
            destroyFullFloors(arena);
        }

        public static SubPiece[] getTetrominoSubPieces(GameObject tetromino)
        {
            Transform[] list = tetromino.GetComponentsInChildren<Transform>();
            List<SubPiece> pieces = new List<SubPiece>() { };
            for (int i = 0; i < list.Length; i++)
            {
                UnityEngine.Debug.Log(list[i]);
                pieces.Add(new SubPiece()
                {
                    gameObject = list[i].gameObject
                });
            }
            return pieces.ToArray();
        }

        public static void onTetrominoLanding(Arena arena, GameObject tetromino)
        {
            gameIsPaused = true;
            tetromino.SetActive(false);
            updateArenaSubPieces(arena, getTetrominoSubPieces(tetromino));
            destroyFullFloors(arena);
            tetrominoesUsed++;
            // TODO: show winning/losing message for 10s
            gameIsPaused = false;
        }

        public static void rotateTopMostFloor(Arena arena)
        {
            gameIsPaused = true;
            for (int i = 0; i < arena.floors.Length; i++)
            {
                if (arena.floors[i].isOnePieceActive)
                {
                    // Move sub-pieces from (x, y, z) to (r -z , y, r - x ) for a clockwise rotation (90 degrees)
                    bool[,] activations = new bool[10, 10];
                    Material[,] materials = new Material[10, 10];
                    GameObject[,] gameObjects = new GameObject[10, 10];
                    ArenaFloor currentFloor = arena.floors[i];
                    for (int j = 0; j < currentFloor.rows.Length; j++)
                    {
                        ArenaRow row = currentFloor.rows[j];
                        for (int k = 0; k < row.pieces.Length; k++)
                        {
                            activations[j, k] = row.pieces[k].gameObject.activeSelf;
                            materials[j, k] = getGameObjectMaterial(row.pieces[k].gameObject);
                            gameObjects[j, k] = row.pieces[k].gameObject;
                        }
                    }

                    for (int j = 0; j < currentFloor.rows.Length; j++)
                    {
                        ArenaRow row = currentFloor.rows[j];
                        for (int k = 0; k < row.pieces.Length; k++)
                        {
                            gameObjects[j, k].SetActive(activations[row.pieces.Length - 1 - k, currentFloor.rows.Length - 1 - j]);
                            setGameObjectMaterial(gameObjects[j, k], materials[row.pieces.Length - 1 - k, currentFloor.rows.Length - 1 - j]);
                        }
                    }

                    break;
                }
            }
            collapseFloors(arena);
            gameIsPaused = false;
        }

        public static void collapseFloors(Arena arena)
        {

        }
        public static void destroyFullFloors(Arena arena)
        {
            // TODO: Disable pieces in floors that have been filled from top to bottom
            // TODO: If a floor in between the bottom and the top has been disabled, drop everything by one
            // TODO: repeat until there is nothing else to do
            collapseFloors(arena);
        }

        public static GameObject getRandomGameObject(GameObject[] gameObjects)
        {
            return gameObjects[UnityEngine.Random.Range(0, gameObjects.Length - 1)];
        }

        public static Material getRandomMaterial(Material[] materials)
        {
            return materials[UnityEngine.Random.Range(0, materials.Length - 1)];
        }

        public static void setGameObjectMaterial(GameObject gameObject, Material material)
        {
            var renderer = gameObject.GetComponent<Renderer>();

            UnityEngine.Debug.Log("setting material");
            UnityEngine.Debug.Log(material);
            UnityEngine.Debug.Log("on");
            UnityEngine.Debug.Log(gameObject);
            UnityEngine.Debug.Log("renderer");
            UnityEngine.Debug.Log(renderer);
            if (renderer != null)
            {
                renderer.material = material;
            }
            var renderers = gameObject.GetComponentsInChildren<Renderer>();
            UnityEngine.Debug.Log("renderers");
            UnityEngine.Debug.Log(renderers.Length);
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material = material;
            }
        }

        public static Material getGameObjectMaterial(GameObject gameObject)
        {
            var renderer = gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                return renderer.material;
            }
            var renderers = gameObject.GetComponentsInChildren<Renderer>();
            if (renderers.Length < 1)
            {
                return null;
            }
            return renderers[0].material;
        }

        public static void setSkyMaterial(Material material)
        {
            RenderSettings.skybox = material;
        }

        public static void resetTetrominoTransform(GameObject tetromino)
        {
            tetromino.transform.position = new Vector3(initialTetrominoPosition.x, initialTetrominoPosition.y, initialTetrominoPosition.z);
            tetromino.transform.rotation = Quaternion.identity;
        }

        public static void moveTetromino(Rigidbody tetrominoBody, RelativeMovementDirection movement, MovementRestrictions movementRestrictions)
        {
            GameLogic.playerMovingTetromino = true;
            try
            {
                var displacement = new Vector3(0, 0, 0);
                // UnityEngine.Debug.Log(
                //     $"Current tetromino restrictions: ({movementRestrictions.minimum_x},{movementRestrictions.maximum_x},{movementRestrictions.minimum_y},{movementRestrictions.maximum_y},{movementRestrictions.minimum_z},{movementRestrictions.maximum_z},)");
                // UnityEngine.Debug.Log(
                //     $"Current tetromino position: {tetrominoBody.position.x}, {tetrominoBody.position.y}, {tetrominoBody.position.z},");
                // UnityEngine.Debug.Log(
                //     $"Current tetromino rotation: {tetrominoBody.rotation}");
                if (movement == RelativeMovementDirection.TURN_RIGHT)
                {
                    UnityEngine.Debug.Log("Turn tetromino right");
                    tetrominoBody.MoveRotation(tetrominoBody.rotation * Quaternion.Euler(new Vector3(0, -GameLogic.tetrominoUnitRotation, 0) * Time.fixedDeltaTime));
                }
                else if (movement == RelativeMovementDirection.TURN_LEFT)
                {
                    UnityEngine.Debug.Log("Turn tetromino left");
                    tetrominoBody.MoveRotation(tetrominoBody.rotation * Quaternion.Euler(new Vector3(0, GameLogic.tetrominoUnitRotation, 0) * Time.fixedDeltaTime));
                }
                else if (movement == RelativeMovementDirection.TURN_FORWARD)
                {
                    UnityEngine.Debug.Log("Turn tetromino forward");
                    tetrominoBody.MoveRotation(tetrominoBody.rotation * Quaternion.Euler(new Vector3(GameLogic.tetrominoUnitRotation, 0, 0) * Time.fixedDeltaTime));
                }
                else if (movement == RelativeMovementDirection.TURN_BACKWARD)
                {
                    UnityEngine.Debug.Log("Turn tetromino backward");
                    tetrominoBody.MoveRotation(tetrominoBody.rotation * Quaternion.Euler(new Vector3(-GameLogic.tetrominoUnitRotation, 0, 0) * Time.fixedDeltaTime));
                }
                else if (movement == RelativeMovementDirection.TURN_UP)
                {
                    UnityEngine.Debug.Log("Turn tetromino forward");
                    tetrominoBody.MoveRotation(tetrominoBody.rotation * Quaternion.Euler(new Vector3(0, 0, GameLogic.tetrominoUnitRotation) * Time.fixedDeltaTime));
                }
                else if (movement == RelativeMovementDirection.TURN_DOWN)
                {
                    UnityEngine.Debug.Log("Turn tetromino down");
                    tetrominoBody.MoveRotation(tetrominoBody.rotation * Quaternion.Euler(new Vector3(0, 0, -GameLogic.tetrominoUnitRotation) * Time.fixedDeltaTime));
                }
                else
                {
                    if (movement == RelativeMovementDirection.MOVE_FORWARD)
                    {
                        displacement = tetrominoBody.transform.forward * tetrominoForwardDisplacement * Time.fixedDeltaTime;
                        if ((tetrominoBody.position + displacement).z > movementRestrictions.maximum_z)
                        {
                            // UnityEngine.Debug.Log($"Too close to wall to go forward Z would be {(tetrominoBody.position + displacement).z}");
                        }
                        else
                        {
                            UnityEngine.Debug.Log("Moving tetromino forward");
                        }
                    }
                    else if (movement == RelativeMovementDirection.MOVE_BACKWARD)
                    {
                        displacement = -tetrominoBody.transform.forward * tetrominoForwardDisplacement * Time.fixedDeltaTime;
                        if ((tetrominoBody.position + displacement).z < movementRestrictions.minimum_z)
                        {
                            // UnityEngine.Debug.Log("Too close to wall to go backward");
                        }
                        else
                        {
                            UnityEngine.Debug.Log("Moving tetromino backward");
                        }
                    }
                    else if (movement == RelativeMovementDirection.MOVE_LEFT)
                    {
                        displacement = -tetrominoBody.transform.right * tetrominoSidewaysDisplacement * Time.fixedDeltaTime;
                        if ((tetrominoBody.position + displacement).x < movementRestrictions.minimum_x)
                        {
                            // UnityEngine.Debug.Log("Too close to wall to go left");
                        }
                        else
                        {
                            UnityEngine.Debug.Log("Tetromino going left");
                        }
                    }
                    else if (movement == RelativeMovementDirection.MOVE_RIGHT)
                    {
                        displacement = tetrominoBody.transform.right * tetrominoSidewaysDisplacement * Time.fixedDeltaTime;
                        if ((tetrominoBody.position + displacement).x > movementRestrictions.maximum_x)
                        {
                            // UnityEngine.Debug.Log("Too close to wall to go right");
                        }
                        else
                        {
                            UnityEngine.Debug.Log("Tetromino going right");
                        }
                    }
                    else if (movement == RelativeMovementDirection.MOVE_DOWN)
                    {
                        displacement = new Vector3(0, -tetrominoUpwardDisplacement * Time.fixedDeltaTime, 0);
                        if ((tetrominoBody.position + displacement).y < movementRestrictions.minimum_y)
                        {
                            // UnityEngine.Debug.Log("Too low to go down");
                        }
                        else
                        {
                            UnityEngine.Debug.Log("Tetromino going down");
                        }
                    }

                    var newPosition = tetrominoBody.position + displacement;
                    if (newPosition.x <= movementRestrictions.maximum_x && newPosition.x >= movementRestrictions.minimum_x &&
                            newPosition.y <= movementRestrictions.maximum_y && newPosition.y >= movementRestrictions.minimum_y &&
                            newPosition.z <= movementRestrictions.maximum_z && newPosition.z >= movementRestrictions.minimum_z
                        )
                    {
                        UnityEngine.Debug.Log("Tetromino position updated");
                        tetrominoBody.position = newPosition;
                    }
                    else
                    {
                        // UnityEngine.Debug.Log("Tetromino could be out of bounds, not updating");
                    }
                }
            }
            catch (Exception error)
            {
                GameLogic.playerMovingTetromino = false;
                throw error;
            }
            GameLogic.playerMovingTetromino = false;
        }

        public static void movePlayer(Rigidbody playerBody, RelativeMovementDirection movement, MovementRestrictions movementRestrictions)
        {
            if (gameIsPaused)
            {
                return; // DO NOT MOVE IF GAME IS PAUSED
            }
            var displacement = new Vector3(0, 0, 0);
            UnityEngine.Debug.Log(
                $"Current player restrictions: ({movementRestrictions.minimum_x},{movementRestrictions.maximum_x},{movementRestrictions.minimum_y},{movementRestrictions.maximum_y},{movementRestrictions.minimum_z},{movementRestrictions.maximum_z},)");
            UnityEngine.Debug.Log(
                $"Current player position: {playerBody.position.x}, {playerBody.position.y}, {playerBody.position.z},");
            UnityEngine.Debug.Log(
                $"Current player rotation: {playerBody.rotation}");
            if (movement == RelativeMovementDirection.RESET_TO_INITIAL_POSITION)
            {
                UnityEngine.Debug.Log("Resetting player position");
                playerBody.position = new Vector3(GameLogic.initialPosition.x, GameLogic.initialPosition.y, GameLogic.initialPosition.z);
                playerBody.rotation = Quaternion.identity;
            }
            else if (movement == RelativeMovementDirection.TURN_RIGHT)
            {
                UnityEngine.Debug.Log("Turn right");
                playerBody.MoveRotation(playerBody.rotation * Quaternion.Euler(new Vector3(0, -GameLogic.tetrominoUnitRotation, 0) * Time.fixedDeltaTime));
            }
            else if (movement == RelativeMovementDirection.TURN_LEFT)
            {
                UnityEngine.Debug.Log("Turn left");
                playerBody.MoveRotation(playerBody.rotation * Quaternion.Euler(new Vector3(0, GameLogic.tetrominoUnitRotation, 0) * Time.fixedDeltaTime));
            }
            else
            {
                if (movement == RelativeMovementDirection.MOVE_FORWARD)
                {
                    displacement = playerBody.transform.forward * playerForwardDisplacement * Time.fixedDeltaTime;
                    if ((playerBody.position + displacement).z > movementRestrictions.maximum_z)
                    {
                        UnityEngine.Debug.Log($"Too close to wall to go forward Z would be {(playerBody.position + displacement).z}");
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Moving forward");
                    }
                }
                else if (movement == RelativeMovementDirection.MOVE_BACKWARD)
                {
                    displacement = -playerBody.transform.forward * playerForwardDisplacement * Time.fixedDeltaTime;
                    if ((playerBody.position + displacement).z < movementRestrictions.minimum_z)
                    {
                        UnityEngine.Debug.Log("Too close to wall to go backward");
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Moving backward");
                    }
                }
                else if (movement == RelativeMovementDirection.MOVE_LEFT)
                {
                    displacement = -playerBody.transform.right * playerSidewaysDisplacement * Time.fixedDeltaTime;
                    if ((playerBody.position + displacement).x < movementRestrictions.minimum_x)
                    {
                        UnityEngine.Debug.Log("Too close to wall to go left");
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Going left");
                    }
                }
                else if (movement == RelativeMovementDirection.MOVE_RIGHT)
                {
                    displacement = playerBody.transform.right * playerSidewaysDisplacement * Time.fixedDeltaTime;
                    if ((playerBody.position + displacement).x > movementRestrictions.maximum_x)
                    {
                        UnityEngine.Debug.Log("Too close to wall to go right");
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Going right");
                    }
                }
                else if (movement == RelativeMovementDirection.MOVE_UP)
                {
                    displacement = new Vector3(0, playerUpwardDisplacement * Time.fixedDeltaTime, 0);
                    if ((playerBody.position + displacement).y > movementRestrictions.maximum_y)
                    {
                        UnityEngine.Debug.Log("Too high to go up");
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Going up");
                    }
                }
                else if (movement == RelativeMovementDirection.MOVE_DOWN)
                {
                    displacement = new Vector3(0, -playerUpwardDisplacement * Time.fixedDeltaTime, 0);
                    if ((playerBody.position + displacement).y < movementRestrictions.minimum_y)
                    {
                        UnityEngine.Debug.Log("Too low to go down");
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Going down");
                    }
                }

                var newPosition = playerBody.position + displacement;
                if (newPosition.x <= movementRestrictions.maximum_x && newPosition.x >= movementRestrictions.minimum_x &&
                        newPosition.y <= movementRestrictions.maximum_y && newPosition.y >= movementRestrictions.minimum_y &&
                        newPosition.z <= movementRestrictions.maximum_z && newPosition.z >= movementRestrictions.minimum_z
                    )
                {
                    playerBody.position = newPosition;
                }
            }
        }
    }
}
