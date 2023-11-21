using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JQueryAjaxCRUDInASPNETCore.Models
{
    public class TransactionModal
    {
        [Key]
        public int TransActionId { get; set; }
        [Required(ErrorMessage = "this filed is require.")]
        [MaxLength(12)]
        [DisplayName("Account Number")]
        [Column(TypeName ="nvarchar(12)")]
        public string AccountNumber { get; set; }
        [Required(ErrorMessage = "this filed is require.")]

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Beneficiary Name")]
        public string BeneficiaryName { get; set; }
        [Required(ErrorMessage = "this filed is require.")]

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Bank Name")]
        public string BankName { get; set; }
        [Required(ErrorMessage = "this filed is require.")]

        [Column(TypeName = "nvarchar(11)")]
        [DisplayName("WWIFT Code")]
        [MaxLength(12)]
        public string WWIFTCode { get; set; }
        [Required(ErrorMessage = "this filed is require.")]
        
        public int Amount { get; set; }

        [DisplayFormat(DataFormatString ="0:MM/dd/yyyy")]
        public DateTime Date { get; set; }
    }
}
