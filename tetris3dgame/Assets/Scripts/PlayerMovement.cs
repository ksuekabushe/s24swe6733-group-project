using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody body;
    public float sidewaysDisplacement = 10f;
    public float forwardDisplacement = 10f;
    public float upwardDisplacement = 10f;
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
        if (Input.GetKeyDown("w"))
        {
            UnityEngine.Debug.Log("Moving forward");
            if ((body.position + new Vector3(0, 0, forwardDisplacement * Time.deltaTime)).z <= maximumZ)
            {
                body.position = body.position + new Vector3(0, 0, forwardDisplacement * Time.deltaTime);
            }
            else
            {
                UnityEngine.Debug.Log("Too close to wall to go forward");
            }
        }
        else if (Input.GetKeyDown("s"))
        {
            UnityEngine.Debug.Log("Moving backward");
            if ((body.position + new Vector3(0, 0, -forwardDisplacement * Time.deltaTime)).z >= minimumZ)
            {
                body.position = body.position + new Vector3(0, 0, -forwardDisplacement * Time.deltaTime);
            }
            else
            {
                UnityEngine.Debug.Log("Too close to wall to go backward");
            }
        }
        else if (Input.GetKeyDown("a"))
        {
            UnityEngine.Debug.Log("Going left");
            if ((body.position + new Vector3(-sidewaysDisplacement * Time.deltaTime, 0, 0)).x >= minimumX)
            {
                body.position = body.position + new Vector3(-sidewaysDisplacement * Time.deltaTime, 0, 0);
            }
            else
            {
                UnityEngine.Debug.Log("Too close to wall to go left");
            }
        }
        else if (Input.GetKeyDown("d"))
        {
            UnityEngine.Debug.Log("Going right");
            if ((body.position + new Vector3(sidewaysDisplacement * Time.deltaTime, 0, 0)).x <= maximumX)
            {
                body.position = body.position + new Vector3(sidewaysDisplacement * Time.deltaTime, 0, 0);
            }
            else
            {
                UnityEngine.Debug.Log("Too close to wall to go right");
            }
        }
        else if (Input.GetKeyDown("q"))
        {
            UnityEngine.Debug.Log("Going up");
            if ((body.position + new Vector3(0, upwardDisplacement * Time.deltaTime, 0)).y <= maximumY)
            {
                body.position = body.position + new Vector3(0, upwardDisplacement * Time.deltaTime, 0);
            }
            else
            {
                UnityEngine.Debug.Log("Too high to go up");
            }
        }
        else if (Input.GetKeyDown("e"))
        {
            UnityEngine.Debug.Log("Going down");
            if ((body.position + new Vector3(0, -upwardDisplacement * Time.deltaTime, 0)).y >= minimumY)
            {
                body.position = body.position + new Vector3(0, -upwardDisplacement * Time.deltaTime, 0);
            }
            else
            {
                UnityEngine.Debug.Log("Too low to go down");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            UnityEngine.Debug.Log("Resetting player position");
            body.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
            // TODO : reset position
        }
    }
}
