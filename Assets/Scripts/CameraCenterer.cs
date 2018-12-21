using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraCenterer : MonoBehaviour {

    public Tilemap tilemap;

	// Use this for initialization
	void Start () {
        tilemap.CompressBounds();
        gameObject.transform.position = tilemap.localBounds.center + new Vector3(0,0,-20);
        gameObject.GetComponent<Camera>().orthographicSize = 6;
    }
}
