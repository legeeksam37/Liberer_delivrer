using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private bool _autoFollowNextObjective;
    private SpriteRenderer _sr;
    public static Color secondaryColor = new Color(35 / 255f, 241 / 255f, 0f);
    private void Start()
    {
        if (_autoFollowNextObjective)
            GameEvents.MissionStarted += (m) => callQuest(m.TargetedBuilding);
        GameEvents.BuildingReached += (b) => Debug.Log("Building reached : " + b.Type);
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
        Debug.Log("NIQUE");
        transform.parent = building.TransformOverride;
        transform.localPosition = Vector3.zero;
        transform.position = building.TransformOverride.position;
    }
    public void callQuest(BuildingType target)
    {
        BuildingID building = BuildingsManager.GetInstance().buildings.First(b => b.Type == target);
        callQuest(building);

    }
        
    public static BuildingID FindBuilding(BuildingType target)
    {
        Debug.Log(FindObjectsOfType<BuildingID>());
        try
        {
            return FindObjectsOfType<BuildingID>().First(b => b.Type == target);
        }
        catch (InvalidOperationException e)
        {
            Debug.Log("With target : " + target + "not found, returning null thrown "+e.Message);
            return null;
        }
    }

}
