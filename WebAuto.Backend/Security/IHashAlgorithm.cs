namespace WebAuto.Backend.Security
{
    public interface IHashAlgorithm
    {
        string Hash(string input);
    }
}
