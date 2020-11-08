using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.DTO.Results
{
    public class SuccessResultDTO: ResultDTO
    {
        public string Token { get; set; }
    }
}
