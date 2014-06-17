using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOD_Club_Acrossdivisions
{
    [TableName("MOD_Club_Acrossdivisions.LoginSchool")]
    public class LoginSchool : ActiveRecord
    {
        /// <summary>
        /// 可連線學校
        /// </summary>
        [Field(Field = "school_name", Indexed = true)]
        public string School_Name { get; set; }


    }
}
