using CTSTools.BLL.Common;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraSpellChecker.Parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CTSTools.BLL.Features.XPO
{
    public class DXFilters_Helper
    {
        public static SortProperty GetDXSorting<T>(PagedResultDTO<T> PagedDataDTO)
        {
            var _sortProperty = new SortProperty();
            try
            {
                if (PagedDataDTO.SortDescending != null)
                {
                    if (PagedDataDTO.SortPropertyName == "ID")
                    {
                        _sortProperty = (bool)PagedDataDTO.SortDescending ? new SortProperty("Oid", SortingDirection.Descending) :
                                                                                    new SortProperty("Oid", SortingDirection.Ascending);
                    }
                    else if (PagedDataDTO.SortPropertyName.Contains("AddedBy"))
                    {
                        _sortProperty = (bool)PagedDataDTO.SortDescending ? new SortProperty("AddedBy", SortingDirection.Descending) :
                                                                                    new SortProperty("AddedBy", SortingDirection.Ascending);
                    }
                    else if (PagedDataDTO.SortPropertyName.Contains("LastUpdateBy"))
                    {
                        _sortProperty = (bool)PagedDataDTO.SortDescending ? new SortProperty("LastUpdateBy", SortingDirection.Descending) :
                                                                                    new SortProperty("LastUpdateBy", SortingDirection.Ascending);
                    }
                    else
                    {
                        if (!PagedDataDTO.SortPropertyName.StartsWith("Name") && PagedDataDTO.SortPropertyName.EndsWith("Name"))
                        {
                            PagedDataDTO.SortPropertyName = PagedDataDTO.SortPropertyName.Replace("Name", ".") + "Name";
                        }
                        if (!PagedDataDTO.SortPropertyName.StartsWith("ID") && PagedDataDTO.SortPropertyName.EndsWith("ID"))
                        {
                            PagedDataDTO.SortPropertyName = PagedDataDTO.SortPropertyName.Replace("ID", ".") + "Oid";
                        }
                        _sortProperty = (bool)PagedDataDTO.SortDescending ? new SortProperty(PagedDataDTO.SortPropertyName, SortingDirection.Descending) :
                                                                                new SortProperty(PagedDataDTO.SortPropertyName, SortingDirection.Ascending);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _sortProperty;
        }

        public static GroupOperator GetDevExtremeFilters<T>(PagedResultDTO<T> PagedDataDTO)
        {
            var _groupOperator = new GroupOperator();
            try
            {
                _groupOperator = GetSerializeDXFilters_List(PagedDataDTO.dxFilters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _groupOperator;
        }


        public static GroupOperator GenerateGroupOperator(List<DXFilterDTO> FilterList)
        {
            var _groupOperator = new GroupOperator();
            try
            {
                foreach (var item in FilterList)
                {
                    switch (item.Operation)
                    {
                        case "=":
                            if (item.Operator == "or")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.Or(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.Equal)));
                                _groupOperator.OperatorType = GroupOperatorType.Or;
                            }
                            else if (item.Operator == "and")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.And(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.Equal)));
                            }
                            else
                            {
                                _groupOperator.Operands.Add(new BinaryOperator(item.Field, item.Value));
                            }
                            break;
                        case "<":
                            if (item.Operator == "or")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.Or(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.Less)));
                                _groupOperator.OperatorType = GroupOperatorType.Or;
                            }
                            else if (item.Operator == "and")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.And(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.Less)));
                            }
                            else
                            {
                                _groupOperator.Operands.Add(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.Less));
                            }
                            break;
                        case ">":
                            if (item.Operator == "or")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.Or(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.Greater)));
                                _groupOperator.OperatorType = GroupOperatorType.Or;
                            }
                            else if (item.Operator == "and")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.And(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.Greater)));
                            }
                            else
                            {
                                _groupOperator.Operands.Add(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.Greater));
                            }
                            break;
                        case ">=":
                            if (item.Operator == "or")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.Or(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.GreaterOrEqual)));
                                _groupOperator.OperatorType = GroupOperatorType.Or;
                            }
                            else if (item.Operator == "and")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.And(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.GreaterOrEqual)));
                            }
                            else
                            {
                                _groupOperator.Operands.Add(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.GreaterOrEqual));
                            }
                            break;
                        case "<=":
                            if (item.Operator == "or")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.Or(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.LessOrEqual)));
                                _groupOperator.OperatorType = GroupOperatorType.Or;
                            }
                            else if (item.Operator == "and")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.And(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.LessOrEqual)));
                            }
                            else
                            {
                                _groupOperator.Operands.Add(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.LessOrEqual));
                            }
                            break;
                        case "contains":
                            if (item.Operator == "or")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.Or(new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(item.Field), new OperandValue(item.Value))));
                                _groupOperator.OperatorType = GroupOperatorType.Or;
                            }
                            else if (item.Operator == "and")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.And(new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(item.Field), new OperandValue(item.Value))));
                            }
                            else
                            {
                                _groupOperator.Operands.Add(new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(item.Field), new OperandValue(item.Value)));
                            }
                            break;
                        case "notcontains":
                            if (item.Operator == "or")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.Or(new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(item.Field), new OperandValue(item.Value)).Not()));
                                _groupOperator.OperatorType = GroupOperatorType.Or;
                            }
                            else if (item.Operator == "and")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.And(new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(item.Field), new OperandValue(item.Value)).Not()));
                            }
                            else
                            {
                                _groupOperator.Operands.Add(new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(item.Field), new OperandValue(item.Value)).Not());
                            }
                            break;
                        case "<>":
                            if (item.Operator == "or")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.Or(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.NotEqual)));
                                _groupOperator.OperatorType = GroupOperatorType.Or;
                            }
                            else if (item.Operator == "and")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.And(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.NotEqual)));
                            }
                            else
                            {
                                _groupOperator.Operands.Add(new BinaryOperator(item.Field, item.Value, BinaryOperatorType.NotEqual));
                            }
                            break;
                        case "startswith":
                            if (item.Operator == "or")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.Or(new FunctionOperator(FunctionOperatorType.StartsWith, new OperandProperty(item.Field), new OperandValue(item.Value))));
                                _groupOperator.OperatorType = GroupOperatorType.Or;
                            }
                            else if (item.Operator == "and")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.And(new FunctionOperator(FunctionOperatorType.StartsWith, new OperandProperty(item.Field), new OperandValue(item.Value))));
                            }
                            else
                            {
                                _groupOperator.Operands.Add(new FunctionOperator(FunctionOperatorType.StartsWith, new OperandProperty(item.Field), new OperandValue(item.Value)));
                            }
                            break;
                        case "endswith":
                            if (item.Operator == "or")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.Or(new FunctionOperator(FunctionOperatorType.EndsWith, new OperandProperty(item.Field), new OperandValue(item.Value))));
                                _groupOperator.OperatorType = GroupOperatorType.Or;
                            }
                            else if (item.Operator == "and")
                            {
                                _groupOperator.Operands.Add(CriteriaOperator.And(new FunctionOperator(FunctionOperatorType.EndsWith, new OperandProperty(item.Field), new OperandValue(item.Value))));

                            }
                            else
                            {
                                _groupOperator.Operands.Add(new FunctionOperator(FunctionOperatorType.EndsWith, new OperandProperty(item.Field), new OperandValue(item.Value)));
                            }
                            break;
                        default:
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _groupOperator;
        }


        public static string[] SplitDXFilterList(IList DXFilterList, int position)
        {
            if (DXFilterList == null || DXFilterList.Count <= position)
                return Array.Empty<string>();

            var item = DXFilterList[position];

            // Si el item es una lista interna como ["field", "operator", "value"]
            if (item is IList sublist)
            {
                return sublist
                    .Cast<object>()
                    .Select(o => o?.ToString().Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .ToArray();
            }

            // Si no es una lista, intentar convertirlo a string plano (último recurso)
            string[] filterArray = Regex
                .Replace(item.ToString(), @"(@|&|'|\(|\)|#|\]|\[|"")|[\r\n]", "")
                .Split(',')
                .Select(s => s.Trim())
                .ToArray();

            return filterArray;
            //string[] _filterArray = Regex.Replace(DXFilterList[position].ToString(), @"(@|&|'|\(|\)|#|\]|\[|"")|[\r\n]", "").Split(',');//Does not remove blanks

            ////remove blanks at the beginning and end of each element
            //return _filterArray.Select(s => s.Trim()).ToArray();
        }

        public static List<GroupOperator> SplitIDArrayToGroupOperator(int?[] IDArray)
        {
            var _groupOperatorList = new List<GroupOperator>();
            try
            {
                int paramsSize = 2000;
                for (int i = 0; i < IDArray.Length; i += paramsSize)
                {
                    int?[] _idParams = IDArray.Skip(i).Take(paramsSize).ToArray();
                    GroupOperator _groupOperator = new GroupOperator();
                    _groupOperator.Operands.Add(new InOperator("Oid", _idParams));
                    _groupOperatorList.Add(_groupOperator);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _groupOperatorList;
        }

        private static GroupOperator GetSerializeDXFilters_List(IList DXFilterList)
        {
            //Array of operation types of dev extreme filters
            string[] _operationTypeArray = { "=", "<", ">", ">=", "<=", "contains", "notcontains", "<>", "startswith", "endswith" };
            //counter, normally the dev extreme filters come with 3 important properties 1.-Field , 2.-Operation 3.Value
            var _filterFieldCounter = 1;
            //main group operator, if several group operators are nested it, they will be connected by "AND".
            var _groupOperator = new GroupOperator();
            //declare the dxfilter list
            var _filterList = new List<DXFilterDTO>();
            DXFilterDTO _filterDTO = new DXFilterDTO();
            try
            {
                //validate if it contains an operator
                if (DXFilterList.Contains("or") || DXFilterList.Contains("and"))
                {

                    for (int i = 0; i < DXFilterList.Count; i++)
                    {

                        string[] _filterArray = { };
                        //Split each element of DXFilter List
                        _filterArray = SplitDXFilterList(DXFilterList, i);

                        //loop each element of array
                        //because sometimes an element is another array of element
                        //which are grouped by 3 elements(field,operation,value) and connected by the operator type(and,or).
                        for (int y = 0; y < _filterArray.Length; y++)
                        {
                            //if element is a operator type continue loop, because this is a conector the arrays
                            if (_filterArray[y].ToString() == "or" || _filterArray[y].ToString() == "and")
                            {
                                continue;
                            }
                            //Element 1 is usually the position of the field
                            if (_filterFieldCounter == 1)
                            {
                                _filterDTO.Field = GetFieldName(_filterArray, y);
                            }
                            //Element 2 is usually the position of the operation
                            else if (_filterFieldCounter == 2)
                            {
                                //verify if element is a operation type
                                if (_operationTypeArray.Contains(_filterArray[y]))
                                {
                                    _filterDTO.Operation = _filterArray[y].Trim();
                                }
                            }
                            //Element 3 is usually the position of the value
                            else if (_filterFieldCounter == 3)
                            {
                                //set value
                                _filterDTO.Value = (_filterArray[y] != null && _filterArray[y] != "null") ? _filterArray[y].Trim() : null;
                                //set operator
                                //if the array contains more than 3 elements then it is a set of arrays connected by an operator, get that operator
                                if ((y < _filterArray.Length - 1) && (_filterArray.Length > 3))
                                {
                                    //operator will be in the next position as long as it is not the last element of the list or array
                                    _filterDTO.Operator = _filterArray[y + 1];
                                }
                                else
                                {
                                    //if the array contains only 3 elements, get the main operator from the DX Filter List
                                    if (i < DXFilterList.Count - 1)
                                    {
                                        //operator will be in the next position as long as it is not the last element of the list or array
                                        _filterDTO.Operator = SplitDXFilterList(DXFilterList, i + 1)[0];
                                    }
                                }
                                //reset the counter
                                _filterFieldCounter = 0;
                                //add filter DTO to list
                                _filterList.Add(_filterDTO);
                                //declare a new DXFilterDTO
                                _filterDTO = new DXFilterDTO();
                            }
                            _filterFieldCounter++;

                        }
                        //end _filter array loop
                        //validate if filter list isn't empty
                        if (_filterList.Count() > 0)
                        {
                            if (DXFilterList.Contains("or"))
                            {
                                //if the main list of are nested by the "Or" operator, get the group operators once the whole main list has been completed
                                if (i == DXFilterList.Count - 1)
                                {
                                    _groupOperator.Operands.Add(GenerateGroupOperator(_filterList));
                                }
                            }
                            else
                            {
                                _groupOperator.Operands.Add(GenerateGroupOperator(_filterList));
                                _filterList = new List<DXFilterDTO>();
                            }
                        }

                    }
                    //end main list loop
                }
                else
                {
                    //check for a single filter in a row or multiple rows for an unique filter
                    DXFilterList = (DXFilterList.Count == 1) ? SplitDXFilterList(DXFilterList, 0) : DXFilterList;
                    for (int i = 0; i < DXFilterList.Count; i++)
                    {
                        string[] _filterArray = { };




                        if (i == 0)
                        {
                            _filterArray = SplitDXFilterList(DXFilterList, i);
                            _filterDTO.Field = GetFieldName(_filterArray, i);
                        }
                        else if (i == 1)
                        {
                            _filterArray = SplitDXFilterList(DXFilterList, i);
                            _filterDTO.Operation = _filterArray[0];
                        }
                        else
                        {
                            _filterDTO.Value = (DXFilterList[i] != null) ? DXFilterList[i].ToString().Trim() : DXFilterList[i];
                            _filterList.Add(_filterDTO);

                        }


                    }
                    _groupOperator.Operands.Add(GenerateGroupOperator(_filterList));

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _groupOperator;
        }

        private static string GetFieldName(string[] FilterArray, int position)
        {
            if (FilterArray[position] == "ID" || FilterArray[position] == "this")
            {
                return "Oid";
            }
            else if (!FilterArray[position].StartsWith("ID") && FilterArray[position].EndsWith("DTO.ID"))
            {               
                return FilterArray[position].ToString().Replace("DTO.ID", ".") + "Oid";
            }
            else if (!FilterArray[position].StartsWith("ID") && FilterArray[position].EndsWith("ID"))
            {
                return FilterArray[position].ToString().Replace("ID", ".") + "Oid";
            }

            else if (!FilterArray[position].StartsWith("Name") && FilterArray[position].EndsWith("ByName"))
            {
                return FilterArray[position].ToString().Replace("DTO", "").Replace("Name", ".") + "Name";
            }
            else if (!FilterArray[position].StartsWith("Name") && FilterArray[position].EndsWith("Name"))
            {
                return FilterArray[position].ToString().Replace("Name", ".") + "Name";
            }
            else if (!FilterArray[position].StartsWith("String") && FilterArray[position].EndsWith("String"))
            {
                return FilterArray[position].ToString().Replace("DTO", "").Replace("String", "");
            }
            else
            {
                return FilterArray[position].ToString().Replace("DTO", ""); ;
            }
        }
    }
}
