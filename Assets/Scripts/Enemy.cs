using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100; //don't you have to put an f after a float???
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyProjectileSpeed = 10f;
    [SerializeField] GameObject enemyExplosionParticles;
    [SerializeField] float enemyExplosionDuration = 1f;
    [SerializeField] AudioClip enemyDeathSFX;
    [SerializeField] [Range(0,1)] float deathSFXVolume = 0.7f;
    [SerializeField] AudioClip enemyLaserSFX;
    [SerializeField] [Range(0, 1)] float EnemyLaserSFXVolume = 0.7f;



    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <=0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        }
    }

    private void Fire()
    {
        GameObject EnemyLaser = Instantiate
            (enemyLaserPrefab, transform.position, Quaternion.identity) //Quaternion.identity means just use the rotation that you have
                as GameObject; // what does this mean?????
        EnemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyProjectileSpeed);
        AudioSource.PlayClipAtPoint(enemyLaserSFX, Camera.main.transform.position, EnemyLaserSFXVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>(); //retrieves the DamageDealer info from the object being collided with
        if (!damageDealer) { return; } //protects us against null references 
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();//retrieves damage data from DamageDealer script and subtracts it from enemy health
        damageDealer.Hit();
        if (health <= 0)
        {
            enemyDies();
        }
    }



    private void enemyDies()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(enemyExplosionParticles, transform.position, transform.rotation);
        Destroy(explosion, enemyExplosionDuration);
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
