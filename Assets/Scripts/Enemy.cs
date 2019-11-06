using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100; //don't you have to put an f after a float???

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>(); //retrieves the DamageDealer info from the object being collided with
        health -= damageDealer.GetDamage();//retrieves damage data from DamageDealer script and subtracts it from enemy health
    }
}
