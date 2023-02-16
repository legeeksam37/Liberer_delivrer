using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _playerBag;
 
     public void DisplayFurnitures(bool value)
    {
        _playerBag.SetActive(_playerBag);
    }
}