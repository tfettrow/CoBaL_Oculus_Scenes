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
	    public Renderer main_floor;
		public Renderer left_path;
		public Renderer right_path;

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

		// Treadmill//
		private float beltSpeed;
		private float ground_translation_y;
		private float falling_rotation_y_labview;
		private float ground_translation_y_labview;
		private float ground_translation_z_unity;


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


	    void Awake() {
	        // Find the game ojbects
	        OrigObject = GameObject.Find("OrigCube");
			ChildObjectPlacement = GameObject.Find ("Object Placement Origin").transform.position;
			SceneObject = GameObject.Find ("SceneOrigin");
			PlayerPerspective = GameObject.Find("PlayerPerspective");

			init();
			rtClient = RTClient.GetInstance();
			markers = new List<GameObject>();
			markerRoot = gameObject;

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

	    public void Start(){
		 	////////// ******* Why doesn't this get assigned here??? ///////////////////////////
	        // Find the Camera within omnity script (takes a while to load so may not grab it here)

			init();
			rtClient = RTClient.GetInstance();
			markers = new List<GameObject>();
			markerRoot = gameObject;

			PlayerPerspective = GameObject.Find("PlayerPerspective");

			////////////////////////////////////////////////////////////////////////////////////


			// // // // // INSTANTIATE SCENE OBJECTS // // // // // // // // // // // // // // // // // // // // //// // // // // // // // // // // // // //// // // // // // // // // // // // // // 
	        HalfSideWall = .5f * LeftRightSideLength;
			HalfTopWall = .5f * TopWallLength;
	            // Right Wall
				for (int i = 0; i < num_Objects / 2; i++){
					this_pyramid_position.x = (ChildObjectPlacement.x + UnityEngine.Random.Range(HalfTopWall, HalfTopWall + DepthofWall));
					this_pyramid_position.y = 2*Math.Abs(UnityEngine.Random.Range(-HalfSideWall, HalfSideWall));
					this_pyramid_position.z = (UnityEngine.Random.Range(ChildObjectPlacement.z, Distance));
					
						GameObject clone = Instantiate(OrigObject, this_pyramid_position, transform.rotation * Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f))) as GameObject;
					
	            }
	            // Left Wall
				for (int i = num_Objects / 2; i < num_Objects; i++){
					this_pyramid_position.x = (ChildObjectPlacement.x - UnityEngine.Random.Range(HalfTopWall, HalfTopWall + DepthofWall)); 
	                this_pyramid_position.y = 2*Math.Abs(UnityEngine.Random.Range(-HalfSideWall, HalfSideWall));
					this_pyramid_position.z = (UnityEngine.Random.Range(ChildObjectPlacement.z, Distance));
					
						GameObject clone = Instantiate(SquarePrefab, this_pyramid_position, transform.rotation * Quaternion.Euler(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f))) as GameObject;
					
	            }
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


			// // // // // // // // UDP SETUP // // // // // // // //// // // // // // // //// // // // // // // //
	            port = 6843;
	            receiveThread = new Thread(
	            new ThreadStart(ReceiveData));
	            receiveThread.IsBackground = true;
	            receiveThread.Start();
			///////////////////////////////////////////////////////////////////////////////////////////////////////// 
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
			markers = new List<GameObject>();
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

			if (markers.Count != markerData.Count)
			{
				InitiateMarkers();
			}
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

					markers[i].name = markerData[i].Label;
					//markers[i].GetComponent<Renderer>().material.color = markerData[i].Color;
					//markers[i].transform.localPosition = markerData[i].Position;
					// markers[i].SetActive(true);
					// markers[i].GetComponent<Renderer>().enabled = visibleMarkers;
					// markers[i].transform.localScale = Vector3.one * markerScale;
				}
				else
				{
					//hide markers if we cant find them.
					markers[i].SetActive(false);
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
			}
			num_of_UDP_vals = 0;

			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// // // // // // MOVE THE SCENE OBJECTS // // // // // // // //// // // // // // // //// // // // // // // //// // // // // // // //
			// Moves the original/ parent object, thus moving all children objects in the scene assigned to the parent object
			OrigObjectPosition.z = - ground_translation_z_unity - 50f; // 50 = starting position Parent Object in scene
			OrigObject.transform.position = OrigObjectPosition;
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			// // // // // // Move THE SCENE GROUND // // // // // // // //// // // // // // // //// // // // // // // //// // // // // // // 
			// Move the texture with speed of treadmill
			// need to implement beltspeed instead.. Are there problems with variable update with this?
	        TextureOffset[1] = - ground_translation_z_unity/4f; // offset update based on scale of plane being used... find a way to automate..
			TextureOffset_left[1] = -ground_translation_z_unity/2f;
			TextureOffset_right[1] = -ground_translation_z_unity/2f;
			main_floor.material.SetTextureOffset("_MainTex", TextureOffset); // Get rid of New Vector...
			left_path.material.SetTextureOffset("_MainTex", TextureOffset_left); // Get rid of New Vector...
			right_path.material.SetTextureOffset("_MainTex", TextureOffset_right); // Get rid of New Vector...

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