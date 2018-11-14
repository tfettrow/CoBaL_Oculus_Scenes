using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.VR;

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
		private string HeadMarker = "L-frame - 1";
		// from marker set
		//private string HeadMarker = "LFHD";
		private RTClient rtClient;
		private GameObject markerRoot;
		private List<GameObject> markers;


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

		private GameObject right_walkway1;
		private GameObject right_walkway2;
		private GameObject right_walkway3;
		private GameObject right_walkway4;
		private GameObject right_walkway5;
		private GameObject right_walkway6;
		private GameObject right_walkway7;
		private GameObject right_walkway8;
		private GameObject right_walkway9;

		private GameObject center_walkway1;
		private GameObject center_walkway2;
		private GameObject center_walkway3;
		private GameObject center_walkway4;
		private GameObject center_walkway5;
		private GameObject center_walkway6;
		private GameObject center_walkway7;
		private GameObject center_walkway8;
		private GameObject center_walkway9;


		//private GameObject right_walkway1;
		//private GameObject right_walkway2;
		//private GameObject right_walkway3;
		//private GameObject right_walkway4;
		//private GameObject right_walkway5;
		//private GameObject right_walkway6;
		//private GameObject right_walkway7;
		//private GameObject right_walkway8;
		//private GameObject right_walkway9;

		//private Vector3 right_walkway1_position;
		//private Vector3 right_walkway2_position;
		//private Vector3 right_walkway3_position;
		//private Vector3 right_walkway4_position;
		//private Vector3 right_walkway5_position;
		//private Vector3 right_walkway6_position;
		//private Vector3 right_walkway7_position;
		//private Vector3 right_walkway8_position;
		//private Vector3 right_walkway9_position;
	
		private Vector3 left_walkway1_position;
		private Vector3 left_walkway2_position;
		private Vector3 left_walkway3_position;
		private Vector3 left_walkway4_position;
		private Vector3 left_walkway5_position;
		private Vector3 left_walkway6_position;
		private Vector3 left_walkway7_position;
		private Vector3 left_walkway8_position;
		private Vector3 left_walkway9_position;

		private Vector3 right_walkway1_position;
		private Vector3 right_walkway2_position;
		private Vector3 right_walkway3_position;
		private Vector3 right_walkway4_position;
		private Vector3 right_walkway5_position;
		private Vector3 right_walkway6_position;
		private Vector3 right_walkway7_position;
		private Vector3 right_walkway8_position;
		private Vector3 right_walkway9_position;

		private Vector3 center_walkway1_position;
		private Vector3 center_walkway2_position;
		private Vector3 center_walkway3_position;
		private Vector3 center_walkway4_position;
		private Vector3 center_walkway5_position;
		private Vector3 center_walkway6_position;
		private Vector3 center_walkway7_position;
		private Vector3 center_walkway8_position;
		private Vector3 center_walkway9_position;

		//private Renderer center_walkway1;
		//private Renderer center_walkway2;
		//private Renderer center_walkway3;
		//private Renderer center_walkway4;
		//private Renderer center_walkway5;
		//private Renderer center_walkway6;
		//private Renderer center_walkway7;
		//private Renderer center_walkway8;
		//private Renderer center_walkway9;

	    private Vector2 TextureOffset;
		private Vector2 TextureOffset_left;
		private Vector2 TextureOffset_right;

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
		public GameObject OrigObject;
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

		public GameObject PlayerPerspective;
		// labview and qtm translation values                         
		private Vector3 HeadPosition_labview;
		private float HeadPosition_labview_x;
		private float HeadPosition_labview_y;
		private float HeadPosition_labview_z;
		private Vector3 HeadPosition_qtm;
		private float HeadPosition_qtm_x;
		private float HeadPosition_qtm_y;
		private float HeadPosition_qtm_z;

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
		Vector3 OrigObjectPosition;

		Vector3 this_pyramid_position;
		Vector3 DomePosition;
		Vector3 ChildObjectPlacement;
		/// //////////////////////////////////////////////////////////////////////////////////////////////	///
		//public float dt;
		/// 

        public void Start()
        {
            init();
            rtClient = RTClient.GetInstance();
            markers = new List<GameObject>();
            markerRoot = gameObject;
            // Find the game ojbects
            OrigObject = GameObject.Find("OrigCube");
            ChildObjectPlacement = GameObject.Find("Object Placement Origin").transform.position;
            SceneObject = GameObject.Find("SceneOrigin");
            PlayerPerspective = GameObject.Find("PlayerPerspective");
            //Plane1 = GameObject.Find("Plane1");
            //Plane2 = GameObject.Find("Plane2");
            //Plane3 = GameObject.Find("Plane3");
            //Plane4 = GameObject.Find("Plane4");

            PlayerPerspective = GameObject.Find("PlayerPerspective");

			// // // // //  What is the game plan? // // // // //
			// Score keep
			// score history
			// 
			// set 1-4 as default color scheme
			// labview updates color at position 50
			// 3 conditions 1:no 2:left 3: right 4: both
			// conditions to analyze biomechanically
			// 1: trigLeft towards constraint
			// 2: trigLeft away from constraint
			// 3: trigRight towards constraint
			// 4: trigRight away from constraint

			// how does labview know w

			left_walkway1 = GameObject.Find ("left_walkway1");
			left_walkway2 = GameObject.Find("left_walkway2");
			left_walkway3 = GameObject.Find("left_walkway3");
			left_walkway4 = GameObject.Find("left_walkway4");
			left_walkway5 = GameObject.Find("left_walkway5");
			left_walkway6 = GameObject.Find("left_walkway6");
			left_walkway7 = GameObject.Find("left_walkway7");
			left_walkway8 = GameObject.Find("left_walkway8");
			left_walkway9 = GameObject.Find("left_walkway9");

			left_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			left_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			left_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			left_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			left_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			left_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			left_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			left_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			left_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);

			right_walkway1 = GameObject.Find ("right_walkway1");
			right_walkway2 = GameObject.Find("right_walkway2");
			right_walkway3 = GameObject.Find("right_walkway3");
			right_walkway4 = GameObject.Find("right_walkway4");
			right_walkway5 = GameObject.Find("right_walkway5");
			right_walkway6 = GameObject.Find("right_walkway6");
			right_walkway7 = GameObject.Find("right_walkway7");
			right_walkway8 = GameObject.Find("right_walkway8");
			right_walkway9 = GameObject.Find("right_walkway9");

			right_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			right_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			right_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			right_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			right_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			right_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			right_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			right_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			right_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);

			center_walkway1 = GameObject.Find ("center_walkway1");
			center_walkway2 = GameObject.Find("center_walkway2");
			center_walkway3 = GameObject.Find("center_walkway3");
			center_walkway4 = GameObject.Find("center_walkway4");
			center_walkway5 = GameObject.Find("center_walkway5");
			center_walkway6 = GameObject.Find("center_walkway6");
			center_walkway7 = GameObject.Find("center_walkway7");
			center_walkway8 = GameObject.Find("center_walkway8");
			center_walkway9 = GameObject.Find("center_walkway9");

			center_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
			center_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
			center_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
			center_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
			center_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
			center_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
			center_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
			center_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
			center_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);

			//left_walkway1.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
			//left_walkway2.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
			//left_walkway3.material.SetColor("_Color", Color.green);
			//left_walkway4.material.SetColor("_Color", Color.green);
			//left_walkway5.material.SetColor("_Color", Color.green);
			//left_walkway6.material.SetColor("_Color", Color.green);
			//left_walkway7.material.SetColor("_Color", Color.green);
			//left_walkway8.material.SetColor("_Color", Color.green);
			//left_walkway9.material.SetColor("_Color", Color.green);

			//center_walkway1.material.SetColor("_Color", Color.gray);
			//center_walkway2.material.SetColor("_Color", Color.gray);
			//center_walkway3.material.SetColor("_Color", Color.gray);
			//center_walkway4.material.SetColor("_Color", Color.gray);
			//center_walkway5.material.SetColor("_Color", Color.gray);
			//center_walkway6.material.SetColor("_Color", Color.gray);
			//center_walkway7.material.SetColor("_Color", Color.gray);
			//center_walkway8.material.SetColor("_Color", Color.gray);
			//center_walkway9.material.SetColor("_Color", Color.gray);

			//right_walkway1.material.SetColor("_Color", Color.green);
			//right_walkway2.material.SetColor("_Color", Color.green);
			//right_walkway3.material.SetColor("_Color", Color.green);
			//right_walkway4.material.SetColor("_Color", Color.green);
			//right_walkway5.material.SetColor("_Color", Color.green);
			//right_walkway6.material.SetColor("_Color", Color.green);
			//right_walkway7.material.SetColor("_Color", Color.green);
			//right_walkway8.material.SetColor("_Color", Color.green);
			//right_walkway9.material.SetColor("_Color", Color.green);
			////////////////////////////////////////////////////////////////////////////////////


            // // // // // INSTANTIATE SCENE OBJECTS // // // // // // // // // // // // // // // // // // // // //// // // // // // // // // // // // // //// // // // // // // // // // // // // // 
            HalfSideWall = .5f * LeftRightSideLength;
            HalfTopWall = .5f * TopWallLength;
            // Right Wall
            for (int i = 0; i < num_Objects / 2; i++)
            {
                this_pyramid_position.x = (ChildObjectPlacement.x + UnityEngine.Random.Range(HalfTopWall, HalfTopWall + DepthofWall));
                this_pyramid_position.y = 2 * Math.Abs(UnityEngine.Random.Range(-HalfSideWall, HalfSideWall));
                this_pyramid_position.z = (UnityEngine.Random.Range(ChildObjectPlacement.z, Distance));

                GameObject clone = Instantiate(OrigObject, this_pyramid_position, transform.rotation * Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f))) as GameObject;

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

            for (int i = 0; i < markerData.Count; i++)
            {
                GameObject newMarker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                newMarker.name = markerData[i].Label;
                //newMarker.transform.parent = markerRoot.transform;
                //newMarker.transform.localScale = Vector3.one * markerScale;
                //newMarker.SetActive(false);
                markers.Add(newMarker);
            }
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
			init();
			rtClient = RTClient.GetInstance();
			//markers = new List<GameObject>();
			markerRoot = gameObject;

			PlayerPerspective = GameObject.Find("PlayerPerspective");

			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			if (rtClient == null) rtClient = RTClient.GetInstance();
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
					if (markerData[i].Label == HeadMarker)
					{
						HeadPosition_qtm_x = markerData[i].Position.z;
						HeadPosition_qtm.x = HeadPosition_qtm_x;
						HeadPosition_qtm_y = markerData[i].Position.y;
						HeadPosition_qtm.y = HeadPosition_qtm_y;
						HeadPosition_qtm_z = markerData[i].Position.x;
						HeadPosition_qtm.z = HeadPosition_qtm_z;
					}

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
					float.TryParse(word, out beltSpeed);
				}
				if (num_of_UDP_vals == 5) {
					float.TryParse (word, out falling_rotation_y_labview);
					falling_rotation_z_unity = falling_rotation_y_labview;
				}
				if (num_of_UDP_vals == 6) {
					float.TryParse (word, out Stim_Zone);
				}
			}
			num_of_UDP_vals = 0;

			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// // // // // // MOVE THE SCENE OBJECTS // // // // // // // //// // // // // // // //// // // // // // // //// // // // // // // //
			// Moves the original/ parent object, thus moving all children objects in the scene assigned to the parent object
			current_object_position = OrigObject.transform.position;//- ground_translation_z_unity - 50f; // 50 = starting position Parent Object in scene
			OrigObjectPosition[2] = (current_object_position[2]) - (beltSpeed * Time.deltaTime);
			OrigObject.transform.position = OrigObjectPosition;
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// // // // // // Move THE SCENE GROUND // // // // // // // //// // // // // // // //// // // // // // // //// // // // // // // 
			// Move the texture with speed of treadmill
			// need to implement beltspeed instead.. Are there problems with variable update with this?
	        //TextureOffset[1] = - ground_translation_z_unity/4f; // offset update based on scale of plane being used... find a way to automate..
			//TextureOffset_left[1] = -ground_translation_z_unity/2f;
			//TextureOffset_right[1] = -ground_translation_z_unity/2f;
			current_texture_offset = main_floor.material.GetTextureOffset("_MainTex");

			dt = Time.deltaTime;


			TextureOffset[1] = current_texture_offset[1] - (beltSpeed/10f * dt);// offset update based on scale of plane being used... find a way to automate..


			main_floor.material.SetTextureOffset("_MainTex", TextureOffset); // Get rid of New Vector...
			//left_path.material.SetTextureOffset("_MainTex", TextureOffset_left); // Get rid of New Vector...
			//right_path.material.SetTextureOffset("_MainTex", TextureOffset_right); // Get rid of New Vector...
            //left_path.material.SetColor("_Color", Color.green);
            //right_path.material.SetColor("_Color", Color.gray);

			left_walkway1.transform.Translate(0, 0, -beltSpeed * dt);
			left_walkway2.transform.Translate(0, 0, -beltSpeed * dt);
			left_walkway3.transform.Translate(0, 0, -beltSpeed * dt);
			left_walkway4.transform.Translate(0, 0, -beltSpeed * dt);
			left_walkway5.transform.Translate(0, 0, -beltSpeed * dt);
			left_walkway6.transform.Translate(0, 0, -beltSpeed * dt);
			left_walkway7.transform.Translate(0, 0, -beltSpeed * dt);
			left_walkway8.transform.Translate(0, 0, -beltSpeed * dt);
			left_walkway9.transform.Translate(0, 0, -beltSpeed * dt);

			right_walkway1.transform.Translate(0, 0, -beltSpeed * dt);
			right_walkway2.transform.Translate(0, 0, -beltSpeed * dt);
			right_walkway3.transform.Translate(0, 0, -beltSpeed * dt);
			right_walkway4.transform.Translate(0, 0, -beltSpeed * dt);
			right_walkway5.transform.Translate(0, 0, -beltSpeed * dt);
			right_walkway6.transform.Translate(0, 0, -beltSpeed * dt);
			right_walkway7.transform.Translate(0, 0, -beltSpeed * dt);
			right_walkway8.transform.Translate(0, 0, -beltSpeed * dt);
			right_walkway9.transform.Translate(0, 0, -beltSpeed * dt);

			center_walkway1.transform.Translate(0, 0, -beltSpeed * dt);
			center_walkway2.transform.Translate(0, 0, -beltSpeed * dt);
			center_walkway3.transform.Translate(0, 0, -beltSpeed * dt);
			center_walkway4.transform.Translate(0, 0, -beltSpeed * dt);
			center_walkway5.transform.Translate(0, 0, -beltSpeed * dt);
			center_walkway6.transform.Translate(0, 0, -beltSpeed * dt);
			center_walkway7.transform.Translate(0, 0, -beltSpeed * dt);
			center_walkway8.transform.Translate(0, 0, -beltSpeed * dt);
			center_walkway9.transform.Translate(0, 0, -beltSpeed * dt);


			left_walkway1_position = left_walkway1.transform.position;
			left_walkway2_position = left_walkway2.transform.position;
			left_walkway3_position = left_walkway3.transform.position;
			left_walkway4_position = left_walkway4.transform.position;
			left_walkway5_position = left_walkway5.transform.position;
			left_walkway6_position = left_walkway6.transform.position;
			left_walkway7_position = left_walkway7.transform.position;
			left_walkway8_position = left_walkway8.transform.position;
			left_walkway9_position = left_walkway9.transform.position;
		

			right_walkway1_position = right_walkway1.transform.position;
			right_walkway2_position = right_walkway2.transform.position;
			right_walkway3_position = right_walkway3.transform.position;
			right_walkway4_position = right_walkway4.transform.position;
			right_walkway5_position = right_walkway5.transform.position;
			right_walkway6_position = right_walkway6.transform.position;
			right_walkway7_position = right_walkway7.transform.position;
			right_walkway8_position = right_walkway8.transform.position;
			right_walkway9_position = right_walkway9.transform.position;

			center_walkway1_position = center_walkway1.transform.position;
			center_walkway2_position = center_walkway2.transform.position;
			center_walkway3_position = center_walkway3.transform.position;
			center_walkway4_position = center_walkway4.transform.position;
			center_walkway5_position = center_walkway5.transform.position;
			center_walkway6_position = center_walkway6.transform.position;
			center_walkway7_position = center_walkway7.transform.position;
			center_walkway8_position = center_walkway8.transform.position;
			center_walkway9_position = center_walkway9.transform.position;



			if (left_walkway1_position.z < -20f)
			{
				left_walkway1_position.z = left_walkway9_position.z + 10f;
				left_walkway1.transform.position = left_walkway1_position;
				left_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (left_walkway2_position.z < -20f)
			{
				left_walkway2_position.z = left_walkway1_position.z + 10f;
				left_walkway2.transform.position = left_walkway2_position;
				left_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (left_walkway3_position.z < -20f)
			{
				left_walkway3_position.z = left_walkway2_position.z + 10f;
				left_walkway3.transform.position = left_walkway3_position;
				left_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (left_walkway4_position.z < -20f)
			{
				left_walkway4_position.z = left_walkway3_position.z + 10f;
				left_walkway4.transform.position = left_walkway4_position;
				left_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (left_walkway5_position.z < -20f)
			{
				left_walkway5_position.z = left_walkway4_position.z + 10f;
				left_walkway5.transform.position = left_walkway5_position;
				left_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (left_walkway6_position.z < -20f)
			{
				left_walkway6_position.z = left_walkway5_position.z + 10f;
				left_walkway6.transform.position = left_walkway6_position;
				left_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (left_walkway7_position.z < -20f)
			{
				left_walkway7_position.z = left_walkway6_position.z + 10f;
				left_walkway7.transform.position = left_walkway7_position;
				left_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (left_walkway8_position.z < -20f)
			{
				left_walkway8_position.z = left_walkway7_position.z + 10f;
				left_walkway8.transform.position = left_walkway8_position;
				left_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (left_walkway9_position.z < -20f)
			{
				left_walkway9_position.z = left_walkway8_position.z + 10f;
				left_walkway9.transform.position = left_walkway9_position;
				left_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}


			left_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);

			if (center_walkway1_position.z < -20f)
			{
				center_walkway1_position.z = center_walkway9_position.z + 10f;
				center_walkway1.transform.position = center_walkway1_position;
			}
			if (center_walkway2_position.z < -20f)
			{
				center_walkway2_position.z = center_walkway1_position.z + 10f;
				center_walkway2.transform.position = center_walkway2_position;
			}
			if (center_walkway3_position.z < -20f)
			{
				center_walkway3_position.z = center_walkway2_position.z + 10f;
				center_walkway3.transform.position = center_walkway3_position;
			}
			if (center_walkway4_position.z < -20f)
			{
				center_walkway4_position.z = center_walkway3_position.z + 10f;
				center_walkway4.transform.position = center_walkway4_position;
			}
			if (center_walkway5_position.z < -20f)
			{
				center_walkway5_position.z = center_walkway4_position.z + 10f;
				center_walkway5.transform.position = center_walkway5_position;
			}
			if (center_walkway6_position.z < -20f)
			{
				center_walkway6_position.z = center_walkway5_position.z + 10f;
				center_walkway6.transform.position = center_walkway6_position;
			}
			if (center_walkway7_position.z < -20f)
			{
				center_walkway7_position.z = center_walkway6_position.z + 10f;
				center_walkway7.transform.position = center_walkway7_position;
			}
			if (center_walkway8_position.z < -20f)
			{
				center_walkway8_position.z = center_walkway7_position.z + 10f;
				center_walkway8.transform.position = center_walkway8_position;
			}
			if (center_walkway9_position.z < -20f)
			{
				center_walkway9_position.z = center_walkway8_position.z + 10f;
				center_walkway9.transform.position = center_walkway9_position;
			}

			if (right_walkway1_position.z < -20f)
			{
				right_walkway1_position.z = right_walkway9_position.z + 10f;
				right_walkway1.transform.position = right_walkway1_position;
				right_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (right_walkway2_position.z < -20f)
			{
				right_walkway2_position.z = right_walkway1_position.z + 10f;
				right_walkway2.transform.position = right_walkway2_position;
				right_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (right_walkway3_position.z < -20f)
			{
				right_walkway3_position.z = right_walkway2_position.z + 10f;
				right_walkway3.transform.position = right_walkway3_position;
				right_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (right_walkway4_position.z < -20f)
			{
				right_walkway4_position.z = right_walkway3_position.z + 10f;
				right_walkway4.transform.position = right_walkway4_position;
				right_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (right_walkway5_position.z < -20f)
			{
				right_walkway5_position.z = right_walkway4_position.z + 10f;
				right_walkway5.transform.position = right_walkway5_position;
				right_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (right_walkway6_position.z < -20f)
			{
				right_walkway6_position.z = right_walkway5_position.z + 10f;
				right_walkway6.transform.position = right_walkway6_position;
				right_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (right_walkway7_position.z < -20f)
			{
				right_walkway7_position.z = right_walkway6_position.z + 10f;
				right_walkway7.transform.position = right_walkway7_position;
				right_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (right_walkway8_position.z < -20f)
			{
				right_walkway8_position.z = right_walkway7_position.z + 10f;
				right_walkway8.transform.position = right_walkway8_position;
				right_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}
			if (right_walkway9_position.z < -20f)
			{
				right_walkway9_position.z = right_walkway8_position.z + 10f;
				right_walkway9.transform.position = right_walkway9_position;
				right_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.grey);
			}

			if (Stim_Zone == 1)
			{
				if (left_walkway1_position[2] >= 40 && left_walkway1_position[2] <= 50) {
					left_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				}
				if (left_walkway2_position[2] >= 40 && left_walkway2_position[2] <= 50) {

					left_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				}
				if (left_walkway3_position[2] >= 40 && left_walkway3_position[2] <= 50) {

					left_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				}
				if (left_walkway4_position[2] >= 40 && left_walkway4_position[2] <= 50) {
					left_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
					//	right_walkway4.material.SetColor("_Color", Color.red);
				}
				if (left_walkway5_position[2] >= 40 && left_walkway5_position[2] <= 50) {
					left_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				//	right_walkway5.material.SetColor("_Color", Color.red);
				}
				if (left_walkway6_position[2] >= 40 && left_walkway6_position[2] <= 50) {
					left_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				//	right_walkway6.material.SetColor("_Color", Color.red);
				}
				if (left_walkway7_position[2] >= 40 && left_walkway7_position[2] <= 50) {
					left_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				//	right_walkway7.material.SetColor("_Color", Color.red);
				}
				if (left_walkway8_position[2] >= 40 && left_walkway8_position[2] <= 50) {
					left_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				//	right_walkway8.material.SetColor("_Color", Color.red);
				}
				if (left_walkway9_position[2] >= 40 && left_walkway9_position[2] <= 50) {
					left_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				//	right_walkway9.material.SetColor("_Color", Color.red);
				}
			}

			if (Stim_Zone == 2) {
				
				if (right_walkway1_position [2] >= 40 && right_walkway1_position [2] <= 50) {
					right_walkway1.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				}
				if (right_walkway2_position [2] >= 40 && right_walkway2_position [2] <= 50) {

					right_walkway2.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				}
				if (right_walkway3_position [2] >= 40 && right_walkway3_position [2] <= 50) {

					right_walkway3.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				}
				if (right_walkway4_position [2] >= 40 && right_walkway4_position [2] <= 50) {
					right_walkway4.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
					//	right_walkway4.material.SetColor("_Color", Color.red);
				}
				if (right_walkway5_position [2] >= 40 && right_walkway5_position [2] <= 50) {
					right_walkway5.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
					//	right_walkway5.material.SetColor("_Color", Color.red);
				}
				if (right_walkway6_position [2] >= 40 && right_walkway6_position [2] <= 50) {
					right_walkway6.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
					//	right_walkway6.material.SetColor("_Color", Color.red);
				}
				if (right_walkway7_position [2] >= 40 && right_walkway7_position [2] <= 50) {
					right_walkway7.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
					//	right_walkway7.material.SetColor("_Color", Color.red);
				}
				if (right_walkway8_position [2] >= 40 && right_walkway8_position [2] <= 50) {
					right_walkway8.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
					//	right_walkway8.material.SetColor("_Color", Color.red);
				}
				if (right_walkway9_position [2] >= 40 && right_walkway9_position [2] <= 50) {
					right_walkway9.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
					//	right_walkway9.material.SetColor("_Color", Color.red);
				}
			}

			////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// // // // // // MOVE PERSPECTIVE CAMERA // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	        // Moves the "Render Chanel Camera" based on real world coordinates gathered from Nexus(motion capture)
			// update camera position based on labview or qtm data stream
			if (QTM == true)
			{
				PlayerPerspective.transform.position = HeadPosition_qtm;
			}
			if (QTM == false)
			{
				PlayerPerspective.transform.position = HeadPosition_labview;
			}

			//CameraPosition.x = camera_translation_x_unity;
			//CameraPosition.y = camera_translation_y_unity - .03f;
			//CameraPosition.z = camera_translation_z_unity; 
			//CameraObject.transform.position = CameraPosition;
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// // // // // // SET DOME/OMNITY POSITION // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //  
			// the values are a manually measure distance from the world/nexus/forceplate origin to the middle of the screen/dome 
			// DomePosition.y = 1.8f;
			// DomePosition.z = 2f;
			// OmnityDomeTransform.position = DomePosition;
			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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