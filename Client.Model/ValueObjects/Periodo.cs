using System;
using System.Collections.Generic;
using System.Text;

namespace Nexo.Contract.Models.ValueObjects
{
    public class Period
    {
        public DateTime InitialDate { get; private set; }
        public DateTime FinalDate { get; private set; }

        private Period(DateTime initialDate, DateTime finalDate)
        {
            InitialDate = initialDate;
            FinalDate = finalDate;
        }

        public static Period OneYearAt(DateTime date)
        {
            return new Period(date.AddDays(-365), date);
        }

        public bool Contains(DateTime date)
        {
            return date >= InitialDate && date <= FinalDate;
        }
    }
}
