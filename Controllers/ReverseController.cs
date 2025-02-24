using Microsoft.AspNetCore.Mvc;
using Practise_Tasks.ExtensionMethods;
using Practise_Tasks.Interfaces;
using Practise_Tasks.Services;
using Practise_Tasks.Services.Sortings;
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
        private readonly IInputValidate _validator;
        private readonly IItemsCounter<char, string> _counter;
        private readonly ISubsetFinder<string> _finder;
        private readonly IRandomNumber _randomNumber;
        private ISorter<string, char> _sorter = new QuickSort();

        public ReverseController(IInputValidate validator, 
            IItemsCounter<char, string> counter, ISubsetFinder<string> finder, IRandomNumber randomNumber) 
        {
            _validator = validator;
            _counter = counter;
            _finder = finder;
            _randomNumber = randomNumber;
        }

        [HttpGet]
        public async Task<ActionResult<string[]>> GetNewString(string? input, string sortAlgorithm = "quicksort")
        {
            if (!_validator.IsValid(input) || input == null)
                return BadRequest(INVALID_CHARS_ERROR_MESSAGE + $"\'{_validator.GetInvalidChars(input)}\'");


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

            string processedString;

            if (input.Length % 2 == 0)
            {
                (string half1, string half2) = DivideInHalf(input);
                 processedString = Reverse(half1) + Reverse(half2);
            }
            else
                processedString = Reverse(input) + input;

            var cutString = new StringBuilder(processedString);
            var randNum = await _randomNumber.GetIntAsync(0, processedString.Length);
            cutString.Remove(randNum, 1);

            string sortedString = processedString;
            _sorter.Sort(ref sortedString);

            var result = new string[] {processedString, GetCharsAmount(processedString),
                GetMaxVowelSpan(processedString), sortedString, cutString.ToString() };
            return Ok(result);
        }

        private string GetMaxVowelSpan(string msg)
        {
            return _finder.FindSubset(msg);
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

        private string Reverse(string input)
        {
            var result = new StringBuilder();
            
            for (int i = input.Length - 1; i >= 0; i--)
                result.Append(input[i]);
            
            return result.ToString();
        }

        private (string, string) DivideInHalf(string input)
        {
            StringBuilder half1 = new(), half2 = new();
            
            for (int i = 0; i < input.Length / 2; i++)
            {
                half1.Append(input[i]);
                half2.Append(input[i + input.Length / 2]);
            }

            return (half1.ToString(), half2.ToString());
        }

    }
}
