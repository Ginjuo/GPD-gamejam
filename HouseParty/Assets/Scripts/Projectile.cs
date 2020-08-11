﻿using UnityEngine;

namespace Assets.Scripts
{
    public class Projectile : MonoBehaviour
    {
        // Start is called before the first frame update
        Rigidbody2D rigidbody2d;

        public AudioClip collissionClip;

        public ParticleSystem spark;

        void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        public void Launch(Vector2 direction, float force)
        {
            rigidbody2d.AddForce(direction * force);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            Instantiate(spark, rigidbody2d.position, Quaternion.identity);
        
            //we also add a debug log to know what the projectile touch
            EnemyController e = other.collider.GetComponent<EnemyController>();
        
            Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.magnitude > 1000.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
