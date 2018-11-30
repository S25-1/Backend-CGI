﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cgiAPI.Models
{
    public class Skill
    {
        public int SkillTypeID { get; set; }
        public string SkillTypeName { get; set; }

        public Skill(int skillTypeID, string skillTypeName)
        {
            SkillTypeID = skillTypeID;
            SkillTypeName = skillTypeName;
        }

        public Skill(int skillTypeID)
        {
            SkillTypeID = skillTypeID;
            SkillTypeName = "null";
        }

    }
}
