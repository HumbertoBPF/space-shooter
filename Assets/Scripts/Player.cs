using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    // PreFabs
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _enemiesPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    // Shooting
    private float _fireRate = 0.1f;
    private float _canFire = -1.0f;
    // Enemies spawn
    private float _enemiesRate = 5.0f;
    private float _spawnEnemies = -1.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    // Power-ups
    [SerializeField]
    private bool _isTripleShotActive = false;
    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position(0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.Log("SpawnManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateObjectPosition();
        ConstraintObjectInScene();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        { 
            Shoot();
        }

        //SpawnEnemies(); 
    }

    void UpdateObjectPosition()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(_speed * direction * Time.deltaTime);
    }

    void ConstraintObjectInScene()
    {
        float ymax = 0f;
        float ymin = -3.8f;
        float xmax = 11.3f;
        float xmin = -11.3f;

        if (transform.position.y <= ymin)
        {
            transform.position = new Vector3(transform.position.x, ymin, 0);
        }
        else if (transform.position.y >= ymax)
        {
            transform.position = new Vector3(transform.position.x, ymax, 0);
        }

        if (transform.position.x <= xmin)
        {
            transform.position = new Vector3(xmax, transform.position.y, 0);
        }
        else if (transform.position.x >= xmax)
        {
            transform.position = new Vector3(xmin, transform.position.y, 0);
        }
    }

    void Shoot()
    {
        _canFire = Time.time + _fireRate;
        // Debug.Log("Space key logged");
        if (_isTripleShotActive)
        {
            Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Vector3 offsetVector = new Vector3(0, 1f, 0);
            Instantiate(_laserPrefab, transform.position + offsetVector, Quaternion.identity);
        }
    }

    void SpawnEnemies()
    {
        if (Time.time > _spawnEnemies)
        {
            _spawnEnemies = Time.time + _enemiesRate;
            Vector3 initialPositionEnemies = new Vector3(0, 7.4f, 0);
            Instantiate(_enemiesPrefab, initialPositionEnemies, Quaternion.identity);
        }
    }

    public void Damage()
    {
        _lives--;

        if (_lives <= 0)
        {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

}
