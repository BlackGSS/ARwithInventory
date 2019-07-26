using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
	void Update()
	{
		Vector3 Target = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
		transform.LookAt(Target);
		transform.Rotate(Vector3.up, 180);
	}
}
