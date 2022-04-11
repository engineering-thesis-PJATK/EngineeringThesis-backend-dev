using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using OneBan_TMS.Interfaces;

namespace OneBan_TMS.Validators
{
    public class ValidatorHandler : IValidatorHandler
    {
        public bool ExistsUpperCharacters(string text)
        {
            return text.Any(char.IsUpper);
        }

        public bool ExistsSpecialSigns(string text)
        {
            Regex regex = new Regex("[^a-zA-Z0-9]");
            return regex.IsMatch(text.Trim());
        }

        public bool ExistsNumbers(string text)
        {
            return text.Any(char.IsNumber);
        }
    }
}