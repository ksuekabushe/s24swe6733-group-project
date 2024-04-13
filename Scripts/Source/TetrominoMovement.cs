using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris3D
{
    public class TetrominoMovement : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame (affecting physics)
        void FixedUpdate()
        {
            var movementRestrictions = GameLogic.tetrominoMovementRestrictions;
            if (GameLogic.currentTetromino && GameLogic.currentTetromino.GetComponent<Rigidbody>()) {
                var body = GameLogic.currentTetromino.GetComponent<Rigidbody>();

                if (Input.GetKeyUp("1"))
                {
                    GameLogic.moveTetromino(body, RelativeMovementDirection.TURN_RIGHT, movementRestrictions);
                }
                else if (Input.GetKeyUp("2"))
                {
                    GameLogic.moveTetromino(body, RelativeMovementDirection.TURN_LEFT, movementRestrictions);
                }
                else if (Input.GetKeyUp("3"))
                {
                    GameLogic.moveTetromino(body, RelativeMovementDirection.TURN_FORWARD, movementRestrictions);
                }
                else if (Input.GetKeyUp("4"))
                {
                    GameLogic.moveTetromino(body, RelativeMovementDirection.TURN_BACKWARD, movementRestrictions);
                }
                else if (Input.GetKeyUp("5"))
                {
                    GameLogic.moveTetromino(body, RelativeMovementDirection.TURN_UP, movementRestrictions);
                }
                else if (Input.GetKeyUp("6"))
                {
                    GameLogic.moveTetromino(body, RelativeMovementDirection.TURN_DOWN, movementRestrictions);
                }
                else if (Input.GetKeyUp("7"))
                {
                    GameLogic.moveTetromino(body, RelativeMovementDirection.MOVE_FORWARD, movementRestrictions);
                }
                else if (Input.GetKeyUp("8"))
                {
                    GameLogic.moveTetromino(body, RelativeMovementDirection.MOVE_BACKWARD, movementRestrictions);
                }
                else if (Input.GetKeyUp("9"))
                {
                    GameLogic.moveTetromino(body, RelativeMovementDirection.MOVE_LEFT, movementRestrictions);
                }
                else if (Input.GetKeyUp("0"))
                {
                    GameLogic.moveTetromino(body, RelativeMovementDirection.MOVE_RIGHT, movementRestrictions);
                }
            }
        }
    }
}
