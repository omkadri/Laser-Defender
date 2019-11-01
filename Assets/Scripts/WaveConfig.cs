using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{   //when using scriptable objects, serialized floats and integers will not show up on the script in the editor (only seriealized prefab types)
    //they will however show up on the instance of the scriptable object
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor;//what is randomly spawning??????????????
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;// speed of wave? or speed of enemies within wave???????????????????

    
    
    // these next set of functions create a reference point to access each respective object from other scripts
    public GameObject GetEnemyPrefab() {return enemyPrefab; }

    //this next method gets us the transform data on all available objects in the path prefab
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)//child is refering to objects within pathPrefab with transform data (in this case, waypoints)
        {
            waveWaypoints.Add(child);// adds transform data of child into waveWaypoints(this is a benefit of using a list)

        }
        return waveWaypoints;//this data know contains the transform data of each waypoint from pathPrefab
    }
    public float GettimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }
}
