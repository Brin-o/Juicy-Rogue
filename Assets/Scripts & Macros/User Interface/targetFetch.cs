using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class targetFetch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer spriteRenderer = default;
    [SerializeField] Text scoreText = default;
    [SerializeField] Text finalScoreCount = default;

    float playerScore;
    
    private void Start() {
        playerScore = 0;
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            spriteRenderer.enabled = false;
            playerScore++;
            SetScore();
        }
    }

    public void SetScore()
    {
        scoreText.text = playerScore.ToString();
        finalScoreCount.text = playerScore.ToString();
    }
    
}
