using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints; // The type is Transform because that is the only data type one the waypoint gameObject we are interested in
    [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;//


    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position; // this lines spawns the enemy object at the first waypoint on the path(index 0)

    }

    // Update is called once per frame
    void Update()
    {   
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1) 
        // we use .Count because waypoints is a List. If it was an array, we would use .Length
        {
            var targetPostion = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPostion, movementThisFrame);

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
