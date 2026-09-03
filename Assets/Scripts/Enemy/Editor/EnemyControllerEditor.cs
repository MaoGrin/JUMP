using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// EnemyController 的自定义 Inspector：
/// 给 Move Behaviour 提供一个明确可点击的标准下拉框，避免 [SerializeReference] 默认 UI 不弹窗的问题。
/// </summary>
[CustomEditor(typeof(EnemyController))]
public class EnemyControllerEditor : Editor
{
    // 运行时通过反射收集所有继承 BaseEnemyMove 的非抽象类型，新增移动方式无需改这里
    private static readonly Type[] MoveTypes;
    private static readonly string[] MoveTypeDisplayNames;

    static EnemyControllerEditor()
    {
        MoveTypes = TypeCache.GetTypesDerivedFrom<BaseEnemyMove>()
            .Where(t => !t.IsAbstract && !t.IsGenericType)
            .ToArray();

        // 第 0 项固定为 "(None)"，后面依次是具体移动类型名
        MoveTypeDisplayNames = new string[MoveTypes.Length + 1];
        MoveTypeDisplayNames[0] = "(None)";
        for (int i = 0; i < MoveTypes.Length; i++)
        {
            MoveTypeDisplayNames[i + 1] = MoveTypes[i].Name;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 1. model 字段
        SerializedProperty modelProp = serializedObject.FindProperty("model");
        if (modelProp != null)
        {
            EditorGUILayout.PropertyField(modelProp, true);
        }

        // 2. moveBehaviour 字段：自定义下拉
        SerializedProperty moveProp = serializedObject.FindProperty("moveBehaviour");
        if (moveProp != null)
        {
            DrawMoveBehaviourField(moveProp);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawMoveBehaviourField(SerializedProperty moveProp)
    {
        var current = moveProp.managedReferenceValue;

        // 当前选中项在数组里的下标（0 = None）
        int currentIndex = 0;
        if (current != null)
        {
            int typeIndex = Array.IndexOf(MoveTypes, current.GetType());
            if (typeIndex >= 0) currentIndex = typeIndex + 1;
        }

        int newIndex = EditorGUILayout.Popup("Move Behaviour", currentIndex, MoveTypeDisplayNames);

        // 选择变化时：重建或清空实例
        if (newIndex != currentIndex)
        {
            if (newIndex <= 0)
            {
                moveProp.managedReferenceValue = null;
            }
            else
            {
                moveProp.managedReferenceValue = Activator.CreateInstance(MoveTypes[newIndex - 1]);
            }
        }

        // 展开绘制选中实例的所有子字段（如 pointA / pointB / speed）
        if (moveProp.managedReferenceValue != null)
        {
            EditorGUI.indentLevel++;

            SerializedProperty child = moveProp.Copy();
            int depth = child.depth;
            bool enterChildren = true;
            while (child.NextVisible(enterChildren))
            {
                if (child.depth <= depth) break;
                enterChildren = false;
                EditorGUILayout.PropertyField(child, true);
            }

            EditorGUI.indentLevel--;
        }
    }
}
