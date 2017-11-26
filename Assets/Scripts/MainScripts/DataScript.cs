using System;

[Serializable]
public class DataScript
{
    private int TopScore;
    private int TotalBugCaught;
    private int CurrentBugCount;
    private float TotalDistanceTravelled;
    private float TripsTaken;
    
    public DataScript()
    {
    }

    public int totalBugCaught
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

    public float totalDistanceTravelled
    {
        get
        {
            return TotalDistanceTravelled;
        }

        set
        {
            TotalDistanceTravelled = value;
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

    public float tripsTaken
    {
        get
        {
            return TripsTaken;
        }

        set
        {
            TripsTaken = value;
        }
    }
}