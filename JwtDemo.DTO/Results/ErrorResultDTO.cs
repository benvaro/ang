using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.DTO.Results
{
    public class ErrorResultDTO: ResultDTO
    {
        public List<string> Errors { get; set; } = new List<string>();
    }
}
