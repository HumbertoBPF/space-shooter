using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    private bool _isPlayerAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {   
        while (_isPlayerAlive)
        {
            float ymax = 7.4f;
            float xmax = 9.3f;
            float xmin = -9.3f;

            float randomX = Random.Range(xmin, xmax);

            GameObject newEnemy = Instantiate(_enemy, new Vector3(randomX, ymax, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_isPlayerAlive)
        {
            float ymax = 7.4f;
            float xmax = 9.3f;
            float xmin = -9.3f;

            float randomX = Random.Range(xmin, xmax);

            int waitingTime = Random.Range(3, 8);
            Debug.Log("Waiting time = "+waitingTime);
            yield return new WaitForSeconds(waitingTime);

            int randomIndex = Random.Range(0, powerups.Length);
            Instantiate(powerups[randomIndex], new Vector3(randomX, ymax, 0), Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _isPlayerAlive = false;
    }

}
