using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableTracking : MonoBehaviour {
    private Camera cam;

    public Vector3 HeadRotation_mocap;
    public Vector3 HeadRotation_imu;

    public float VRreset;
    public float Adjustment = 0;
    // private Vector3 startPos;
    //private Vector3 
    // Use this for initialization
    void Start () {
        cam = GetComponentInChildren <Camera>();
        //startPos = transform.localPosition;

        //HeadRotation = (Vector3)GameObject.Find("SceneControls").GetComponent<QualisysRealTime.Unity.Infinite_World_LR_Game>().HeadRotation_qtm;
        VRreset = GameObject.Find("SceneControls").GetComponent<QualisysRealTime.Unity.Infinite_World_LR_Game>().VRreset;
            //QualisysRealTime.Unity.Infinite_World_LR_Game>().HeadRotation_qtm;
            //    Infinite_World_LR.GetComponent<HeadRotation_qtm>;

    }

    // Update is called once per frame
    void Update() {
        //transform.rotation = Quaternion.Inverse(cam.transform.localRotation);

        //transform.localEulerAngles = HeadRotation;
        HeadRotation_mocap = transform.eulerAngles;
        HeadRotation_imu = cam.transform.eulerAngles;

        if(VRreset == 1)
        {
            Adjustment = Adjustment + 5;
            HeadRotation_mocap.y = HeadRotation_mocap.y + Adjustment;
        }


        transform.eulerAngles = HeadRotation_mocap;
    }
}
