namespace Practise_Tasks.Interfaces
{
    public interface IInputValidate
    {
        string GetInvalidChars(string? input);
        bool IsValid(string? input);
    }
}
