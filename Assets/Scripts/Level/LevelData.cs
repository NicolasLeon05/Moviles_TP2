using UnityEngine;

[CreateAssetMenu(menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    public int difficulty;
    public PlatformSegmentData[] segments;
}