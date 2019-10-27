using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config parameters
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f; // to stop the player from going partially off screen
    [SerializeField] GameObject laserPrefab; //all prefabs are of game object type
    [SerializeField] float projectileSpeed = 20f;

    float xMin;
    float xMax;

    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }



    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject laser = Instantiate
                (laserPrefab, transform.position, Quaternion.identity) //Quaternion.identity means just use the rotation that you have
                    as GameObject; // what does this mean?????
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        }
    }


    //functions
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; // this line is gets the input from unity's vertical axis
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed; //time.deltaTime is used to make the game's frame rate consistent across different PCs

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding; // we have to use a vector3 here because in a 2D game, cameras exist in a 3D space
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding; // we have to use a vector3 here because in a 2D game, cameras exist in a 3D space
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
