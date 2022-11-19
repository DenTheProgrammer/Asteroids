using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject startText;
    public GameObject controls;
    public GameObject deathScreen;
    public TMP_Text score;
    public TMP_Text lvl;

    public TMP_Text lazerCount;
    public Image lazerProgressBar;

    public TMP_Text cords;
    public TMP_Text angle;
    public TMP_Text speed;

    void Start()
    {
        GameManager.OnGameStart += () => { 
            startText.SetActive(false);
            controls.SetActive(false);
            deathScreen.SetActive(false);
            score.gameObject.SetActive(true);
            lvl.gameObject.SetActive(true);
            lazerProgressBar.transform.parent.gameObject.SetActive(true);
            cords.transform.parent.gameObject.SetActive(true);
        };


        GameManager.OnGameOver += () => {
            startText.SetActive(true);
            controls.SetActive(true);
            deathScreen.SetActive(true);

            lazerProgressBar.transform.parent.gameObject.SetActive(false);
            cords.transform.parent.gameObject.SetActive(false);
        };

        GameManager.OnScoreUpdate += UpdateScoreUI;
        GameManager.OnNextLvl += UpdateLvlUI;
        Gun.OnLazerUpdate += UpdateLazerUI;
        Ship.OnMovement += UpdateDebugUI;
    }

    private void UpdateLvlUI(int lvl)
    {
        this.lvl.text = $"Lvl {lvl}";
    }

    private void UpdateDebugUI(Vector2 cords, float angle, float speed)
    {
        this.cords.text = $"x: {cords.x:F2}, y: {cords.y:F2}";
        this.angle.text = $"angle: {angle:F2}";
        this.speed.text = $"speed: {speed:F2}";
    }

    private void UpdateLazerUI(float perc, int count)
    {
        lazerProgressBar.fillAmount = perc;
        lazerCount.text = count.ToString();
    }

    private void UpdateScoreUI(int addedScore, int totalScore)
    {
        score.text = $"{totalScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
