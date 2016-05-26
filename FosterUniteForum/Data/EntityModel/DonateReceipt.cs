using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Data.EntityModel
{
    [Table("DonateReceipt")]
    public partial class DonateReceipt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonateReceipt()
        {
        }

        public int Id { get; set; }

        public string Card_Token { get; set; }

        public string Token { get; set; }
        
        public string Amount { get; set; }
        
        public string Currency { get; set; }
        
        public string Description { get; set; }
        
        public string Email { get; set; }
        
        public string IP_address { get; set; }
        
        public DateTime Created { get; set; }
    }
}
