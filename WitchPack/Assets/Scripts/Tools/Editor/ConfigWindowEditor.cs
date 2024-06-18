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
        tree.AddAllAssetsAtPath("Units/Enemies/Base",                   "Assets/Configs/EnemyConfigs",typeof(EnemyConfig));
        tree.AddAllAssetsAtPath("Units/Enemies/Bosses",                   "Assets/Configs/EnemyConfigs/Bosses",typeof(EnemyConfig));
        tree.AddAllAssetsAtPath("Units/Shamans/Base",                   "Assets/Configs/ShamanConfigs",typeof(ShamanConfig));
        tree.AddAllAssetsAtPath("Units/Shamans/Energy",                        "Assets/Configs/ExpAndEnergy/Energy",typeof(EnergyConfig));
        tree.AddAllAssetsAtPath("Units/Shamans/Exp",                        "Assets/Configs/ExpAndEnergy/Experience",typeof(ExperienceConfig));
        tree.AddAllAssetsAtPath("PowerStructures",                "Assets/Configs/StructuresConfig",typeof(PowerStructureConfig));
        tree.AddAllAssetsAtPath("Abilities/Shamans/AutoAttack",         "Assets/Configs/Abilities/Shamans",typeof(AbilitySO));
        tree.AddAllAssetsAtPath("Abilities/Shamans/Toor/Multishot",     "Assets/Configs/Abilities/Shamans/Toor/Multishot",typeof(AbilitySO));
        tree.AddAllAssetsAtPath("Abilities/Shamans/Toor/PiercingShot",  "Assets/Configs/Abilities/Shamans/Toor/PiercingShot",typeof(AbilitySO));
        tree.AddAllAssetsAtPath("Abilities/Shamans/Toor/Passives",      "Assets/Configs/Abilities/Shamans/Toor/Passives",typeof(AbilitySO));
        tree.AddAllAssetsAtPath("Abilities/Shamans/Nadia/RootingVines", "Assets/Configs/Abilities/Shamans/Nadia/RootingVines",typeof(AbilitySO));
        tree.AddAllAssetsAtPath("Abilities/Shamans/Nadia/Heal",         "Assets/Configs/Abilities/Shamans/Nadia/Heal",typeof(AbilitySO));
        tree.AddAllAssetsAtPath("Abilities/Shamans/Nadia/Passives",     "Assets/Configs/Abilities/Shamans/Nadia/Passives",typeof(AbilitySO));
        tree.AddAllAssetsAtPath("Abilities/Shamans/Javan/SmokeBomb",    "Assets/Configs/Abilities/Shamans/Javan/SmokeBomb",typeof(AbilitySO));
        tree.AddAllAssetsAtPath("Abilities/Shamans/Javan/Charm",        "Assets/Configs/Abilities/Shamans/Javan/Charm",typeof(AbilitySO));
        tree.AddAllAssetsAtPath("Abilities/Shamans/Javan/Passives",     "Assets/Configs/Abilities/Shamans/Javan/Passives",typeof(AbilitySO));
        tree.AddAllAssetsAtPath("Abilities/Enemies",                    "Assets/Configs/Abilities/Enemies",typeof(AbilitySO));
        tree.AddAllAssetsAtPath("Abilities/StatusEffects/Roots",        "Assets/Configs/Abilities/StatusEffects/Roots",typeof(StatusEffectConfig));
        tree.AddAllAssetsAtPath("Abilities/StatusEffects/SmokeBomb",    "Assets/Configs/Abilities/StatusEffects/SmokeBomb",typeof(StatusEffectConfig));
        tree.AddAllAssetsAtPath("MetaUpgrades",    "Assets/Configs/MetaUpgrades/ShamanConfigs",typeof(ShamanMetaUpgradeConfig));
        tree.AddAllAssetsAtPath("Level/Levels",                         "Assets/Configs/Levels",typeof(LevelConfig));
        tree.AddAllAssetsAtPath("Level/Waves",                          "Assets/Configs/Waves",typeof(WaveData));
        return tree;
    }

    // protected override void OnBeginDrawEditors()
    // {
    //     var selected = this.MenuTree.Selection;
    //
    //     SirenixEditorGUI.BeginHorizontalToolbar();
    //     {
    //         GUILayout.FlexibleSpace();
    //
    //         if (SirenixEditorGUI.ToolbarButton("Delete"))
    //         {
    //             var asset = selected.SelectedValue;
    //             string path = AssetDatabase.GetAssetPath((Object)asset);
    //             AssetDatabase.DeleteAsset(path);
    //             AssetDatabase.SaveAssets();
    //         }
    //     }
    //     SirenixEditorGUI.EndHorizontalToolbar();
    // }

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
