using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        { 
            if(Input.GetKey("w"))
            {
                Vector3 movement = new Vector3(0.0f, 0.0f, speed * Time.deltaTime);
                transform.position += movement;
            }
            if (Input.GetKey("s"))
            {
                Vector3 movement = new Vector3(0.0f, 0.0f, -speed * Time.deltaTime);
                transform.position += movement;
            }
            if (Input.GetKey("a"))
            {
                Vector3 movement = new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f);
                transform.position += movement;
            }
            if (Input.GetKey("d"))
            {
                Vector3 movement = new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
                transform.position += movement;
            }
        }
    }
}
