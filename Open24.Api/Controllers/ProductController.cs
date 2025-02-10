using Microsoft.AspNetCore.Mvc;
using Open24.Application.Interfaces;
using Open24.Shared.Constants;
using Open24.Shared.DTOs.Requests;
using Open24.Shared.DTOs.Responses;

namespace Open24.Api.Controllers
{
    [Route("api/product")]
    public class ProductController : BaseController
    {
        #region Properties
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Create
        [HttpPost()]
        public Task<IActionResult> Create(ProductRequest productRequest) =>
            ExecuteAsync(async () =>
            {
                var createdEntity = await _productService.CreateAsync(productRequest);
                return CreateSuccessResponse(createdEntity, StatusCodes.Status201Created);
            });
        #endregion

        #region GetAll
        [HttpGet()]
        public Task<IActionResult> GetAll(int count = -1, int skip = -1, string search = "") =>
            ExecuteAsync(async () =>
            {
                var entities = await _productService.GetAllAsync(count, skip, search);
                return CreateSuccessResponse(entities, StatusCodes.Status200OK);
            });
        #endregion

        #region GetById
        [HttpGet("{id}")]
        public Task<IActionResult> GetById(Guid id) =>
            ExecuteAsync(async () =>
            {
                var entity = await _productService.GetByIdAsync(id);
                if (entity == null)
                    return CreateErrorResponse<ProductResponse>(StatusCodes.Status404NotFound, ResponseMessages.NotFound);

                return CreateSuccessResponse(entity, StatusCodes.Status200OK);
            });
        #endregion

        #region Update
        [HttpPut()]
        public Task<IActionResult> Update(ProductRequest productRequest) =>
            ExecuteAsync(async () =>
            {
                var updatedEntity = await _productService.UpdateAsync(productRequest);
                if (updatedEntity == null)
                    return CreateErrorResponse<ProductResponse>(StatusCodes.Status404NotFound, ResponseMessages.UpdateFailed);

                return CreateSuccessResponse(updatedEntity, StatusCodes.Status200OK);
            });
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public Task<IActionResult> Delete(Guid id) =>
            ExecuteAsync(async () =>
            {
                var entity = await _productService.GetByIdAsync(id);
                if (entity == null)
                    return CreateErrorResponse<ProductResponse>(StatusCodes.Status404NotFound, ResponseMessages.NotFound);

                await _productService.DeleteAsync(id);
                return CreateSuccessResponse<ProductResponse>(null, StatusCodes.Status204NoContent);
            });
        #endregion
    }
}
