using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UserInterfaceHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnScoreChanged.AddListener(UpdateScoreUI);
    }

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = newScore.ToString();

    }
}
