using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Healt")]
    [SerializeField] Slider healtSlider;
    [SerializeField] Health playerHealt;


    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    void Awake() 
    {   
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start() 
    {
        healtSlider.maxValue = playerHealt.GetHealt();
    }
    void Update() 
    {
        healtSlider.value = playerHealt.GetHealt();
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }

}
