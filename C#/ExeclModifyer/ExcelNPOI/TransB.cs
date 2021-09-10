using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelNPOI
{
    public class TransB
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public int RowNumber { get; set; }
    }
}
