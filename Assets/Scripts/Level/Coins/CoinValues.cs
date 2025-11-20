public static class CoinValues
{
    public static int GetValue(CoinType type)
    {
        return type switch
        {
            CoinType.Silver => 1,
            CoinType.Gold => 5,
            _ => 1
        };
    }
}