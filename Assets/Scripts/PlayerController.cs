using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSensitivity;
    public TMP_Text name;
    public GameObject FPSCamera;

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        name.text = view.Owner.NickName;
        if(!view.IsMine)
        {
            FPSCamera.GetComponent<Camera>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        { 
            if(Input.GetKey("w"))
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey("s"))
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey("a"))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }
            if (Input.GetKey("d"))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }

            float rotation_Y = Input.GetAxis("Mouse X");
            Vector3 rotationVectorY = new Vector3(0.0f, rotation_Y, 0.0f) * rotationSensitivity;

            Vector3 currRotation = transform.rotation.eulerAngles;
            Vector3 finalRotation = currRotation + rotationVectorY;

            transform.rotation = Quaternion.Euler(finalRotation);
        }
    }
}
