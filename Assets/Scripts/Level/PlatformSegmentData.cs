public enum PlatformType
{
    Basic,
    Moving,
    Breaking
}

[System.Serializable]
public class PlatformSegmentData
{
    public float startY;
    public float endY;
    public float platformDistanceY;
    public float platformDistanceX;
    public PlatformType platformType;
}
