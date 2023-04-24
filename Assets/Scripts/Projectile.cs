using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public int speed;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime));
    }

    public void SetProjectile(Vector2 newDirection, int newSpeed, int newDamage) 
    {
        direction = newDirection;
        speed = newSpeed;
        damage = newDamage;
    }
}
