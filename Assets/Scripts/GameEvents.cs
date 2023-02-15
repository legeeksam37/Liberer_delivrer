using ScenarioStructures;
using System;

public static class GameEvents
{
    public static Action<OnlineOrLive> OnlineOrLiveSelected { get; set; }
    public static Action<TravelMethod> TravelMethodSelected { get; set; }
    public static Action<WithdrawalType> WithdrawalTypeSelected { get; set; }
    public static Action<DelayType> DelayTypeSelected { get; set; }
    public static Action<(string message,Result result)> ScenarioEnded { get; set; }
    public static Action<Mission> MissionStarted { get; set; }
        
}