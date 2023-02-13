using System;

public static class GameEvents
{ 
    public static Action<TravelMethod> TravelMethodSelected { get; set; }
    public static Action<WithdrawalType> WithdrawalTypeSelected { get; set; }
    public static Action<DelayType> DelayTypeSelected { get; set; }
}