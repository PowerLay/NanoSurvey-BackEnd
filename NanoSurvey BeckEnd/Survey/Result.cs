﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NanoSurvey_BeckEnd.Survey
{
    public class Result
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Question Question { get; set; }
        public Interview Interview { get; set; }
    }
}