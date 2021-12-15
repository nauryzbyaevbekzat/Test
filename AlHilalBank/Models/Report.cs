using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlHilalBank.Models
{
   
        public class Report
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int RNUM { get; set; }

            public string REPORT_DATE { get; set; }

            public int CREADITOR_ID { get; set; }

            public string GL_NUMBER { get; set; }

            public string DESC { get; set; }

            public string RESIDENT_CODE { get; set; }

            public string SECTOR_ECO { get; set; }

            public string CURR_CODE { get; set; }

            public int AMOUNT_LCY { get; set; }


        }
    
}
