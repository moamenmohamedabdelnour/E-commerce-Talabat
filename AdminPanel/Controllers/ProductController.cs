using AdminPanel.Helpers;
using AdminPanel.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat2.Core;
using Talabat2.Core.Entites;
using Talabat2.Core.Specifications;

namespace AdminPanel.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork=unitOfWork;
            this.mapper=mapper;
        }
        public async Task<IActionResult> Index()
        {
            //Get All Products
          
            var Products = await unitOfWork.Repository<Product>().GetAllAsync();
            var MappedProducts = mapper.Map<IReadOnlyList< Product>,IReadOnlyList< ProductViewModel>>(Products);
            return View(MappedProducts);
        }
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(ProductViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Image != null)
					model.PictureUrl = PictureSetting.UploadFile(model.Image, "products");
				else
					model.PictureUrl = "images/products/hat-react2.png";

				var mappedProduct = mapper.Map<ProductViewModel, Product>(model);
				await unitOfWork.Repository<Product>().Add(mappedProduct);
				await unitOfWork.Complete();
				return RedirectToAction("Index");
			}
			return View(model);
		}
        public async Task<IActionResult> Edit(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
            var mappedProduct = mapper.Map<Product, ProductViewModel>(product);
            return View(mappedProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if (id != model.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                if (model.PictureUrl != null)
                {
                    if (model.PictureUrl != null)
                    {
                        PictureSetting.DeleteFile(model.PictureUrl, "products");
                        model.PictureUrl = PictureSetting.UploadFile(model.Image, "products");
                    }
                    else
                    {
                        model.PictureUrl = PictureSetting.UploadFile(model.Image, "products");

                    }

                }
                var mappedProduct = mapper.Map<ProductViewModel, Product>(model);
                unitOfWork.Repository<Product>().Update(mappedProduct);
                var result = await unitOfWork.Complete();
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
            var mappedProduct = mapper.Map<Product, ProductViewModel>(product);
            return View(mappedProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, ProductViewModel model)
        {
            if (id !=model.Id)
                return NotFound();
            try
            {
                var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
                if (product.PictureUrl != null)
                    PictureSetting.DeleteFile(product.PictureUrl, "products");

                unitOfWork.Repository<Product>().Delete(product);
                await unitOfWork.Complete();
                return RedirectToAction("Index");

            }
            catch (System.Exception)
            {

                return View(model);
            }
        }
    }
}
