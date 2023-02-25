﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnProgramming.Core.Dto
{
    public class TasksInfoDto : TasksInfoBaseDto
    {
        public string Description { get; set; }
        public string DataAndAnswers { get; set; }
        public byte[] AdditionalInformacionFile { get; set; }
    }
}
