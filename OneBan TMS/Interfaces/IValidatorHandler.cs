namespace OneBan_TMS.Interfaces
{
    public interface IValidatorHandler
    {
        bool ExistsUpperCharacters(string text);
        bool ExistsSpecialSigns(string text);
        bool ExistsNumbers(string text);
    }
}