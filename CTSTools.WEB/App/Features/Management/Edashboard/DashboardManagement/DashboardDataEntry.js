import { GetDashboardInformation } from './Dashboard/Dashboard_Service.js';
import { AddMonthlyValue, GetDashboardLineInformation, GetDashboard_KPITendence } from './DashboardLine/DashboardLine_Service.js';
import { GetDashboard_KPIWithUI } from './Dashboard_KPI/Dashboard_KPI_Service.js';
import { HostResponse } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { GetURLParameter } from '../../../../Common/Utils/GetURLParameter.js'
import { Dashboard_Category_Enum } from '../Settings/DashboardCategory/Dashboard_Category_Enum.js';
import { Month_Enum } from '../../../../Common/Utils/Month_Enum.js';
import { ValueType_Enum } from '../Settings/ValueType/ValueType_Enum.js';
import { UnitOfMeasure_Enum } from '../../../AdvancedSettings/UnitOfMeasure/UnitOfMeasure_Enum.js'
import { GetDXDashboard_KPIDataSource, UpdateDashboard_KPIOrder, DeleteDashboard_KPI, CreateDashboard_KPIFromKPIList } from './Dashboard_KPI/Dashboard_KPI_Service.js';
import { GetDXKPIDataSource } from './KPI/KPI_Service.js'




document.addEventListener("DOMContentLoaded", async function () {
    await dxLoadPanel.show();

    //#region Data Entry
    await InitializeDashboardDataEntryControls();
    document.getElementById('SaveDashboardLine').addEventListener('click', UpdateDashboardLine);
    document.getElementById('PrintDashboardBtn').addEventListener('click', printDashboard);
    document.getElementById('KPIButton').addEventListener('click', function (e) {
        $("#dxQualityKPIs").dxDataGrid("instance").refresh();
        e.preventDefault()
        $('#tab2 a[href="#KPITab"]').tab('show')
        $("#KPIButton").hide();
        $("#PrintDashboardBtn").hide();
        document.getElementById('dashboardButton').hidden = false;

    })
    document.getElementById('dashboardButton').addEventListener('click', function (e) {
        e.preventDefault()
        $('#tab1 a[href="#DashboardTab"]').tab('show')
        $("#PrintDashboardBtn").show();
        document.getElementById('dashboardButton').hidden = true;
        $("#KPIButton").show();

    })
    await GetDashboardIDByURL();
    await dxLoadPanel.hide();

    //#endregion

    //#region Assign KPI

    document.getElementById('AddKPIButton').addEventListener('click', CreateDashboard_KPI_Global);
    document.getElementById('AddKPIBtn').addEventListener('click', function () {
        $('#AddKPIsModal').modal('show');
    });
    document.getElementById('XBtnModal').addEventListener('click', ClearDashboard_KPIFields);
    document.getElementById('CloseBtnModal').addEventListener('click', ClearDashboard_KPIFields);
    //#endregion



});

//#region Data Entry
async function GetDashboardIDByURL() {
    let _dashboardID = GetURLParameter("DashboardID");
    let _dashboardDTO = await GetDashboardInformation({ ID: _dashboardID })
    if (_dashboardID != null && _dashboardID != undefined && _dashboardID != 0 && !Number.isNaN(_dashboardID)) {
        document.getElementById('hiddenDashboardID').value = _dashboardID;
        await InitializeTemplateAdministrationControls();

        await GetDashboard_KPIListForTable(_dashboardDTO);
        await GetDashboard_KPIListForGrid(_dashboardDTO);


    } else {
        toastr["error"]("Please, select a dashboard to get the information", "Dashboard Not selected");
    }
}
async function InitializeDashboardDataEntryControls() {

    $("#dxKPITendenceChart").dxChart({
        dataSource: "",
        title: {
            text: "KPI Tendence",
        },
        legend: {
            visible: false
        },
        resolveLabelOverlapping: "shift",
        "export": {
            enabled: true
        },
        tooltip: {
            enabled: true,
            font: {
                size: '16px',
                weight: 700
            },
        },
        series: [{
            argumentField: "Month",
            valueField: "Tendence",

            label: {
                visible: true,
                connector: {
                    visible: true,
                    width: 0.5
                },
                customizeText: function (e) {
                    return e.value;
                }
            },
        }],
    });
}
//#region TQC Format
function FilterKPIListByCategory(Dashboard_KPIList) {
    BuildTQCFormat2(Dashboard_KPIList);
    document.getElementById('NoDashboardMessage').classList.add('d-none');
    document.getElementById('Dashboard_KPIList').classList.remove('d-none');
}
function BuildTQCFormat2(Dashboard_KPIList) {
    let _TQCFormatHTML = "";
    let _panelBodyCategory = document.getElementById(`DashboardPanel`);
    _panelBodyCategory.innerHTML = "";
    //Insert header titles of the format
    if (Dashboard_KPIList.length > 0) {
        _TQCFormatHTML += "<table class=\"table table-bordered\">" +
            "<thead>" +
            "<tr>" +
            "<th style=\"width: 95px;\">TQC</th>" +
            "<th style=\"width: 80px;\">Trend</th>" +
            "<th style=\"width: 95px;\">Owner</th>" +
            "<th style=\"width: 400px;\">KPI (Key Process Indicator)</th>" +
            "<th>FY Goal</th>" +
            "<th>Apr</th>" +
            "<th>May</th>" +
            "<th>Jun</th>" +
            "<th>Jul</th>" +
            "<th>Aug</th>" +
            "<th>Sep</th>" +
            "<th>Oct</th>" +
            "<th>Nov</th>" +
            "<th>Dec</th>" +
            "<th>Jan</th>" +
            "<th>Feb</th>" +
            "<th>Mar</th>" +
            "</tr>" +
            "</thead>";

        // Safety
        let _sortSafetyList = Dashboard_KPIList.filter(function (x) {
            return x.DashboardCategoryID == Dashboard_Category_Enum.Safety
        });
        //Start build body of the format
        let _safetyAmount = _sortSafetyList.length + 1;
        _TQCFormatHTML +=
            "<tbody>" +
            "<tr valign='middle'>" +
            "<td style=\"width: 95px;\" rowspan=\"" + _safetyAmount + "\">" +
            "<h5><strong>" + "S" + "</strong></h5><strong><p><strong>" + "Safety" + "</strong></p>" +
            "</td>" +
            "</tr>";
        _sortSafetyList.forEach(function (Dashboard_KPIDTO) {
            let _fyGoalSymbol = (Dashboard_KPIDTO.KPIDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(Dashboard_KPIDTO.KPIDTO);
            let _fyGoalFormat = Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                ConvertToMoney(Dashboard_KPIDTO.KPIDTO.Goal) : Dashboard_KPIDTO.KPIDTO.Goal;
            _TQCFormatHTML +=
                `<tr valign='middle'><td style="width: 80px;"><a class="btn-modal-tendency" ` +
                `data-dashboardcategoryid=${Dashboard_KPIDTO.DashboardCategoryID} data-valuetypeid=${Dashboard_KPIDTO.KPIDTO.ValueTypeID} ` +
                `data-equivalenceicon=${Dashboard_KPIDTO.KPIDTO.EquivalenceIcon} data-KPIid=${Dashboard_KPIDTO.KPIID} ` +
                `data-unitofmeasureid=${Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID} ` +
                `data-KPIgoal=${Dashboard_KPIDTO.KPIDTO.Goal} data-KPIname='${Dashboard_KPIDTO.KPIDTO.Name}' data-bs-toggle="modal" ` +
                `data-bs-target="#Dashboard_KPITendencyModal" id=\"Dashboard_KPITendencyBtn${Dashboard_KPIDTO.ID}\")\"" >` +
                `</i><i class=\"fas fa-chart-line me-2 fa-2x\"></i></a></td>` +
                `<td style="width: 95px;">${Dashboard_KPIDTO.KPIDTO.OwnerName}</td>` +
                `<td style="width: 400px;" class=\"bg-yellow\">${Dashboard_KPIDTO.KPIName}</td>` +
                `<td class=\"bg-info fw-bold\"> ${Dashboard_KPIDTO.KPIDTO.EquivalenceIcon} ${_fyGoalFormat}${_fyGoalSymbol}</td>`;
            Month_Enum.forEach(function (MonthDTO) {
                let _bgColor = "";
                let _fontColor = "";
                let _value;
                let _valueTypeIcon;

                let _dasboardLineDTO = Dashboard_KPIDTO.DashboardLineList.filter(function (x) {

                    return x.Month == MonthDTO.value
                });

                if (_dasboardLineDTO.length > 0 && _dasboardLineDTO[0].Validated) {
                    _bgColor = _dasboardLineDTO[0].MonthValue.KPIBackgroundColor;
                    _fontColor = "000";
                    _value = Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                        ConvertToMoney(_dasboardLineDTO[0].Value) : _dasboardLineDTO[0].Value;

                    _valueTypeIcon = _fyGoalSymbol;
                } else {
                    _value = ""
                    _bgColor = "FFF"
                    _fontColor = "000"
                    _valueTypeIcon = ""
                }

                _TQCFormatHTML += `<td class="btn-modal-KPI" data-monthname=${MonthDTO.name} data-dashboardlineid=${(_dasboardLineDTO.length > 0) ? _dasboardLineDTO[0].ID : 0} style=background-color:#${_bgColor};cursor:pointer;><a class="fw-bold" style="color:#${_fontColor} !important;text-decoration:none;" id="KPIInformationByDashboardAndMonthBtn${Dashboard_KPIDTO.ID}" >${_value}${_valueTypeIcon}</a></td>`;
            });
        });
        _TQCFormatHTML += '</tr>'

        // Quality
        let _sortQualityList = Dashboard_KPIList.filter(function (x) {
            return x.DashboardCategoryID == Dashboard_Category_Enum.Quality
        });
        //Start build body of the format
        let _qualityAmount = _sortQualityList.length + 1;
        _TQCFormatHTML +=
            "<tbody>" +
            "<tr  valign='middle' >" +
            "<td style=\"width: 95px;\" rowspan=\"" + _qualityAmount + "\">" +
            "<h5><strong>" + "Q" + "</strong></h5><strong><p><strong>" + "Quality" + "</strong></p>" +
            "</td>" +
            "</tr>";
        _sortQualityList.forEach(function (Dashboard_KPIDTO) {
            let _fyGoalSymbol = (Dashboard_KPIDTO.KPIDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(Dashboard_KPIDTO.KPIDTO);
            let _fyGoalFormat = Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                ConvertToMoney(Dashboard_KPIDTO.KPIDTO.Goal) : Dashboard_KPIDTO.KPIDTO.Goal;
            _TQCFormatHTML +=
                `<tr valign='middle'><td style="width: 80px;"><a class="btn-modal-tendency" ` +
                `data-dashboardcategoryid=${Dashboard_KPIDTO.DashboardCategoryID} data-valuetypeid=${Dashboard_KPIDTO.KPIDTO.ValueTypeID} ` +
                `data-equivalenceicon=${Dashboard_KPIDTO.KPIDTO.EquivalenceIcon} data-KPIid=${Dashboard_KPIDTO.KPIID} ` +
                `data-unitofmeasureid=${Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID} ` +
                `data-KPIgoal=${Dashboard_KPIDTO.KPIDTO.Goal} data-KPIname='${Dashboard_KPIDTO.KPIDTO.Name}' data-bs-toggle="modal" ` +
                `data-bs-target="#Dashboard_KPITendencyModal" id=\"Dashboard_KPITendencyBtn${Dashboard_KPIDTO.ID}\")\"" >` +
                `</i><i class=\"fas fa-chart-line me-2 fa-2x\"></i></a></td>` +
                `<td style="width: 95px;">${Dashboard_KPIDTO.KPIDTO.OwnerName}</td>` +
                `<td style="width: 400px;" class=\"bg-yellow\">${Dashboard_KPIDTO.KPIName}</td>` +
                `<td class=\"bg-info fw-bold\"> ${Dashboard_KPIDTO.KPIDTO.EquivalenceIcon} ${_fyGoalFormat}${_fyGoalSymbol}</td>`;
            Month_Enum.forEach(function (MonthDTO) {
                let _bgColor = "";
                let _fontColor = "";
                let _value;
                let _valueTypeIcon;

                let _dasboardLineDTO = Dashboard_KPIDTO.DashboardLineList.filter(function (x) {
                    return x.Month == MonthDTO.value
                });

                if (_dasboardLineDTO.length > 0 && _dasboardLineDTO[0].Validated) {
                    _bgColor = _dasboardLineDTO[0].MonthValue.KPIBackgroundColor;
                    _fontColor = "000";
                    _value = Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                        ConvertToMoney(_dasboardLineDTO[0].Value) : _dasboardLineDTO[0].Value;

                    _valueTypeIcon = _fyGoalSymbol;
                } else {
                    _value = ""
                    _bgColor = "FFF"
                    _fontColor = "000"
                    _valueTypeIcon = ""
                }

                _TQCFormatHTML += `<td class="btn-modal-KPI" data-monthname=${MonthDTO.name} data-dashboardlineid=${(_dasboardLineDTO.length > 0) ? _dasboardLineDTO[0].ID : 0} style=background-color:#${_bgColor};cursor:pointer;><a class="fw-bold" style="color:#${_fontColor} !important;text-decoration:none;" id="KPIInformationByDashboardAndMonthBtn${Dashboard_KPIDTO.ID}" >${_value}${_valueTypeIcon}</a></td>`;
            });
        });
        _TQCFormatHTML += '</tr>'
        // Delivery
        let _sortDeliveryList = Dashboard_KPIList.filter(function (x) {
            return x.DashboardCategoryID == Dashboard_Category_Enum.Delivery
        });
        //Start build body of the format
        let _deliveryAmount = _sortDeliveryList.length + 1;
        _TQCFormatHTML +=
            "<tbody>" +
            "<tr valign='middle' >" +
            "<td style=\"width: 95px;\" rowspan=\"" + _deliveryAmount + "\">" +
            "<h5><strong>" + "D" + "</strong></h5><strong><p><strong>" + "Delivery" + "</strong></p>" +
            "</td>" +
            "</tr>";
        _sortDeliveryList.forEach(function (Dashboard_KPIDTO) {
            let _fyGoalSymbol = (Dashboard_KPIDTO.KPIDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(Dashboard_KPIDTO.KPIDTO);
            let _fyGoalFormat = Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                ConvertToMoney(Dashboard_KPIDTO.KPIDTO.Goal) : Dashboard_KPIDTO.KPIDTO.Goal;
            _TQCFormatHTML +=
                `<tr  valign='middle'><td style="width: 80px;"><a class="btn-modal-tendency" ` +
                `data-dashboardcategoryid=${Dashboard_KPIDTO.DashboardCategoryID} data-valuetypeid=${Dashboard_KPIDTO.KPIDTO.ValueTypeID} ` +
                `data-equivalenceicon=${Dashboard_KPIDTO.KPIDTO.EquivalenceIcon} data-KPIid=${Dashboard_KPIDTO.KPIID} ` +
                `data-unitofmeasureid=${Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID} ` +
                `data-KPIgoal=${Dashboard_KPIDTO.KPIDTO.Goal} data-KPIname='${Dashboard_KPIDTO.KPIDTO.Name}' data-bs-toggle="modal" ` +
                `data-bs-target="#Dashboard_KPITendencyModal" id=\"Dashboard_KPITendencyBtn${Dashboard_KPIDTO.ID}\")\"" >` +
                `</i><i class=\"fas fa-chart-line me-2 fa-2x\"></i></a></td>` +
                `<td style="width: 95px;">${Dashboard_KPIDTO.KPIDTO.OwnerName}</td>` +
                `<td style="width: 400px;" class=\"bg-yellow\">${Dashboard_KPIDTO.KPIName}</td>` +
                `<td class=\"bg-info fw-bold\"> ${Dashboard_KPIDTO.KPIDTO.EquivalenceIcon} ${_fyGoalFormat}${_fyGoalSymbol}</td>`;
            Month_Enum.forEach(function (MonthDTO) {
                let _bgColor = "";
                let _fontColor = "";
                let _value;
                let _valueTypeIcon;

                let _dasboardLineDTO = Dashboard_KPIDTO.DashboardLineList.filter(function (x) {

                    return x.Month == MonthDTO.value
                });

                if (_dasboardLineDTO.length > 0 && _dasboardLineDTO[0].Validated) {
                    _bgColor = _dasboardLineDTO[0].MonthValue.KPIBackgroundColor;
                    _fontColor = "000";
                    _value = Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                        ConvertToMoney(_dasboardLineDTO[0].Value) : _dasboardLineDTO[0].Value;

                    _valueTypeIcon = _fyGoalSymbol;
                } else {
                    _value = ""
                    _bgColor = "FFF"
                    _fontColor = "000"
                    _valueTypeIcon = ""
                }

                _TQCFormatHTML += `<td class="btn-modal-KPI" data-monthname=${MonthDTO.name} data-dashboardlineid=${(_dasboardLineDTO.length > 0) ? _dasboardLineDTO[0].ID : 0} style=background-color:#${_bgColor};cursor:pointer;><a class="fw-bold" style="color:#${_fontColor} !important;text-decoration:none;" id="KPIInformationByDashboardAndMonthBtn${Dashboard_KPIDTO.ID}" >${_value}${_valueTypeIcon}</a></td>`;
            });
        });
        _TQCFormatHTML += '</tr>'
        // Cost
        let _sortCostList = Dashboard_KPIList.filter(function (x) {
            return x.DashboardCategoryID == Dashboard_Category_Enum.Cost
        });
        //Start build body of the format
        let _costAmount = _sortCostList.length + 1;
        _TQCFormatHTML +=
            "<tbody>" +
            "<tr valign='middle'>" +
            "<td style=\"width: 95px;\" rowspan=\"" + _costAmount + "\">" +
            "<h5><strong>" + "C" + "</strong></h5><strong><p><strong>" + "Cost" + "</strong></p>" +
            "</td>" +
            "</tr>";
        _sortCostList.forEach(function (Dashboard_KPIDTO) {
            let _fyGoalSymbol = (Dashboard_KPIDTO.KPIDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(Dashboard_KPIDTO.KPIDTO);
            let _fyGoalFormat = Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                ConvertToMoney(Dashboard_KPIDTO.KPIDTO.Goal) : Dashboard_KPIDTO.KPIDTO.Goal;
            _TQCFormatHTML +=
                `<tr valign='middle'><td style="width: 80px;"><a class="btn-modal-tendency" ` +
                `data-dashboardcategoryid=${Dashboard_KPIDTO.DashboardCategoryID} data-valuetypeid=${Dashboard_KPIDTO.KPIDTO.ValueTypeID} ` +
                `data-equivalenceicon=${Dashboard_KPIDTO.KPIDTO.EquivalenceIcon} data-KPIid=${Dashboard_KPIDTO.KPIID} ` +
                `data-unitofmeasureid=${Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID} ` +
                `data-KPIgoal=${Dashboard_KPIDTO.KPIDTO.Goal} data-KPIname='${Dashboard_KPIDTO.KPIDTO.Name}' data-bs-toggle="modal" ` +
                `data-bs-target="#Dashboard_KPITendencyModal" id=\"Dashboard_KPITendencyBtn${Dashboard_KPIDTO.ID}\")\"" >` +
                `</i><i class=\"fas fa-chart-line me-2 fa-2x\"></i></a></td>` +
                `<td style="width: 95px;">${Dashboard_KPIDTO.KPIDTO.OwnerName}</td>` +
                `<td style="width: 400px;" class=\"bg-yellow\">${Dashboard_KPIDTO.KPIName}</td>` +
                `<td class=\"bg-info fw-bold\"> ${Dashboard_KPIDTO.KPIDTO.EquivalenceIcon} ${_fyGoalFormat}${_fyGoalSymbol}</td>`;
            Month_Enum.forEach(function (MonthDTO) {
                let _bgColor = "";
                let _fontColor = "";
                let _value;
                let _valueTypeIcon;

                let _dasboardLineDTO = Dashboard_KPIDTO.DashboardLineList.filter(function (x) {

                    return x.Month == MonthDTO.value
                });

                if (_dasboardLineDTO.length > 0 && _dasboardLineDTO[0].Validated) {
                    _bgColor = _dasboardLineDTO[0].MonthValue.KPIBackgroundColor;
                    _fontColor = "000";
                    _value = Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                        ConvertToMoney(_dasboardLineDTO[0].Value) : _dasboardLineDTO[0].Value;

                    _valueTypeIcon = _fyGoalSymbol;
                } else {
                    _value = ""
                    _bgColor = "FFF"
                    _fontColor = "000"
                    _valueTypeIcon = ""
                }

                _TQCFormatHTML += `<td class="btn-modal-KPI" data-monthname=${MonthDTO.name} data-dashboardlineid=${(_dasboardLineDTO.length > 0) ? _dasboardLineDTO[0].ID : 0} style=background-color:#${_bgColor};cursor:pointer;><a class="fw-bold" style="color:#${_fontColor} !important;text-decoration:none;" id="KPIInformationByDashboardAndMonthBtn${Dashboard_KPIDTO.ID}" >${_value}${_valueTypeIcon}</a></td>`;
            });
        });
        _TQCFormatHTML += '</tr>'
        // Moral
        let _sortMoralList = Dashboard_KPIList.filter(function (x) {
            return x.DashboardCategoryID == Dashboard_Category_Enum.Moral
        });
        //Start build body of the format
        let _moralAmount = _sortMoralList.length + 1;
        _TQCFormatHTML +=
            "<tbody>" +
            "<tr  valign='middle'>" +
            "<td style=\"width: 95px;\" rowspan=\"" + _moralAmount + "\">" +
            "<h5><strong>" + "M" + "</strong></h5><strong><p><strong>" + "Moral" + "</strong></p>" +
            "</td>" +
            "</tr>";
        _sortMoralList.forEach(function (Dashboard_KPIDTO) {
            let _fyGoalSymbol = (Dashboard_KPIDTO.KPIDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(Dashboard_KPIDTO.KPIDTO);
            let _fyGoalFormat = Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                ConvertToMoney(Dashboard_KPIDTO.KPIDTO.Goal) : Dashboard_KPIDTO.KPIDTO.Goal;
            _TQCFormatHTML +=
                `<tr valign='middle' ><td style="width: 80px;"><a class="btn-modal-tendency" ` +
                `data-dashboardcategoryid=${Dashboard_KPIDTO.DashboardCategoryID} data-valuetypeid=${Dashboard_KPIDTO.KPIDTO.ValueTypeID} ` +
                `data-equivalenceicon=${Dashboard_KPIDTO.KPIDTO.EquivalenceIcon} data-KPIid=${Dashboard_KPIDTO.KPIID} ` +
                `data-unitofmeasureid=${Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID} ` +
                `data-KPIgoal=${Dashboard_KPIDTO.KPIDTO.Goal} data-KPIname='${Dashboard_KPIDTO.KPIDTO.Name}' data-bs-toggle="modal" ` +
                `data-bs-target="#Dashboard_KPITendencyModal" id=\"Dashboard_KPITendencyBtn${Dashboard_KPIDTO.ID}\")\"" >` +
                `</i><i class=\"fas fa-chart-line me-2 fa-2x\"></i></a></td>` +
                `<td style="width: 95px;">${Dashboard_KPIDTO.KPIDTO.OwnerName}</td>` +
                `<td style="width: 400px;" class=\"bg-yellow\">${Dashboard_KPIDTO.KPIName}</td>` +
                `<td class=\"bg-info fw-bold\"> ${Dashboard_KPIDTO.KPIDTO.EquivalenceIcon} ${_fyGoalFormat}${_fyGoalSymbol}</td>`;
            Month_Enum.forEach(function (MonthDTO) {
                let _bgColor = "";
                let _fontColor = "";
                let _value;
                let _valueTypeIcon;

                let _dasboardLineDTO = Dashboard_KPIDTO.DashboardLineList.filter(function (x) {

                    return x.Month == MonthDTO.value
                });

                if (_dasboardLineDTO.length > 0 && _dasboardLineDTO[0].Validated) {
                    _bgColor = _dasboardLineDTO[0].MonthValue.KPIBackgroundColor;
                    _fontColor = "000";
                    _value = Dashboard_KPIDTO.KPIDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                        ConvertToMoney(_dasboardLineDTO[0].Value) : _dasboardLineDTO[0].Value;

                    _valueTypeIcon = _fyGoalSymbol;
                } else {
                    _value = ""
                    _bgColor = "FFF"
                    _fontColor = "000"
                    _valueTypeIcon = ""
                }

                _TQCFormatHTML += `<td class="btn-modal-KPI" data-monthname=${MonthDTO.name} data-dashboardlineid=${(_dasboardLineDTO.length > 0) ? _dasboardLineDTO[0].ID : 0} style=background-color:#${_bgColor};cursor:pointer;><a class="fw-bold" style="color:#${_fontColor} !important;text-decoration:none;" id="KPIInformationByDashboardAndMonthBtn${Dashboard_KPIDTO.ID}" >${_value}${_valueTypeIcon}</a></td>`;
            });
        });
        _TQCFormatHTML += '</tr>'


        _TQCFormatHTML +=
            "</tbody>" +
            "</table>";
        _panelBodyCategory.innerHTML = _TQCFormatHTML;
        //Add event to tendency information
        let _tendencyKPIList = document.querySelectorAll('.btn-modal-tendency');
        _tendencyKPIList.forEach(function (_KPI) {
            _KPI.addEventListener('click', TendencyKPIEventHandler);
        });

        //Add evento to update month value
        let _monthValueList = document.querySelectorAll('.btn-modal-KPI');
        _monthValueList.forEach(function (_month) {
            _month.addEventListener('click', MonthValueEventHandler);
        })
    }
}
//#endregion
//#region Event handlers
function TendencyKPIEventHandler() {
    let _Dashboard_KPIDTO = {
        DashboardCategoryID: this.dataset.dashboardcategoryid,
        KPIDTO: {
            ID: this.dataset.kpiid,
            Name: this.dataset.kpiname,
            Goal: this.dataset.kpigoal,
            UnitOfMeasureID: parseInt(this.dataset.unitofmeasureid),
            EquivalenceIcon: this.dataset.equivalenceicon,
            ValueTypeID: this.dataset.valuetypeid
        }
    }
    $('#Dashboard_KPITendencyModal').on('shown.bs.modal', function () {
        $("#dxKPITendenceChart").dxChart("instance").render();
    });
    GetDashboard_KPITendence_Global(_Dashboard_KPIDTO);
}
function MonthValueEventHandler() {
    let _dashboardLineID = this.dataset.dashboardlineid;
    let _monthName = this.dataset.monthname;
    if (_dashboardLineID > 0) {
        GetDashboardLineInformation_Global(_dashboardLineID);
        document.getElementById('KPIValueMonth').innerHTML = _monthName;
    } else {
        toastr["error"]("There isn't information for this month", "Month not available");
    }
}
//#endregion
//#Update KPI Month Value
async function PopulateKPIInformationByDashboardAndMonth(DashboardLineDTO) {
    document.getElementById('KPIColumn').innerText = DashboardLineDTO.KPIDTO.Name;
    document.getElementById('KPIDescriptionColumn').innerText = DashboardLineDTO.KPIDTO.Description
    document.getElementById('GoalColumn').innerHTML = DashboardLineDTO.KPIDTO.EquivalenceIcon + ' ' + DashboardLineDTO.KPIDTO.Goal;
    document.getElementById('DashboardDataEntryValue').value = DashboardLineDTO.Value;
    document.getElementById('DashboardDataEntryComments').value = DashboardLineDTO.Comment;
    document.getElementById('hiddenDashboardLineID').value = DashboardLineDTO.ID;
    $("#DataEntryKPIInfoModal").modal("show");
}
function ClearMonthValueModal() {
    document.getElementById('hiddenDashboardLineID').value = "0"
    document.getElementById('DashboardDataEntryValue').value = "0"
    document.getElementById('DashboardDataEntryComments').value = ""
}
function SetSubtitles(KPIDTO) {
    console.log(KPIDTO)
    let _fyGoalSymbol = (KPIDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(KPIDTO);
    let _fyGoalFormat = KPIDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
        ConvertToMoney(KPIDTO.Goal) : KPIDTO.Goal;

    let _subtitle;
    _subtitle = '<form class="form-inline">' +
        '<div class="form-group">' +
        '<label ><strong>KPI : </strong></label>' +
        '<label >' + KPIDTO.Name + '</label>' +
        '<label ><strong> - Goal : </strong></label>' +
        '<label >' + KPIDTO.EquivalenceIcon + _fyGoalFormat + _fyGoalSymbol + '</label>' +
        '</div>' +
        '</form>';
    $("#dxKPITendenceChart").dxChart("instance").option("title", {
        subtitle: {
            text: _subtitle
        }
    });
}
function UnitOfMeasureFormat(KPIDTO) {

    //let valueType;
    switch (KPIDTO.UnitOfMeasureID) {
        // Mi base de datos tiene datos que no pude quitar desde Unit Of Measure, por eso los IDs deformes para cada case
        case UnitOfMeasure_Enum.USD:
            return ""
            break;
        case UnitOfMeasure_Enum.KG:
            return " KG"
            break;
        case UnitOfMeasure_Enum.Numeric:
            return " Units"
            break;
        case UnitOfMeasure_Enum.KWHRHR:
            return " KWHR/HR"
            break;
        case UnitOfMeasure_Enum.Hour:
            return " Hr"
            break;
        case UnitOfMeasure_Enum.Minutes:
            return " Min"
            break;
        case UnitOfMeasure_Enum.Days:
            return " D"
            break;
        default:
            return "";
            break;
    }

}

function ConvertToMoney(Goal) {
    let USDollar = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
    });
    return USDollar.format(Goal);
}
//#region Call service functions
async function GetDashboard_KPIListForTable(DashboardDTO) {
    if (DashboardDTO != null && DashboardDTO != undefined) {
        document.getElementById("dashboardtitle").innerHTML = DashboardDTO[0].Name;
    }
    const _dashboard_kpiDTO = {
        DashboardID: document.getElementById('hiddenDashboardID').value,
        IsActive: true,
        GetDashboardLineList: true
    }
    const _Dashboard_KPIList = await GetDashboard_KPIWithUI(_dashboard_kpiDTO);
    FilterKPIListByCategory(_Dashboard_KPIList);
}
async function GetDashboardLineInformation_Global(DashboardLineID) {
    await dxLoadPanel.show();
    const _dashboardLineDTO = await GetDashboardLineInformation({
        ID: DashboardLineID,
        GetKPIDTO: true
    })
    PopulateKPIInformationByDashboardAndMonth(_dashboardLineDTO[0]);
    dxLoadPanel.hide();
}
async function GetDashboard_KPITendence_Global(Dashboard_KPIDTO) {
    await dxLoadPanel.show();
    const _Dashboard_KPITendence = await GetDashboard_KPITendence({
        DashboardID: document.getElementById('hiddenDashboardID').value,
        DashboardCategoryID: Dashboard_KPIDTO.DashboardCategoryID,
        KPIID: Dashboard_KPIDTO.KPIDTO.ID,

    });
    console.log(_Dashboard_KPITendence);
    $("#dxKPITendenceChart").dxChart('option', 'dataSource', _Dashboard_KPITendence);
    $("#dxKPITendenceChart").dxChart('instance').render();
    SetSubtitles(Dashboard_KPIDTO.KPIDTO)
    dxLoadPanel.hide();
}
function GetDashboardLineDTO() {
    let _dashboardLineDTO = {
        ID: document.getElementById('hiddenDashboardLineID').value,
        Value: document.getElementById('DashboardDataEntryValue').value,
        Comment: document.getElementById('DashboardDataEntryComments').value,
    }
    return _dashboardLineDTO;
}
async function UpdateDashboardLine() {
    await dxLoadPanel.show()
    let _dashboardLineDTO = GetDashboardLineDTO();
    const _validation_resultDTO = await AddMonthlyValue(_dashboardLineDTO);
    if (_validation_resultDTO.Result) {
        ClearMonthValueModal();
        $('#DataEntryKPIInfoModal').modal('hide');
    }
    HostResponse(_validation_resultDTO);
    $('#Dashboard_KPITendencyModal').on('shown.bs.modal', function () {
        $("#dxKPITendenceChart").dxChart("instance").render();
    });
    GetDashboard_KPIListForTable();
    dxLoadPanel.hide();
}
//#endregion

//#region Export Dashboard
function printDashboard() {
    let _dashboardID = document.getElementById("hiddenDashboardID").value;
    if (_dashboardID != 0 && _dashboardID != undefined && _dashboardID != null) {
        window.open("/App/Features/Management/Edashboard/DashboardManagement/PrintDashboard.aspx?DashboardID=" + _dashboardID)
    }
    //let divToPrint = document.getElementById("DashboardPanel");
    //let newWin = window.open("");
    //newWin.document.write('<p style="font-family:Open Sans, sans-serif;font-size:22px;">CTS Tools: <strong>Dashboard Format</strong></p><br>');
    //newWin.document.write('<style>td,th {border: 1px solid black;padding: 10px;font-size:14px;' +
    //    'color: black!important; text-align: center;} ' + 'th{color:black !important;font-weight:bold;font-family:Open Sans, sans-serif;}</style>');
    //newWin.document.write(divToPrint.outerHTML);
    //newWin.print();
    //newWin.close();
}
function ExportDashboardToPDF() {

    html2canvas($('#DashboardPanel')[0], {
        scale: 2,
        onrendered: function (canvas) {
            var data = canvas.toDataURL();
            var docDefinition = {
                //compress: false,

                content: [{
                    image: data,
                    width: 760
                }],
                info: {
                    title: 'Dashboard_Format',
                    author: 'CTS Tools',
                },
                pageOrientation: 'landscape',
            };
            pdfMake.createPdf(docDefinition).open();
        }
    });
}
function ExportDashboardToExcel() {
    const tempContainer = document.createElement("div");
    tempContainer.id = "temp-export-container";
    tempContainer.style.display = "none"; // Ocultar contenedor en pantalla
    const titleTable = document.createElement("table");
    titleTable.innerHTML = `
        <tr>
            <td colspan="17" style="text-align: center; font-weight: bold; font-size: 20px; background-color: #f4f4f4;">
                <strong>${document.getElementById("dashboardtitle").innerText}</strong>
            </td>
        </tr>
    `;
    tempContainer.appendChild(titleTable);

    const tableElement = document.getElementById("DashboardPanel");
    if (tableElement) {
        const clonedTable = tableElement.cloneNode(true); // Clonar tabla
        tempContainer.appendChild(clonedTable); // Agregar tabla clonada al contenedor
    }
    // Agregar el contenedor al DOM temporalmente
    document.body.appendChild(tempContainer);
    // Exportar las tablas combinadas a Excel
    $("#temp-export-container").table2excel({
        exclude: ".noExl",
        preserveColors: true,
        name: "Dashboard - CTS Tools",
        filename: "Edashboard_CTSTools", // No incluyas extensión aquí
        fileext: ".xls", // Extensión del archivo
        exclude_img: true, // Excluir imágenes
        exclude_links: true, // Excluir enlaces
        exclude_inputs: true // Excluir campos input
    });
    // Eliminar el contenedor temporal después de exportar
    document.body.removeChild(tempContainer);
}

//#endregion

//#endregion

//#region Assign KPI

async function InitializeTemplateAdministrationControls() {
    $("#dxDashboard_KPI_KPIDataGrid").dxDataGrid({
        dataSource: await GetDXKPIDataSource({ IsActive: true }),
        allowColumnReordering: true,
        rowAlternationEnabled: true,
        showBorders: true,
        allowColumnReordering: true,
        rowAlternationEnabled: true,
        columnAutoWidth: true,
        columnMinWidth: 250,
        allowColumnResizing: true,
        columnResizingMode: 'widget',
        hoverStateEnabled: true,
        remoteOperations: true,
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 15,
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [20, 50, 100, 200],
            showInfo: true
        },
        searchPanel: {
            visible: false,
            highlightCaseSensitive: true,
        },
        grouping: {
            autoExpandAll: true,
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        headerFilter:
        {
            visible: true
        },
        groupPanel: {
            visible: true,

        },
        selection: {
            mode: 'multiple',
            selectAllMode: 'page',
            showCheckBoxesMode: 'always'
        },
        columns: [
            {
                dataField: 'Name',
                caption: 'KPI',
            },
            {
                dataField: 'DashboardCategoryName',
                caption: 'Category',
                groupIndex: 0,
            },
            {
                dataField: 'OwnerDepartmentName',
                caption: 'Owner Department',
            },
            {
                dataField: 'ResponsibleDepartmentName',
                caption: 'Responsible Department'
            },
            {
                dataField: 'UnitOfMeasureName',
                caption: 'Unit of measure'
            },
            {
                dataField: 'ValueTypeName',
                caption: 'Value Type'
            },
            {
                dataField: 'Goal',
                caption: 'Goal'
            },
            {
                dataField: 'GoalRangeValue',
                caption: 'Goal Range'
            },
            {
                dataField: 'CalculationTypeName',
                caption: 'Calculation Type'
            }
        ]
    });
    $("#dxQualityKPIs").dxDataGrid({
        dataSource: [],
        allowColumnReordering: true,
        rowAlternationEnabled: true,
        showBorders: true,
        allowColumnReordering: true,
        rowAlternationEnabled: true,
        columnAutoWidth: true,
        columnMinWidth: 130,
        allowColumnResizing: true,
        columnResizingMode: 'widget',
        hoverStateEnabled: true,
        remoteOperations: true,
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 10,
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [20, 50, 100, 200],
            showInfo: true
        },
        searchPanel: {
            visible: false,
            highlightCaseSensitive: true,
        },
        grouping: {
            autoExpandAll: true,
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        headerFilter:
        {
            visible: true
        },
        groupPanel: {
            visible: false,

        },
        rowDragging: {
            allowReordering: true,
            dropFeedbackMode: 'push',
            async onReorder(e) {
                let visibleRows = e.component.getVisibleRows();
                // Filter only rows with rowType: 'data'
                let dataRows = visibleRows.filter(row => row.rowType === 'data');
                // Adjust the index to work only with rows of type 'data'
                let adjustedIndex = dataRows.findIndex(row => row === visibleRows[e.toIndex]);
                // Check if the index is valid
                if (adjustedIndex !== -1) {
                    // Get the data of the row of type 'data' that corresponds to the adjusted index
                    let newOrderStructureDTO = dataRows[adjustedIndex].data;
                    var _data = e.itemData;
                    // Change the origin Order to the destination Order
                    _data.Order = newOrderStructureDTO.Order;
                    UpdateDashboard_KPIOrder_Global(_data);
                }
            },
        },

        columns: [
            {
                caption: "Delete",
                alignment: "center",
                allowFiltering: false,
                allowSorting: false,
                width: "auto",
                cellTemplate: function (container, options) {
                    container.height(30);
                    $('<button type="button" class="btn btn-danger ms-2" style="padding-top: 2px; ' +
                        'padding-bottom:5px"><i class="fa fa-trash-alt"></i><span>' +
                        + '</span></button>')
                        .height(30)
                        .on('dxclick', function () {
                            $("#hiddenDashboard_KPIID").val(options.data.ID);
                            ShowDashboard_KPIDeleteQuestion(options.data);
                        }).appendTo(container);
                },
            },
            {
                dataField: 'Order',
                caption: 'Order',
                sortIndex: 0,
                sortOrder: "asc",
                width: 100,
            },
            {
                dataField: 'DashboardCategoryName',
                caption: 'Category',
            },
            {
                dataField: 'KPIDTO.Name',
                caption: 'KPI',
            },
            {
                dataField: 'KPIDTO.Description',
                caption: 'Description',
            },
            {
                dataField: 'KPIDTO.ValueTypeName',
                caption: 'Value Type',
            },
            {
                dataField: 'KPIDTO.OwnerName',
                caption: 'KPI Owner',
            },
            {
                dataField: 'KPIDTO.OwnerDepartmentName',
                caption: 'Department',
            },
            {
                dataField: 'KPIDTO.ResponsibleName',
                caption: 'Responsible',
            },

            {
                dataField: 'KPIDTO.ResponsibleDepartmentName',
                caption: 'Responsible Department',
            },
            {
                dataField: 'KPIDTO.UnitOfMeasureName',
                caption: 'Unit Of Measure',
            },

            {
                dataField: 'KPIDTO.Goal',
                caption: 'Goal',
            },
            {
                dataField: 'KPIDTO.GoalRangeValue',
                caption: 'Goal Range',
            },
            {
                dataField: 'KPIDTO.FacilityName',
                caption: 'Facility',
            },
            {
                dataField: 'KPIDTO.EquivalenceName',
                caption: 'Equivalence',
            },
            {
                dataField: 'KPIDTO.LastUpdateByName',
                caption: 'Last Update By',
            },
            {
                dataField: 'KPIDTO.LastUpdate',
                caption: 'Last Update Date',
            }

        ]
    });

}
function GetDashboard_KPIDTO(DashboardDTO) {
    let _Dashboard_KPIDTO = {
        DashboardID: document.getElementById('hiddenDashboardID').value,
        KPIIDArray: ($("#dxDashboard_KPI_KPIDataGrid").dxDataGrid("instance").getSelectedRowsData()).map(m => m.ID),
        DashboardCategoryIDArray: ($("#dxDashboard_KPI_KPIDataGrid").dxDataGrid("instance").getSelectedRowsData()).map(m => m.DashboardCategoryID),
        GetKPIDTO: true,
        GetDashboardDTO: true,
        GetDashboardCategoryDTO: true,
        IsActive: true,
    }
    return _Dashboard_KPIDTO;
}
function ClearDashboard_KPIFields() {
    let keys = $("#dxDashboard_KPI_KPIDataGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDashboard_KPI_KPIDataGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxDashboard_KPI_KPIDataGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxDashboard_KPI_KPIDataGrid").dxDataGrid("instance").refresh();
}
async function ShowDashboard_KPIDeleteQuestion(Dashboard_KPIDTO) {
    const _alert = await Swal.fire({
        text: 'You will remove this KPI from current dashboard, are you sure?',
        title: 'Warning',
        confirmButtonText: `Delete`,
        icon: 'warning',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        const _Dashboard_KPIDTO = Dashboard_KPIDTO;
        DeleteDashboard_KPI_Global(_Dashboard_KPIDTO);
    }
}


//#region CRUD Functions
async function CreateDashboard_KPI_Global() {
    await dxLoadPanel.show();
    const _Dashboard_KPIDTO = GetDashboard_KPIDTO();
    const _validation_ResultDTO = await CreateDashboard_KPIFromKPIList(_Dashboard_KPIDTO);
    //Poner aqui funcion que va a recargar las KPIas mostradas en pantalla
    if (_validation_ResultDTO.Result) {
        $('#AddKPIsModal').modal('hide');
    }
    ClearDashboard_KPIFields();
    //GetDashboard_KPIList();
    HostResponse(_validation_ResultDTO);
    $("#dxQualityKPIs").dxDataGrid("instance").refresh();
    GetDashboard_KPIListForTable();
    dxLoadPanel.hide();
}
async function DeleteDashboard_KPI_Global(Dashboard_KPIDTO) {
    await dxLoadPanel.show();
    const _Dashboard_KPIDTO = Dashboard_KPIDTO;
    const _validation_ResultDTO = await DeleteDashboard_KPI(_Dashboard_KPIDTO);
    if (_validation_ResultDTO.Result) {
        document.getElementById("hiddenDashboard_KPIID").value = 0;
        //await GetDashboard_KPIList();
    }
    HostResponse(_validation_ResultDTO);
    $("#dxQualityKPIs").dxDataGrid("instance").refresh();
    GetDashboard_KPIListForTable();
    dxLoadPanel.hide();
}
//#endregion

//#region Business Logic functions
async function GetDashboard_KPIListForGrid(DashboardDTO) {
    document.getElementById("dashboardtitle").innerHTML = DashboardDTO[0].Name;
    const _Dashboard_KPIDTO = GetDashboard_KPIDTO(DashboardDTO);
    $("#dxQualityKPIs").dxDataGrid("instance").option("dataSource", await GetDXDashboard_KPIDataSource(_Dashboard_KPIDTO));
    /*document.getElementById('hiddenDashboardID').value = DashboardDTO.DashboardID;*/
    document.getElementById('AddKPIBtn').classList.remove('disabled');
}
//#endregion


// #region Change Order Functions
function GetDashboard_KPIOrderDTO(Data) {
    let _Dashboard_KPIDTO = {
        ID: Data.ID,
        DashboardID: Data.DashboardID,
        KPIID: Data.KPIID,
        DashboardCategoryID: Data.DashboardCategoryID,
        Order: Data.Order
    }
    return _Dashboard_KPIDTO;
}
async function UpdateDashboard_KPIOrder_Global(Data) {
    await dxLoadPanel.show();
    const _dashboardDTO = GetDashboard_KPIOrderDTO(Data);
    const _validation_ResultDTO = await UpdateDashboard_KPIOrder(_dashboardDTO);
    HostResponse(_validation_ResultDTO);
    if (_validation_ResultDTO.Result) {
        $("#dxQualityKPIs").dxDataGrid("instance").refresh();
        GetDashboard_KPIListForTable();
    }
    dxLoadPanel.hide();
}

//#endregion
//#endregion