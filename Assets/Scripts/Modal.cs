using UnityEngine;

[CreateAssetMenu(fileName = "Modal", menuName = "ScriptableObjects/Modal", order = 2)]
public class Modal : ScriptableObject {

    [SerializeField] private SequenceData _data;

    public float BonusWin { get; set; }
    public float TotalScore { get; set; }

    public IWinSequence GetWinSequence() {
        return _data.SequenceToUse;
    }

    public void SetWinSequence(int index) {
        IWinSequence sequence = new WinSequence(_data.Sequences[index]);
        SetWinSequence(sequence);
    }

    public void SetWinSequence(IWinSequence sequence) {
        _data.SequenceToUse = sequence;
    }

    public void Clear()
    {
        BonusWin = 0;
        TotalScore = 0;
    }
}
