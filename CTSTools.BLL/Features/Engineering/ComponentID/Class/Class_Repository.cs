using CTSTools.BLL.Common;
using CTSTools.BLL.Features.XPO;
using CTSTools.DAL.Common;
using CTSTools.DAL.Features.Engineering.ComponentID.Class;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTSTools.BLL.Features.Engineering.ComponentID.Class
{
    public class Class_Repository
    {
        public static List<ClassDTO> GetClassList(ClassDTO ClassDTO, PagedResultDTO<ClassDTO> PagedResultDTO = null)
        {
            var _classList = new List<ClassDTO>();
            try
            {
                // Class Filters
                var _groupOperator = Class_DXFilter.GetClass_DXFilter(ClassDTO);
                var _sortProperty = new SortProperty();
                if (PagedResultDTO?.SortPropertyName != null)
                {
                    //Sorting
                    _sortProperty = DXFilters_Helper.GetDXSorting(PagedResultDTO);
                }
                if (PagedResultDTO?.dxFilters != null)
                {
                    //DevExtreme Filter
                    _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
                }
                using var _session = XPO_Helper.GetNewSession();
                var _classCollection = new XPCollection<ClassXPO>(_session, _groupOperator, _sortProperty)
                {
                    TopReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Take,
                    SkipReturnedObjects = PagedResultDTO == null ? 0 : PagedResultDTO.Skip,
                    Sorting = (PagedResultDTO?.SortPropertyName != null) ? new SortingCollection(_sortProperty) : new SortingCollection(new SortProperty(nameof(ClassXPO.Oid), SortingDirection.Ascending))
                };

                if (_classCollection.AsQueryable().Count() > 0)
                {
                    _classList = _classCollection.Select(ClassXPO => ClassMap.XPOToDTO(ClassXPO)).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return _classList;
        }
        public static ClassDTO GetClassByID(int ClassID)
        {
            var _ClassDTO = new ClassDTO();
            try
            {
                using var _unit = XPO_Helper.GetNewUnitOfWork();
                var _ClassXPO = _unit.GetObjectByKey<ClassXPO>(ClassID);
                if (_ClassXPO != null)
                    _ClassDTO = ClassMap.XPOToDTO(_ClassXPO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ClassDTO;
        }
        public static int GetClassCount(ClassDTO ClassDTO, PagedResultDTO<ClassDTO> PagedResultDTO = null)
        {
            try
            {
                var _groupOperator = Class_DXFilter.GetClass_DXFilter(ClassDTO);
                if (PagedResultDTO?.dxFilters != null)
                {
                    //DevExtreme Filter
                    _groupOperator.Operands.Add(DXFilters_Helper.GetDevExtremeFilters(PagedResultDTO));
                }
                using (var _session = XPO_Helper.GetNewSession())
                {
                    return (int)_session.Evaluate<ClassXPO>(new AggregateOperand(null, Aggregate.Count), _groupOperator);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ValidationResultDTO CreateClass(ClassDTO ClassDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been saved successfully."
            };
            try
            {
                using var _unit = XPO_Helper.GetNewUnitOfWork();
                var _classXPO = ClassMap.DTOtoXPO(ClassDTO, _unit);
                _unit.Save(_classXPO);
                _unit.CommitChanges();
                _validationResultDTO.Data = _classXPO.Oid;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationResultDTO.Result = false;
                _validationResultDTO.Message = "Error!";
                _validationResultDTO.Description = string.Format("There was an error trying to save the record. ");
            }
            return _validationResultDTO;
        }
        public static ValidationResultDTO UpdateClass(ClassDTO ClassDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been updated successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _classXPO = ClassMap.DTOtoXPO(ClassDTO, _unit);
                    _unit.Save(_classXPO);
                    _unit.CommitChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationResultDTO.Result = false;
                _validationResultDTO.Message = "Error!";
                _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
            }
            return _validationResultDTO;
        }
        public static ValidationResultDTO DeleteClass(ClassDTO ClassDTO)
        {
            var _validationResultDTO = new ValidationResultDTO
            {
                Description = "The record has been deleted successfully."
            };
            try
            {
                using (var _unit = XPO_Helper.GetNewUnitOfWork())
                {
                    var _classXPO = ClassMap.DTOtoXPO(ClassDTO, _unit);
                    _unit.Delete(_classXPO);
                    _unit.CommitChanges();
                    _unit.PurgeDeletedObjects();
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationResultDTO.Result = false;
                _validationResultDTO.Message = "Error!";
                _validationResultDTO.Description = string.Format("There was an error trying to save the record.");
            }
            return _validationResultDTO;
        }
    }
}
