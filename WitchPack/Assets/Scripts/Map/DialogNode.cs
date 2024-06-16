using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogNode : MapNode
{
    [BoxGroup("Dialog")][SerializeField] private DialogSequence _dialogConfig;
    protected override void OnNodeClick(PointerEventData.InputButton button)
    {
        DialogBox.Instance.SetDialogSequence(_dialogConfig,FinishDialog);
        base.OnNodeClick(button);
    }

    private void FinishDialog()
    {
        //change state to complete
    }
}