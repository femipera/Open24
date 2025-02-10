using AutoMapper;
using Open24.Application.Interfaces;
using Open24.Domain.Entities;
using Open24.Domain.Interfaces;
using Open24.Shared.Constants;
using Open24.Shared.DTOs.Requests;
using Open24.Shared.DTOs.Responses;

namespace Open24.Application.Services;

public class ProductService : IProductService
{
    #region Properties
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(IMapper mapper, IProductRepository productRepository) 
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    #endregion

    #region Create
    public async Task<ProductResponse> CreateAsync(ProductRequest productRequest)
    {
        var product = _mapper.Map<Product>(productRequest);

        var response = await _productRepository.CreateAsync(product);
        return _mapper.Map<ProductResponse>(response);
    }
    #endregion

    #region Delete
    public async Task DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            throw new KeyNotFoundException(ResponseMessages.NotFound);

        await _productRepository.DeleteAsync(product);
    }
    #endregion

    #region GetAll
    public async Task<IEnumerable<ProductResponse>> GetAllAsync(int count, int skip, string search)
    {
        var products = await _productRepository.GetAllAsync(count, skip, search);
        return _mapper.Map<IEnumerable<ProductResponse>>(products);
    }
    #endregion

    #region GetById
    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            throw new KeyNotFoundException(ResponseMessages.NotFound);

        return _mapper.Map<ProductResponse>(product);
    }
    #endregion

    #region Update
    public async Task<ProductResponse> UpdateAsync(ProductRequest productRequest)
    {
        var product = await _productRepository.GetByIdAsync(productRequest.Id);
        if (product == null)
            throw new KeyNotFoundException(ResponseMessages.NotFound);

        _mapper.Map(productRequest, product);
        product.UpdatedAt = DateTime.UtcNow;

        await _productRepository.UpdateAsync(product);

        return _mapper.Map<ProductResponse>(product);
    }
    #endregion
}