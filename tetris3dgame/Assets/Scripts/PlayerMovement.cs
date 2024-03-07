using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Vector3 initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("Game Start!");
        var bodyAtStart = this.gameObject.GetComponent<Rigidbody>();
        initialPosition = new Vector3(bodyAtStart.position.x, bodyAtStart.position.y, bodyAtStart.position.z);
        initialRotation = new Vector3(bodyAtStart.rotation.x, bodyAtStart.rotation.y, bodyAtStart.rotation.z);
    }

    // Update is called once per frame (affecting physics)
    void FixedUpdate()
    {
        var movementRestrictions = new PlayerMovementRestrictions(
            minimumX,
            maximumX,
            minimumY,
            maximumY,
            minimumZ,
            maximumZ
        );

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameLogic.movePlayer(body, RelativeMovementDirection.RESET_TO_INITIAL_POSITION, movementRestrictions);
        } else if (Input.GetKeyDown("r"))
        {
            GameLogic.movePlayer(body, RelativeMovementDirection.TURN_LEFT, movementRestrictions);
        }
        else if (Input.GetKeyDown("t"))
        {
            GameLogic.movePlayer(body, RelativeMovementDirection.TURN_RIGHT, movementRestrictions);
        } else {
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
