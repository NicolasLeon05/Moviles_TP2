using UnityEngine;

public static class CurrencySystem
{
    private const string COINS_KEY = "coins";
    private const string JUMP_UPGRADE_KEY = "jump_upgrade";
    private const string SPEED_UPGRADE_KEY = "speed_upgrade";

    public static int Coins
    {
        get => PlayerPrefs.GetInt(COINS_KEY, 0);
        private set
        {
            PlayerPrefs.SetInt(COINS_KEY, value);
            PlayerPrefs.Save();
        }
    }

    public static bool JumpUpgrade
    {
        get => PlayerPrefs.GetInt(JUMP_UPGRADE_KEY, 0) == 1;
        private set
        {
            PlayerPrefs.SetInt(JUMP_UPGRADE_KEY, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool SpeedUpgrade
    {
        get => PlayerPrefs.GetInt(SPEED_UPGRADE_KEY, 0) == 1;
        private set
        {
            PlayerPrefs.SetInt(SPEED_UPGRADE_KEY, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static void AddCoins(int amount)
    {
        Coins += amount;
    }

    public static bool TryBuyJump(int cost)
    {
        if (Coins < cost || JumpUpgrade)
            return false;

        Coins -= cost;
        JumpUpgrade = true;
        return true;
    }

    public static bool TryBuySpeed(int cost)
    {
        if (Coins < cost || SpeedUpgrade)
            return false;

        Coins -= cost;
        SpeedUpgrade = true;
        return true;
    }

    public static void ResetForEditor()
    {
#if UNITY_EDITOR
        PlayerPrefs.DeleteKey(COINS_KEY);
        PlayerPrefs.DeleteKey(JUMP_UPGRADE_KEY);
        PlayerPrefs.DeleteKey(SPEED_UPGRADE_KEY);
        PlayerPrefs.Save();
#endif
    }
}
