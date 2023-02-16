using System.Linq;
using UnityEngine;

public class Quest : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.MissionStarted += (m) => callQuest(m.TargetedBuilding);
        GameEvents.BuildingReached += (b) => Debug.Log("Building reached : " + b.Type);
        hide();
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void callQuest(BuildingID building)
    {
        gameObject.SetActive(true);
        transform.parent = building.TransformOverride;
        transform.localPosition = Vector3.zero;
        //transform.position = building.TransformOverride.position;
    }
    public void callQuest(BuildingType target) => callQuest(FindBuilding(target));
    public static BuildingID FindBuilding(BuildingType target) => FindObjectsOfType<BuildingID>().First(b => b.Type == target);

}
