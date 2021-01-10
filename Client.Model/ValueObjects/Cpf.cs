using System;
namespace Nexo.Contract.Models.ValueObjects
{
    public struct Cpf
    {
        private readonly string _value;

        public readonly bool IsValid;
        private Cpf(string value)
        {
            _value = value;

            if (value == null)
            {
                IsValid = false;
                return;
            }

            var position = 0;
            var firstTotalDigit = 0;
            var secondTotalDigit = 0;
            var firstCheckerDigit = 0;
            var secondCheckerDigit = 0;

            bool sameDigit = true;
            var lastDigit = -1;

            foreach (var c in value)
            {
                if (char.IsDigit(c))
                {
                    var digito = c - '0';
                    if (position != 0 && lastDigit != digito)
                    {
                        sameDigit = false;
                    }

                    lastDigit = digito;
                    if (position < 9)
                    {
                        firstTotalDigit += digito * (10 - position);
                        secondTotalDigit += digito * (11 - position);
                    }
                    else if (position == 9)
                    {
                        firstCheckerDigit = digito;
                    }
                    else if (position == 10)
                    {
                        secondCheckerDigit = digito;
                    }

                    position++;
                }
            }

            if (position > 11)
            {
                IsValid = false;
                return;
            }

            if (sameDigit)
            {
                IsValid = false;
                return;
            }

            var digit1 = firstTotalDigit % 11;
            digit1 = digit1 < 2
                ? 0
                : 11 - digit1;

            if (firstCheckerDigit != digit1)
            {
                IsValid = false;
                return;
            }

            secondTotalDigit += digit1 * 2;

            var digit2 = secondTotalDigit % 11;

            digit2 = digit2 < 2
                ? 0
                : 11 - digit2;

            IsValid = secondCheckerDigit == digit2;
        }

        public static implicit operator Cpf(string value)
            => new Cpf(value);

        public override string ToString() => _value;

        public static bool ValidateCpf(Cpf sourceCPF) => sourceCPF.IsValid;
    }

}
