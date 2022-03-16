using Business.Abstract;
using Business.Contants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        private IProductDal _productDal;
        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
        }
        //Priority, eğer validasyon için başka bi kural daha yazılmışsa sıralama bildiriyor.
        //[ValidationAspect(typeof(ProductValidator), Priority = 2)]
        [ValidationAspect(typeof(ProductValidator), Priority = 1)] 
        
        public IResult Add(Product product)
        {

            #region MyNotes
            /* 
                 * Evet çalışır fakat best practies yapısına uygun değil. çünkü update işlemi için ben bunu tekrar yazmam gerekicek. 
                 * Business kuralı için uygun bi yapı da değil extra maliyet.
                 * Bunun için ben bunu bir Business kuralı olarak belirleyip hem clean code hem best practies yapısına uygun hale getirmeliyim. (FluentValidation kullandım. Nedir? FluentValidation bir veri doğrulama kütüphanesidir.)             
                 */
            //if (!String.IsNullOrEmpty(product.ProductName)) 
            //{
            //    _productDal.Add(product);
            //} 
            #endregion

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);

        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(c => c.CategoryId == categoryId).ToList());
        }

        public IResult Update(Product product)
        {
            //product.UpdatedDate = DateTime.Now;
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
