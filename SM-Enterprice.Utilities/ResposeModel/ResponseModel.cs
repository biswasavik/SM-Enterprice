using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM_Enterprice.Utilities.ResposeModel
{
    public class ResponseModel
    {
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public bool Status { get; set; }
        public List<String> Errors { get; set; }
        public Guid CreatedId { get; set; }
        public Object Result { get; set; }

    }
}
