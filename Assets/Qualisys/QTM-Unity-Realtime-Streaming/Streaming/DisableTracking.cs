using System.Collections;
using System.Collections.Generic;
//using System.Collections.Generic.Queue;
using UnityEngine;
using QualisysRealTime.Unity;
using System.Linq;
public class DisableTracking : MonoBehaviour {
    private Camera cam;

    public Vector3 HeadRotation_mocap;
    public Vector3 HeadRotation_imu;
	public Vector3 adjustment_vector;
	public float angle_difference;
    
	//public float VRreset;
    //public float Adjustment = 0;
	public bool Take_Mocap_Avg;
	public bool Correct_Gryo_Drift;
	public bool Buffer_Data;
	public float correct_gyro;

    // private Vector3 startPos;
    //private Vector3 
    // Use this for initialization
	public float MaxCapacity = 500;

	Queue<float> mocap_data = new Queue<float>();
	//public float queue_display;
	public float mocap_data_avg;
	public float mocap_data_sum;
	public float mocap_data_count;

	Queue<float> imu_data = new Queue<float>();
	//public float queue_display;
	public float imu_data_avg;
	public float imu_data_sum;
	public float imu_data_count;

	void Start () 
	{
        cam = GetComponentInChildren <Camera>();
        //startPos = transform.localPosition;

        //HeadRotation = (Vector3)GameObject.Find("SceneControls").GetComponent<QualisysRealTime.Unity.Infinite_World_LR_Game>().HeadRotation_qtm;
        //VRreset = GameObject.Find("SceneControls").GetComponent<QualisysRealTime.Unity.Infinite_World_LR_Game>().VRreset;

		//GameObject go = GameObject.Find ("SceneControls");
		//QualisysRealTime.Unity.Infinite_World_LR_Game infinite_World_LR_Game = go.GetComponent <QualisysRealTime.Unity.Infinite_World_LR_Game> ();
		//correct_gyro = infinite_World_LR_Game.VRreset;
        
		//correct_gyro = Infinite_World_LR_Game.VRreset;

		//QualisysRealTime.Unity.Infinite_World_LR_Game>().HeadRotation_qtm;
        //    Infinite_World_LR.GetComponent<HeadRotation_qtm>;
    }

    // Update is called once per frame
    void Update() {
        //transform.rotation = Quaternion.Inverse(cam.transform.localRotation);

        //transform.localEulerAngles = HeadRotation;
        HeadRotation_mocap = transform.eulerAngles;
        HeadRotation_imu = cam.transform.eulerAngles;

		correct_gyro = Infinite_World_LR_Game.correct_gyro_infiniteworld;

		if(correct_gyro == 1)
        {
			Buffer_Data = true;
            //Adjustment = Adjustment + 5;
            //HeadRotation_mocap.y = HeadRotation_mocap.y + Adjustment;
        }
		if (Buffer_Data) 
		{
			if (mocap_data.Count >= MaxCapacity)
			{
				mocap_data.Dequeue ();
				imu_data.Dequeue ();
				Buffer_Data = false;
				Take_Mocap_Avg = true;
			}
			mocap_data.Enqueue (HeadRotation_mocap.y);
			imu_data.Enqueue (HeadRotation_imu.y);
		}
		if (Take_Mocap_Avg)
		{
			// take average of mocap
			//mocap_data_sum = mocap_data.Sum();
			//mocap_data_count = mocap_data.Count();
			mocap_data_avg = mocap_data.Sum() / mocap_data.Count();
			imu_data_avg = imu_data.Sum () / imu_data.Count ();
			// take average of imu

			//return mocap_data;

			Take_Mocap_Avg = false;
			Correct_Gryo_Drift = true;
		}

		if (Correct_Gryo_Drift) 
		{


			//mocap_data

			// determine difference
			//angle_difference = HeadRotation_imu.y - HeadRotation_mocap.y;
			//adjustment_vector.y = adjustment_vector.y + -angle_difference;

			angle_difference = imu_data_avg - mocap_data_avg;
			adjustment_vector.y = -angle_difference;

			// apply inverse difference to parent transform (this one)
			transform.eulerAngles = adjustment_vector;
			Correct_Gryo_Drift = false;
		}



		//if (correct_gyro == 0) 
		//{
			//Correct_Gryo_Drift = false;
		//}
        //transform.eulerAngles = HeadRotation_mocap;
    }
}
