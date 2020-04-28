using UnityEditor;
using UnityEngine;

public class ChangeEnemy : EditorWindow
{
    string enemyName = "Enemy";
    GameObject wantedEnemy;

    [MenuItem("Tools/EnemyPicker")]
    public static void WindowPopUp()
    {
        GetWindow(typeof(ChangeEnemy));
    }

    private void OnGUI()
    {
        enemyName = EditorGUILayout.TextField("Enemy", enemyName);
        wantedEnemy = EditorGUILayout.ObjectField("Enemy To Spawn", wantedEnemy, typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("Replace Enemy"))
        {
            ReplaceEnemy();
        }
    }

    private void ReplaceEnemy()
    {
        if (wantedEnemy == null)
        {
            Debug.LogError("There is no object to replace");
        }

        if (enemyName == null)
        {
            Debug.LogError("Please Insert the enemy name so we can replace");
        }

        if (enemyName == "Enemy")
        {
            DestroyImmediate(GameObject.FindGameObjectWithTag("Enemy"));
            GameObject Enemy = Instantiate(wantedEnemy, GameObject.FindGameObjectWithTag("EnemyParent").transform.position, GameObject.FindGameObjectWithTag("EnemyParent").transform.rotation) as GameObject;
            Enemy.transform.parent = GameObject.FindGameObjectWithTag("EnemyParent").transform;
            Enemy.transform.localScale = Enemy.transform.lossyScale;
            Enemy.transform.tag = "Enemy";
            Enemy.name = wantedEnemy.name;
        }
    }
}
