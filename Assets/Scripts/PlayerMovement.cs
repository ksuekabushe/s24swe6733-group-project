using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tetris3D.Logic;

namespace Tetris3D
{
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody body;
        public float sidewaysDisplacement = 10f;
        public float forwardDisplacement = 10f;
        public float upwardDisplacement = 10f;
        public float unitRotation = 500f;
        public float minimumX = 1f;
        public float maximumX = 1f;
        public float minimumY = 1f;
        public float maximumY = 1f;
        public float minimumZ = 1f;
        public float maximumZ = 1f;
        private Vector3 initialPosition;

        // Start is called before the first frame update
        void Start()
        {
            UnityEngine.Debug.Log("Game Start!");
            var bodyAtStart = this.gameObject.GetComponent<Rigidbody>();
            initialPosition = new Vector3(bodyAtStart.position.x, bodyAtStart.position.y, bodyAtStart.position.z);
        }

        // Update is called once per frame (affecting physics)
        void FixedUpdate()
        {
            var movementRestrictions = new MovementRestrictions(
                minimumX,
                maximumX,
                minimumY,
                maximumY,
                minimumZ,
                maximumZ
            );

            GameLogic.initialPosition = initialPosition;
            GameLogic.body = body;
            GameLogic.sidewaysDisplacement = sidewaysDisplacement;
            GameLogic.forwardDisplacement = forwardDisplacement;
            GameLogic.upwardDisplacement = upwardDisplacement;
            GameLogic.unitRotation = unitRotation;

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
                    GameLogic.movePlayer(body, RelativeMovementDirection.MOVE_FRONT, movementRestrictions);
                }
                else if (Input.GetKeyDown("s"))
                {
                    GameLogic.movePlayer(body, RelativeMovementDirection.MOVE_BACK, movementRestrictions);
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
