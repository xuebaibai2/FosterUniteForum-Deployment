using FosterUniteForum.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain
{
    public class DonateReceiptService : BasicService
    {
        private DonateReceipt receipt;
        public DonateReceiptService()
        {
            receipt = new DonateReceipt();
        }
        public DonateReceiptService(ForumDbContext context) : base(context) { }

        public void AddReceipt(DonateReceipt receipt)
        {
            context.DonateReceipt.Add(receipt);
            context.SaveChanges();
        }

        public DonateReceipt GetDonateReceiptByToken(string token)
        {
            return context.DonateReceipt.Where(m => m.Token.Equals(token)).Single();
        }
    }
}
