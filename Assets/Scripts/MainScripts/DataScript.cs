using System;

[Serializable]
public class DataScript
{
    private int TopScore;
    private int TotalBugCaught;
    private int CurrentBugCount;
    private float totalDistanceTraveled;

    public DataScript()
    {
    }

    public int t
    {
        get
        {
            return TotalBugCaught;
        }

        set
        {
            TotalBugCaught = value;
        }
    }

    public int currentBugCount
    {
        get
        {
            return CurrentBugCount;
        }

        set
        {
            CurrentBugCount = value;
        }
    }

    public float TotalDistanceTraveled
    {
        get
        {
            return totalDistanceTraveled;
        }

        set
        {
            totalDistanceTraveled = value;
        }
    }

    public int topScore
    {
        get
        {
            return TopScore;
        }

        set
        {
            TopScore = value;
        }
    }
}