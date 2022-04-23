namespace OneBan_TMS.Interfaces.Handlers
{
    public interface IValidatorHandler
    {
        bool ExistsUpperCharacters(string text);
        bool ExistsSpecialSigns(string text);
        bool ExistsNumbers(string text);
    }
}