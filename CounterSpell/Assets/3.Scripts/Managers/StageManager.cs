using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject loveLetterPrefab;
    [SerializeField] private GameObject titleTextParent;

    public void UpdateStage(int stageIndex)
    {
        StageData stageData = Resources.Load<StageData>($"Prefabs/StageData/Stage {stageIndex}");

        UpdateTitleText(stageData.StageName);
        StartCoroutine(SpawnLoveLetter(stageData.LoveLetterSpawnPos));
    }

    private void UpdateTitleText(string stageName)
    {
        foreach (Transform child in titleTextParent.transform)
        {
            Text text = child.GetComponent<Text>();
            if (text != null)
            {
                text.text = stageName;
            }
        }
    }

    private IEnumerator SpawnLoveLetter(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(1.25f);
        Instantiate(loveLetterPrefab, spawnPosition, Quaternion.identity);
    }
}