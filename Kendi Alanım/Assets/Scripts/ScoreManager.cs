using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Player player;
    public float score;

    private void  Awake()
    {
        
        instance = this;
    }
    
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        score = player.score;
        scoreText.text = "Score: " + score.ToString() ;
    }
    void Update()
    {
        
    }

    
    public void Score()
    {
        score += 500f;
        scoreText.text = "Score: " + score.ToString() ;
    }
}
