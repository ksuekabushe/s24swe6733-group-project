using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris3D
{
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody body;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame (affecting physics)
        void FixedUpdate()
        {
            var movementRestrictions = GameLogic.playerMovementRestrictions;

            if (Input.GetKeyUp(KeyCode.Space))
            {
                GameLogic.movePlayer(body, RelativeMovementDirection.RESET_TO_INITIAL_POSITION, movementRestrictions);
            }
            else if (Input.GetKeyDown("r"))
            {
                GameLogic.movePlayer(body, RelativeMovementDirection.TURN_RIGHT, movementRestrictions);
            }
            else if (Input.GetKeyDown("t"))
            {
                GameLogic.movePlayer(body, RelativeMovementDirection.TURN_LEFT, movementRestrictions);
            }
            else
            {
                if (Input.GetKeyDown("w"))
                {
                    GameLogic.movePlayer(body, RelativeMovementDirection.MOVE_FORWARD, movementRestrictions);
                }
                else if (Input.GetKeyDown("s"))
                {
                    GameLogic.movePlayer(body, RelativeMovementDirection.MOVE_BACKWARD, movementRestrictions);
                }
                else if (Input.GetKeyDown("a"))
                {
                    GameLogic.movePlayer(body, RelativeMovementDirection.MOVE_LEFT, movementRestrictions);
                }
                else if (Input.GetKeyDown("d"))
                {
                    GameLogic.movePlayer(body, RelativeMovementDirection.MOVE_RIGHT, movementRestrictions);
                }
                else if (Input.GetKeyDown("q"))
                {
                    GameLogic.movePlayer(body, RelativeMovementDirection.MOVE_UP, movementRestrictions);
                }
                else if (Input.GetKeyDown("e"))
                {
                    GameLogic.movePlayer(body, RelativeMovementDirection.MOVE_DOWN, movementRestrictions);
                }
            }
        }
    }
}
