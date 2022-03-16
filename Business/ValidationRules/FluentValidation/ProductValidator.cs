using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("ProductName boş olamaz!");
            RuleFor(p => p.ProductName).Length(2,30);
            RuleFor(p => p.UnitPrice).NotEmpty().WithMessage("UnitPrice boş olamaz!");
            RuleFor(p => p.UnitPrice).NotNull();
        }
    }
}
