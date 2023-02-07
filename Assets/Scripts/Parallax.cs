using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;

    }

    void Start () {
        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];
        

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;

        }
	}
	
	
	void Update () {

        for (int i = 0; i< backgrounds.Length; i++)
        {
            float parallaxX = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float parallaxY = (previousCamPos.y - cam.position.y) * parallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallaxX;
            float backgroundTargetPosY = backgrounds[i].position.y;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
	}
}
