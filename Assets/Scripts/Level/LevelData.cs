using UnityEngine;

[CreateAssetMenu(menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    public PlatformSegmentData[] segments;

    [Header("Coins")]
    public int totalCurrency;
}