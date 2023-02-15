using System.Collections.Generic;

public interface IDisplay
{
    void Expand();
    void Collapse();
    void Delay(List<DelayType> options = null);
    void OnlineOrLive(List<OnlineOrLive> options = null);
    void WithDrawal(List<WithdrawalType> options = null);
    void Travel(List<TravelMethod> options = null);
}