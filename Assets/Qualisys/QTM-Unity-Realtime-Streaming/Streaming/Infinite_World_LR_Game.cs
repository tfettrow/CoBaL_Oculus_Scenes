using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using UnityEngine.UI;


// Options
// Depth of wall
// Walls Present (TopBottom and LeftRight)
// Length of Walls (TopBottom and LeftRight)
// 
namespace QualisysRealTime.Unity
{
	public class Infinite_World_LR_Game: MonoBehaviour{
		// Initialize udp variables
		private List<LabeledMarker> markerData;
		// qtm demo test
		// private string HeadMarker = "L-frame - 1";

        // from mocap marker set
        private string HeadMarker1 = "LFHD";
        private string HeadMarker2 = "RFHD";
        private string LHeelMarker = "LHEEL";
        private string RHeelMarker = "RHEEL";
        
		// setup some qtm sdk variables
		private RTClient rtClient;
		private GameObject markerRoot;
		private List<GameObject> markers;

		// variables assigned from qtm sdk read
        private Vector3 HeadPosition_qtm;
        private float HeadPosition1_qtm_x;
        private float HeadPosition1_qtm_y;
        private float HeadPosition1_qtm_z;
        private float HeadPosition2_qtm_x;
        private float HeadPosition2_qtm_y;
        private float HeadPosition2_qtm_z;

        private float lheel_pos_labview_x;
        private float rheel_pos_labview_x;
        private float lheel_pos_qtm_x;
        private float rheel_pos_qtm_x;

		// // labview and head translation values                         
	    public GameObject PlayerPerspective;
        private Vector3 HeadPosition_labview;
        private float HeadPosition_labview_x;
        private float HeadPosition_labview_y;
        private float HeadPosition_labview_z;

       

        // // // // // // // INITIALIZE VARIABLES // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
        //public Transform OmnityDomeTransform;
        // Number of Cubes in Scene
        private int num_Objects = 500;
		// for plane ground
		//private int num_walkway_planes = 9;
	    public Renderer main_floor;

		private GameObject left_walkway1;
		private GameObject left_walkway2;
		private GameObject left_walkway3;
		private GameObject left_walkway4;
		private GameObject left_walkway5;
		private GameObject left_walkway6;
		private GameObject left_walkway7;
		private GameObject left_walkway8;
		private GameObject left_walkway9;
		private GameObject left_walkway10;
		private GameObject left_walkway11;
		private GameObject left_walkway12;
		private GameObject left_walkway13;
		private GameObject left_walkway14;
		private GameObject left_walkway15;
		private GameObject left_walkway16;
		private GameObject left_walkway17;
		private GameObject left_walkway18;

		private GameObject right_walkway1;
		private GameObject right_walkway2;
		private GameObject right_walkway3;
		private GameObject right_walkway4;
		private GameObject right_walkway5;
		private GameObject right_walkway6;
		private GameObject right_walkway7;
		private GameObject right_walkway8;
		private GameObject right_walkway9;
		private GameObject right_walkway10;
		private GameObject right_walkway11;
		private GameObject right_walkway12;
		private GameObject right_walkway13;
		private GameObject right_walkway14;
		private GameObject right_walkway15;
		private GameObject right_walkway16;
		private GameObject right_walkway17;
		private GameObject right_walkway18;

		private GameObject center_walkway1;
		private GameObject center_walkway2;
		private GameObject center_walkway3;
		private GameObject center_walkway4;
		private GameObject center_walkway5;
		private GameObject center_walkway6;
		private GameObject center_walkway7;
		private GameObject center_walkway8;
		private GameObject center_walkway9;
		private GameObject center_walkway10;
		private GameObject center_walkway11;
		private GameObject center_walkway12;
		private GameObject center_walkway13;
		private GameObject center_walkway14;
		private GameObject center_walkway15;
		private GameObject center_walkway16;
		private GameObject center_walkway17;
		private GameObject center_walkway18;

		private Vector3 left_walkway1_position;
		private Vector3 left_walkway2_position;
		private Vector3 left_walkway3_position;
		private Vector3 left_walkway4_position;
		private Vector3 left_walkway5_position;
		private Vector3 left_walkway6_position;
		private Vector3 left_walkway7_position;
		private Vector3 left_walkway8_position;
		private Vector3 left_walkway9_position;
		private Vector3 left_walkway10_position;
		private Vector3 left_walkway11_position;
		private Vector3 left_walkway12_position;
		private Vector3 left_walkway13_position;
		private Vector3 left_walkway14_position;
		private Vector3 left_walkway15_position;
		private Vector3 left_walkway16_position;
		private Vector3 left_walkway17_position;
		private Vector3 left_walkway18_position;


		private Vector3 right_walkway1_position;
		private Vector3 right_walkway2_position;
		private Vector3 right_walkway3_position;
		private Vector3 right_walkway4_position;
		private Vector3 right_walkway5_position;
		private Vector3 right_walkway6_position;
		private Vector3 right_walkway7_position;
		private Vector3 right_walkway8_position;
		private Vector3 right_walkway9_position;
		private Vector3 right_walkway10_position;
		private Vector3 right_walkway11_position;
		private Vector3 right_walkway12_position;
		private Vector3 right_walkway13_position;
		private Vector3 right_walkway14_position;
		private Vector3 right_walkway15_position;
		private Vector3 right_walkway16_position;
		private Vector3 right_walkway17_position;
		private Vector3 right_walkway18_position;

		private Vector3 center_walkway1_position;
		private Vector3 center_walkway2_position;
		private Vector3 center_walkway3_position;
		private Vector3 center_walkway4_position;
		private Vector3 center_walkway5_position;
		private Vector3 center_walkway6_position;
		private Vector3 center_walkway7_position;
		private Vector3 center_walkway8_position;
		private Vector3 center_walkway9_position;
		private Vector3 center_walkway10_position;
		private Vector3 center_walkway11_position;
		private Vector3 center_walkway12_position;
		private Vector3 center_walkway13_position;
		private Vector3 center_walkway14_position;
		private Vector3 center_walkway15_position;
		private Vector3 center_walkway16_position;
		private Vector3 center_walkway17_position;
		private Vector3 center_walkway18_position;

		private Vector3 left_walkway1_localPosition;
        private Vector3 left_walkway2_localPosition;
        private Vector3 left_walkway3_localPosition;
        private Vector3 left_walkway4_localPosition;
        private Vector3 left_walkway5_localPosition;
        private Vector3 left_walkway6_localPosition;
        private Vector3 left_walkway7_localPosition;
        private Vector3 left_walkway8_localPosition;
        private Vector3 left_walkway9_localPosition;
		private Vector3 left_walkway10_localPosition;
		private Vector3 left_walkway11_localPosition;
		private Vector3 left_walkway12_localPosition;
		private Vector3 left_walkway13_localPosition;
		private Vector3 left_walkway14_localPosition;
		private Vector3 left_walkway15_localPosition;
		private Vector3 left_walkway16_localPosition;
		private Vector3 left_walkway17_localPosition;
		private Vector3 left_walkway18_localPosition;

        private Vector3 right_walkway1_localPosition;
        private Vector3 right_walkway2_localPosition;
        private Vector3 right_walkway3_localPosition;
        private Vector3 right_walkway4_localPosition;
        private Vector3 right_walkway5_localPosition;
        private Vector3 right_walkway6_localPosition;
        private Vector3 right_walkway7_localPosition;
        private Vector3 right_walkway8_localPosition;
        private Vector3 right_walkway9_localPosition;
		private Vector3 right_walkway10_localPosition;
		private Vector3 right_walkway11_localPosition;
		private Vector3 right_walkway12_localPosition;
		private Vector3 right_walkway13_localPosition;
		private Vector3 right_walkway14_localPosition;
		private Vector3 right_walkway15_localPosition;
		private Vector3 right_walkway16_localPosition;
		private Vector3 right_walkway17_localPosition;
		private Vector3 right_walkway18_localPosition;

        private Vector3 center_walkway1_localPosition;
        private Vector3 center_walkway2_localPosition;
        private Vector3 center_walkway3_localPosition;
        private Vector3 center_walkway4_localPosition;
        private Vector3 center_walkway5_localPosition;
        private Vector3 center_walkway6_localPosition;
        private Vector3 center_walkway7_localPosition;
        private Vector3 center_walkway8_localPosition;
        private Vector3 center_walkway9_localPosition;
		private Vector3 center_walkway10_localPosition;
		private Vector3 center_walkway11_localPosition;
		private Vector3 center_walkway12_localPosition;
		private Vector3 center_walkway13_localPosition;
		private Vector3 center_walkway14_localPosition;
		private Vector3 center_walkway15_localPosition;
		private Vector3 center_walkway16_localPosition;
		private Vector3 center_walkway17_localPosition;
		private Vector3 center_walkway18_localPosition;

        private Vector2 TextureOffset;
		private Vector2 TextureOffset_left;
		private Vector2 TextureOffset_right;

        private float row_doubles;
        private float LaneLengths;
        private float LaneColors;
        
        // Should hard code this once determine appropriate values...
        private float Distance = 125f;
	    private float LeftRightSideLength = 10f;
		private float TopWallLength = 4f;
	    private float DepthofWall = 15f;
		private float HalfTopWall;
	    private float HalfSideWall;
		// Receiving Thread
		Thread receiveThread;
		UdpClient client;
		// Define all game objects
		public GameObject SquarePrefab;
		//public GameObject CameraObject;
		public GameObject OrigCubeObject;
        public GameObject OrigPlaneObject;
		public GameObject SceneObject;

		// Variables being read from UDP
		//UDP route//
		[Header("QTM")]
		public bool QTM;
		private bool streaming = false;

		private float num_of_UDP_vals;

		private float Stim_Zone;

		private float dt;

		// Treadmill//
		private float beltSpeed;
		private float ground_translation_y;
		private float falling_rotation_y_labview;
		private float ground_translation_y_labview;
		private float ground_translation_z_unity;

		private Vector3 current_texture_offset;
		public Vector3 current_object_position;

        private float falling_position_z;
		private float falling_position_x;
		private float falling_rotation_z_unity;
	
		public string text = ""; //received text
		public string IP = "192.168.253.2";
		public int port; // Defined on "init 
		public string lastReceivedUDPPacket = "";
		// Variables used for updating object positions in unity environment
		Vector3 CameraPosition;
		Vector3 FallingRotation;
		Vector3 OrigCubeObjectPosition;
        Vector3 OrigPlaneObjectPosition;

		Vector3 this_pyramid_position;
		Vector3 DomePosition;
		Vector3 ChildObjectPlacement;

        public void Start()
        {
            init();
            rtClient = RTClient.GetInstance();
            markers = new List<GameObject>();
            markerRoot = gameObject;
            // Find the game ojbects
            OrigCubeObject = GameObject.Find("OrigCube");
            OrigPlaneObject = GameObject.Find("OrigPlane");
            ChildObjectPlacement = GameObject.Find("Object Placement Origin").transform.position;
            SceneObject = GameObject.Find("SceneOrigin");
            PlayerPerspective = GameObject.Find("PlayerPerspective");

            // Assign walkways
            left_walkway1 = GameObject.Find("left_walkway1");
            left_walkway2 = GameObject.Find("left_walkway2");
            left_walkway3 = GameObject.Find("left_walkway3");
            left_walkway4 = GameObject.Find("left_walkway4");
            left_walkway5 = GameObject.Find("left_walkway5");
            left_walkway6 = GameObject.Find("left_walkway6");
            left_walkway7 = GameObject.Find("left_walkway7");
            left_walkway8 = GameObject.Find("left_walkway8");
            left_walkway9 = GameObject.Find("left_walkway9");
			left_walkway10 = GameObject.Find("left_walkway10");
//			left_walkway11 = GameObject.Find("left_walkway11");
//			left_walkway12 = GameObject.Find("left_walkway12");
//			left_walkway13 = GameObject.Find("left_walkway13");
//			left_walkway14 = GameObject.Find("left_walkway14");
//			left_walkway15 = GameObject.Find("left_walkway15");
//			left_walkway16 = GameObject.Find("left_walkway16");
//			left_walkway17 = GameObject.Find("left_walkway17");
//			left_walkway18 = GameObject.Find("left_walkway18");

            center_walkway1 = GameObject.Find("center_walkway1");
            center_walkway2 = GameObject.Find("center_walkway2");
            center_walkway3 = GameObject.Find("center_walkway3");
            center_walkway4 = GameObject.Find("center_walkway4");
            center_walkway5 = GameObject.Find("center_walkway5");
            center_walkway6 = GameObject.Find("center_walkway6");
            center_walkway7 = GameObject.Find("center_walkway7");
            center_walkway8 = GameObject.Find("center_walkway8");
            center_walkway9 = GameObject.Find("center_walkway9");
			center_walkway10 = GameObject.Find("center_walkway10");
//			center_walkway11 = GameObject.Find("center_walkway11");
//			center_walkway12 = GameObject.Find("center_walkway12");
//			center_walkway13 = GameObject.Find("center_walkway13");
//			center_walkway14 = GameObject.Find("center_walkway14");
//			center_walkway15 = GameObject.Find("center_walkway15");
//			center_walkway16 = GameObject.Find("center_walkway16");
//			center_walkway17 = GameObject.Find("center_walkway17");
//			center_walkway18 = GameObject.Find("center_walkway18");

            right_walkway1 = GameObject.Find("right_walkway1");
            right_walkway2 = GameObject.Find("right_walkway2");
            right_walkway3 = GameObject.Find("right_walkway3");
            right_walkway4 = GameObject.Find("right_walkway4");
            right_walkway5 = GameObject.Find("right_walkway5");
            right_walkway6 = GameObject.Find("right_walkway6");
            right_walkway7 = GameObject.Find("right_walkway7");
            right_walkway8 = GameObject.Find("right_walkway8");
            right_walkway9 = GameObject.Find("right_walkway9");
			right_walkway10 = GameObject.Find("right_walkway10");
//			right_walkway11 = GameObject.Find("right_walkway11");
//			right_walkway12 = GameObject.Find("right_walkway12");
//			right_walkway13 = GameObject.Find("right_walkway13");
//			right_walkway14 = GameObject.Find("right_walkway14");
//			right_walkway15 = GameObject.Find("right_walkway15");
//			right_walkway16 = GameObject.Find("right_walkway16");
//			right_walkway17 = GameObject.Find("right_walkway17");
//			right_walkway18 = GameObject.Find("right_walkway18");
            // // // Load the VR Randomization CSV // // // //
            //PlayerPerspective = GameObject.Find("PlayerPerspective");
            TextAsset PlaneInfo = Resources.Load<TextAsset>("ObjectInfo_unity");
            string[] PlaneInfo_string = PlaneInfo.text.Split(new char[] { '\n' });
            // Debug.Log(PlaneInfo_string.Length);

           for (int i = 0; i < PlaneInfo_string.Length - 1; i ++)
           {
               String[] row_string = PlaneInfo_string[i].Split(new char[] {','});
                //Debug.Log(row_string.Length);
                for (int i2 = 0; i2 < row_string.Length; i2 ++)
                {
                    float.TryParse(row_string[i2], out row_doubles);
                    if (i == 0)
                    {
                        LaneLengths = row_doubles;
						if (i2 == 0)
						{
							left_walkway1.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							left_walkway1.transform.localPosition = new Vector3(-10f, .01f, 25f + LaneLengths/2);

							center_walkway1.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							center_walkway1.transform.localPosition = new Vector3(0f, .01f, 25f + LaneLengths/2);

							right_walkway1.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							right_walkway1.transform.localPosition = new Vector3(10f, .01f, 25f + LaneLengths/2);
						}
						if (i2 == 1)
						{
							left_walkway2.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							left_walkway2.transform.localPosition = new Vector3(-10f, .01f, left_walkway1.transform.localPosition.z + (left_walkway1.transform.localScale.z*10)/2 + LaneLengths/2);

							center_walkway2.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							center_walkway2.transform.localPosition = new Vector3(0f, .01f, center_walkway1.transform.localPosition.z + (center_walkway1.transform.localScale.z*10)/2 + LaneLengths/2);

							right_walkway2.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							right_walkway2.transform.localPosition = new Vector3(10f, .01f, right_walkway1.transform.localPosition.z + (right_walkway1.transform.localScale.z*10)/2 + LaneLengths/2);
						}
						if (i2 == 2)
						{
							left_walkway3.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							left_walkway3.transform.localPosition = new Vector3(-10f, .01f, left_walkway2.transform.localPosition.z + (left_walkway2.transform.localScale.z*10)/2 + LaneLengths/2);

							center_walkway3.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							center_walkway3.transform.localPosition = new Vector3(0f, .01f, center_walkway2.transform.localPosition.z + (center_walkway2.transform.localScale.z*10)/2 + LaneLengths/2);

							right_walkway3.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							right_walkway3.transform.localPosition = new Vector3(10f, .01f, right_walkway2.transform.localPosition.z + (right_walkway2.transform.localScale.z*10)/2 + LaneLengths/2);
						}
						if (i2 == 3)
						{
							left_walkway4.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							left_walkway4.transform.localPosition = new Vector3(-10f, .01f, left_walkway3.transform.localPosition.z + (left_walkway3.transform.localScale.z*10)/2 + LaneLengths/2);

							center_walkway4.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							center_walkway4.transform.localPosition = new Vector3(0f, .01f, center_walkway3.transform.localPosition.z + (center_walkway3.transform.localScale.z*10)/2 + LaneLengths/2);

							right_walkway4.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							right_walkway4.transform.localPosition = new Vector3(10f, .01f, right_walkway3.transform.localPosition.z + (right_walkway3.transform.localScale.z*10)/2 + LaneLengths/2);
						}
						if (i2 == 4)
						{
							left_walkway5.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							left_walkway5.transform.localPosition = new Vector3(-10f, .01f, left_walkway4.transform.localPosition.z + (left_walkway4.transform.localScale.z*10)/2 + LaneLengths/2);

							center_walkway5.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							center_walkway5.transform.localPosition = new Vector3(0f, .01f, center_walkway4.transform.localPosition.z + (center_walkway4.transform.localScale.z*10)/2 + LaneLengths/2);

							right_walkway5.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							right_walkway5.transform.localPosition = new Vector3(10f, .01f, right_walkway4.transform.localPosition.z + (right_walkway4.transform.localScale.z*10)/2 + LaneLengths/2);
						}
						if (i2 == 5)
						{
							left_walkway6.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							left_walkway6.transform.localPosition = new Vector3(-10f, .01f, left_walkway5.transform.localPosition.z + (left_walkway5.transform.localScale.z*10)/2 + LaneLengths/2);

							center_walkway6.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							center_walkway6.transform.localPosition = new Vector3(0f, .01f, center_walkway5.transform.localPosition.z + (center_walkway5.transform.localScale.z*10)/2 + LaneLengths/2);

							right_walkway6.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							right_walkway6.transform.localPosition = new Vector3(10f, .01f, right_walkway5.transform.localPosition.z + (right_walkway5.transform.localScale.z*10)/2 + LaneLengths/2);
						}
						if (i2 == 6)
						{
							left_walkway7.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							left_walkway7.transform.localPosition = new Vector3(-10f, .01f, left_walkway6.transform.localPosition.z + (left_walkway6.transform.localScale.z*10)/2 + LaneLengths/2);

							center_walkway7.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							center_walkway7.transform.localPosition = new Vector3(0f, .01f, center_walkway6.transform.localPosition.z + (center_walkway6.transform.localScale.z*10)/2 + LaneLengths/2);

							right_walkway7.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							right_walkway7.transform.localPosition = new Vector3(10f, .01f, right_walkway6.transform.localPosition.z + (right_walkway6.transform.localScale.z*10)/2 + LaneLengths/2);
						}
						if (i2 == 7)
						{
							left_walkway8.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							left_walkway8.transform.localPosition = new Vector3(-10f, .01f, left_walkway7.transform.localPosition.z + (left_walkway7.transform.localScale.z*10)/2 + LaneLengths/2);

							center_walkway8.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							center_walkway8.transform.localPosition = new Vector3(0f, .01f, center_walkway7.transform.localPosition.z + (center_walkway7.transform.localScale.z*10)/2 + LaneLengths/2);

							right_walkway8.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							right_walkway8.transform.localPosition = new Vector3(10f, .01f, right_walkway7.transform.localPosition.z + (right_walkway7.transform.localScale.z*10)/2 + LaneLengths/2);
						}
						if (i2 == 8)
						{
							left_walkway9.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							left_walkway9.transform.localPosition = new Vector3(-10f, .01f, left_walkway8.transform.localPosition.z + (left_walkway8.transform.localScale.z*10)/2 + LaneLengths/2);

							center_walkway9.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							center_walkway9.transform.localPosition = new Vector3(0f, .01f, center_walkway8.transform.localPosition.z + (center_walkway8.transform.localScale.z*10)/2 + LaneLengths/2);

							right_walkway9.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							right_walkway9.transform.localPosition = new Vector3(10f, .01f, right_walkway8.transform.localPosition.z + (right_walkway8.transform.localScale.z*10)/2 + LaneLengths/2);
						}
						if (i2 == 9)
						{
							left_walkway10.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							left_walkway10.transform.localPosition = new Vector3(-10f, .01f, left_walkway9.transform.localPosition.z + (left_walkway9.transform.localScale.z*10)/2 + LaneLengths/2);

							center_walkway10.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							center_walkway10.transform.localPosition = new Vector3(0f, .01f, center_walkway9.transform.localPosition.z + (center_walkway9.transform.localScale.z*10)/2 + LaneLengths/2);

							right_walkway10.transform.localScale = new Vector3(1, 1, LaneLengths / 10);
							right_walkway10.transform.localPosition = new Vector3(10f, .01f, right_walkway9.transform.localPosition.z + (right_walkway9.transform.localScale.z*10)/2 + LaneLengths/2);
						}

                    }
                    if (i == 1)
                    {
                         LaneColors = row_doubles;
                         //Debug.Log(LeftLaneColors);
						if (i2 == 0) 
						{
							if (LaneColors == 0) {
								left_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
								right_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
							}
							if (LaneColors == 1) {
								left_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
								right_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
							}
							if (LaneColors == 2) {
								left_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
								right_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
							}
							center_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
						}
						if (i2 == 1) 
						{
							if (LaneColors == 0) {
								left_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
								right_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
							}
							if (LaneColors == 1) {
								left_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
								right_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
							}
							if (LaneColors == 2) {
								left_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
								right_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
							}
							center_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
						}
						if (i2 == 2) 
						{
							if (LaneColors == 0) {
								left_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
								right_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
							}
							if (LaneColors == 1) {
								left_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
								right_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
							}
							if (LaneColors == 2) {
								left_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
								right_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
							}
							center_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
						}
						if (i2 == 3) 
						{
							if (LaneColors == 0) {
								left_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
								right_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
							}
							if (LaneColors == 1) {
								left_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
								right_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
							}
							if (LaneColors == 2) {
								left_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
								right_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
							}
							center_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
						}
						if (i2 == 4) 
						{
							if (LaneColors == 0) {
								left_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
								right_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
							}
							if (LaneColors == 1) {
								left_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
								right_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
							}
							if (LaneColors == 2) {
								left_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
								right_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
							}
							center_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
						}
						if (i2 == 5) 
						{
							if (LaneColors == 0) {
								left_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
								right_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
							}
							if (LaneColors == 1) {
								left_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
								right_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
							}
							if (LaneColors == 2) {
								left_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
								right_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
							}
							center_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
						}
						if (i2 == 6) 
						{
							if (LaneColors == 0) {
								left_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
								right_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
							}
							if (LaneColors == 1) {
								left_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
								right_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
							}
							if (LaneColors == 2) {
								left_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
								right_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
							}
							center_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
						}
						if (i2 == 7) 
						{
							if (LaneColors == 0) {
								left_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
								right_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
							}
							if (LaneColors == 1) {
								left_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
								right_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
							}
							if (LaneColors == 2) {
								left_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
								right_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
							}
							center_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
						}
						if (i2 == 8) 
						{
							if (LaneColors == 0) {
								left_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
								right_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
							}
							if (LaneColors == 1) {
								left_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
								right_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
							}
							if (LaneColors == 2) {
								left_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
								right_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
							}
							center_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
						}
						if (i2 == 9) 
						{
							if (LaneColors == 0) {
								left_walkway10.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
								right_walkway10.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
							}
							if (LaneColors == 1) {
								left_walkway10.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
								right_walkway10.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
							}
							if (LaneColors == 2) {
								left_walkway10.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
								right_walkway10.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
							}
							center_walkway10.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
						}
                    }
                }
            }
            // // // // // // // // // // // // // // // // // // // // // // // //	

            // // // // // INSTANTIATE SCENE OBJECTS // // // // // // // // // // // // // // // // // // // // //// // // // // // // // // // // // // //// // // // // // // // // // // // // // 
            HalfSideWall = .5f * LeftRightSideLength;
            HalfTopWall = .5f * TopWallLength;
            // Right Wall
            for (int i = 0; i < num_Objects / 2; i++)
            {
                this_pyramid_position.x = (ChildObjectPlacement.x + UnityEngine.Random.Range(HalfTopWall, HalfTopWall + DepthofWall));
                this_pyramid_position.y = 2 * Math.Abs(UnityEngine.Random.Range(-HalfSideWall, HalfSideWall));
                this_pyramid_position.z = (UnityEngine.Random.Range(ChildObjectPlacement.z, Distance));

                GameObject clone = Instantiate(OrigCubeObject, this_pyramid_position, transform.rotation * Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f))) as GameObject;

            }
            // Left Wall
            for (int i = num_Objects / 2; i < num_Objects; i++)
            {
                this_pyramid_position.x = (ChildObjectPlacement.x - UnityEngine.Random.Range(HalfTopWall, HalfTopWall + DepthofWall));
                this_pyramid_position.y = 2 * Math.Abs(UnityEngine.Random.Range(-HalfSideWall, HalfSideWall));
                this_pyramid_position.z = (UnityEngine.Random.Range(ChildObjectPlacement.z, Distance));

                GameObject clone = Instantiate(SquarePrefab, this_pyramid_position, transform.rotation * Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f))) as GameObject;
            }
        }


       // public void AddScore(int newScoreValue)
       // {
       ////     score = +newScoreValue;
       //     UpdateScore();
      ///  }

      //  void UpdateScore()
      //  {
      //      scoreText.text = "SCORE:" + (int)score;
      //  }

        private void InitiateMarkers()
        {
            foreach (var marker in markers)
            {
                Destroy(marker);
            }

            markers.Clear();
            markerData = rtClient.Markers;

           // for (int i = 0; i < markerData.Count; i++)
           // {
           //     GameObject newMarker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
           //     newMarker.name = markerData[i].Label;
           //     //newMarker.transform.parent = markerRoot.transform;
           //     //newMarker.transform.localScale = Vector3.one * markerScale;
           //     //newMarker.SetActive(false);
           //     markers.Add(newMarker);
           // }
        }

        // UDP Start //
        private void init()
		{
			// ----------------------------
			// Listener
			// ----------------------------
			port = 6843;
			receiveThread = new Thread(new ThreadStart(ReceiveData));
			receiveThread.IsBackground = true;
			receiveThread.Start();
		}

		// // // // // // // // RECEIVE DATA FUNCTION for UDP SETUP // // // // // // // //// // // // // // // //
		private void ReceiveData(){
			client = new UdpClient(port);
			while (true){
				try{
					IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
					byte[] data = client.Receive(ref anyIP);			
					text = Encoding.UTF8.GetString(data);				
					lastReceivedUDPPacket = text;
				}
				catch (Exception err){
					print(err.ToString());
				}
			}
		}
		/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void Update(){
			// // // // // // // // GRAB OMNITY OBJECTS // // // // // // // //// // // // // // // //// // // // // // // //
	        // Figure out why this isn't grabbing during Start!!!
	        // Only runs if CameraObject is empty
			//init();
			// rtClient = RTClient.GetInstance();
			//markers = new List<GameObject>();
			//markerRoot = gameObject;

			//PlayerPerspective = GameObject.Find("PlayerPerspective");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (rtClient == null)
            {
                rtClient = RTClient.GetInstance();
            }
			if (rtClient.GetStreamingStatus() && !streaming)
			{
				InitiateMarkers();
				streaming = true;
			}
			if (!rtClient.GetStreamingStatus() && streaming)
			{
				streaming = false;
				InitiateMarkers();
			}

			markerData = rtClient.Markers;

			if (markerData == null && markerData.Count == 0)
				return;

			//if (markers.Count != markerData.Count)
			//{
			//	InitiateMarkers();
			//}
			for (int i = 0; i < markerData.Count; i++)
			{
				if (markerData[i].Position.magnitude > 0)
				{
					if (markerData[i].Label == HeadMarker1)
					{
						HeadPosition1_qtm_x = markerData[i].Position.z;
						HeadPosition1_qtm_y = markerData[i].Position.y;
						HeadPosition1_qtm_z = markerData[i].Position.x;
					}
                    if (markerData[i].Label == HeadMarker2)
                    {
                        HeadPosition2_qtm_x = markerData[i].Position.z;
                        HeadPosition2_qtm_y = markerData[i].Position.y;
                        HeadPosition2_qtm_z = markerData[i].Position.x;
                    }
                    HeadPosition_qtm.x = -(HeadPosition1_qtm_x + HeadPosition2_qtm_x) / 2;
                    HeadPosition_qtm.y = (HeadPosition1_qtm_y + HeadPosition2_qtm_y) / 2;
                    HeadPosition_qtm.z = (HeadPosition1_qtm_z + HeadPosition2_qtm_z) / 2;
                    //markers[i].name = markerData[i].Label;
                    //markers[i].GetComponent<Renderer>().material.color = markerData[i].Color;
                    //markers[i].transform.localPosition = markerData[i].Position;
                    // markers[i].SetActive(true);
                    // markers[i].GetComponent<Renderer>().enabled = visibleMarkers;
                    // markers[i].transform.localScale = Vector3.one * markerScale;
                }
			}
			// // // // // // // // READ LABVIEW UDP STRING // // // // // // // //// // // // // // // //// // // // // // // //// // 
			// reads string - splits the characters based on "," - assigns the characters to the variables based on the order within the string
			char[] delimiter1 = new char[] { ',' };
			var strvalues = lastReceivedUDPPacket.Split (delimiter1, StringSplitOptions.None);
			
			foreach (string word in strvalues) {
				num_of_UDP_vals++;
				if (num_of_UDP_vals == 1) {
					// float.TryParse (word, out camera_translation_x_labview);
					float.TryParse(word, out HeadPosition_labview_x);
					HeadPosition_labview.x = HeadPosition_labview_x / 1000;
				}
				if (num_of_UDP_vals == 2) {
					//float.TryParse (word, out camera_translation_y_labview);
					float.TryParse(word, out HeadPosition_labview_y);
					HeadPosition_labview.z = HeadPosition_labview_y / 1000;
				}
				if (num_of_UDP_vals == 3) {
					//float.TryParse (word, out camera_translation_z_labview);
					float.TryParse(word, out HeadPosition_labview_z);
					HeadPosition_labview.y = HeadPosition_labview_z / 1000;
				}
				if (num_of_UDP_vals == 4) {
					//float.TryParse (word, out ground_translation_y_labview);
					float.TryParse(word, out ground_translation_y_labview);
                    ground_translation_z_unity = ground_translation_y_labview;
                }
				if (num_of_UDP_vals == 5) {
					float.TryParse (word, out falling_rotation_y_labview);
					falling_rotation_z_unity = falling_rotation_y_labview;
				}
				if (num_of_UDP_vals == 6) {
					float.TryParse (word, out Stim_Zone);
				}
                if (num_of_UDP_vals == 7)
                {
                    float.TryParse(word, out lheel_pos_labview_x);
                }
                if (num_of_UDP_vals == 8)
                {
                    float.TryParse(word, out rheel_pos_labview_x);
                }
            }
			num_of_UDP_vals = 0;

			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// // // // // // MOVE THE SCENE OBJECTS // // // // // // // //// // // // // // // //// // // // // // // //// // // // // // // //
			// Moves the original/ parent object, thus moving all children objects in the scene assigned to the parent object
			//current_object_position = OrigCubeObject.transform.position;//- ground_translation_z_unity - 50f; // 50 = starting position Parent Object in scene
			OrigCubeObjectPosition[2] =  - (ground_translation_z_unity) - 50f;
			OrigCubeObject.transform.position = OrigCubeObjectPosition;
            OrigPlaneObject.transform.position = OrigCubeObjectPosition;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // // // // // // Move THE SCENE GROUND // // // // // // // //// // // // // // // //// // // // // // // //// // // // // // // 
            // Move the texture with speed of treadmill
            // need to implement beltspeed instead.. Are there problems with variable update with this?
            TextureOffset[1] = - ground_translation_z_unity/10f; // offset update based on scale of plane being used... find a way to automate..
            //TextureOffset_left[1] = -ground_translation_z_unity/2f;
            //TextureOffset_right[1] = -ground_translation_z_unity/2f;
            current_texture_offset = main_floor.material.GetTextureOffset("_MainTex");

			//dt = Time.deltaTime;


			//TextureOffset[1] = current_texture_offset[1] - (ground_translation_z_unity/ 4f);// offset update based on scale of plane being used... find a way to automate..


			main_floor.material.SetTextureOffset("_MainTex", TextureOffset); // Get rid of New Vector...

            left_walkway1_position = left_walkway1.transform.position;
            left_walkway2_position = left_walkway2.transform.position;
            left_walkway3_position = left_walkway3.transform.position;
            left_walkway4_position = left_walkway4.transform.position;
            left_walkway5_position = left_walkway5.transform.position;
            left_walkway6_position = left_walkway6.transform.position;
            left_walkway7_position = left_walkway7.transform.position;
            left_walkway8_position = left_walkway8.transform.position;
            left_walkway9_position = left_walkway9.transform.position;
			left_walkway10_position = left_walkway10.transform.position;
//			left_walkway11_position = left_walkway11.transform.position;
//			left_walkway12_position = left_walkway12.transform.position;
//			left_walkway13_position = left_walkway13.transform.position;
//			left_walkway14_position = left_walkway14.transform.position;
//			left_walkway15_position = left_walkway15.transform.position;
//			left_walkway16_position = left_walkway16.transform.position;
//			left_walkway17_position = left_walkway17.transform.position;
//			left_walkway18_position = left_walkway18.transform.position;


            right_walkway1_position = right_walkway1.transform.position;
            right_walkway2_position = right_walkway2.transform.position;
            right_walkway3_position = right_walkway3.transform.position;
            right_walkway4_position = right_walkway4.transform.position;
            right_walkway5_position = right_walkway5.transform.position;
            right_walkway6_position = right_walkway6.transform.position;
            right_walkway7_position = right_walkway7.transform.position;
            right_walkway8_position = right_walkway8.transform.position;
            right_walkway9_position = right_walkway9.transform.position;
			right_walkway10_position = right_walkway10.transform.position;
//			right_walkway11_position = right_walkway11.transform.position;
//			right_walkway12_position = right_walkway12.transform.position;
//			right_walkway13_position = right_walkway13.transform.position;
//			right_walkway14_position = right_walkway14.transform.position;
//			right_walkway15_position = right_walkway15.transform.position;
//			right_walkway16_position = right_walkway16.transform.position;
//			right_walkway17_position = right_walkway17.transform.position;
//			right_walkway18_position = right_walkway18.transform.position;


            center_walkway1_position = center_walkway1.transform.position;
            center_walkway2_position = center_walkway2.transform.position;
            center_walkway3_position = center_walkway3.transform.position;
            center_walkway4_position = center_walkway4.transform.position;
            center_walkway5_position = center_walkway5.transform.position;
            center_walkway6_position = center_walkway6.transform.position;
            center_walkway7_position = center_walkway7.transform.position;
            center_walkway8_position = center_walkway8.transform.position;
            center_walkway9_position = center_walkway9.transform.position;
		    center_walkway10_position = center_walkway10.transform.position;
//			center_walkway11_position = center_walkway11.transform.position;
//			center_walkway12_position = center_walkway12.transform.position;
//			center_walkway13_position = center_walkway13.transform.position;
//			center_walkway14_position = center_walkway14.transform.position;
//			center_walkway15_position = center_walkway15.transform.position;
//			center_walkway16_position = center_walkway16.transform.position;
//			center_walkway17_position = center_walkway17.transform.position;
//			center_walkway18_position = center_walkway18.transform.position;

			left_walkway1_localPosition = left_walkway1.transform.localPosition;
            left_walkway2_localPosition = left_walkway2.transform.localPosition;
            left_walkway3_localPosition = left_walkway3.transform.localPosition;
            left_walkway4_localPosition = left_walkway4.transform.localPosition;
            left_walkway5_localPosition = left_walkway5.transform.localPosition;
            left_walkway6_localPosition = left_walkway6.transform.localPosition;
            left_walkway7_localPosition = left_walkway7.transform.localPosition;
            left_walkway8_localPosition = left_walkway8.transform.localPosition;
            left_walkway9_localPosition = left_walkway9.transform.localPosition;
            left_walkway10_localPosition = left_walkway10.transform.localPosition;
//            left_walkway11_localPosition = left_walkway11.transform.localPosition;
//            left_walkway12_localPosition = left_walkway12.transform.localPosition;
//            left_walkway13_localPosition = left_walkway13.transform.localPosition;
//            left_walkway14_localPosition = left_walkway14.transform.localPosition;
//            left_walkway15_localPosition = left_walkway15.transform.localPosition;
//            left_walkway16_localPosition = left_walkway16.transform.localPosition;
//            left_walkway17_localPosition = left_walkway17.transform.localPosition;
//            left_walkway18_localPosition = left_walkway18.transform.localPosition;

            center_walkway1_localPosition = center_walkway1.transform.localPosition;
            center_walkway2_localPosition = center_walkway2.transform.localPosition;
            center_walkway3_localPosition = center_walkway3.transform.localPosition;
            center_walkway4_localPosition = center_walkway4.transform.localPosition;
            center_walkway5_localPosition = center_walkway5.transform.localPosition;
            center_walkway6_localPosition = center_walkway6.transform.localPosition;
            center_walkway7_localPosition = center_walkway7.transform.localPosition;
            center_walkway8_localPosition = center_walkway8.transform.localPosition;
            center_walkway9_localPosition = center_walkway9.transform.localPosition;
            center_walkway10_localPosition = center_walkway10.transform.localPosition;
//            center_walkway11_localPosition = center_walkway11.transform.localPosition;
//            center_walkway12_localPosition = center_walkway12.transform.localPosition;
//            center_walkway13_localPosition = center_walkway13.transform.localPosition;
//            center_walkway14_localPosition = center_walkway14.transform.localPosition;
//            center_walkway15_localPosition = center_walkway15.transform.localPosition;
//            center_walkway16_localPosition = center_walkway16.transform.localPosition;
//            center_walkway17_localPosition = center_walkway17.transform.localPosition;
//            center_walkway18_localPosition = center_walkway18.transform.localPosition;

            right_walkway1_localPosition = right_walkway1.transform.localPosition;
            right_walkway2_localPosition = right_walkway2.transform.localPosition;
            right_walkway3_localPosition = right_walkway3.transform.localPosition;
            right_walkway4_localPosition = right_walkway4.transform.localPosition;
            right_walkway5_localPosition = right_walkway5.transform.localPosition;
            right_walkway6_localPosition = right_walkway6.transform.localPosition;
            right_walkway7_localPosition = right_walkway7.transform.localPosition;
            right_walkway8_localPosition = right_walkway8.transform.localPosition;
            right_walkway9_localPosition = right_walkway9.transform.localPosition;
            right_walkway10_localPosition = right_walkway10.transform.localPosition;
//            right_walkway11_localPosition = right_walkway11.transform.localPosition;
//            right_walkway12_localPosition = right_walkway12.transform.localPosition;
//            right_walkway13_localPosition = right_walkway13.transform.localPosition;
//            right_walkway14_localPosition = right_walkway14.transform.localPosition;
//            right_walkway15_localPosition = right_walkway15.transform.localPosition;
//            right_walkway16_localPosition = right_walkway16.transform.localPosition;
//            right_walkway17_localPosition = right_walkway17.transform.localPosition;
//            right_walkway18_localPosition = right_walkway18.transform.localPosition;

            if (left_walkway1_position.z < -25f)
            {
				left_walkway1_localPosition.z = left_walkway10.transform.localPosition.z + left_walkway10.transform.localScale.z*10/2 + left_walkway1.transform.localScale.z *10/2;
				left_walkway1.transform.localPosition = left_walkway1_localPosition;
            //    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
            }
			if (left_walkway2_position.z < -25f)
			{
				left_walkway2_localPosition.z = left_walkway1.transform.localPosition.z + left_walkway1.transform.localScale.z*10/2 + left_walkway2.transform.localScale.z *10/2;
				left_walkway2.transform.localPosition = left_walkway2_localPosition;
				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (left_walkway3_position.z < -25f)
			{
				left_walkway3_localPosition.z = left_walkway2.transform.localPosition.z + left_walkway2.transform.localScale.z*10/2 + left_walkway3.transform.localScale.z *10/2;
				left_walkway3.transform.localPosition = left_walkway3_localPosition;
				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (left_walkway4_position.z < -25f)
			{
				left_walkway4_localPosition.z = left_walkway3.transform.localPosition.z + left_walkway3.transform.localScale.z*10/2 + left_walkway4.transform.localScale.z *10/2;
				left_walkway4.transform.localPosition = left_walkway4_localPosition;
				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (left_walkway5_position.z < -25f)
			{
				left_walkway5_localPosition.z = left_walkway4.transform.localPosition.z + left_walkway4.transform.localScale.z*10/2 + left_walkway5.transform.localScale.z *10/2;
				left_walkway5.transform.localPosition = left_walkway5_localPosition;
				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (left_walkway6_position.z < -25f)
			{
				left_walkway6_localPosition.z = left_walkway5.transform.localPosition.z + left_walkway5.transform.localScale.z*10/2 + left_walkway6.transform.localScale.z *10/2;
				left_walkway6.transform.localPosition = left_walkway6_localPosition;
				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (left_walkway7_position.z < -25f)
			{
				left_walkway7_localPosition.z = left_walkway6.transform.localPosition.z + left_walkway6.transform.localScale.z*10/2 + left_walkway7.transform.localScale.z *10/2;
				left_walkway7.transform.localPosition = left_walkway7_localPosition;
				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (left_walkway8_position.z < -25f)
			{
				left_walkway8_localPosition.z = left_walkway7.transform.localPosition.z + left_walkway7.transform.localScale.z*10/2 + left_walkway8.transform.localScale.z *10/2;
				left_walkway8.transform.localPosition = left_walkway8_localPosition;
				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}

			if (left_walkway9_position.z < -25f)
			{
				left_walkway9_localPosition.z = left_walkway8.transform.localPosition.z + left_walkway8.transform.localScale.z*10/2 + left_walkway9.transform.localScale.z *10/2;
				left_walkway9.transform.localPosition = left_walkway9_localPosition;
				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}

			if (left_walkway10_position.z < -25f)
			{
				left_walkway10_localPosition.z = left_walkway9.transform.localPosition.z + left_walkway9.transform.localScale.z*10/2 + left_walkway10.transform.localScale.z *10/2;
				left_walkway10.transform.localPosition = left_walkway10_localPosition;
				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
//	
//			if (left_walkway11_position.z < -25f)
//			{
//				left_walkway11_localPosition.z = left_walkway10.transform.localPosition.z + left_walkway10.transform.localScale.z*10/2 + left_walkway11.transform.localScale.z *10/2;
//				left_walkway11.transform.localPosition = left_walkway11_localPosition;
//				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (left_walkway12_position.z < -25f)
//			{
//				left_walkway12_localPosition.z = left_walkway11.transform.localPosition.z + left_walkway11.transform.localScale.z*10/2 + left_walkway12.transform.localScale.z *10/2;
//				left_walkway12.transform.localPosition = left_walkway12_localPosition;
//				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (left_walkway13_position.z < -25f)
//			{
//				left_walkway13_localPosition.z = left_walkway12.transform.localPosition.z + left_walkway12.transform.localScale.z*10/2 + left_walkway13.transform.localScale.z *10/2;
//				left_walkway13.transform.localPosition = left_walkway13_localPosition;
//				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (left_walkway14_position.z < -25f)
//			{
//				left_walkway14_localPosition.z = left_walkway13.transform.localPosition.z + left_walkway13.transform.localScale.z*10/2 + left_walkway14.transform.localScale.z *10/2;
//				left_walkway14.transform.localPosition = left_walkway14_localPosition;
//				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (left_walkway15_position.z < -25f)
//			{
//				left_walkway15_localPosition.z = left_walkway14.transform.localPosition.z + left_walkway14.transform.localScale.z*10/2 + left_walkway15.transform.localScale.z *10/2;
//				left_walkway15.transform.localPosition = left_walkway15_localPosition;
//				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (left_walkway16_position.z < -25f)
//			{
//				left_walkway16_localPosition.z = left_walkway15.transform.localPosition.z + left_walkway15.transform.localScale.z*10/2 + left_walkway16.transform.localScale.z *10/2;
//				left_walkway16.transform.localPosition = left_walkway16_localPosition;
//				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (left_walkway17_position.z < -25f)
//			{
//				left_walkway17_localPosition.z = left_walkway16.transform.localPosition.z + left_walkway16.transform.localScale.z*10/2 + left_walkway17.transform.localScale.z *10/2;
//				left_walkway17.transform.localPosition = left_walkway17_localPosition;
//				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (left_walkway18_position.z < -25f)
//			{
//				left_walkway18_localPosition.z = left_walkway17.transform.localPosition.z + left_walkway17.transform.localScale.z*10/2 + left_walkway18.transform.localScale.z *10/2;
//				left_walkway18.transform.localPosition = left_walkway18_localPosition;
//				//    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
				


			/// // // // // center lane /// // 
			if (center_walkway1_position.z < -25f)
			{
				center_walkway1_localPosition.z = center_walkway10.transform.localPosition.z + center_walkway10.transform.localScale.z*10/2 + center_walkway1.transform.localScale.z *10/2;
				center_walkway1.transform.localPosition = center_walkway1_localPosition;
				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (center_walkway2_position.z < -25f)
			{
				center_walkway2_localPosition.z = center_walkway1.transform.localPosition.z + center_walkway1.transform.localScale.z*10/2 + center_walkway2.transform.localScale.z *10/2;
				center_walkway2.transform.localPosition = center_walkway2_localPosition;
				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (center_walkway3_position.z < -25f)
			{
				center_walkway3_localPosition.z = center_walkway2.transform.localPosition.z + center_walkway2.transform.localScale.z*10/2 + center_walkway3.transform.localScale.z *10/2;
				center_walkway3.transform.localPosition = center_walkway3_localPosition;
				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (center_walkway4_position.z < -25f)
			{
				center_walkway4_localPosition.z = center_walkway3.transform.localPosition.z + center_walkway3.transform.localScale.z*10/2 + center_walkway4.transform.localScale.z *10/2;
				center_walkway4.transform.localPosition = center_walkway4_localPosition;
				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (center_walkway5_position.z < -25f)
			{
				center_walkway5_localPosition.z = center_walkway4.transform.localPosition.z + center_walkway4.transform.localScale.z*10/2 + center_walkway5.transform.localScale.z *10/2;
				center_walkway5.transform.localPosition = center_walkway5_localPosition;
				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (center_walkway6_position.z < -25f)
			{
				center_walkway6_localPosition.z = center_walkway5.transform.localPosition.z + center_walkway5.transform.localScale.z*10/2 + center_walkway6.transform.localScale.z *10/2;
				center_walkway6.transform.localPosition = center_walkway6_localPosition;
				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (center_walkway7_position.z < -25f)
			{
				center_walkway7_localPosition.z = center_walkway6.transform.localPosition.z + center_walkway6.transform.localScale.z*10/2 + center_walkway7.transform.localScale.z *10/2;
				center_walkway7.transform.localPosition = center_walkway7_localPosition;
				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (center_walkway8_position.z < -25f)
			{
				center_walkway8_localPosition.z = center_walkway7.transform.localPosition.z + center_walkway7.transform.localScale.z*10/2 + center_walkway8.transform.localScale.z *10/2;
				center_walkway8.transform.localPosition = center_walkway8_localPosition;
				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}

			if (center_walkway9_position.z < -25f)
			{
				center_walkway9_localPosition.z = center_walkway8.transform.localPosition.z + center_walkway8.transform.localScale.z*10/2 + center_walkway9.transform.localScale.z *10/2;
				center_walkway9.transform.localPosition = center_walkway9_localPosition;
				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}

			if (center_walkway10_position.z < -25f)
			{
				center_walkway10_localPosition.z = center_walkway9.transform.localPosition.z + center_walkway9.transform.localScale.z*10/2 + center_walkway10.transform.localScale.z *10/2;
				center_walkway10.transform.localPosition = center_walkway10_localPosition;
				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
//
//			if (center_walkway11_position.z < -25f)
//			{
//				center_walkway11_localPosition.z = center_walkway10.transform.localPosition.z + center_walkway10.transform.localScale.z*10/2 + center_walkway11.transform.localScale.z *10/2;
//				center_walkway11.transform.localPosition = center_walkway11_localPosition;
//				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//				
//			if (center_walkway12_position.z < -25f)
//			{
//				center_walkway12_localPosition.z = center_walkway11.transform.localPosition.z + center_walkway11.transform.localScale.z*10/2 + center_walkway12.transform.localScale.z *10/2;
//				center_walkway12.transform.localPosition = center_walkway12_localPosition;
//				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (center_walkway13_position.z < -25f)
//			{
//				center_walkway13_localPosition.z = center_walkway12.transform.localPosition.z + center_walkway12.transform.localScale.z*10/2 + center_walkway13.transform.localScale.z *10/2;
//				center_walkway13.transform.localPosition = center_walkway13_localPosition;
//				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (center_walkway14_position.z < -25f)
//			{
//				center_walkway14_localPosition.z = center_walkway13.transform.localPosition.z + center_walkway13.transform.localScale.z*10/2 + center_walkway14.transform.localScale.z *10/2;
//				center_walkway14.transform.localPosition = center_walkway14_localPosition;
//				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (center_walkway15_position.z < -25f)
//			{
//				center_walkway15_localPosition.z = center_walkway14.transform.localPosition.z + center_walkway14.transform.localScale.z*10/2 + center_walkway15.transform.localScale.z *10/2;
//				center_walkway15.transform.localPosition = center_walkway15_localPosition;
//				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (center_walkway16_position.z < -25f)
//			{
//				center_walkway16_localPosition.z = center_walkway15.transform.localPosition.z + center_walkway15.transform.localScale.z*10/2 + center_walkway16.transform.localScale.z *10/2;
//				center_walkway16.transform.localPosition = center_walkway16_localPosition;
//				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (center_walkway17_position.z < -25f)
//			{
//				center_walkway17_localPosition.z = center_walkway16.transform.localPosition.z + center_walkway16.transform.localScale.z*10/2 + center_walkway17.transform.localScale.z *10/2;
//				center_walkway17.transform.localPosition = center_walkway17_localPosition;
//				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (center_walkway18_position.z < -25f)
//			{
//				center_walkway18_localPosition.z = center_walkway17.transform.localPosition.z + center_walkway17.transform.localScale.z*10/2 + center_walkway18.transform.localScale.z *10/2;
//				center_walkway18.transform.localPosition = center_walkway18_localPosition;
//				//    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}


			// // // // // // right lane // // // // // // // 
			if (right_walkway1_position.z < -25f)
			{
				right_walkway1_localPosition.z = right_walkway10.transform.localPosition.z + right_walkway10.transform.localScale.z*10/2 + right_walkway1.transform.localScale.z *10/2;
				right_walkway1.transform.localPosition = right_walkway1_localPosition;
				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (right_walkway2_position.z < -25f)
			{
				right_walkway2_localPosition.z = right_walkway1.transform.localPosition.z + right_walkway1.transform.localScale.z*10/2 + right_walkway2.transform.localScale.z *10/2;
				right_walkway2.transform.localPosition = right_walkway2_localPosition;
				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (right_walkway3_position.z < -25f)
			{
				right_walkway3_localPosition.z = right_walkway2.transform.localPosition.z + right_walkway2.transform.localScale.z*10/2 + right_walkway3.transform.localScale.z *10/2;
				right_walkway3.transform.localPosition = right_walkway3_localPosition;
				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (right_walkway4_position.z < -25f)
			{
				right_walkway4_localPosition.z = right_walkway3.transform.localPosition.z + right_walkway3.transform.localScale.z*10/2 + right_walkway4.transform.localScale.z *10/2;
				right_walkway4.transform.localPosition = right_walkway4_localPosition;
				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (right_walkway5_position.z < -25f)
			{
				right_walkway5_localPosition.z = right_walkway4.transform.localPosition.z + right_walkway4.transform.localScale.z*10/2 + right_walkway5.transform.localScale.z *10/2;
				right_walkway5.transform.localPosition = right_walkway5_localPosition;
				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (right_walkway6_position.z < -25f)
			{
				right_walkway6_localPosition.z = right_walkway5.transform.localPosition.z + right_walkway5.transform.localScale.z*10/2 + right_walkway6.transform.localScale.z *10/2;
				right_walkway6.transform.localPosition = right_walkway6_localPosition;
				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (right_walkway7_position.z < -25f)
			{
				right_walkway7_localPosition.z = right_walkway6.transform.localPosition.z + right_walkway6.transform.localScale.z*10/2 + right_walkway7.transform.localScale.z *10/2;
				right_walkway7.transform.localPosition = right_walkway7_localPosition;
				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
			if (right_walkway8_position.z < -25f)
			{
				right_walkway8_localPosition.z = right_walkway7.transform.localPosition.z + right_walkway7.transform.localScale.z*10/2 + right_walkway8.transform.localScale.z *10/2;
				right_walkway8.transform.localPosition = right_walkway8_localPosition;
				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}

			if (right_walkway9_position.z < -25f)
			{
				right_walkway9_localPosition.z = right_walkway8.transform.localPosition.z + right_walkway8.transform.localScale.z*10/2 + right_walkway9.transform.localScale.z *10/2;
				right_walkway9.transform.localPosition = right_walkway9_localPosition;
				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}

			if (right_walkway10_position.z < -25f)
			{
				right_walkway10_localPosition.z = right_walkway9.transform.localPosition.z + right_walkway9.transform.localScale.z*10/2 + right_walkway10.transform.localScale.z *10/2;
				right_walkway10.transform.localPosition = right_walkway10_localPosition;
				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
			}
//
//			if (right_walkway11_position.z < -25f)
//			{
//				right_walkway11_localPosition.z = right_walkway10.transform.localPosition.z + right_walkway10.transform.localScale.z*10/2 + right_walkway11.transform.localScale.z *10/2;
//				right_walkway11.transform.localPosition = right_walkway11_localPosition;
//				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//
//			if (right_walkway12_position.z < -25f)
//			{
//				right_walkway12_localPosition.z = right_walkway11.transform.localPosition.z + right_walkway11.transform.localScale.z*10/2 + right_walkway12.transform.localScale.z *10/2;
//				right_walkway12.transform.localPosition = right_walkway12_localPosition;
//				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (right_walkway13_position.z < -25f)
//			{
//				right_walkway13_localPosition.z = right_walkway12.transform.localPosition.z + right_walkway12.transform.localScale.z*10/2 + right_walkway13.transform.localScale.z *10/2;
//				right_walkway13.transform.localPosition = right_walkway13_localPosition;
//				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (right_walkway14_position.z < -25f)
//			{
//				right_walkway14_localPosition.z = right_walkway13.transform.localPosition.z + right_walkway13.transform.localScale.z*10/2 + right_walkway14.transform.localScale.z *10/2;
//				right_walkway14.transform.localPosition = right_walkway14_localPosition;
//				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (right_walkway15_position.z < -25f)
//			{
//				right_walkway15_localPosition.z = right_walkway14.transform.localPosition.z + right_walkway14.transform.localScale.z*10/2 + right_walkway15.transform.localScale.z *10/2;
//				right_walkway15.transform.localPosition = right_walkway15_localPosition;
//				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (right_walkway16_position.z < -25f)
//			{
//				right_walkway16_localPosition.z = right_walkway15.transform.localPosition.z + right_walkway15.transform.localScale.z*10/2 + right_walkway16.transform.localScale.z *10/2;
//				right_walkway16.transform.localPosition = right_walkway16_localPosition;
//				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (right_walkway17_position.z < -25f)
//			{
//				right_walkway17_localPosition.z = right_walkway16.transform.localPosition.z + right_walkway16.transform.localScale.z*10/2 + right_walkway17.transform.localScale.z *10/2;
//				right_walkway17.transform.localPosition = right_walkway17_localPosition;
//				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//			if (right_walkway18_position.z < -25f)
//			{
//				right_walkway18_localPosition.z = right_walkway17.transform.localPosition.z + right_walkway17.transform.localScale.z*10/2 + right_walkway18.transform.localScale.z *10/2;
//				right_walkway18.transform.localPosition = right_walkway18_localPosition;
//				//    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
//			}
//				

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // // // // // // MOVE PERSPECTIVE CAMERA // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
            // Moves the "Render Chanel Camera" based on real world coordinates gathered from Nexus (motion capture)
            // update camera position based on labview or qtm data stream
            if (QTM == true)
			{
				PlayerPerspective.transform.position = HeadPosition_qtm;
			}
			if (QTM == false)
			{
				PlayerPerspective.transform.position = HeadPosition_labview;
			}
				
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// // // // // // ROTATE THE WORLD // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
			// rotate the world around SceneOrigin to allow for a proper perception of a fall
			FallingRotation.z = falling_rotation_z_unity;
			SceneObject.transform.localEulerAngles = FallingRotation;
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		}
		// Necessary in order to properly close udp connection
		void OnDisable(){
			if (receiveThread != null)
				receiveThread.Abort();
			client.Close();
		}
	}
}