using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Handle to text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _restartGameText;
    [SerializeField]
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _scoreText.text = "Score: 0";

        if (_gameManager == null)
        {
            Debug.Log("GameManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
    }

    public void ShowGameOverText()
    {
        _gameManager.GameOver();
        _restartGameText.gameObject.SetActive(true);
        StartCoroutine(FlickeringEffectCoroutine());
    }

    IEnumerator FlickeringEffectCoroutine()
    {
        bool isActive = true;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(isActive);
            isActive = !isActive;
        }
    }

}
