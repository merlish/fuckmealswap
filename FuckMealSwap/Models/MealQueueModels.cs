using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Linq;

namespace FuckMealSwap.Models
{
    public class MealQueueContext : DbContext
    {
        public MealQueue sundayQueue { get; set; }
        public MealQueue todayQueue { get; set; }
    }

    public class MealQueue
    {
        public DateTime MealWaitStart { get; set; }
        public DateTime MealWaitEnd { get; set; }
        public Dictionary<string, DateTime> UserLastCheckedIn { get; set; }

        public List<string> ValidUsers
        {
            get
            {
                var threshold = DateTime.Now.AddSeconds(-60);
                return new List<string>(UserLastCheckedIn.Keys.Where<string>(name => UserLastCheckedIn[name] >= threshold));
            }
        }

        public MealQueue(DateTime start, DateTime end)
        {
            MealWaitStart = start; MealWaitEnd = end; 
            UserLastCheckedIn = new Dictionary<string, DateTime>();
        }
    }
}