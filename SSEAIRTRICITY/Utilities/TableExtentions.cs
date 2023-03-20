using OpenQA.Selenium.DevTools.V108.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SSEAIRTRICITY.Utilities
{
    public class TableExtensions
    {
        public static Dictionary<string, string> ToDictionary(Table table)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            return dictionary;
        }

        public static List<AppliancesDetails> GetTable(Table table)
        {
            return table.CreateSet<AppliancesDetails>().ToList();
        }

    }
}
