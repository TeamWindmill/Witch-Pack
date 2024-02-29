using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class ConfigWindowEditor : OdinMenuEditorWindow
{

    private CreateNewConfigData<EnemyConfig> _createNewEnemyData;
    
    [MenuItem("Tools/ConfigEditor")]
    private static void OpenWindow()
    {
        GetWindow<ConfigWindowEditor>().Show();
    }
    
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        _createNewEnemyData = new CreateNewConfigData<EnemyConfig>("NewEnemyConfig", "Assets/Configs/UnitConfigs/EnemyConfigs");
        tree.Add("Create New Enemy Config", _createNewEnemyData);
        tree.AddAllAssetsAtPath("Enemies","Assets/Configs/UnitConfigs/EnemyConfigs",typeof(EnemyConfig));
        tree.AddAllAssetsAtPath("Shamans","Assets/Configs/UnitConfigs/ShamanConfigs",typeof(ShamanConfig));
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
        
        if(_createNewEnemyData != null)
            DestroyImmediate(_createNewEnemyData.CofigData);
    }

    public class CreateNewConfigDatas
    {
        public CreateNewConfigDatas()
        {
            
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
        
        [InlineEditor(objectFieldMode: InlineEditorObjectFieldModes.Hidden)]
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
