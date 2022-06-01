using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed*Vector3.down*Time.deltaTime);

        float ymax = 7.4f;
        float ymin = -7.4f;
        float xmax = 11.3f;
        float xmin = -11.3f;

        if (transform.position.y < ymin)
        {
            float randomX = Random.Range(xmin, xmax);
            //Debug.Log("random value = "+randomX);
            transform.position = new Vector3(randomX, ymax, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }else if (other.tag == "Laser")
        {
            _player.IncrementScore(10);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

}
