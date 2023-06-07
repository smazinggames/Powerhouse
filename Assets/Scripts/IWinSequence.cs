public interface IWinSequence 
{
    public float[] GetSequence();

    // uses a comma seperated string of values
    public void SetSequence(string sequence);

    public void SetSequence(float[] sequence);
}
