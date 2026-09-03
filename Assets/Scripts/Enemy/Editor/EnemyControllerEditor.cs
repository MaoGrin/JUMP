using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// EnemyController 的自定义 Inspector：
/// 给 Move / Hurt Behaviour 提供明确可点击的标准下拉框，避免 [SerializeReference] 默认 UI 不弹窗的问题。
/// 注意：自定义 Inspector 会完全接管界面，这里没画的字段就不会显示。
/// </summary>
[CustomEditor(typeof(EnemyController))]
public class EnemyControllerEditor : Editor
{
    private static readonly Type[] MoveTypes;
    private static readonly string[] MoveTypeNames;

    private static readonly Type[] HurtTypes;
    private static readonly string[] HurtTypeNames;

    static EnemyControllerEditor()
    {
        MoveTypes = CollectTypes<BaseEnemyMove>();
        MoveTypeNames = BuildNames(MoveTypes);

        HurtTypes = CollectTypes<BaseEnemyHurt>();
        HurtTypeNames = BuildNames(HurtTypes);
    }

    // 反射收集所有继承 T 的非抽象类型，新增行为子类无需改这里
    private static Type[] CollectTypes<T>()
    {
        return TypeCache.GetTypesDerivedFrom<T>()
            .Where(t => !t.IsAbstract && !t.IsGenericType)
            .ToArray();
    }

    // 第 0 项固定为 "(None)"，后面依次是具体类型名
    private static string[] BuildNames(Type[] types)
    {
        var names = new string[types.Length + 1];
        names[0] = "(None)";
        for (int i = 0; i < types.Length; i++)
        {
            names[i + 1] = types[i].Name;
        }
        return names;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 1. model 字段
        var modelProp = serializedObject.FindProperty("model");
        if (modelProp != null)
        {
            EditorGUILayout.PropertyField(modelProp, true);
        }

        // 2. moveBehaviour 字段
        var moveProp = serializedObject.FindProperty("moveBehaviour");
        if (moveProp != null)
        {
            DrawBehaviourField(moveProp, "Move Behaviour", MoveTypes, MoveTypeNames);
        }

        // 3. hurtBehaviour 字段
        var hurtProp = serializedObject.FindProperty("hurtBehaviour");
        if (hurtProp != null)
        {
            DrawBehaviourField(hurtProp, "Hurt Behaviour", HurtTypes, HurtTypeNames);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawBehaviourField(SerializedProperty prop, string label, Type[] types, string[] names)
    {
        var current = prop.managedReferenceValue;

        // 当前选中项在数组里的下标（0 = None）
        int currentIndex = 0;
        if (current != null)
        {
            int typeIndex = Array.IndexOf(types, current.GetType());
            if (typeIndex >= 0) currentIndex = typeIndex + 1;
        }

        int newIndex = EditorGUILayout.Popup(label, currentIndex, names);

        // 选择变化时：重建或清空实例
        if (newIndex != currentIndex)
        {
            prop.managedReferenceValue = newIndex <= 0
                ? null
                : Activator.CreateInstance(types[newIndex - 1]);
        }

        // 展开绘制选中实例的所有子字段（如 pointA / pointB / speed）
        if (prop.managedReferenceValue != null)
        {
            EditorGUI.indentLevel++;

            var child = prop.Copy();
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
