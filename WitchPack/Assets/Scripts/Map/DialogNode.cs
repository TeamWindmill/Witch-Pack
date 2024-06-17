using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogNode : MapNode
{
    [BoxGroup("Dialog")][SerializeField] private DialogSequence _dialogConfig;
    protected override void OnNodeClick(PointerEventData.InputButton button)
    {
        base.OnNodeClick(button);
        DialogBox.Instance.SetDialogSequence(_dialogConfig,FinishDialog);
        DialogBox.Instance.Show();
    }

    protected override void Complete()
    {
        base.Complete();
        GameManager.SaveData.MapNodes[Index].SetState(NodeState.Completed);
        GameManager.SaveData.LastLevelCompletedIndex = Index;
    }

    private void FinishDialog()
    {
        Complete();
        MapManager.Instance.Init();
    }
}