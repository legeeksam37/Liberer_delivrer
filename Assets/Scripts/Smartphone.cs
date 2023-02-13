using UnityEngine;

public class Smartphone : MonoBehaviour
{
    public void Order()
    {
        Debug.Log("Started online order.");
    }

    public void SetWithdrawalType(WithdrawalType withdrawalType)
    {
        Debug.Log($"Withdrawal type selected : {withdrawalType}");
        
        GameEvents.WithdrawalTypeSelected?.Invoke(withdrawalType);
    }
    
    public void SetTravelMethod(TravelMethod travelMethod)
    {
        Debug.Log($"Travel method selected : {travelMethod}");
        
        GameEvents.TravelMethodSelected?.Invoke(travelMethod);
    }
}