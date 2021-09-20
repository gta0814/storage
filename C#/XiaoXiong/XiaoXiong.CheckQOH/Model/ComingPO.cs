using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoXiong.CheckQOH.Model
{
    public class ComingPO
    {
        public int Id { get; set; }
        public string CPOInternalRef { get; set; }
        public int Qty { get; set; }
        public DateTime ComingDate { get; set; }
    }
}
