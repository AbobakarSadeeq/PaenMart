using Business_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IServices
{
    public interface IDynamicFormStructureService
    {
        Task<DynamicFormStructure> InsertDynamicFormStructure(DynamicFormStructure dynamicFormStructure);
        Task<IEnumerable<DynamicFormStructure>> GetDynamicFormAllStructures();
        Task<DynamicFormStructure> GetDynamicFormStructure(int Id);
        Task<DynamicFormStructure> DeleteDynamicFormStructure(DynamicFormStructure dynamicFormStructure);
        Task<DynamicFormStructure> UpdateDynamicFormStructure(DynamicFormStructure OldData, DynamicFormStructure UpdateData);
    }
}
