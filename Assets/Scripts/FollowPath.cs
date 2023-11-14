using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaypointLab
{
    public class FollowPath : MonoBehaviour
    {
        // public GameObject[] waypoints;
        int currentWP = 0;
        private GameObject[] waypoints;
        public CourseRoute courseRoute;
        GameObject tracker;
        public float speed = 10.0f;
        public float rotSpeed = 10.0f;
        public float lookAhead = 10.0f;

        // Start is called before the first frame update
        void Start()
        {
            waypoints = courseRoute.waypoints;
            tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            DestroyImmediate(tracker.GetComponent<Collider>());

            tracker.transform.position = this.transform.position;
            tracker.transform.rotation = this.transform.rotation;       //22. Determine the direction an object should rotate to and store it in a variable

        }
        //End Of Start

        // Update is called once per frame
        void Update()
        {
            ProgressTracker();
                

            //this.transform.LookAt(waypoints[currentWP].transform);

            Quaternion lookatWP = Quaternion.LookRotation(waypoints[currentWP].transform.position - this.transform.position);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookatWP, rotSpeed * Time.deltaTime);

            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        //End Of Update

        void ProgressTracker ()
        {
            float distance = Vector3.Distance(tracker.transform.position, this.transform.position);     //15. Determine the distance between 2 objects
            if (distance > lookAhead) return; 

            if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 3)
                currentWP++;

            if (currentWP >= waypoints.Length)
                currentWP = 0;

            tracker.transform.LookAt(waypoints[currentWP].transform);
            tracker.transform.Translate(0.0f, 0.0f, (speed + 20.0f) * Time.deltaTime);

        }
        //End Of ProgressTracker

    }
}
