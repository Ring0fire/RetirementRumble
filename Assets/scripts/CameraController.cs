using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public PlayerController thePlayer;
	
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (thePlayer.transform.position.x, transform.position.y, transform.position.z);
    }
}
