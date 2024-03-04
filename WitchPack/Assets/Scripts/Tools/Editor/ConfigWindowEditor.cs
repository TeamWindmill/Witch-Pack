using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class ConfigWindowEditor : OdinMenuEditorWindow
{

    private CreateNewConfigDatas _createNewConfigDatas;
    
    [MenuItem("Tools/ConfigEditor")]
    private static void OpenWindow()
    {
        GetWindow<ConfigWindowEditor>().Show();
    }
    
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        //_createNewConfigDatas = new CreateNewConfigDatas();
        //tree.Add("Create New Configs", _createNewConfigDatas);
        tree.AddAllAssetsAtPath("Units/Enemies/Base","Assets/Configs/UnitConfigs/EnemyConfigs",typeof(EnemyConfig));
        tree.AddAllAssetsAtPath("Units/Enemies/Stats","Assets/Configs/UnitConfigs/EnemyConfigs",typeof(StatSheet));
        tree.AddAllAssetsAtPath("Units/Shamans/Base","Assets/Configs/UnitConfigs/ShamanConfigs",typeof(ShamanConfig));
        tree.AddAllAssetsAtPath("Units/Shamans/Stats","Assets/Configs/UnitConfigs/ShamanConfigs",typeof(StatSheet));
        tree.AddAllAssetsAtPath("Units/PowerStructures","Assets/Configs/UnitConfigs/StructuresConfig",typeof(PowerStructureConfig));
        tree.AddAllAssetsAtPath("Units/Shamans","Assets/Configs/UnitConfigs/ShamanConfigs",typeof(EnergyLevels));
        tree.AddAllAssetsAtPath("Abilities/AutoAttack","Assets/Configs/Abilities",typeof(BaseAbility));
        tree.AddAllAssetsAtPath("Abilities/Toor/Multishot","Assets/Configs/Abilities/Toor/Multishot",typeof(BaseAbility));
        tree.AddAllAssetsAtPath("Abilities/Toor/PiercingShot","Assets/Configs/Abilities/Toor/PiercingShot",typeof(BaseAbility));
        tree.AddAllAssetsAtPath("Abilities/Toor/Passives","Assets/Configs/Abilities/Toor/Passives",typeof(BaseAbility));
        tree.AddAllAssetsAtPath("Abilities/Nadia/RootingVines","Assets/Configs/Abilities/Nadia/RootingVines",typeof(BaseAbility));
        tree.AddAllAssetsAtPath("Abilities/Nadia/Heal","Assets/Configs/Abilities/Nadia/Heal",typeof(BaseAbility));
        tree.AddAllAssetsAtPath("Abilities/Nadia/Passives","Assets/Configs/Abilities/Nadia/Passives",typeof(BaseAbility));
        tree.AddAllAssetsAtPath("Abilities/Javan/SmokeBomb","Assets/Configs/Abilities/Javan/SmokeBomb",typeof(BaseAbility));
        tree.AddAllAssetsAtPath("Abilities/Javan/Charm","Assets/Configs/Abilities/Javan/Charm",typeof(BaseAbility));
        tree.AddAllAssetsAtPath("Abilities/Javan/Passives","Assets/Configs/Abilities/Javan/Passives",typeof(BaseAbility));
        tree.AddAllAssetsAtPath("Abilities/StatusEffects/Roots","Assets/Configs/Abilities/StatusEffects/Roots",typeof(StatusEffectConfig));
        tree.AddAllAssetsAtPath("Abilities/StatusEffects/SmokeBomb","Assets/Configs/Abilities/StatusEffects/SmokeBomb",typeof(StatusEffectConfig));
        tree.AddAllAssetsAtPath("Level/Levels","Assets/Configs/Levels",typeof(LevelConfig));
        tree.AddAllAssetsAtPath("Level/Waves","Assets/Configs/Waves",typeof(WaveData));
        return tree;
    }

    protected override void OnBeginDrawEditors()
    {
        var selected = this.MenuTree.Selection;

        SirenixEditorGUI.BeginHorizontalToolbar();
        {
            GUILayout.FlexibleSpace();

            if (SirenixEditorGUI.ToolbarButton("Delete"))
            {
                var asset = selected.SelectedValue;
                string path = AssetDatabase.GetAssetPath((Object)asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        //_createNewConfigDatas.OnDestroy();
    }

    public class CreateNewConfigDatas
    {

        private void SetConfigData(BaseConfig configData)
        {
            if(ConfigData != null) 
                DestroyImmediate(ConfigData);

            ConfigData = configData;
        }

        [HorizontalGroup]
        [Button("Enemy")]
        private void SetEnemyConfig()
        {
            var newConfigData = new CreateNewConfigData<EnemyConfig>("NewEnemyConfig", "Assets/Configs/UnitConfigs/EnemyConfigs");
            SetConfigData(newConfigData.CofigData);
        }
        [HorizontalGroup]
        [Button("Shaman")]
        private void SetShamanConfig()
        {
            var newConfigData = new CreateNewConfigData<ShamanConfig>("NewShamanConfig", "Assets/Configs/UnitConfigs/ShamanConfigs");
            SetConfigData(newConfigData.CofigData);
        }
        
        [InlineEditor(objectFieldMode: InlineEditorObjectFieldModes.CompletelyHidden)]
        public BaseConfig ConfigData;
        public void OnDestroy()
        {
            if(ConfigData != null)
                DestroyImmediate(ConfigData);
        }
    }
    public class CreateNewConfigData<T> where T : BaseConfig
    {
        private readonly string _configPath;
        private readonly string _configName;
        
        public CreateNewConfigData(string name,string path)
        {
            CofigData = ScriptableObject.CreateInstance<T>();
            _configPath = path;
            _configName = name;
            CofigData.Name = name;
        }
        
        public T CofigData;
    
        [Button("Add New Config")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(CofigData,_configPath + "/" + CofigData.Name + ".asset");
            AssetDatabase.SaveAssets();
            
            
            CofigData = ScriptableObject.CreateInstance<T>();
            CofigData.Name = _configName;
        }
    }
}
