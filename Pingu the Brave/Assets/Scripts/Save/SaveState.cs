using System;

[System.Serializable]
public class SaveState
{
    [NonSerialized] private const int HAT_COUNT = 16;

    public int Highscore { set; get; }
    public int Fish { set; get; }
    public DateTime LastSaveTime { set; get; }
    public int CurrentHatIndex { set; get; }
    public byte[] UnlockedHatFlag { set; get; }

    public SaveState()
    {
        Highscore = 0;
        Fish = 0;
        LastSaveTime = DateTime.Now;
        CurrentHatIndex = 0;
        UnlockedHatFlag = new byte[HAT_COUNT];
        UnlockedHatFlag[0] = 1;
    }
}
