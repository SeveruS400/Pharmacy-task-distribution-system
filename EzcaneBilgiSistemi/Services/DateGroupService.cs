using System;
using System.Collections.Generic;
using System.Linq;
using Repositories.Contracts; // eksik olan using ifadesi
using Repositories.Implementations;

namespace EzcaneBilgiSistemi.Services
{
    public class DateGroupService
    {
        #region Ctor
        public List<DateTime> Weekdays { get; set; }
        public List<DateTime> Saturdays { get; set; }
        public List<DateTime> Sundays { get; set; }
        public List<DateTime> OfficialHolidays { get; set; }
        private readonly IResmiTatillerRepository _resmiTatillerRepository;


        // Constructor aracılığıyla _resmiTatillerRepository nesnesini alabilirsiniz.
        public DateGroupService(IResmiTatillerRepository resmiTatillerRepository)
        {
            _resmiTatillerRepository = resmiTatillerRepository;

        }
        #endregion
        public DateGroupService GroupDates(DateTime startD, DateTime endD)
        {
            //DateTime startDate = new DateTime(2024, 01, 01);
            //DateTime endDate = new DateTime(2024, 12, 30);
            DateTime startDate = startD;
            DateTime endDate = endD;

            List<DateTime> allDates = GetAllDatesBetween(startDate, endDate);

            DateGroupService groupedDates = GroupDatesByDay(allDates);

            return groupedDates; // Eğer bir değer döndürmek istiyorsanız, DateGroup nesnesini döndürün.
        }

        private List<DateTime> GetAllDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            return allDates;
        }

        private DateGroupService GroupDatesByDay(List<DateTime> allDates)
        {
            DateGroupService groupedDates = new DateGroupService(_resmiTatillerRepository)
            {
                Weekdays = new List<DateTime>(),
                Saturdays = new List<DateTime>(),
                Sundays = new List<DateTime>(),
                OfficialHolidays = new List<DateTime>()
            };

            foreach (var date in allDates)
            {
                //if (date.DayOfWeek == DayOfWeek.Saturday)
                //{
                //    groupedDates.Saturdays.Add(date);
                //}
                //else if (date.DayOfWeek == DayOfWeek.Sunday)
                //{
                //    groupedDates.Sundays.Add(date);
                //}

                // Tüm günler için resmi tatil kontrolü
                var isOfficialHoliday = IsOfficialHoliday(date);
                if (isOfficialHoliday)
                {
                    groupedDates.OfficialHolidays.Add(date);
                }
                else
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        groupedDates.Saturdays.Add(date);
                    }
                    else if (date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        groupedDates.Sundays.Add(date);
                    }
                    else
                    {
                        groupedDates.Weekdays.Add(date);
                    }

                }
            }

            return groupedDates;
        }

        private bool IsOfficialHoliday(DateTime date)
        {
            var resmiTatiller = _resmiTatillerRepository.GetResmiTatillerByDate(date, date);
            return resmiTatiller.Any();
        }
    }
}
