using ProductManagementMS.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementMS.Application.Interface
{
    public interface IInsuranceProductService
    {
        Task AddProduct(insurance_productDTO dto);

        Task<List<insurance_productDTO>> FetchAllProducts();

        Task<insurance_productDTO?> FindProductById(int id);

        Task UpdateProduct(int id, insurance_productDTO dto);

        Task DeactivateProduct(int id);
    }
}
