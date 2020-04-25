using UnityEditor;
using UnityEngine;

public class PlacementTool : EditorWindow
{
    GameObject wantedEquipment;
    RaycastHit hit;
    bool destroyObject;
    bool activePlaceButton = false;
    [MenuItem("Tools/Placement Tool")]
    public static void WindowPopUp()
    {
        GetWindow(typeof(PlacementTool));
    }
    void OnFocus()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }
    private void OnGUI()
    {
        wantedEquipment = EditorGUILayout.ObjectField("Equipment To Spawn", wantedEquipment, typeof(GameObject), true) as GameObject;

        if(GUILayout.Button("Reset Creation"))
        {
            DestroyImmediate(GameObject.FindGameObjectWithTag("Table"));
        }
    }

    public void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        e.clickCount = 1;
        Vector2 mousePosition = Event.current.mousePosition;
        Ray pickRay = HandleUtility.GUIPointToWorldRay(mousePosition);

        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftControl)
        {
            activePlaceButton = true;
        }
        else if (e.type == EventType.KeyUp && e.keyCode == KeyCode.LeftControl)
        {
            activePlaceButton = false;
        }

        if (activePlaceButton && Physics.Raycast(pickRay, out hit))
        {          
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.E && e.clickCount == 1)
            {
                Create();                
            }
        }

        if (activePlaceButton && e.type == EventType.KeyDown && e.keyCode == KeyCode.W && e.clickCount == 1 && !hit.transform.CompareTag("Plane"))
        {
            GameObject go = HandleUtility.PickGameObject(e.mousePosition, true);

            DestroyImmediate(go);
        }
    }

    void Create()
    {
        Transform spawnedObject = Instantiate(wantedEquipment, hit.point, wantedEquipment.transform.rotation).transform;
        spawnedObject.up = hit.normal;
    }
}
