using System.ComponentModel.DataAnnotations;

namespace FirstMVCApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }


        [Required(ErrorMessage = "Product Name is Required")]
        [MinLength(4, ErrorMessage = "Product Name should be 4 or more characters")]
        [StringLength(50, ErrorMessage = "Product Name cannot exceed 50 characters")]
        [NoSuccessiveChars()]
        public string ProductName { get; set; } = "";


        [Required(ErrorMessage = "Unit Price is Required")]
        [Range(minimum: 1, maximum: 10000, ErrorMessage = "Unit price should be between 1 and 1000")]
        [Display(Name = "Price per Unit")]
        public decimal UnitPrice { get; set; }


        [Required(ErrorMessage = "Units in stock is required")]
        [Range(minimum: 1, maximum: 1000, ErrorMessage = "Units in stock must be between 1 and 1000")]
        [Display(Name = "Stock Level")]
        public short UnitsInStock { get; set; }

        //StringLength, Compare, CreditCard, Datatype, Display, EmailAddress, Key, Phone, RegularExpression, Url
    }

    public class NoSuccessiveCharsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var item = (validationContext.ObjectInstance as Product);
            if (item is null)
            {
                return ValidationResult.Success;
            }
            var valueString = item.ProductName;

            if (valueString.Length > 1)
            {
                char c = valueString[0];
                for (int i = 1; i < valueString.Length; i++)
                {
                    var ch = valueString[i];
                    if (c == ch)
                    {
                        return new ValidationResult("Successive chars are not allowed.");
                    }
                    else
                        c = ch;
                }
            }
            return ValidationResult.Success;
        }
    }
}

