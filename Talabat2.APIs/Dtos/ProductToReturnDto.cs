namespace Talabat2.APIs.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int ProductBrandId { set; get; }
        public string ProductBrand { get; set;}
        public int ProductTypeId { set; get; }
        public string ProductType { get; set; }
    }
}
