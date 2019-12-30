using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config parameters
    [Header ("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f; // to stop the player from going partially off screen
    [SerializeField] float playerHealth = 1000;

    [Header ("Projectile")]
    [SerializeField] GameObject laserPrefab; //all prefabs are of game object type
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFireRate = 0.3f;

    [SerializeField] AudioClip playerLaserSFX;
    [SerializeField] AudioClip playerDeathSFX;

    [SerializeField] [Range(0, 1)] float PlayerLaserSFXVolume = 0.7f;
    [SerializeField] [Range(0, 1)] float PlayerDeathSFXVolume = 0.7f;



    Coroutine autoFireCoroutine;

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
            autoFireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(autoFireCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true) // we have to put the coroutine in a while loop because it i occurs continously while the input butto is pressed.
        {
            GameObject laser = Instantiate
             (laserPrefab, transform.position, Quaternion.identity) //Quaternion.identity means just use the rotation that you have
                 as GameObject; // what does this mean?????
            AudioSource.PlayClipAtPoint(playerLaserSFX, Camera.main.transform.position, PlayerLaserSFXVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFireRate); //when using WaitForSeconds for the first time, we must use new beforehand for syntax
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>(); //retrieves the DamageDealer info from the object being collided with
        if (!damageDealer) { return; } //protects us against null references 
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        playerHealth -= damageDealer.GetDamage();//retrieves damage data from DamageDealer script and subtracts it from enemy health
        damageDealer.Hit();//destroys laser prefab after collision
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position, PlayerDeathSFXVolume);
            
        }
    }
}
