using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints; // The type is Transform because that is the only data type one the waypoint gameObject we are interested in
    int waypointIndex = 0;//


    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints(); //gets GetWaypoints from waveConfig script
        transform.position = waypoints[waypointIndex].transform.position; // this lines spawns the enemy object at the first waypoint on the path(index 0)

    }

    // Update is called once per frame
    void Update()
    {   
        Move();
    }

    //this next method is meant to get waveConfig data from another script
    public void SetWaveConfig(WaveConfig waveConfig) //this waveConfig is a seperate variable from the waveConfig above. It is local to this function only.
    {
        this.waveConfig = waveConfig;// this. is used when dealing with a local and global varianble of the same name - this. is refering to the BIG waveConfig outside of this function.
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1) 
        // we use .Count because waypoints is a List. If it was an array, we would use .Length
        {
            var targetPostion = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPostion, movementThisFrame);//MoveTowards allows us to move to a specified position

            if (transform.position == targetPostion)
            {
                waypointIndex++;
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
