using System;
using System.Linq;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private bool _autoFollowNextObjective;
    private SpriteRenderer _sr;
    public static Color secondaryColor = new Color(35 / 255f, 241 / 255f, 0f);
    private void Awake()
    {
        if (_autoFollowNextObjective)
            GameEvents.MissionStarted += (m) => callQuest(m.TargetedBuilding);
        //GameEvents.BuildingReached += (b) => Debug.Log("Building reached : " + b.Type);
        _sr = GetComponentInChildren<SpriteRenderer>();
        hide();
    }
    public void Custom(Color color, Vector3 scale)
    {
        _sr.color = color;
        transform.localScale = scale;
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void callQuest(IDBase building)
    {
        gameObject.SetActive(true);
        transform.parent = building.TransformOverride;
        transform.localPosition = Vector3.zero;
        transform.position = building.TransformOverride.position;
    }
    public void callQuest(BuildingType target)
    {
        BuildingID building = BuildingsManager.GetInstance().Buildings.First(b => b.Type == target);
        callQuest(building);

    }
    //public void callQuest(BuildingType target) => callQuest(FindID(target));
    public static IDBase FindID<T>(T target) where T : Enum
    {
        Debug.Log(FindObjectsOfType<BuildingID>());
        try
        {
            return FindObjectsOfType<IDBase<T>>().First(b => b.Type.CompareTo(target)==0);
        }
        catch (InvalidOperationException e)
        {
            Debug.Log("With target : " + target + "not found, returning null thrown "+e.Message);
            return null;
        }
    }


}
