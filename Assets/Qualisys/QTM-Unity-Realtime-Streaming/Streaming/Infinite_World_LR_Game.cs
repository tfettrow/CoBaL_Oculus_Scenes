using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.VR;
using System.Collections;


// Options
// Depth of wall
// Walls Present (TopBottom and LeftRight)
// Length of Walls (TopBottom and LeftRight)
// 
namespace QualisysRealTime.Unity
{
    public class Infinite_World_LR_Game : MonoBehaviour {
        // Initialize udp variables
        private List<LabeledMarker> markerData;
        private List<SixDOFBody> QTMrigidBodies;
        // qtm demo test
        // private string HeadMarker = "L-frame - 1";

        // from mocap marker set
        private string LFHeadMarker = "LFHD";
        private string RFHeadMarker = "RFHD";
        private string LBHeadMarker = "LBHD";
        private string RBHeadMarker = "RBHD";

        private string C7Marker = "C7";
        private string ClavicleMarker = "CLAV";

        private string LShoulderMarker = "LSHO";
        private string LUpperArmMarker = "LUPA";
        private string LElbowMarker = "LELB";
        private string LForearmMarker = "LFRA";
        private string LWristAntMarker = "LWRA";
        private string LWristPostMarker = "LWRB";

        private string RShoulderMarker = "RSHO";
        private string RUpperArmMarker = "RUPA";
        private string RElbowMarker = "RELB";
        private string RForearmMarker = "RFRA";
        private string RWristAntMarker = "RWRA";
        private string RWristPostMarker = "RWRB";

        private string LASIMarker = "LASI";
        private string RASIMarker = "RASI";
        private string LPSIMarker = "LPSI";
        private string RPSIMarker = "RPSI";

        private string LThighProxMarker = "LTHIP";
        private string LThighMarker = "LTHI";
        private string LThighDistMarker = "LTHID";
        private string LKneeMarker = "LKNE";
        private string LTibiaProxMarker = "LTIBP";
        private string LTibiaMarker = "LTIB";
        private string LTibiaDistMarker = "LTIBD";
        private string LAnkleMarker = "LANK";
        private string LHeelMarker = "LHEE";
        private string LToeMarker = "LTOE";
        private string LToeLatMarker = "LTOEL";

        private string RThighProxMarker = "RTHIP";
        private string RThighMarker = "RTHI";
        private string RThighDistMarker = "RTHID";
        private string RKneeMarker = "RKNE";
        private string RTibiaProxMarker = "RTIBP";
        private string RTibiaMarker = "RTIB";
        private string RTibiaDistMarker = "RTIBD";
        private string RAnkleMarker = "RANK";
        private string RHeelMarker = "RHEE";
        private string RToeMarker = "RTOE";
        private string RToeLatMarker = "RTOEL";


        private string OculusHeadBody = "Head";

        // setup some qtm sdk variables
        private RTClient rtClient;
        private GameObject markerRoot;
        private List<GameObject> markers;

        // variables assigned from qtm sdk read
        private Vector3 FHeadPosition_qtm;
        private Vector3 BHeadPosition_qtm;
        private Vector3 leftHeadPosition_qtm;
        private Vector3 rightHeadPosition_qtm;

        private Vector3 C7Position_qtm;
        private Vector3 ClaviclePosition_qtm;

        private Vector3 LShoulderPosition_qtm;
        private Vector3 LUpperArmPosition_qtm;
        private Vector3 LElbowPosition_qtm;
        private Vector3 LForearmPosition_qtm;
        private Vector3 LWristAntPosition_qtm;
        private Vector3 LWristPostPosition_qtm;

        private Vector3 RShoulderPosition_qtm;
        private Vector3 RUpperArmPosition_qtm;
        private Vector3 RElbowPosition_qtm;
        private Vector3 RForearmPosition_qtm;
        private Vector3 RWristAntPosition_qtm;
        private Vector3 RWristPostPosition_qtm;

        private Vector3 LASIPosition_qtm;
        private Vector3 RASIPosition_qtm;
        private Vector3 LPSIPosition_qtm;
        private Vector3 RPSIPosition_qtm;

        private Vector3 LThighProxPosition_qtm;
        private Vector3 LThighPosition_qtm;
        private Vector3 LThighDistPosition_qtm;
        private Vector3 LKneePosition_qtm;
        private Vector3 LTibiaProxPosition_qtm;
        private Vector3 LTibiaPosition_qtm;
        private Vector3 LTibiaDistPosition_qtm;
        private Vector3 LAnklePosition_qtm;
        private Vector3 LHeelPosition_qtm;
        private Vector3 LToePosition_qtm;
        private Vector3 LToeLatPosition_qtm;

        private Vector3 RThighProxPosition_qtm;
        private Vector3 RThighPosition_qtm;
        private Vector3 RThighDistPosition_qtm;
        private Vector3 RKneePosition_qtm;
        private Vector3 RTibiaProxPosition_qtm;
        private Vector3 RTibiaPosition_qtm;
        private Vector3 RTibiaDistPosition_qtm;
        private Vector3 RAnklePosition_qtm;
        private Vector3 RHeelPosition_qtm;
        private Vector3 RToePosition_qtm;
        private Vector3 RToeLatPosition_qtm;

        
        private Vector3 HeadPosition_qtm;

        private float LFHeadPosition_qtm_x;
        private float LFHeadPosition_qtm_y;
        private float LFHeadPosition_qtm_z;
        private float RFHeadPosition_qtm_x;
        private float RFHeadPosition_qtm_y;
        private float RFHeadPosition_qtm_z;
        private float LBHeadPosition_qtm_x;
        private float LBHeadPosition_qtm_y;
        private float LBHeadPosition_qtm_z;
        private float RBHeadPosition_qtm_x;
        private float RBHeadPosition_qtm_y;
        private float RBHeadPosition_qtm_z;

        //private float RHeelPosition_qtm_x;
       // private float RHeelPosition_qtm_y;
       // private float RHeelPosition_qtm_z;
       // private float LHeelPosition_qtm_x;
       // private float LHeelPosition_qtm_y;
       // private float LHeelPosition_qtm_z;

       // private float lheel_pos_labview_x;
        //private float rheel_pos_labview_x;
        //private float lheel_pos_qtm_x;
        //private float rheel_pos_qtm_x;
        //private float c7_pos_labview_x

        // // labview and head translation values                         
        public GameObject PlayerPerspective;
        private Vector3 HeadPosition_labview;
        private float HeadPosition_labview_x;
        private float HeadPosition_labview_y;
        private float HeadPosition_labview_z;

        // Markers As Game Objects
        private GameObject C7;
        private GameObject Clavicle;
        private GameObject LShoulder;
        private GameObject LUpperArm;
        private GameObject LElbow;
        private GameObject LForearm;
        private GameObject LWristAnt;
        private GameObject LWristPost;
        private GameObject RShoulder;
        private GameObject RUpperArm;
        private GameObject RElbow;
        private GameObject RForearm;
        private GameObject RWristAnt;
        private GameObject RWristPost;
        private GameObject LASI;
        private GameObject RASI;
        private GameObject LPSI;
        private GameObject RPSI;
        private GameObject LThighProx;
        private GameObject LThigh;
        private GameObject LThighDist;
        private GameObject LKnee;
        private GameObject LTibiaProx;
        private GameObject LTibia;
        private GameObject LTibiaDist;
        private GameObject LAnkle;
        private GameObject LHeel;
        private GameObject LToe;
        private GameObject LToeLat;
        private GameObject RThighProx;
        private GameObject RThigh;
        private GameObject RThighDist;
        private GameObject RKnee;
        private GameObject RTibiaProx;
        private GameObject RTibia;
        private GameObject RTibiaDist;
        private GameObject RAnkle;
        private GameObject RHeel;
        private GameObject RToe;
        private GameObject RToeLat;



        // Labview Heelstrike variables
        private float right_heelstrike_lightup;
        private float left_heelstrike_lightup;
       

        // // // // // // // INITIALIZE VARIABLES // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
        // Number of Cubes in Scene
        private int num_Objects = 500;
        public Renderer main_floor;

        // Define all walkways
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

        // Define game objects
        public GameObject SquarePrefab;
        public GameObject OrigCubeObject;
        public GameObject OrigPlaneObject;
        public GameObject SceneObject;

        // Variables being read from UDP
        //UDP route//
        [Header("QTM")]
        public bool QTM;
        private bool streaming = false;
        private float num_of_UDP_vals;

        // Treadmill//
        private float beltSpeed;
        private float ground_translation_y;
        private float falling_rotation_y_labview;
        private float ground_translation_y_labview;
        private float ground_translation_z_unity;
        private float correct_gyro_labview;
        private static float correct_gyro_infiniteworld { get; set; }

        public static float HeadRotation_qtm_y {get;set;}

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

            C7 = GameObject.Find("C7");
            Clavicle = GameObject.Find("Clavicle");
            LShoulder = GameObject.Find ("LShoulder");
            LUpperArm = GameObject.Find("LUpperArm");
            LElbow = GameObject.Find("LElbow");
            LForearm = GameObject.Find("LForearm");
            LWristAnt = GameObject.Find("LWristAnt");
            LWristPost = GameObject.Find("LWristPost");
            RShoulder = GameObject.Find("RShoulder");
            RUpperArm = GameObject.Find("RUpperArm");
            RElbow = GameObject.Find("RElbow");
            RForearm = GameObject.Find("RForearm");
            RWristAnt = GameObject.Find("RWristAnt");
            RWristPost = GameObject.Find("RWristPost");
            LASI = GameObject.Find("LASI");
            RASI = GameObject.Find("RASI");
            LPSI = GameObject.Find("LPSI");
            RPSI = GameObject.Find("RPSI");
            LThighProx = GameObject.Find("LThighProx");
            LThigh = GameObject.Find("LThigh");
            LThighDist = GameObject.Find("LThighDist");
            LKnee = GameObject.Find("LKnee");
            LTibiaProx = GameObject.Find("LTibiaProx");
            LTibia = GameObject.Find("LTibia");
            LTibiaDist = GameObject.Find("LTibiaDist");
            LAnkle = GameObject.Find("LAnkle");
            LHeel = GameObject.Find("LHeel");
            LToe = GameObject.Find("LToe");
            LToeLat = GameObject.Find("LToeLat");
            RThighProx = GameObject.Find("RThighProx");
            RThigh = GameObject.Find("RThigh");
            RThighDist = GameObject.Find("RThighDist");
            RKnee = GameObject.Find("RKnee");
            RTibiaProx = GameObject.Find("RTibiaProx");
            RTibia = GameObject.Find("RTibia");
            RTibiaDist = GameObject.Find("RTibiaDist");
            RAnkle = GameObject.Find("RAnkle");
            RHeel = GameObject.Find("RHeel");
            RToe = GameObject.Find("RToe");
            RToeLat = GameObject.Find("RToeLat");


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

            // // // Load the VR Randomization CSV // // // //
            TextAsset PlaneInfo = Resources.Load<TextAsset>("ObjectInfo_unity");
            string[] PlaneInfo_string = PlaneInfo.text.Split(new char[] { '\n' });


            // // // Assign Lane Positions // // // //
            for (int i = 0; i < PlaneInfo_string.Length - 1; i ++)
           {
               String[] row_string = PlaneInfo_string[i].Split(new char[] {','});
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
                    // // // Assign Lane Colors // // // //
                    if (i == 1)
                    {
                        LaneColors = row_doubles;
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

        private void InitiateMarkers()
        {
            foreach (var marker in markers)
            {
                Destroy(marker);
            }

            markers.Clear();
            markerData = rtClient.Markers;
            QTMrigidBodies = rtClient.Bodies;
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

        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
        }

        void Update(){

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
             // /// // QTM SDK // /// //
            // 
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
            QTMrigidBodies = rtClient.Bodies;

			if (markerData == null && markerData.Count == 0)
				return;
			for (int i = 0; i < markerData.Count; i++)
			{
				if (markerData[i].Position.magnitude > 0)
				{
					if (markerData[i].Label == LFHeadMarker)
					{
						LFHeadPosition_qtm_x = markerData[i].Position.z;
						LFHeadPosition_qtm_y = markerData[i].Position.y;
						LFHeadPosition_qtm_z = markerData[i].Position.x;
					}
                    if (markerData[i].Label == RFHeadMarker)
                    {
                        RFHeadPosition_qtm_x = markerData[i].Position.z;
                        RFHeadPosition_qtm_y = markerData[i].Position.y;
                        RFHeadPosition_qtm_z = markerData[i].Position.x;
                    }
					if (markerData[i].Label == LBHeadMarker)
					{
						LBHeadPosition_qtm_x = markerData[i].Position.z;
						LBHeadPosition_qtm_y = markerData[i].Position.y;
						LBHeadPosition_qtm_z = markerData[i].Position.x;
					}
					if (markerData[i].Label == RBHeadMarker)
					{
						RBHeadPosition_qtm_x = markerData[i].Position.z;
						RBHeadPosition_qtm_y = markerData[i].Position.y;
						RBHeadPosition_qtm_z = markerData[i].Position.x;
					}
                    if (markerData[i].Label == C7Marker)
                    {
                        C7Position_qtm.x = -markerData[i].Position.z;
                        C7Position_qtm.y = markerData[i].Position.y;
                        C7Position_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == ClavicleMarker)
                    {
                        ClaviclePosition_qtm.x = -markerData[i].Position.z;
                        ClaviclePosition_qtm.y = markerData[i].Position.y;
                        ClaviclePosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LShoulderMarker)
                    {
                        LShoulderPosition_qtm.x = -markerData[i].Position.z;
                        LShoulderPosition_qtm.y = markerData[i].Position.y;
                        LShoulderPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LUpperArmMarker)
                    {
                        LUpperArmPosition_qtm.x = -markerData[i].Position.z;
                        LUpperArmPosition_qtm.y = markerData[i].Position.y;
                        LUpperArmPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LElbowMarker)
                    {
                        LElbowPosition_qtm.x = -markerData[i].Position.z;
                        LElbowPosition_qtm.y = markerData[i].Position.y;
                        LElbowPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LForearmMarker)
                    {
                        LForearmPosition_qtm.x = -markerData[i].Position.z;
                        LForearmPosition_qtm.y = markerData[i].Position.y;
                        LForearmPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LWristAntMarker)
                    {
                        LWristAntPosition_qtm.x = -markerData[i].Position.z;
                        LWristAntPosition_qtm.y = markerData[i].Position.y;
                        LWristAntPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LWristPostMarker)
                    {
                        LWristPostPosition_qtm.x = -markerData[i].Position.z;
                        LWristPostPosition_qtm.y = markerData[i].Position.y;
                        LWristPostPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RShoulderMarker)
                    {
                        RShoulderPosition_qtm.x = -markerData[i].Position.z;
                        RShoulderPosition_qtm.y = markerData[i].Position.y;
                        RShoulderPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RUpperArmMarker)
                    {
                        RUpperArmPosition_qtm.x = -markerData[i].Position.z;
                        RUpperArmPosition_qtm.y = markerData[i].Position.y;
                        RUpperArmPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RElbowMarker)
                    {
                        RElbowPosition_qtm.x = -markerData[i].Position.z;
                        RElbowPosition_qtm.y = markerData[i].Position.y;
                        RElbowPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RForearmMarker)
                    {
                        RForearmPosition_qtm.x = -markerData[i].Position.z;
                        RForearmPosition_qtm.y = markerData[i].Position.y;
                        RForearmPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RWristAntMarker)
                    {
                        RWristAntPosition_qtm.x = -markerData[i].Position.z;
                        RWristAntPosition_qtm.y = markerData[i].Position.y;
                        RWristAntPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RWristPostMarker)
                    {
                        RWristPostPosition_qtm.x = -markerData[i].Position.z;
                        RWristPostPosition_qtm.y = markerData[i].Position.y;
                        RWristPostPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LASIMarker)
                    {
                        LASIPosition_qtm.x = -markerData[i].Position.z;
                        LASIPosition_qtm.y = markerData[i].Position.y;
                        LASIPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RASIMarker)
                    {
                        RASIPosition_qtm.x = -markerData[i].Position.z;
                        RASIPosition_qtm.y = markerData[i].Position.y;
                        RASIPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LPSIMarker)
                    {
                        LPSIPosition_qtm.x = -markerData[i].Position.z;
                        LPSIPosition_qtm.y = markerData[i].Position.y;
                        LPSIPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RPSIMarker)
                    {
                        RPSIPosition_qtm.x = -markerData[i].Position.z;
                        RPSIPosition_qtm.y = markerData[i].Position.y;
                        RPSIPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LThighProxMarker)
                    {
                        LThighProxPosition_qtm.x = -markerData[i].Position.z;
                        LThighProxPosition_qtm.y = markerData[i].Position.y;
                        LThighProxPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LThighMarker)
                    {
                        LThighPosition_qtm.x = -markerData[i].Position.z;
                        LThighPosition_qtm.y = markerData[i].Position.y;
                        LThighPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LThighDistMarker)
                    {
                        LThighDistPosition_qtm.x = -markerData[i].Position.z;
                        LThighDistPosition_qtm.y = markerData[i].Position.y;
                        LThighDistPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LKneeMarker)
                    {
                        LKneePosition_qtm.x = -markerData[i].Position.z;
                        LKneePosition_qtm.y = markerData[i].Position.y;
                        LKneePosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LTibiaProxMarker)
                    {
                        LTibiaProxPosition_qtm.x = -markerData[i].Position.z;
                        LTibiaProxPosition_qtm.y = markerData[i].Position.y;
                        LTibiaProxPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LTibiaMarker)
                    {
                        LTibiaPosition_qtm.x = -markerData[i].Position.z;
                        LTibiaPosition_qtm.y = markerData[i].Position.y;
                        LTibiaPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LTibiaDistMarker)
                    {
                        LTibiaDistPosition_qtm.x = -markerData[i].Position.z;
                        LTibiaDistPosition_qtm.y = markerData[i].Position.y;
                        LTibiaDistPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LAnkleMarker)
                    {
                        LAnklePosition_qtm.x = -markerData[i].Position.z;
                        LAnklePosition_qtm.y = markerData[i].Position.y;
                        LAnklePosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LHeelMarker)
                    {
                        LHeelPosition_qtm.x = -markerData[i].Position.z;
                        LHeelPosition_qtm.y = markerData[i].Position.y;
                        LHeelPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LToeMarker)
                    {
                        LToePosition_qtm.x = -markerData[i].Position.z;
                        LToePosition_qtm.y = markerData[i].Position.y;
                        LToePosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == LToeLatMarker)
                    {
                        LToeLatPosition_qtm.x = -markerData[i].Position.z;
                        LToeLatPosition_qtm.y = markerData[i].Position.y;
                        LToeLatPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RThighProxMarker)
                    {
                        RThighProxPosition_qtm.x = -markerData[i].Position.z;
                        RThighProxPosition_qtm.y = markerData[i].Position.y;
                        RThighProxPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RThighMarker)
                    {
                        RThighPosition_qtm.x = -markerData[i].Position.z;
                        RThighPosition_qtm.y = markerData[i].Position.y;
                        RThighPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RThighDistMarker)
                    {
                        RThighDistPosition_qtm.x = -markerData[i].Position.z;
                        RThighDistPosition_qtm.y = markerData[i].Position.y;
                        RThighDistPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RKneeMarker)
                    {
                        RKneePosition_qtm.x = -markerData[i].Position.z;
                        RKneePosition_qtm.y = markerData[i].Position.y;
                        RKneePosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RTibiaProxMarker)
                    {
                        RTibiaProxPosition_qtm.x = -markerData[i].Position.z;
                        RTibiaProxPosition_qtm.y = markerData[i].Position.y;
                        RTibiaProxPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RTibiaMarker)
                    {
                        RTibiaPosition_qtm.x = -markerData[i].Position.z;
                        RTibiaPosition_qtm.y = markerData[i].Position.y;
                        RTibiaPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RTibiaDistMarker)
                    {
                        RTibiaDistPosition_qtm.x = -markerData[i].Position.z;
                        RTibiaDistPosition_qtm.y = markerData[i].Position.y;
                        RTibiaDistPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RAnkleMarker)
                    {
                        RAnklePosition_qtm.x = -markerData[i].Position.z;
                        RAnklePosition_qtm.y = markerData[i].Position.y;
                        RAnklePosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RHeelMarker)
                    {
                        RHeelPosition_qtm.x = -markerData[i].Position.z;
                        RHeelPosition_qtm.y = markerData[i].Position.y;
                        RHeelPosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RToeMarker)
                    {
                        RToePosition_qtm.x = -markerData[i].Position.z;
                        RToePosition_qtm.y = markerData[i].Position.y;
                        RToePosition_qtm.z = markerData[i].Position.x;
                    }
                    if (markerData[i].Label == RToeLatMarker)
                    {
                        RToeLatPosition_qtm.x = -markerData[i].Position.z;
                        RToeLatPosition_qtm.y = markerData[i].Position.y;
                        RToeLatPosition_qtm.z = markerData[i].Position.x;
                    }

                    FHeadPosition_qtm.x = -(LFHeadPosition_qtm_x + RFHeadPosition_qtm_x) / 2;
                    FHeadPosition_qtm.y = (LFHeadPosition_qtm_y + RFHeadPosition_qtm_y) / 2;
                    FHeadPosition_qtm.z = (LFHeadPosition_qtm_z + RFHeadPosition_qtm_z) / 2;
                      
                    HeadPosition_qtm = FHeadPosition_qtm;
                }
			}




            for (int i = 0; i < QTMrigidBodies.Count; i++)
            {
                if (QTMrigidBodies[i].Name == OculusHeadBody)
                {
                    HeadRotation_qtm_y = QTMrigidBodies[i].Rotation.eulerAngles.y;
                }
            }
            // // // // // // // // READ LABVIEW UDP STRING // // // // // // // //// // // // // // // //// // // // // // // //// // 
            // reads string - splits the characters based on "," - assigns the characters to the variables based on the order within the string
            char[] delimiter1 = new char[] { ',' };
			var strvalues = lastReceivedUDPPacket.Split (delimiter1, StringSplitOptions.None);

            foreach (string word in strvalues)
            {
                num_of_UDP_vals++;
                if (num_of_UDP_vals == 1)
                {
                    float.TryParse(word, out HeadPosition_labview_x);
                    HeadPosition_labview.x = HeadPosition_labview_x / 1000;
                }
                if (num_of_UDP_vals == 2)
                {
                    float.TryParse(word, out HeadPosition_labview_y);
                    HeadPosition_labview.z = HeadPosition_labview_y / 1000;
                }
                if (num_of_UDP_vals == 3)
                {
                    float.TryParse(word, out HeadPosition_labview_z);
                    HeadPosition_labview.y = HeadPosition_labview_z / 1000;
                }
                if (num_of_UDP_vals == 4)
                {
                    float.TryParse(word, out ground_translation_y_labview);
                    ground_translation_z_unity = ground_translation_y_labview;
                }
                if (num_of_UDP_vals == 5)
                {
                    float.TryParse(word, out falling_rotation_y_labview);
                    falling_rotation_z_unity = falling_rotation_y_labview;
                }

                if (num_of_UDP_vals == 6)
                {
                    float.TryParse(word, out right_heelstrike_lightup);   
                }
                if (num_of_UDP_vals == 7)
                {
                    float.TryParse(word, out left_heelstrike_lightup);
                }
            }
                num_of_UDP_vals = 0;

    /////////////////////////// Heel strikes in each lane ////////////////////////////////////

            if (center_walkway1_position.z > -5f & center_walkway1_position.z < 5)
            {
                //Right Heel Strike
                if (right_heelstrike_lightup == 1)
                {
                    if (RHeelPosition_qtm.x < .18 & RHeelPosition_qtm.x > -.18)
                    {
                        center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                    }

                    if (RHeelPosition_qtm.x < -.18)
                    {
                        left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                    }
                    if (RHeelPosition_qtm.x > .18)
                    {
                        right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                    }
                }

                //Left Heel Strike
                if (left_heelstrike_lightup == 1)
                {
                    if (LHeelPosition_qtm.x < .18 & LHeelPosition_qtm.x > -.18)
                    {
                        center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                    }

                    if (LHeelPosition_qtm.x < -.18)
                    {
                        left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                    }
                    if (LHeelPosition_qtm.x > .18)
                    {
                        right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                    }
                }

                //No  Heel Strike
                if (right_heelstrike_lightup == 0 & left_heelstrike_lightup == 0)
                {
                    center_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    right_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                }
            }

            if (center_walkway2_position.z < -25f)
            {
                //Right Heel Strike
                if (right_heelstrike_lightup == 1)
                {
                    if (RHeelPosition_qtm.x < .18 & RHeelPosition_qtm.x > -.18)
                    {
                        center_walkway2.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                    }

                    if (RHeelPosition_qtm.x < -.18)
                    {
                        left_walkway2.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                    }
                    if (RHeelPosition_qtm.x > .18)
                    {
                        right_walkway2.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                    }
                }

                //Left Heel Strike
                if (left_heelstrike_lightup == 1)
                {
                    if (LHeelPosition_qtm.x < .18 & LHeelPosition_qtm.x > -.18)
                    {
                        center_walkway2.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                    }

                    if (LHeelPosition_qtm.x < -.18)
                    {
                        left_walkway2.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                    }
                    if (LHeelPosition_qtm.x > .18)
                    {
                        right_walkway2.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                    }
                }

                //No  Heel Strike
                if (right_heelstrike_lightup == 0 & left_heelstrike_lightup == 0)
                {
                    center_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    left_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                    right_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
            }
            if (center_walkway3_position.z > -5f& center_walkway3_position.z < 5)
            {
                //Right Heel Strike
                if (right_heelstrike_lightup == 1)
                {
                    if (RHeelPosition_qtm.x < .18 & RHeelPosition_qtm.x > -.18)
                    {
                        center_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                    }

                    if (RHeelPosition_qtm.x < -.18)
                    {
                        left_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                    }
                    if (RHeelPosition_qtm.x > .18)
                    {
                        right_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                    }
                }

                //Left Heel Strike
                if (left_heelstrike_lightup == 1)
                {
                    if (LHeelPosition_qtm.x < .18 & LHeelPosition_qtm.x > -.18)
                    {
                        center_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                    }

                    if (LHeelPosition_qtm.x < -.18)
                    {
                        left_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                    }
                    if (LHeelPosition_qtm.x > .18)
                    {
                        right_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                    }
                }

                //No  Heel Strike
                if (right_heelstrike_lightup == 0 & left_heelstrike_lightup == 0)
                {
                    center_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    left_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    right_walkway3.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                }
            }


            if (center_walkway4_position.z < -25f)
            {
                if (center_walkway4_position.z > -5f & center_walkway4_position.z < 5)
                {
                    //Right Heel Strike
                    if (right_heelstrike_lightup == 1)
                    {
                        if (RHeelPosition_qtm.x < .18 & RHeelPosition_qtm.x > -.18)
                        {
                            center_walkway4.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (RHeelPosition_qtm.x < -.18)
                        {
                            left_walkway4.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                        if (RHeelPosition_qtm.x > .18)
                        {
                            right_walkway4.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                    }

                    //Left Heel Strike
                    if (left_heelstrike_lightup == 1)
                    {
                        if (LHeelPosition_qtm.x < .18 & LHeelPosition_qtm.x > -.18)
                        {
                            center_walkway4.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (LHeelPosition_qtm.x < -.18)
                        {
                            left_walkway4.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                        if (LHeelPosition_qtm.x > .18)
                        {
                            right_walkway4.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                    }

                    //No  Heel Strike
                    if (right_heelstrike_lightup == 0 & left_heelstrike_lightup == 0)
                    {
                        center_walkway4.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        left_walkway4.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                        right_walkway4.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    }
                }
            }
            if (center_walkway5_position.z < -25f)
            {
                if (center_walkway5_position.z > -5f & center_walkway5_position.z < 5)
                {
                    //Right Heel Strike
                    if (right_heelstrike_lightup == 1)
                    {
                        if (RHeelPosition_qtm.x < .18 & RHeelPosition_qtm.x > -.18)
                        {
                            center_walkway5.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (RHeelPosition_qtm.x < -.18)
                        {
                            left_walkway5.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                        if (RHeelPosition_qtm.x > .18)
                        {
                            right_walkway5.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                    }

                    //Left Heel Strike
                    if (left_heelstrike_lightup == 1)
                    {
                        if (LHeelPosition_qtm.x < .18 & LHeelPosition_qtm.x > -.18)
                        {
                            center_walkway5.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (LHeelPosition_qtm.x < -.18)
                        {
                            left_walkway5.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                        if (LHeelPosition_qtm.x > .18)
                        {
                            right_walkway5.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                    }

                    //No  Heel Strike
                    if (right_heelstrike_lightup == 0 & left_heelstrike_lightup == 0)
                    {
                        center_walkway5.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        left_walkway5.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        right_walkway5.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                    }
                }
            }
            if (center_walkway6_position.z < -25f)
            {
                if (center_walkway6_position.z > -5f & center_walkway6_position.z < 5)
                {
                    //Right Heel Strike
                    if (right_heelstrike_lightup == 1)
                    {
                        if (RHeelPosition_qtm.x < .18 & RHeelPosition_qtm.x > -.18)
                        {
                            center_walkway6.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (RHeelPosition_qtm.x < -.18)
                        {
                            left_walkway6.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                        if (RHeelPosition_qtm.x > .18)
                        {
                            right_walkway6.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                    }

                    //Left Heel Strike
                    if (left_heelstrike_lightup == 1)
                    {
                        if (LHeelPosition_qtm.x < .18 & LHeelPosition_qtm.x > -.18)
                        {
                            center_walkway6.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (LHeelPosition_qtm.x < -.18)
                        {
                            left_walkway6.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                        if (LHeelPosition_qtm.x > .18)
                        {
                            right_walkway6.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                    }

                    //No  Heel Strike
                    if (right_heelstrike_lightup == 0 & left_heelstrike_lightup == 0)
                    {
                        center_walkway6.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        left_walkway6.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                        right_walkway6.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    }
                }
            }
            if (center_walkway7_position.z < -25f)
            {
                if (center_walkway7_position.z > -5f & center_walkway7_position.z < 5)
                {
                    //Right Heel Strike
                    if (right_heelstrike_lightup == 1)
                    {
                        if (RHeelPosition_qtm.x < .18 & RHeelPosition_qtm.x > -.18)
                        {
                            center_walkway7.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (RHeelPosition_qtm.x < -.18)
                        {
                            left_walkway7.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                        if (RHeelPosition_qtm.x > .18)
                        {
                            right_walkway7.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                    }

                    //Left Heel Strike
                    if (left_heelstrike_lightup == 1)
                    {
                        if (LHeelPosition_qtm.x < .18 & LHeelPosition_qtm.x > -.18)
                        {
                            center_walkway7.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (LHeelPosition_qtm.x < -.18)
                        {
                            left_walkway7.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                        if (LHeelPosition_qtm.x > .18)
                        {
                            right_walkway7.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                    }

                    //No  Heel Strike
                    if (right_heelstrike_lightup == 0 & left_heelstrike_lightup == 0)
                    {
                        center_walkway7.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        left_walkway7.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        right_walkway7.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                    }
                }
            }
            if (center_walkway8_position.z < -25f)
            {
                if (center_walkway8_position.z > -5f & center_walkway8_position.z < 5)
                {
                    //Right Heel Strike
                    if (right_heelstrike_lightup == 1)
                    {
                        if (RHeelPosition_qtm.x < .18 & RHeelPosition_qtm.x > -.18)
                        {
                            center_walkway8.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (RHeelPosition_qtm.x < -.18)
                        {
                            left_walkway8.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                        if (RHeelPosition_qtm.x > .18)
                        {
                            right_walkway8.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                    }

                    //Left Heel Strike
                    if (left_heelstrike_lightup == 1)
                    {
                        if (LHeelPosition_qtm.x < .18 & LHeelPosition_qtm.x > -.18)
                        {
                            center_walkway8.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (LHeelPosition_qtm.x < -.18)
                        {
                            left_walkway8.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                        if (LHeelPosition_qtm.x > .18)
                        {
                            right_walkway8.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                    }

                    //No  Heel Strike
                    if (right_heelstrike_lightup == 0 & left_heelstrike_lightup == 0)
                    {
                        center_walkway8.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        left_walkway8.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                        right_walkway8.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    }
                }
            }

            if (center_walkway9_position.z < -25f)
            {
                if (center_walkway9_position.z > -5f & center_walkway9_position.z < 5)
                {
                    //Right Heel Strike
                    if (right_heelstrike_lightup == 1)
                    {
                        if (RHeelPosition_qtm.x < .18 & RHeelPosition_qtm.x > -.18)
                        {
                            center_walkway9.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (RHeelPosition_qtm.x < -.18)
                        {
                            left_walkway9.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                        if (RHeelPosition_qtm.x > .18)
                        {
                            right_walkway9.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                    }

                    //Left Heel Strike
                    if (left_heelstrike_lightup == 1)
                    {
                        if (LHeelPosition_qtm.x < .18 & LHeelPosition_qtm.x > -.18)
                        {
                            center_walkway9.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (LHeelPosition_qtm.x < -.18)
                        {
                            left_walkway9.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                        if (LHeelPosition_qtm.x > .18)
                        {
                            right_walkway9.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                    }

                    //No  Heel Strike
                    if (right_heelstrike_lightup == 0 & left_heelstrike_lightup == 0)
                    {
                        center_walkway9.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        left_walkway9.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        right_walkway9.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                    }
                }
            }

            if (center_walkway10_position.z < -25f)
            {
                if (center_walkway10_position.z > -5f & center_walkway10_position.z < 5)
                {
                    //Right Heel Strike
                    if (right_heelstrike_lightup == 1)
                    {
                        if (RHeelPosition_qtm.x < .18 & RHeelPosition_qtm.x > -.18)
                        {
                            center_walkway10.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (RHeelPosition_qtm.x < -.18)
                        {
                            left_walkway10.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                        if (RHeelPosition_qtm.x > .18)
                        {
                            right_walkway10.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                    }

                    //Left Heel Strike
                    if (left_heelstrike_lightup == 1)
                    {
                        if (LHeelPosition_qtm.x < .18 & LHeelPosition_qtm.x > -.18)
                        {
                            center_walkway10.GetComponent<Renderer>().material.SetColor("_Color", Color.green * 1.5f);
                        }

                        if (LHeelPosition_qtm.x < -.18)
                        {
                            left_walkway10.GetComponent<Renderer>().material.SetColor("_Color", Color.gray * 1.5f);
                        }
                        if (LHeelPosition_qtm.x > .18)
                        {
                            right_walkway10.GetComponent<Renderer>().material.SetColor("_Color", Color.red * 1.5f);
                        }
                    }

                    //No  Heel Strike
                    if (right_heelstrike_lightup == 0 & left_heelstrike_lightup == 0)
                    {
                        center_walkway10.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        left_walkway10.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
                        right_walkway10.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    }
                }
            }

           

          ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // // // // // // MOVE THE SCENE OBJECTS // // // // // // // //// // // // // // // //// // // // // // // //// // // // // // // //
            // Moves the original/ parent object, thus moving all children objects in the scene assigned to the parent object
            OrigCubeObjectPosition[2] =  - (ground_translation_z_unity) - 50f;
			OrigCubeObject.transform.position = OrigCubeObjectPosition;
            OrigPlaneObject.transform.position = OrigCubeObjectPosition;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // // // // // // Move THE SCENE GROUND // // // // // // // //// // // // // // // //// // // // // // // //// // // // // // // 
            // Move the texture with speed of treadmill
            // need to implement beltspeed instead.. Are there problems with variable update with this?
            TextureOffset[1] = - ground_translation_z_unity/5f; // offset update based on scale of plane being used... find a way to automate..
            current_texture_offset = main_floor.material.GetTextureOffset("_MainTex");

			main_floor.material.SetTextureOffset("_MainTex", TextureOffset); // Get rid of New Vector...

            // Global Position (with respect to scene origin)
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

            // Local Position (with respect to origPlane)
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

            if (left_walkway1_position.z < -25f)
            {
				left_walkway1_localPosition.z = left_walkway10.transform.localPosition.z + left_walkway10.transform.localScale.z*10/2 + left_walkway1.transform.localScale.z *10/2;
				left_walkway1.transform.localPosition = left_walkway1_localPosition;
            }
			if (left_walkway2_position.z < -25f)
			{
				left_walkway2_localPosition.z = left_walkway1.transform.localPosition.z + left_walkway1.transform.localScale.z*10/2 + left_walkway2.transform.localScale.z *10/2;
				left_walkway2.transform.localPosition = left_walkway2_localPosition;
			}
			if (left_walkway3_position.z < -25f)
			{
				left_walkway3_localPosition.z = left_walkway2.transform.localPosition.z + left_walkway2.transform.localScale.z*10/2 + left_walkway3.transform.localScale.z *10/2;
				left_walkway3.transform.localPosition = left_walkway3_localPosition;
			}
			if (left_walkway4_position.z < -25f)
			{
				left_walkway4_localPosition.z = left_walkway3.transform.localPosition.z + left_walkway3.transform.localScale.z*10/2 + left_walkway4.transform.localScale.z *10/2;
				left_walkway4.transform.localPosition = left_walkway4_localPosition;
			}
			if (left_walkway5_position.z < -25f)
			{
				left_walkway5_localPosition.z = left_walkway4.transform.localPosition.z + left_walkway4.transform.localScale.z*10/2 + left_walkway5.transform.localScale.z *10/2;
				left_walkway5.transform.localPosition = left_walkway5_localPosition;
			}
			if (left_walkway6_position.z < -25f)
			{
				left_walkway6_localPosition.z = left_walkway5.transform.localPosition.z + left_walkway5.transform.localScale.z*10/2 + left_walkway6.transform.localScale.z *10/2;
				left_walkway6.transform.localPosition = left_walkway6_localPosition;
			}
			if (left_walkway7_position.z < -25f)
			{
				left_walkway7_localPosition.z = left_walkway6.transform.localPosition.z + left_walkway6.transform.localScale.z*10/2 + left_walkway7.transform.localScale.z *10/2;
				left_walkway7.transform.localPosition = left_walkway7_localPosition;
			}
			if (left_walkway8_position.z < -25f)
			{
				left_walkway8_localPosition.z = left_walkway7.transform.localPosition.z + left_walkway7.transform.localScale.z*10/2 + left_walkway8.transform.localScale.z *10/2;
				left_walkway8.transform.localPosition = left_walkway8_localPosition;
			}

			if (left_walkway9_position.z < -25f)
			{
				left_walkway9_localPosition.z = left_walkway8.transform.localPosition.z + left_walkway8.transform.localScale.z*10/2 + left_walkway9.transform.localScale.z *10/2;
				left_walkway9.transform.localPosition = left_walkway9_localPosition;
			}

			if (left_walkway10_position.z < -25f)
			{
				left_walkway10_localPosition.z = left_walkway9.transform.localPosition.z + left_walkway9.transform.localScale.z*10/2 + left_walkway10.transform.localScale.z *10/2;
				left_walkway10.transform.localPosition = left_walkway10_localPosition;
			}


			/// // // // // center lane /// // 
			if (center_walkway1_position.z < -25f)
			{
				center_walkway1_localPosition.z = center_walkway10.transform.localPosition.z + center_walkway10.transform.localScale.z*10/2 + center_walkway1.transform.localScale.z *10/2;
				center_walkway1.transform.localPosition = center_walkway1_localPosition;
			}
			if (center_walkway2_position.z < -25f)
			{
				center_walkway2_localPosition.z = center_walkway1.transform.localPosition.z + center_walkway1.transform.localScale.z*10/2 + center_walkway2.transform.localScale.z *10/2;
				center_walkway2.transform.localPosition = center_walkway2_localPosition;
			}
			if (center_walkway3_position.z < -25f)
			{
				center_walkway3_localPosition.z = center_walkway2.transform.localPosition.z + center_walkway2.transform.localScale.z*10/2 + center_walkway3.transform.localScale.z *10/2;
				center_walkway3.transform.localPosition = center_walkway3_localPosition;
			}
			if (center_walkway4_position.z < -25f)
			{
				center_walkway4_localPosition.z = center_walkway3.transform.localPosition.z + center_walkway3.transform.localScale.z*10/2 + center_walkway4.transform.localScale.z *10/2;
				center_walkway4.transform.localPosition = center_walkway4_localPosition;
			}
			if (center_walkway5_position.z < -25f)
			{
				center_walkway5_localPosition.z = center_walkway4.transform.localPosition.z + center_walkway4.transform.localScale.z*10/2 + center_walkway5.transform.localScale.z *10/2;
				center_walkway5.transform.localPosition = center_walkway5_localPosition;
			}
			if (center_walkway6_position.z < -25f)
			{
				center_walkway6_localPosition.z = center_walkway5.transform.localPosition.z + center_walkway5.transform.localScale.z*10/2 + center_walkway6.transform.localScale.z *10/2;
				center_walkway6.transform.localPosition = center_walkway6_localPosition;
			}
			if (center_walkway7_position.z < -25f)
			{
				center_walkway7_localPosition.z = center_walkway6.transform.localPosition.z + center_walkway6.transform.localScale.z*10/2 + center_walkway7.transform.localScale.z *10/2;
				center_walkway7.transform.localPosition = center_walkway7_localPosition;
			}
			if (center_walkway8_position.z < -25f)
			{
				center_walkway8_localPosition.z = center_walkway7.transform.localPosition.z + center_walkway7.transform.localScale.z*10/2 + center_walkway8.transform.localScale.z *10/2;
				center_walkway8.transform.localPosition = center_walkway8_localPosition;
			}

			if (center_walkway9_position.z < -25f)
			{
				center_walkway9_localPosition.z = center_walkway8.transform.localPosition.z + center_walkway8.transform.localScale.z*10/2 + center_walkway9.transform.localScale.z *10/2;
				center_walkway9.transform.localPosition = center_walkway9_localPosition;
			}

			if (center_walkway10_position.z < -25f)
			{
				center_walkway10_localPosition.z = center_walkway9.transform.localPosition.z + center_walkway9.transform.localScale.z*10/2 + center_walkway10.transform.localScale.z *10/2;
				center_walkway10.transform.localPosition = center_walkway10_localPosition;
			}
           
			// // // // // // right lane // // // // // // // 
			if (right_walkway1_position.z < -25f)
			{
				right_walkway1_localPosition.z = right_walkway10.transform.localPosition.z + right_walkway10.transform.localScale.z*10/2 + right_walkway1.transform.localScale.z *10/2;
				right_walkway1.transform.localPosition = right_walkway1_localPosition;
			}
			if (right_walkway2_position.z < -25f)
			{
				right_walkway2_localPosition.z = right_walkway1.transform.localPosition.z + right_walkway1.transform.localScale.z*10/2 + right_walkway2.transform.localScale.z *10/2;
				right_walkway2.transform.localPosition = right_walkway2_localPosition;
			}
			if (right_walkway3_position.z < -25f)
			{
				right_walkway3_localPosition.z = right_walkway2.transform.localPosition.z + right_walkway2.transform.localScale.z*10/2 + right_walkway3.transform.localScale.z *10/2;
				right_walkway3.transform.localPosition = right_walkway3_localPosition;
			}
			if (right_walkway4_position.z < -25f)
			{
				right_walkway4_localPosition.z = right_walkway3.transform.localPosition.z + right_walkway3.transform.localScale.z*10/2 + right_walkway4.transform.localScale.z *10/2;
				right_walkway4.transform.localPosition = right_walkway4_localPosition;
			}
			if (right_walkway5_position.z < -25f)
			{
				right_walkway5_localPosition.z = right_walkway4.transform.localPosition.z + right_walkway4.transform.localScale.z*10/2 + right_walkway5.transform.localScale.z *10/2;
				right_walkway5.transform.localPosition = right_walkway5_localPosition;
			}
			if (right_walkway6_position.z < -25f)
			{
				right_walkway6_localPosition.z = right_walkway5.transform.localPosition.z + right_walkway5.transform.localScale.z*10/2 + right_walkway6.transform.localScale.z *10/2;
				right_walkway6.transform.localPosition = right_walkway6_localPosition;
			}
			if (right_walkway7_position.z < -25f)
			{
				right_walkway7_localPosition.z = right_walkway6.transform.localPosition.z + right_walkway6.transform.localScale.z*10/2 + right_walkway7.transform.localScale.z *10/2;
				right_walkway7.transform.localPosition = right_walkway7_localPosition;
			}
			if (right_walkway8_position.z < -25f)
			{
				right_walkway8_localPosition.z = right_walkway7.transform.localPosition.z + right_walkway7.transform.localScale.z*10/2 + right_walkway8.transform.localScale.z *10/2;
				right_walkway8.transform.localPosition = right_walkway8_localPosition;
			}

			if (right_walkway9_position.z < -25f)
			{
				right_walkway9_localPosition.z = right_walkway8.transform.localPosition.z + right_walkway8.transform.localScale.z*10/2 + right_walkway9.transform.localScale.z *10/2;
				right_walkway9.transform.localPosition = right_walkway9_localPosition;
			}

			if (right_walkway10_position.z < -25f)
			{
				right_walkway10_localPosition.z = right_walkway9.transform.localPosition.z + right_walkway9.transform.localScale.z*10/2 + right_walkway10.transform.localScale.z *10/2;
				right_walkway10.transform.localPosition = right_walkway10_localPosition;
			}

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // // // // // // MOVE PERSPECTIVE CAMERA // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
            // Moves the "Render Chanel Camera" based on real world coordinates gathered from Nexus (motion capture)
            // update camera position based on labview or qtm data stream
            if (QTM == true)
			{
				PlayerPerspective.transform.position = HeadPosition_qtm;

               
                C7.transform.position = C7Position_qtm;
                Clavicle.transform.position = ClaviclePosition_qtm;

                LShoulder.transform.position = LShoulderPosition_qtm;
                LUpperArm.transform.position = LUpperArmPosition_qtm;
                LElbow.transform.position = LElbowPosition_qtm;
                LForearm.transform.position = LForearmPosition_qtm;
                LWristAnt.transform.position = LWristAntPosition_qtm;
                LWristPost.transform.position = LWristPostPosition_qtm;

                RShoulder.transform.position = RShoulderPosition_qtm;
                RUpperArm.transform.position = RUpperArmPosition_qtm;
                RElbow.transform.position = RElbowPosition_qtm;
                RForearm.transform.position = RForearmPosition_qtm;
                RWristAnt.transform.position = RWristAntPosition_qtm;
                RWristPost.transform.position = RWristPostPosition_qtm;

                LASI.transform.position = LASIPosition_qtm;
                RASI.transform.position = RASIPosition_qtm;
                LPSI.transform.position = LPSIPosition_qtm;
                RPSI.transform.position = RPSIPosition_qtm;

                LThighProx.transform.position = LThighProxPosition_qtm;
                LThigh.transform.position = LThighPosition_qtm;
                LThighDist.transform.position = LThighDistPosition_qtm;
                LKnee.transform.position = LKneePosition_qtm;
                LTibiaProx.transform.position = LTibiaProxPosition_qtm;
                LTibia.transform.position = LTibiaPosition_qtm;
                LTibiaDist.transform.position = LTibiaDistPosition_qtm;
                LAnkle.transform.position = LAnklePosition_qtm;
                LHeel.transform.position = LHeelPosition_qtm;
                LToe.transform.position = LToePosition_qtm;
                LToeLat.transform.position = LToeLatPosition_qtm;

                RThighProx.transform.position = RThighProxPosition_qtm;
                RThigh.transform.position = RThighPosition_qtm;
                RThighDist.transform.position = RThighDistPosition_qtm;
                RKnee.transform.position = RKneePosition_qtm;
                RTibiaProx.transform.position = RTibiaProxPosition_qtm;
                RTibia.transform.position = RTibiaPosition_qtm;
                RTibiaDist.transform.position = RTibiaDistPosition_qtm;
                RAnkle.transform.position = RAnklePosition_qtm;
                RHeel.transform.position = RHeelPosition_qtm;
                RToe.transform.position = RToePosition_qtm;
                RToeLat.transform.position = RToeLatPosition_qtm;

               


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