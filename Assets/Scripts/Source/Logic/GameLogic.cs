using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris3D
{
    public static class GameLogic
    {
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
        public static int floorsDestroyed = 0;
        public static GameObject currentTetromino;
        public static GameObject nextTetromino;
        public static MovementRestrictions playerMovementRestrictions;
        public static MovementRestrictions tetrominoMovementRestrictions;

        public static GameObject getRandomGameObject(GameObject[] gameObjects){
            return gameObjects[UnityEngine.Random.Range(0, gameObjects.Length -1)];
        }

        public static Material getRandomMaterial(Material[] materials){
            return materials[UnityEngine.Random.Range(0, materials.Length -1)];
        }

        public static void setTetrominoMaterial(GameObject tetromino, Material material){
            var renderers = tetromino.GetComponentsInChildren<Renderer>();
            for(int i = 0; i < renderers.Length;i++)
            {
                renderers[i].material = material;
            }
        }

        public static void setSkyMaterial(Material material){
            RenderSettings.skybox = material;
        }

        public static void setGameObjectMaterial(GameObject gameObject, Material material){
            var renderer = gameObject.GetComponent<Renderer>();
            renderer.material = material;
        }

        public static void resetTetrominoTransform(GameObject tetromino){
            tetromino.transform.position = new Vector3(initialTetrominoPosition.x, initialTetrominoPosition.y, initialTetrominoPosition.z);
            tetromino.transform.rotation = Quaternion.identity;
        }

        public static void moveTetromino(Rigidbody tetrominoBody, RelativeMovementDirection movement, MovementRestrictions movementRestrictions)
        {
            GameLogic.playerMovingTetromino = true;
            try {
                var displacement = new Vector3(0, 0, 0);
                UnityEngine.Debug.Log(
                    $"Current tetromino restrictions: ({movementRestrictions.minimum_x},{movementRestrictions.maximum_x},{movementRestrictions.minimum_y},{movementRestrictions.maximum_y},{movementRestrictions.minimum_z},{movementRestrictions.maximum_z},)");
                UnityEngine.Debug.Log(
                    $"Current tetromino position: {tetrominoBody.position.x}, {tetrominoBody.position.y}, {tetrominoBody.position.z},");
                UnityEngine.Debug.Log(
                    $"Current tetromino rotation: {tetrominoBody.rotation}");
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
                    UnityEngine.Debug.Log("Turn tetromino backward");
                    tetrominoBody.MoveRotation(tetrominoBody.rotation * Quaternion.Euler(new Vector3(0, 0, -GameLogic.tetrominoUnitRotation) * Time.fixedDeltaTime));
                }
                else
                {
                    if (movement == RelativeMovementDirection.MOVE_FORWARD)
                    {
                        displacement = tetrominoBody.transform.forward * tetrominoForwardDisplacement * Time.fixedDeltaTime;
                        if ((tetrominoBody.position + displacement).z > movementRestrictions.maximum_z)
                        {
                            UnityEngine.Debug.Log($"Too close to wall to go forward Z would be {(tetrominoBody.position + displacement).z}");
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
                            UnityEngine.Debug.Log("Too close to wall to go backward");
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
                            UnityEngine.Debug.Log("Too close to wall to go left");
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
                            UnityEngine.Debug.Log("Too close to wall to go right");
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
                            UnityEngine.Debug.Log("Too low to go down");
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
                        tetrominoBody.position = newPosition;
                    }
                }
            } catch (Exception error){
                GameLogic.playerMovingTetromino = false;
                throw error;
            }
        }

        public static void movePlayer(Rigidbody playerBody, RelativeMovementDirection movement, MovementRestrictions movementRestrictions)
        {
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
