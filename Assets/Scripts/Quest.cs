using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer _sr;

    public void hide()
    {
        _sr.enabled = false;
    }

    public void callQuest(GameObject building)
    {
        transform.position = building.transform.position;
    }
}
