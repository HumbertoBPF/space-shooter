using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    // powerupID = 1 -> triple shot powerup
    // powerupID = 2 -> speed increase powerup
    // powerupID = 3 -> shield powerup
    [SerializeField]
    private int _powerupID;
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float ymin = -5.4f;
        
        transform.Translate(_speed*Vector3.down*Time.deltaTime);
        
        if (transform.position.y < ymin)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                if (_powerupID == 0)
                {
                    player.TripleShotActive();
                }
                else if (_powerupID == 1)
                {
                    player.SpeedBoostActive();
                }
                else if (_powerupID == 2)
                {
                    player.ShieldActive();
                }
            }

            AudioSource.PlayClipAtPoint(_clip, transform.position);
            //this.GetComponent<Renderer>().enabled = false;
            Destroy(this.gameObject);
        }
    }
}
