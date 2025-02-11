﻿using Microsoft.AspNetCore.Mvc;
using Practise_Tasks.Interfaces;
using System.Text;

namespace Practise_Tasks.Controllers
{
    [ApiController]
    [Route("/reverser")]
    public class ReverseController
    {
        private const string INVALID_CHARS_ERROR_MESSAGE = "Были введены неподходящие символы. " +
            "Необходимо использовать только латинские буквы в нижнем регистре. Вы использовали: ";
        private IInputValidate _validator;

        public ReverseController(IInputValidate validator) =>
            _validator = validator;

        [HttpGet]
        public string GetNewString(string? input)
        {
            if (!_validator.IsValid(input))
                return INVALID_CHARS_ERROR_MESSAGE + $"\'{_validator.GetInvalidChars(input)}\'";

            if (input.Length % 2 == 0)
            {
                (string half1, string half2) = DivideInHalf(input);

                (half1, half2) = (Reverse(half1), Reverse(half2));

                var result = new StringBuilder(half1 + half2);
                
                return result.ToString();
            }
            else
            {
                var result = new StringBuilder(Reverse(input));
                result.Append(input);
                return result.ToString();
            }
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
