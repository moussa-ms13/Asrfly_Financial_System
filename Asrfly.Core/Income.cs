using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asrfly.Core
{
    public class Income
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public int ProjectId { get; set; }
        public DateTime IncomeDate { get; set; }
        public string RecNo { get; set; }
        public double Amount { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }
        [NotMapped]
        public string SupplierName { get; set; }
        [NotMapped]
        public string ProjectName { get; set; }
    }
}
