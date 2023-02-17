using ScenarioStructures;
using System;

public static class GameEvents
{
    public static Action GameStarted { get; set; }
    public static Action<Mission> MissionStarted { get; set; }
    public static Action<OnlineOrLive> OnlineOrLiveSelected { get; set; }
    public static Action<TravelMethod> TravelMethodSelected { get; set; }
    public static Action<WithdrawalType> WithdrawalTypeSelected { get; set; }
    public static Action<DelayType> DelayTypeSelected { get; set; }
    public static Action<BuildingID> BuildingReached { get; set;}
    public static Action<string> SequenceProcessed { get; set; }
    public static Action<(string message, Result result)> ScenarioEnded { get; set; }
    public static Action GameEnded { get; set; }
    public static Action<TravelID> TravelReached { get; set; }
    
}