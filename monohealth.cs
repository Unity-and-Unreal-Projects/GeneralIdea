using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    [Serializable]
    public struct BodyPart
    {
        public string name;
        public Vector3 position;
    }

    public float totalHealth = 100f;
    public BodyPart[] bodyParts;
    public Vector3 spineVector;

    void Start()
    {
        // Initialize the array with body parts and their positions
        bodyParts = new BodyPart[]
        {
            new BodyPart { name = "head", position = new Vector3(0, 2, 0) },
            new BodyPart { name = "neck", position = new Vector3(0, 1.5f, 0) },
            new BodyPart { name = "spine", position = new Vector3(0, 1.3f, 0) },
            new BodyPart { name = "throat", position = new Vector3(0, 1.4f, 0) },
            new BodyPart { name = "left shoulder", position = new Vector3(-0.5f, 1.5f, 0) },
            new BodyPart { name = "abdomen", position = new Vector3(0, 1, 0) }
        };

        // Initialize the spine vector
        spineVector = new Vector3(0, 1.5f, 0);
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            foreach (var part in bodyParts)
            {
                if (hit.point == part.position)
                {
                    TakeDamage(ref totalHealth, 10, part.name);
                    Debug.Log("Damaged: " + part.name + ", Total Health = " + totalHealth);
                }
            }
        }
    }

    void TakeDamage(ref float health, int damage, string hitPart)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0.001f; // Set to a minimal value
        }

        if (hitPart == "neck" || hitPart == "throat" || hitPart == "spine")
        {
            Vector3 hitPartPosition = Array.Find(bodyParts, part => part.name == hitPart).position;
            if (Vector3.Distance(spineVector, hitPartPosition) < 0.1f)
            {
                Paralyze();
            }
        }
    }

    void Paralyze()
    {
        Debug.Log("Paralyze successful");
        // Implement your 'mortally wounded' logic - just have them lay still and emit groaning pains.
    }
}
