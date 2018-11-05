using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine.VR;

namespace QualisysRealTime.Unity
{
    public class QTMSDK_SceneControl : MonoBehaviour
    {
        private List<LabeledMarker> markerData;
		// qtm demo test
		private string HeadMarker = "L-frame - 1";
		// from marker set
        //private string HeadMarker = "LFHD";
        private RTClient rtClient;
        private GameObject markerRoot;
        private List<GameObject> markers;

        public GameObject PlayerPerspective;
        private Vector3 HeadPosition_labview;
        private float HeadPosition_labview_x;
        private float HeadPosition_labview_y;
        private float HeadPosition_labview_z;

        private Vector3 HeadPosition_qtm;
        private float HeadPosition_qtm_x;
        private float HeadPosition_qtm_y;
        private float HeadPosition_qtm_z;

        // Treadmill//
        private float beltSpeed;
        private float ground_translation_y;
        private float falling_rotation_y_labview;

        // Tunnel //
        private float pos_z1;
        private float pos_z2;
        private float pos_z3;
        private float pos_z4;
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

        //Data shown on Inspector //
        private string text = "";
        //received text
        [Header("UDP LabView Connection")]
        public string IP = "192.168.61.73";
        public int port = 6843;
        public string lastReceivedUDPPacket = "";

        //UDP route//
        [Header("QTM")]
        public bool QTM;

        private bool streaming = false;

        // Use this for initialization
        void Start()
        {
            init();
            rtClient = RTClient.GetInstance();
            markers = new List<GameObject>();
            markerRoot = gameObject;

            PlayerPerspective = GameObject.Find("PlayerPerspective");

            Plane1 = GameObject.Find("Plane1");
            Plane2 = GameObject.Find("Plane2");
            Plane3 = GameObject.Find("Plane3");
            Plane4 = GameObject.Find("Plane4");
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

        // receive thread
        private void ReceiveData()
        {
            client = new UdpClient(port);
            while (true)
            {
                try
                {
                    // Bytes received.
                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = client.Receive(ref anyIP);
                    text = Encoding.UTF8.GetString(data);
                    lastReceivedUDPPacket = text;

                }
                catch (Exception err)
                {
                    print(err.ToString());
                }
            }
        }



        void Update()
        {
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

            char[] delimiter1 = new char[] { ',' };
            var strvalues = lastReceivedUDPPacket.Split(delimiter1, StringSplitOptions.None);

            foreach (string word in strvalues)
            {
                UDP_values++;

                // Qualysis x direction
                if (UDP_values == 1)
                {
                    float.TryParse(word, out HeadPosition_labview_x);
                    HeadPosition_labview.x = HeadPosition_labview_x / 1000;
                    //ParticipantPosition.x = HeadPosition.x;
                }

                //Qualysis y direction = AP direction
                if (UDP_values == 2)
                {
                    float.TryParse(word, out HeadPosition_labview_y);
                    HeadPosition_labview.z = HeadPosition_labview_y / 1000;
                    //ParticipantPosition.z = HeadPosition.y;
                    // NOT USED IN TREADMILL - AP DIRECTION //

                }
                //Qualysis z direction = Vertical
                if (UDP_values == 3)
                {
                    float.TryParse(word, out HeadPosition_labview_z);
                    HeadPosition_labview.y = HeadPosition_labview_z / 1000;
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

            // update camera position based on labview or qtm data stream
            if (QTM == true)
            {
                PlayerPerspective.transform.position = HeadPosition_qtm;
            }
            if (QTM == false)
            {
                PlayerPerspective.transform.position = HeadPosition_labview;
            }

            // visual perturbation
            //PlayerPerspective.transform.localRotation.z = falling_rotation_y_labview;

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
        }
    }
}
