using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Models.Api
{
    public class UpdateRankRequest
    {
        public int TodoItemId { get; set; }
        public int Rank { get; set; }
    }
}
