using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Practise_Tasks.ExtensionMethods;
using Practise_Tasks.Interfaces;
using Practise_Tasks.Services;
using Practise_Tasks.Services.Sortings;
using Practise_Tasks.Settings;
using System.Text;

namespace Practise_Tasks.Controllers
{
    [ApiController]
    [Route("/reverser")]
    public class ReverseController : ControllerBase
    {
        private const string INVALID_CHARS_ERROR_MESSAGE = "Были введены неподходящие символы. " +
            "Необходимо использовать только латинские буквы в нижнем регистре. Вы использовали: ";
        private const string INVALID_SORTING_METHOD_MESSAGE = "Некорректное название алгоритма. Используйте 'quicksort' или 'treesort'.";
        private const string BLACK_LIST_WARNING = "Данная строка находится в чёрном списке, попробуйте использовать другую.";
        private readonly IInputValidate _validator;
        private readonly IItemsCounter<char, string> _counter;
        private readonly ISubsetFinder<string> _finder;
        private readonly IRandomNumber _randomNumber;
        private readonly IStringHandler _handler;
        private readonly ReverseControllerSettings _settings;
        private ISorter<string, char> _sorter = new QuickSort();

        public ReverseController(IInputValidate validator, 
            IItemsCounter<char, string> counter, ISubsetFinder<string> finder, IRandomNumber randomNumber,
            IOptions<ReverseControllerSettings> settings, IStringHandler handler) 
        {
            _validator = validator;
            _counter = counter;
            _finder = finder;
            _randomNumber = randomNumber;
            _handler = handler;
            _settings = settings.Value;
        }

        [HttpGet]
        public async Task<ActionResult<string[]>> GetNewString(string? input, string sortAlgorithm = "quicksort")
        {
            if (!_validator.IsValid(input) || input == null)
                return BadRequest(INVALID_CHARS_ERROR_MESSAGE + $"\'{_validator.GetInvalidChars(input)}\'");

            if (_settings.BlackList.Contains(input))
                return BadRequest(BLACK_LIST_WARNING);

            switch (sortAlgorithm.ToLower())
            {
                case "quicksort":
                    _sorter = new QuickSort();
                    break;
                case "treesort":
                    _sorter = new TreeSort();
                    break;
                default:
                    return BadRequest(INVALID_SORTING_METHOD_MESSAGE);
            }

            string processedString = _handler.GetNewString(input);

            var cutString = new StringBuilder(processedString);
            var randNum = await _randomNumber.GetIntAsync(0, processedString.Length);
            cutString.Remove(randNum, 1);

            string sortedString = processedString;
            _sorter.Sort(ref sortedString);

            var result = new string[] {
                processedString,
                GetCharsAmount(processedString),
                _finder.FindSubset(processedString),
                sortedString,
                cutString.ToString()
            };
            return Ok(result);
        }

        private string GetCharsAmount(string msg)
        {
            var amountInfoBuilder = new StringBuilder();
            Dictionary<char, int> amounts = _counter.CountItems(msg);

            foreach (char item in msg.ToUniqueCharsArray())
            {
                amountInfoBuilder.Append($" {item}:{amounts[item]}");
            }
            amountInfoBuilder.Remove(0, 1);
            return amountInfoBuilder.ToString();
        }
    }
}
