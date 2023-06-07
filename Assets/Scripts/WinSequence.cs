using System.Collections.Generic;
using System.Linq;

public class WinSequence: IWinSequence 
{
    private List<float> _sequence = new();

    public float[] GetSequence() {
        return _sequence.ToArray();
    }

    public WinSequence(string sequence) {
        SetSequence(sequence);
    }
    public WinSequence(float[] sequence) {
        SetSequence(sequence);
    }

    // takes a comma seperated list of values
    public void SetSequence(string sequence) {
        string[] values = sequence.Split(',');
        foreach(string val in values) {
            if(float.TryParse(val, out var result)) {
                _sequence.Add(result);
            }
        }
    }

    public void SetSequence(float[] sequence) {
        _sequence = sequence.OfType<float>().ToList();
    }
}
