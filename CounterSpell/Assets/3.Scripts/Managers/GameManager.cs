using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject[] stageUI;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform startPos;

    public int Stage { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            stageManager.UpdateStage(Stage);
            SetPlayerPos();
        }
    }

    public void UpdateStage()
    {
        if (Stage < stageUI.Length)
        {
            stageUI[Stage].SetActive(false);
        }

        Stage++;

        if (Stage < stageUI.Length)
        {
            stageUI[Stage].SetActive(true);
            stageManager.UpdateStage(Stage);
            SetPlayerPos();
        }
    }

    private void SetPlayerPos()
    {
        if (startPos != null)
        {
            player.transform.position = startPos.position;
        }
    }
}