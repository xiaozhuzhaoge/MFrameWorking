using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 offset;
    public Transform target;
    public float followSpeed;
    public bool isFollow;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void LateUpdate () {
        if (isFollow) {
            transform.position =  target.position + offset;
        }
	}
}
