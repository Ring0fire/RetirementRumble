using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public PlayerController thePlayer;
//	private Vector3 cameraCalibrate;
	
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
	//	cameraCalibrate = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (thePlayer.transform.position.x, (thePlayer.transform.position.y *1.2f) + 2.5f, transform.position.z);
    }
}
