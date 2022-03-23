using Business_Core.Entities;
using Business_Core.IServices;
using Business_Core.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Services_Implement
{
    public class DynamicFormStructureService : IDynamicFormStructureService
    {
        private readonly IUnitofWork _unitofWork;
        public DynamicFormStructureService(IUnitofWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }
        public async Task<DynamicFormStructure> DeleteDynamicFormStructure(DynamicFormStructure dynamicFormStructure)
        {
            _unitofWork._DynamicFormStructureRepository.DeleteAsync(dynamicFormStructure);
            await _unitofWork.CommitAsync();
            return dynamicFormStructure;
        }

        public async Task<IEnumerable<DynamicFormStructure>> GetDynamicFormAllStructures()
        {
            throw new NotImplementedException();
        }

        public async Task<DynamicFormStructure> GetDynamicFormStructure(int Id)
        {
            return await _unitofWork._DynamicFormStructureRepository.GetByKeyAsync(Id);

        }

        public async Task<DynamicFormStructure> InsertDynamicFormStructure(DynamicFormStructure dynamicFormStructure)
        {
            dynamicFormStructure.Created_At = DateTime.Now;
           await _unitofWork._DynamicFormStructureRepository.AddAsync(dynamicFormStructure);
           await _unitofWork.CommitAsync();
           return dynamicFormStructure;
        }

        public async Task<DynamicFormStructure> UpdateDynamicFormStructure(DynamicFormStructure OldData, DynamicFormStructure UpdateData)
        {
            OldData.FormStructure = UpdateData.FormStructure;
            await _unitofWork.CommitAsync();
            return OldData;
        }
    }
}
