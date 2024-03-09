using UnityEngine;

namespace Tetris3D.Logic
{
    public static class GameLogic
    {
        public static Rigidbody body;
        public static float sidewaysDisplacement = 10f;
        public static float forwardDisplacement = 10f;
        public static float upwardDisplacement = 10f;
        public static float unitRotation = 500f;
        public static Vector3 initialPosition;
        public static MovementRestrictions movementRestrictions;
        public static bool gameIsPaused = false;
        public static int highScore = 0;
        public static int currentScore = 0;
        public static int currentLevels = 0;
        public static int floorsDestroyed = 0;
        public static Tetromino currentTetromino;
        public static Tetromino nextTetromino;

        private static Tetromino[] _tetrominoesUsed = { };

        public static void moveTetromino(Rigidbody tetrominoBody, RelativeMovementDirection movement, MovementRestrictions movementRestrictions)
        {
            // TODO implement (1-6 rotations + 7-0 translations and "-" for down translations)
        }
        public static void movePlayer(Rigidbody playerBody, RelativeMovementDirection movement, MovementRestrictions movementRestrictions)
        {
            var displacement = new Vector3(0, 0, 0);
            UnityEngine.Debug.Log(
                $"Current restrictions: ({movementRestrictions.minimum_x}");
            UnityEngine.Debug.Log(
                $"Current restrictions: ({movementRestrictions.minimum_x},{movementRestrictions.maximum_x},{movementRestrictions.minimum_y},{movementRestrictions.maximum_y},{movementRestrictions.minimum_z},{movementRestrictions.maximum_z},)");
            UnityEngine.Debug.Log(
                $"Current position: {playerBody.position.x}, {playerBody.position.y}, {playerBody.position.z},");
            UnityEngine.Debug.Log(
                $"Current rotation: {playerBody.rotation}");
            if (movement == RelativeMovementDirection.RESET_TO_INITIAL_POSITION)
            {
                UnityEngine.Debug.Log("Resetting player position");
                playerBody.position = new Vector3(GameLogic.initialPosition.x, GameLogic.initialPosition.y, GameLogic.initialPosition.z);
                playerBody.rotation = Quaternion.identity;
            }
            else if (movement == RelativeMovementDirection.TURN_RIGHT)
            {
                UnityEngine.Debug.Log("Turn right");
                playerBody.MoveRotation(playerBody.rotation * Quaternion.Euler(new Vector3(0, -GameLogic.unitRotation, 0) * Time.fixedDeltaTime));
            }
            else if (movement == RelativeMovementDirection.TURN_LEFT)
            {
                UnityEngine.Debug.Log("Turn left");
                playerBody.MoveRotation(playerBody.rotation * Quaternion.Euler(new Vector3(0, GameLogic.unitRotation, 0) * Time.fixedDeltaTime));
            }
            else
            {
                if (movement == RelativeMovementDirection.MOVE_FRONT)
                {
                    displacement = playerBody.transform.forward * forwardDisplacement * Time.fixedDeltaTime;
                    if ((playerBody.position + displacement).z > movementRestrictions.maximum_z)
                    {
                        UnityEngine.Debug.Log($"Too close to wall to go forward Z would be {(playerBody.position + displacement).z}");
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Moving forward");
                    }
                }
                else if (movement == RelativeMovementDirection.MOVE_BACK)
                {
                    displacement = -playerBody.transform.forward * forwardDisplacement * Time.fixedDeltaTime;
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
                    displacement = -playerBody.transform.right * sidewaysDisplacement * Time.fixedDeltaTime;
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
                    displacement = playerBody.transform.right * sidewaysDisplacement * Time.fixedDeltaTime;
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
                    displacement = new Vector3(0, upwardDisplacement * Time.fixedDeltaTime, 0);
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
                    displacement = new Vector3(0, -upwardDisplacement * Time.fixedDeltaTime, 0);
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
