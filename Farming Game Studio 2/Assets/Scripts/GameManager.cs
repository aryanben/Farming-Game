using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Texture2D combatCursor;
    public Texture2D normalCursor;
    CursorMode cursorMode = CursorMode.Auto;
    public static bool isAllowedToAttack;
    void Start()
    {
        Cursor.visible = true;
    }        
    void Update()
    {
        Vector2 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursor;
        Cursor.SetCursor(normalCursor, Vector2.zero, cursorMode);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                isAllowedToAttack = true;
                Cursor.SetCursor(combatCursor, Vector2.one, cursorMode);
            }
            else isAllowedToAttack = false;                                
        }
    }
}
