using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(EnemySpawner))]
class EnemySpawnerEditorr : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("Waves"), true, true, true, true);

        list.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, 30, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Amount"), GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x + 35, rect.y, rect.width - 35 - 70 - 65, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Enemy"), GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x + rect.width - 70 - 65, rect.y, 60, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("SpawnTime"), GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x + rect.width - 65, rect.y, 60, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("RestTime"), GUIContent.none);
            };

        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(new Rect(rect.x + 13, rect.y, 30, rect.height + 10), "#");
            EditorGUI.LabelField(new Rect(rect.x + 13 + 35, rect.y, rect.width - 35 - 70 - 65, rect.height + 10), "enemy");
            EditorGUI.LabelField(new Rect(rect.x + rect.width - 70 - 65, rect.y, 60, rect.height + 10), "period");
            EditorGUI.LabelField(new Rect(rect.x + rect.width - 65, rect.y, 60, rect.height + 10), "rest");
        };

        list.onSelectCallback = (ReorderableList l) => {
            var prefab = l.serializedProperty.GetArrayElementAtIndex(l.index).FindPropertyRelative("Enemy").objectReferenceValue as GameObject;
            if (prefab)
                EditorGUIUtility.PingObject(prefab.gameObject);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
