using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB5.WebAPI.Database.AppDbContextModels;

namespace TB5.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        // CREATE
        [HttpPost]
        public IActionResult Create(string productName, decimal productPrice)
        {
            var product = new TblProduct
            {
                Name = productName,
                Price = productPrice,
                CreatedDateTime = DateTime.Now
            };

            try
            {
                _db.TblProducts.Add(product);
                var result = _db.SaveChanges();

                if (result > 0)
                    return Ok("A new product created successfully.");

                return BadRequest("Failed to create a new product.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message,
                    Detail = ex.InnerException?.ToString()
                });
            }
        }

        // READ ALL
        [HttpGet]
        public IActionResult Read()
        {
            var lstProducts = _db.TblProducts.ToList();

            if (lstProducts.Count == 0)
            {
                return NotFound("No products found.");
            }

            return Ok(lstProducts);
        }

        // READ ONE (Edit)
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var product = _db.TblProducts.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product not found.");
            }

            return Ok(product);
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(int id, string productName, decimal productPrice)
        {
            var product = _db.TblProducts.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product not found.");
            }

            product.Name = productName;
            product.Price = productPrice;
            product.ModifiedDateTime = DateTime.Now;

            try
            {
                var result = _db.SaveChanges();

                if (result > 0)
                    return Ok($"Product {id} is updated successfully.");

                return BadRequest($"Failed to update the product {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message,
                    Detail = ex.InnerException?.ToString()
                });
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] ProductPatchDto dto)
        {
            var product = _db.TblProducts.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product not found.");
            }

            // Apply partial updates
            if (!string.IsNullOrEmpty(dto.ProductName))
            {
                product.Name = dto.ProductName;
            }

            if (dto.ProductPrice.HasValue)
            {
                product.Price = dto.ProductPrice.Value;
            }

            product.ModifiedDateTime = DateTime.Now;

            try
            {
                var result = _db.SaveChanges();

                if (result > 0)
                    return Ok($"Product {id} is updated successfully.");

                return BadRequest($"Failed to update the product {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message,
                    Detail = ex.InnerException?.ToString()
                });
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _db.TblProducts.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product not found.");
            }

            try
            {
                _db.TblProducts.Remove(product);
                var result = _db.SaveChanges();

                if (result > 0)
                    return Ok($"Product {id} is deleted successfully.");

                return BadRequest($"Failed to delete the product {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message,
                    Detail = ex.InnerException?.ToString()
                });
            }
        }

    }

    public class ProductPatchDto
    {
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
    }
}
