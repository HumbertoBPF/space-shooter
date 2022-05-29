using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed*Vector3.up*Time.deltaTime);

        if (transform.position.y > 10)
        {
            // Destroy parent game object for triple shots
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            // Debug.Log("Destroy!");
            Destroy(gameObject);
        }
    }
}
