using UnityEditor;
using UnityEngine;


public class CreateCharaterWindow : EditorWindow
{

    static string myTitle = "Create Character";
    public Character Target;
    bool isSet = false;

    #region Variable
    
     uint Level = 1;
     uint HP = 10;
     uint Str = 0;
     uint Magic = 0;
     uint Skill = 0;
     uint Spd = 0;
     uint Def = 0;
     uint Lck = 0;
     uint Res = 0;
     uint Move = 0;


     uint MaxLevel = 20;
     uint MaxHP = 40;
     uint MaxStr = 20;
     uint MaxMagic = 20;
     uint MaxSkill = 20;
     uint MaxSpd = 20;
     uint MaxDef = 20;
     uint MaxLck = 20;
     uint MaxRes = 20;
     uint MaxMove = 10;

     uint HPGrowth = 0;
     uint StrGrowth = 0;
     uint MagicGrowth = 0;
     uint SkillGrowth = 0;
     uint SpdGrowth = 0;
     uint DefGrowth = 0;
     uint LckGrowth = 0;
     uint ResGrowth = 0;

    #endregion

    [MenuItem("Set/Character")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CreateCharaterWindow),false, myTitle, true);
    }

    private void Reset()
    {
        Level = 1;
        HP = 10;
        Str = 0;
        Magic = 0;
        Skill = 0;
        Spd = 0;
        Def = 0;
        Lck = 0;
        Res = 0;
        Move = 0;


        MaxLevel = 20;
        MaxHP = 40;
        MaxStr = 20;
        MaxMagic = 20;
        MaxSkill = 20;
        MaxSpd = 20;
        MaxDef = 20;
        MaxLck = 20;
        MaxRes = 20;
        MaxMove = 10;

        HPGrowth = 0;
        StrGrowth = 0;
        MagicGrowth = 0;
        SkillGrowth = 0;
        SpdGrowth = 0;
        DefGrowth = 0;
        LckGrowth = 0;
        ResGrowth = 0;
    }

    public void OnGUI()
    {
        
        GUILayout.Label("Base Information", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        Target = EditorGUILayout.ObjectField("Character", Target, typeof(Character), true) as Character;
        if(!isSet && Target != null && Target.GetValue("Level") != null)
        {
            Level = Target.GetValue("Level").Value;
            HP = Target.GetValue("HP").Value;
            Str = Target.GetValue("Str").Value;
            Magic = Target.GetValue("Magic").Value;
            Skill = Target.GetValue("Skill").Value;
            Spd = Target.GetValue("Spd").Value;
            Def = Target.GetValue("Def").Value;
            Lck = Target.GetValue("Lck").Value;
            Res = Target.GetValue("Res").Value;
            Move = Target.GetValue("Move").Value;
            isSet = true;
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.LabelField("Level");
        Level = (uint)EditorGUILayout.IntSlider((int)Level, 0, (int)MaxLevel);
        EditorGUILayout.LabelField("HP");
        HP = (uint)EditorGUILayout.IntSlider((int)HP, 0, (int)MaxHP);
        EditorGUILayout.LabelField("Str");
        Str = (uint)EditorGUILayout.IntSlider((int)Str, 0, (int)MaxStr);
        EditorGUILayout.LabelField("Magic");
        Magic = (uint)EditorGUILayout.IntSlider((int)Magic, 0, (int)MaxMagic);
        EditorGUILayout.LabelField("Skill");
        Skill = (uint)EditorGUILayout.IntSlider((int)Skill, 0, (int)MaxSkill);
        EditorGUILayout.LabelField("Spd");
        Spd = (uint)EditorGUILayout.IntSlider((int)Spd, 0, (int)MaxSpd);
        EditorGUILayout.LabelField("Def");
        Def = (uint)EditorGUILayout.IntSlider((int)Def, 0, (int)MaxDef);
        EditorGUILayout.LabelField("Lck");
        Lck = (uint)EditorGUILayout.IntSlider((int)Lck, 0, (int)MaxLck);
        EditorGUILayout.LabelField("Res");
        Res = (uint)EditorGUILayout.IntSlider((int)Res, 0, (int)MaxRes);
        EditorGUILayout.LabelField("Move");
        Move = (uint)EditorGUILayout.IntSlider((int)Move, 0, (int)MaxMove);

        GUILayout.Label("Growth Information", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("HP Growth");
        HPGrowth = (uint)EditorGUILayout.IntSlider((int)HPGrowth, 0, 100);
        EditorGUILayout.LabelField("Strong Growth");
        StrGrowth = (uint)EditorGUILayout.IntSlider((int)StrGrowth, 0, 100);
        EditorGUILayout.LabelField("Magic Growth");
        MagicGrowth = (uint)EditorGUILayout.IntSlider((int)MagicGrowth, 0, 100);
        EditorGUILayout.LabelField("Skill Growth");
        SkillGrowth = (uint)EditorGUILayout.IntSlider((int)SkillGrowth, 0, 100);
        EditorGUILayout.LabelField("Speed Growth");
        SpdGrowth = (uint)EditorGUILayout.IntSlider((int)SpdGrowth, 0, 100);
        EditorGUILayout.LabelField("Defend Growth");
        DefGrowth = (uint)EditorGUILayout.IntSlider((int)DefGrowth, 0, 100);
        EditorGUILayout.LabelField("Luck Growth");
        LckGrowth = (uint)EditorGUILayout.IntSlider((int)LckGrowth, 0, 100);
        EditorGUILayout.LabelField("Magic Resistance Growth");
        ResGrowth = (uint)EditorGUILayout.IntSlider((int)ResGrowth, 0, 100);

        if (GUILayout.Button("Save"))
        {
            if (Target != null)
            {
                Target.Create(Level, HP, Str, Magic, Skill, Spd, Def, Lck, Res, Move);
            }
            this.Close();
        }
        if (GUILayout.Button("Cancel"))
        {
            Reset();
            this.Close();
        }
    }

    public void OnInspectorUpdate()
    {

    }
}

