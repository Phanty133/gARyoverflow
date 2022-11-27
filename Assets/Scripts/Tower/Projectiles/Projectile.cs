using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    public Enemy target;
    public BulletTower parent;
    public float velocity;
    public float damage;

    public int charges;
    public float lifetime;

    // Update is called once per frame
    void Update()
    {
        if(charges <= 0 || lifetime <= 0) Destroy(gameObject);

        if(target == null) {
            target = parent.FindNewTarget(transform.position);
            if(target == null) Destroy(gameObject);
        }

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, velocity * Time.deltaTime);

        lifetime -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        enemy.Damage(damage);
        charges--;
    }
}
