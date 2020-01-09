using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float speed = 5f;

    private float yaw = 0f;
    private float pitch = 0f;
    private float inputZ;
    private float inputX;

    public bool freezeMouseCamera;

    // Start is called before the first frame update
    void Start()
    {
        freezeMouseCamera = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && freezeMouseCamera == false)
        {
            freezeMouseCamera = true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && freezeMouseCamera == true)
        {
            freezeMouseCamera = false;
        }

        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        if(inputZ != 0)
        {
            transform.position += transform.forward * inputZ * 50* Time.deltaTime;
        }

        if (inputX != 0)
        {
            transform.position += transform.right * inputX * 50 * Time.deltaTime;
        }

        if (freezeMouseCamera != true)
        {
            yaw += speed * Input.GetAxis("Mouse X");
            pitch -= speed * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }
}
