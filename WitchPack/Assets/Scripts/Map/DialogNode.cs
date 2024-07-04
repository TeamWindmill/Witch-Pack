using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogNode : MapNode
{
    [BoxGroup("Dialog")][SerializeField] private DialogSequence _dialogConfig;
    [BoxGroup("Dialog")] public ShamanConfig[] shamansToAddAfterComplete;

    protected override void OnNodeClick(PointerEventData.InputButton button)
    {
        base.OnNodeClick(button);
        DialogBox.Instance.SetDialogSequence(_dialogConfig,FinishDialog);
        DialogBox.Instance.Show();
    }

    public override void Complete()
    {
        base.Complete();
        GameManager.ShamansManager.AddShamanToRoster(shamansToAddAfterComplete);
        //GameManager.SaveData.MapNodes[Index].Complete();
        GameManager.SaveData.LastLevelCompletedIndex = Index;
    }

    private void FinishDialog()
    {
        Complete();
        MapManager.Instance.Init();
    }
}