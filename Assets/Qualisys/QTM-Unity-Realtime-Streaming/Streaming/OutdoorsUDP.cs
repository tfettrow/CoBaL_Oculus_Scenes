using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using UnityEngine.SceneManagement;
using UnityEngine.VR;

public class OutdoorsUDP : MonoBehaviour {

    //LabView Code//
    [Header("LabView")]
    public bool SoccerLabView;

    [Header("Participant")]
    public GameObject Participant;
	public Vector3 ParticipantPosition;
	public Vector3 HeadPosition;

    // Treadmill//
      
    private float beltSpeed;
    private float ground_translation_y;
    private float falling_rotation_y_labview;

    // Tunnel //
    private float pos_z1;
	private float pos_z2;
	private float pos_z3;
	private float pos_z4;

	private GameObject controllers;
    [Header("Enviroment")]  
	public GameObject Plane1;
	public GameObject Plane2;
	public GameObject Plane3;
	public GameObject Plane4;

	// Receiving Thread //
	Thread receiveThread;
	private float UDP_section;
	private float UDP_values;
	UdpClient client;
    UdpClient Qualisys;

	//Data shown on Inspector //
	private string text = "";
    //received text
    [Header("UDP LabView Connection")]
    public string IP = "192.168.61.73";
	public int port = 6843;	
	public string lastReceivedUDPPacket = "";

    [Header("UDP Qualisys Connection")]
    public string QTMIp = "Waiting for development";
    public int QTMPort;
    public string QTMLastReceived = "";

    // start //
    private static void Main ()
	{
        OutdoorsUDP receiveObj = new OutdoorsUDP();
		receiveObj.init ();			 
		string text = "";
		do {
			text = Console.ReadLine (); // Shows text received in the Console
		} while(!text.Equals ("exit"));
	}

	// Use this for initialization
	void Start () {

        SoccerLabView = false;
		init ();

        Participant = GameObject.Find("Participant");

        controllers = GameObject.Find("Controller");

		Plane1 = GameObject.Find("Plane1");
		Plane2 = GameObject.Find("Plane2");
		Plane3 = GameObject.Find("Plane3");
		Plane4 = GameObject.Find("Plane4");
	
	}

	// UDP Start //
	private void init ()
	{
		// ----------------------------
		// Listener
		// ----------------------------
		port = 6843;
		receiveThread = new Thread (new ThreadStart (ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start ();			 
	}

	// receive thread
	private  void ReceiveData ()
	{			 
		client = new UdpClient (port);
		while (true) {				 
			try {
				// Bytes received.
				IPEndPoint anyIP = new IPEndPoint (IPAddress.Any, 0);
				byte[] data = client.Receive (ref anyIP);		
				text = Encoding.UTF8.GetString (data);
				lastReceivedUDPPacket = text;

			} catch (Exception err) {
				print (err.ToString ());
			}
		}
	}
	// getLatestUDPPacket and cleans up what was already read //
	public string getLatestUDPPacket ()
	{
		return lastReceivedUDPPacket;
	}


	// Update is called once per frame
	void Update () {
        if (SoccerLabView == true)
        {
            char[] delimiter1 = new char[] { ',' };
            var strvalues = lastReceivedUDPPacket.Split(delimiter1, StringSplitOptions.None);

            foreach (string word in strvalues)
            {
                UDP_values++;

                // Qualysis x direction
                if (UDP_values == 1)
                {
                    float.TryParse(word, out HeadPosition.x);
                    ParticipantPosition.x = HeadPosition.x / 1000;
                    //ParticipantPosition.x = HeadPosition.x;
                }

                //Qualysis y direction = AP direction
                if (UDP_values == 2)
                {
                    float.TryParse(word, out HeadPosition.y);
                    ParticipantPosition.z = HeadPosition.y / 1000;
                    //ParticipantPosition.z = HeadPosition.y;
                    // NOT USED IN TREADMILL - AP DIRECTION //

                }
                //Qualysis z direction = Vertical
                if (UDP_values == 3)
                {
                    float.TryParse(word, out HeadPosition.z);
                    ParticipantPosition.y = HeadPosition.z / 1000;
                    //ParticipantPosition.y = HeadPosition.z;
                }
                if (UDP_values == 4)
                {
                    // float.TryParse (word, out beltSpeed);
                    float.TryParse(word, out beltSpeed);
                }
                if (UDP_values == 5)
                {
                    float.TryParse(word, out falling_rotation_y_labview);
                }

            }

            UDP_values = 0;
        }
        if (SoccerLabView == false)
        {
            char[] delimiter1 = new char[] { ',' };
            var strvalues = lastReceivedUDPPacket.Split(delimiter1, StringSplitOptions.None);

            foreach (string word in strvalues)
            {
                UDP_values++;

                // Qualysis x direction
                if (UDP_values == 1)
                {
                    float.TryParse(word, out HeadPosition.x );
                    
                   ParticipantPosition.x = HeadPosition.x / 1000;
                }

                //Qualysis y direction = AP direction
                if (UDP_values == 2)
                {
                    float.TryParse(word, out HeadPosition.y);

                   ParticipantPosition.z = HeadPosition.y / 1000;
                    // NOT USED IN TREADMILL - AP DIRECTION //

                }
                //Qualysis z direction = Vertical
                if (UDP_values == 3)
                {
                    float.TryParse(word, out HeadPosition.z);
                  
                    ParticipantPosition.y = HeadPosition.z / 1000;
                }
                if (UDP_values == 4)
                {
                    // float.TryParse (word, out beltSpeed);
                    float.TryParse(word, out beltSpeed);
                }
                if (UDP_values == 5)
                {
                    float.TryParse(word, out falling_rotation_y_labview);
                }

            }

            UDP_values = 0;
        }

        // update player view //
        Participant.transform.position = ParticipantPosition;

        // Tunnel Movement //
        pos_z1 = Plane1.transform.position.z;
		pos_z2 = Plane2.transform.position.z;
		pos_z3 = Plane3.transform.position.z;
		pos_z4 = Plane4.transform.position.z;


		Plane1.transform.Translate(0, 0, -beltSpeed * Time.deltaTime);
        Plane2.transform.Translate(0, 0, -beltSpeed * Time.deltaTime);
        Plane3.transform.Translate(0, 0, -beltSpeed * Time.deltaTime);
        Plane4.transform.Translate(0, 0, -beltSpeed * Time.deltaTime);


        if (pos_z1 < -10.2f)
		{
			Plane1.transform.position = new Vector3(0, 0, pos_z4 + 10.2f);
		}
		if (pos_z2 < -10.2f)
		{
			Plane2.transform.position = new Vector3(0, 0, pos_z1 + 10.2f);
		}
		if (pos_z3 < -10.2f)
		{
			Plane3.transform.position = new Vector3(0, 0, pos_z2 + 10.2f);
		}
		if (pos_z4 < -10.2f)
		{
			Plane4.transform.position = new Vector3(0, 0, pos_z3 + 10.2f);
		}
		if (Input.GetKeyDown(KeyCode.R)){

			SceneManager.LoadScene (0);
		}
	
	}

	void OnDisable ()
	{
		if (receiveThread != null)
			receiveThread.Abort ();
		client.Close ();
	}
}
