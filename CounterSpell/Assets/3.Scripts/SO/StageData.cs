using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObject/StageData", order = int.MaxValue)]
public class StageData : ScriptableObject
{
    [SerializeField] private string stageName;
    public string StageName => stageName;
    [SerializeField] private Vector2 loveLetterSpawnPos;
    public Vector2 LoveLetterSpawnPos => loveLetterSpawnPos;
}