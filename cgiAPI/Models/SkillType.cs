using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cgiAPI.Controllers
{
    public class SkillType
    {
        public int SkillTypeID { get; set; }
        public string SkillTypeName { get; set; }

        public SkillType(int skillTypeID, string skillTypeName)
        {
            SkillTypeID = skillTypeID;
            SkillTypeName = skillTypeName;
        }

    }
}
