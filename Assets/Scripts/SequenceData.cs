using UnityEngine;

[CreateAssetMenu(fileName = "SequenceData", menuName = "ScriptableObjects/SequenceData", order = 1)]

public class SequenceData : ScriptableObject
{
    [SerializeField] private string[] _sequences;

    public IWinSequence SequenceToUse { get; set; }
    public string[] Sequences => _sequences;
}
