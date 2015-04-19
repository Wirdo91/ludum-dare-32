using UnityEngine;
using System.Collections;

public class CamaraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;
	
	void Update ()
    {
        this.transform.position = target.position + (Vector3.back * 10);
	}
}
