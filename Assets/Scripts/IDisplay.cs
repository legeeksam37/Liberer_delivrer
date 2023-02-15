using System.Collections.Generic;

public interface IDisplay
{
    void Expand();
    void Collapse();
    void Delay(RecursiveEnabledChoice currentStep, List<DelayType> options = null);
    void OnlineOrLive(RecursiveEnabledChoice currentStep, List<OnlineOrLive> options = null);
    void WithDrawal(RecursiveEnabledChoice currentStep, List<WithdrawalType> options = null);
    void Travel(RecursiveEnabledChoice currentStep, List<TravelMethod> options = null);
}