import { GetDXDashboardDataSource, GetDashboardInformation } from './Dashboard/Dashboard_Service.js';
import { AddMonthlyValue, GetDashboardLineInformation, GetDashboardMetricTendence } from './DashboardLine/DashboardLine_Service.js';
import { GetDashboardMetricWithUI } from './Dashboard_KPI/Dashboard_KPI_Service.js';
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { GetURLParameter } from '../../../../Common/Utils/GetURLParameter.js'
import { Dashboard_Category_Enum } from '../Settings/DashboardCategory/Dashboard_Category_Enum.js';
import { Month_Enum } from '../../../../Common/Utils/Month_Enum.js';
import { ValueType_Enum } from '../Settings/ValueType/ValueType_Enum.js';
import { UnitOfMeasure_Enum } from '../Settings/UnitOfMeasure/UnitOfMeasure_Enum.js'
import { GetDXDashboardMetricDataSource, CreateDashboardMetric, UpdateDashboardMetricOrder, DeleteDashboardMetric, GetDashboardMetricInformation, CreateDashboardMetricFromMetricList } from './Dashboard_KPI/Dashboard_KPI_Service.js';

import { GetDXDashboardCategoryDataSource } from '../Settings/DashboardCategory/DashboardCategory_Service.js'
import { GetDXKPIDataSource } from './KPI/KPI_Service.js'




document.addEventListener("DOMContentLoaded", async function () {
    //#region Data Entry

    await InitializeDashboardDataEntryControls();
    document.getElementById('SaveDashboardLine').addEventListener('click', UpdateDashboardLine);
    await GetDashboardIDByURL();
    $("#dashboardButton").hide();

    document.getElementById('KPIButton').addEventListener('click', function (e) {
        e.preventDefault()
        $('#tab2 a[href="#KPITab"]').tab('show')
        $("#KPIButton").hide();
        $("#dashboardButton").show();


    })
    document.getElementById('dashboardButton').addEventListener('click', function (e) {
        e.preventDefault()
        $('#tab1 a[href="#DashboardTab"]').tab('show')
        $("#dashboardButton").hide();
        $("#KPIButton").show();

    })

    //#endregion

    //#region Assign KPI

    await InitializeTemplateAdministrationControls();
    document.getElementById('AddMetricButton').addEventListener('click', CreateDashboardMetric_Global);
    document.getElementById('AddMetricBtn').addEventListener('click', function () {
        $('#AddMetricsModal').modal('show');
    });
    document.getElementById('XBtnModal').addEventListener('click', ClearDashboardMetricFields);
    document.getElementById('CloseBtnModal').addEventListener('click', ClearDashboardMetricFields);
    //#endregion

});

//#region Data Entry
async function GetDashboardIDByURL() {

    let _dashboardID = GetURLParameter("DashboardID");
    let _dashboardDTO = await GetDashboardInformation({ ID: _dashboardID })
    if (_dashboardID != null && _dashboardID != undefined && _dashboardID != 0 && !Number.isNaN(_dashboardID)) {
        document.getElementById('hiddenDashboardID').value = _dashboardID;
        GetDashboardMetricListForTable(_dashboardDTO);
        
        
    } else {
        toastr["error"]("Please, select a dashboard to get the information", "Dashboard Not selected");
    }
    console.log(_dashboardDTO);

}
async function InitializeDashboardDataEntryControls() {

    $("#dxMetricTendenceChart").dxChart({
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
async function FilterMetricListByCategory(DashboardMetricList) {
    //await dxLoadPanel.show();
    //Quality
    //let _sortMetricQuality = DashboardMetricList.filter(function (x) {
    //    return x.DashboardCategoryID == Dashboard_Category_Enum.Quality
    //});
    BuildTQCFormat2(DashboardMetricList);
    document.getElementById('NoDashboardMessage').classList.add('d-none');
    document.getElementById('DashboardMetricList').classList.remove('d-none');
    //document.getElementById('PrintDashboardMetricData').classList.remove('d-none');
    //dxLoadPanel.hide();
}
function BuildTQCFormat2(DashboardMetricList) {
    let _TQCFormatHTML = "";
    let _panelBodyCategory = document.getElementById(`DashboardPanel`);
    _panelBodyCategory.innerHTML = "";
    //Insert header titles of the format
    if (DashboardMetricList.length > 0) {
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
        // Quality
        let _sortQualityList = DashboardMetricList.filter(function (x) {
            return x.DashboardCategoryID == Dashboard_Category_Enum.Quality
        });
        //Start build body of the format
        let _qualityAmount = _sortQualityList.length + 1;
        _TQCFormatHTML +=
            "<tbody>" +
            "<tr>" +
            "<td style=\"width: 95px;\" rowspan=\"" + _qualityAmount + "\">" +
            "<h5><strong>" + "Q" + "</strong></h5><strong><p><strong>" + "Quality" + "</strong></p>" +
            "</td>" +
            "</tr>";
        _sortQualityList.forEach(function (DashboardMetricDTO) {
            let _fyGoalSymbol = (DashboardMetricDTO.MetricDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(DashboardMetricDTO.MetricDTO);
            let _fyGoalFormat = DashboardMetricDTO.MetricDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                ConvertToMoney(DashboardMetricDTO.MetricDTO.Goal) : DashboardMetricDTO.MetricDTO.Goal;
            _TQCFormatHTML +=
                `<tr><td style="width: 80px;"><a class="btn-modal-tendency" ` +
                `data-dashboardcategoryid=${DashboardMetricDTO.DashboardCategoryID} data-valuetypeid=${DashboardMetricDTO.MetricDTO.ValueTypeID} ` +
                `data-equivalenceicon=${DashboardMetricDTO.MetricDTO.EquivalenceIcon} data-metricid=${DashboardMetricDTO.MetricID} ` +
                `data-unitofmeasureid=${DashboardMetricDTO.MetricDTO.UnitOfMeasureID} ` +
                `data-metricgoal=${DashboardMetricDTO.MetricDTO.Goal} data-metricname='${DashboardMetricDTO.MetricDTO.Name}' data-bs-toggle="modal" ` +
                `data-bs-target="#DashboardMetricTendencyModal" id=\"DashboardMetricTendencyBtn${DashboardMetricDTO.ID}\")\"" >` +
                `</i><i class=\"fas fa-chart-line me-2 fa-2x\"></i></a></td>` +
                `<td style="width: 95px;">${DashboardMetricDTO.MetricDTO.OwnerName}</td>` +
                `<td style="width: 400px;" class=\"bg-yellow\">${DashboardMetricDTO.MetricName}</td>` +
                `<td class=\"bg-info fw-bold\"> ${DashboardMetricDTO.MetricDTO.EquivalenceIcon} ${_fyGoalFormat}${_fyGoalSymbol}</td>`;
            Month_Enum.forEach(function (MonthDTO) {
                let _bgColor = "";
                let _fontColor = "";
                let _value;
                let _valueTypeIcon;

                let _dasboardLineDTO = DashboardMetricDTO.DashboardLineList.filter(function (x) {
                    return x.Month == MonthDTO.value
                });

                if (_dasboardLineDTO.length > 0 && _dasboardLineDTO[0].Validated) {
                    _bgColor = _dasboardLineDTO[0].MonthValue.MetricBackgroundColor;
                    _fontColor = "000";
                    _value = DashboardMetricDTO.MetricDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                        ConvertToMoney(_dasboardLineDTO[0].Value) : _dasboardLineDTO[0].Value;

                    _valueTypeIcon = _fyGoalSymbol;
                } else {
                    _value = ""
                    _bgColor = "FFF"
                    _fontColor = "000"
                    _valueTypeIcon = ""
                }

                _TQCFormatHTML += `<td class="btn-modal-metric" data-monthname=${MonthDTO.name} data-dashboardlineid=${(_dasboardLineDTO.length > 0) ? _dasboardLineDTO[0].ID : 0} style=background-color:#${_bgColor};cursor:pointer;><a class="fw-bold" style="color:#${_fontColor} !important;text-decoration:none;" id="MetricInformationByDashboardAndMonthBtn${DashboardMetricDTO.ID}" >${_value}${_valueTypeIcon}</a></td>`;
            });
        });
        _TQCFormatHTML += '</tr>'


        // Safety
        let _sortSafetyList = DashboardMetricList.filter(function (x) {
            return x.DashboardCategoryID == Dashboard_Category_Enum.Safety
        });
        //Start build body of the format
        let _safetyAmount = _sortSafetyList.length + 1;
        _TQCFormatHTML +=
            "<tbody>" +
            "<tr>" +
            "<td style=\"width: 95px;\" rowspan=\"" + _safetyAmount + "\">" +
            "<h5><strong>" + "S" + "</strong></h5><strong><p><strong>" + "Safety" + "</strong></p>" +
            "</td>" +
            "</tr>";
        _sortSafetyList.forEach(function (DashboardMetricDTO) {
            let _fyGoalSymbol = (DashboardMetricDTO.MetricDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(DashboardMetricDTO.MetricDTO);
            let _fyGoalFormat = DashboardMetricDTO.MetricDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                ConvertToMoney(DashboardMetricDTO.MetricDTO.Goal) : DashboardMetricDTO.MetricDTO.Goal;
            _TQCFormatHTML +=
                `<tr><td style="width: 80px;"><a class="btn-modal-tendency" ` +
                `data-dashboardcategoryid=${DashboardMetricDTO.DashboardCategoryID} data-valuetypeid=${DashboardMetricDTO.MetricDTO.ValueTypeID} ` +
                `data-equivalenceicon=${DashboardMetricDTO.MetricDTO.EquivalenceIcon} data-metricid=${DashboardMetricDTO.MetricID} ` +
                `data-unitofmeasureid=${DashboardMetricDTO.MetricDTO.UnitOfMeasureID} ` +
                `data-metricgoal=${DashboardMetricDTO.MetricDTO.Goal} data-metricname='${DashboardMetricDTO.MetricDTO.Name}' data-bs-toggle="modal" ` +
                `data-bs-target="#DashboardMetricTendencyModal" id=\"DashboardMetricTendencyBtn${DashboardMetricDTO.ID}\")\"" >` +
                `</i><i class=\"fas fa-chart-line me-2 fa-2x\"></i></a></td>` +
                `<td style="width: 95px;">${DashboardMetricDTO.MetricDTO.OwnerName}</td>` +
                `<td style="width: 400px;" class=\"bg-yellow\">${DashboardMetricDTO.MetricName}</td>` +
                `<td class=\"bg-info fw-bold\"> ${DashboardMetricDTO.MetricDTO.EquivalenceIcon} ${_fyGoalFormat}${_fyGoalSymbol}</td>`;
            Month_Enum.forEach(function (MonthDTO) {
                let _bgColor = "";
                let _fontColor = "";
                let _value;
                let _valueTypeIcon;

                let _dasboardLineDTO = DashboardMetricDTO.DashboardLineList.filter(function (x) {

                    return x.Month == MonthDTO.value
                });

                if (_dasboardLineDTO.length > 0 && _dasboardLineDTO[0].Validated) {
                    _bgColor = _dasboardLineDTO[0].MonthValue.MetricBackgroundColor;
                    _fontColor = "000";
                    _value = DashboardMetricDTO.MetricDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                        ConvertToMoney(_dasboardLineDTO[0].Value) : _dasboardLineDTO[0].Value;

                    _valueTypeIcon = _fyGoalSymbol;
                } else {
                    _value = ""
                    _bgColor = "FFF"
                    _fontColor = "000"
                    _valueTypeIcon = ""
                }

                _TQCFormatHTML += `<td class="btn-modal-metric" data-monthname=${MonthDTO.name} data-dashboardlineid=${(_dasboardLineDTO.length > 0) ? _dasboardLineDTO[0].ID : 0} style=background-color:#${_bgColor};cursor:pointer;><a class="fw-bold" style="color:#${_fontColor} !important;text-decoration:none;" id="MetricInformationByDashboardAndMonthBtn${DashboardMetricDTO.ID}" >${_value}${_valueTypeIcon}</a></td>`;
            });
        });
        _TQCFormatHTML += '</tr>'


        // Delivery
        let _sortDeliveryList = DashboardMetricList.filter(function (x) {
            return x.DashboardCategoryID == Dashboard_Category_Enum.Delivery
        });
        //Start build body of the format
        let _deliveryAmount = _sortDeliveryList.length + 1;
        _TQCFormatHTML +=
            "<tbody>" +
            "<tr>" +
            "<td style=\"width: 95px;\" rowspan=\"" + _deliveryAmount + "\">" +
            "<h5><strong>" + "D" + "</strong></h5><strong><p><strong>" + "Delivery" + "</strong></p>" +
            "</td>" +
            "</tr>";
        _sortDeliveryList.forEach(function (DashboardMetricDTO) {
            let _fyGoalSymbol = (DashboardMetricDTO.MetricDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(DashboardMetricDTO.MetricDTO);
            let _fyGoalFormat = DashboardMetricDTO.MetricDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                ConvertToMoney(DashboardMetricDTO.MetricDTO.Goal) : DashboardMetricDTO.MetricDTO.Goal;
            _TQCFormatHTML +=
                `<tr><td style="width: 80px;"><a class="btn-modal-tendency" ` +
                `data-dashboardcategoryid=${DashboardMetricDTO.DashboardCategoryID} data-valuetypeid=${DashboardMetricDTO.MetricDTO.ValueTypeID} ` +
                `data-equivalenceicon=${DashboardMetricDTO.MetricDTO.EquivalenceIcon} data-metricid=${DashboardMetricDTO.MetricID} ` +
                `data-unitofmeasureid=${DashboardMetricDTO.MetricDTO.UnitOfMeasureID} ` +
                `data-metricgoal=${DashboardMetricDTO.MetricDTO.Goal} data-metricname='${DashboardMetricDTO.MetricDTO.Name}' data-bs-toggle="modal" ` +
                `data-bs-target="#DashboardMetricTendencyModal" id=\"DashboardMetricTendencyBtn${DashboardMetricDTO.ID}\")\"" >` +
                `</i><i class=\"fas fa-chart-line me-2 fa-2x\"></i></a></td>` +
                `<td style="width: 95px;">${DashboardMetricDTO.MetricDTO.OwnerName}</td>` +
                `<td style="width: 400px;" class=\"bg-yellow\">${DashboardMetricDTO.MetricName}</td>` +
                `<td class=\"bg-info fw-bold\"> ${DashboardMetricDTO.MetricDTO.EquivalenceIcon} ${_fyGoalFormat}${_fyGoalSymbol}</td>`;
            Month_Enum.forEach(function (MonthDTO) {
                let _bgColor = "";
                let _fontColor = "";
                let _value;
                let _valueTypeIcon;

                let _dasboardLineDTO = DashboardMetricDTO.DashboardLineList.filter(function (x) {

                    return x.Month == MonthDTO.value
                });

                if (_dasboardLineDTO.length > 0 && _dasboardLineDTO[0].Validated) {
                    _bgColor = _dasboardLineDTO[0].MonthValue.MetricBackgroundColor;
                    _fontColor = "000";
                    _value = DashboardMetricDTO.MetricDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                        ConvertToMoney(_dasboardLineDTO[0].Value) : _dasboardLineDTO[0].Value;

                    _valueTypeIcon = _fyGoalSymbol;
                } else {
                    _value = ""
                    _bgColor = "FFF"
                    _fontColor = "000"
                    _valueTypeIcon = ""
                }

                _TQCFormatHTML += `<td class="btn-modal-metric" data-monthname=${MonthDTO.name} data-dashboardlineid=${(_dasboardLineDTO.length > 0) ? _dasboardLineDTO[0].ID : 0} style=background-color:#${_bgColor};cursor:pointer;><a class="fw-bold" style="color:#${_fontColor} !important;text-decoration:none;" id="MetricInformationByDashboardAndMonthBtn${DashboardMetricDTO.ID}" >${_value}${_valueTypeIcon}</a></td>`;
            });
        });
        _TQCFormatHTML += '</tr>'

        // Moral
        let _sortMoralList = DashboardMetricList.filter(function (x) {
            return x.DashboardCategoryID == Dashboard_Category_Enum.Moral
        });
        //Start build body of the format
        let _moralAmount = _sortMoralList.length + 1;
        _TQCFormatHTML +=
            "<tbody>" +
            "<tr>" +
            "<td style=\"width: 95px;\" rowspan=\"" + _moralAmount + "\">" +
            "<h5><strong>" + "M" + "</strong></h5><strong><p><strong>" + "Moral" + "</strong></p>" +
            "</td>" +
            "</tr>";
        _sortMoralList.forEach(function (DashboardMetricDTO) {
            let _fyGoalSymbol = (DashboardMetricDTO.MetricDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(DashboardMetricDTO.MetricDTO);
            let _fyGoalFormat = DashboardMetricDTO.MetricDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                ConvertToMoney(DashboardMetricDTO.MetricDTO.Goal) : DashboardMetricDTO.MetricDTO.Goal;
            _TQCFormatHTML +=
                `<tr><td style="width: 80px;"><a class="btn-modal-tendency" ` +
                `data-dashboardcategoryid=${DashboardMetricDTO.DashboardCategoryID} data-valuetypeid=${DashboardMetricDTO.MetricDTO.ValueTypeID} ` +
                `data-equivalenceicon=${DashboardMetricDTO.MetricDTO.EquivalenceIcon} data-metricid=${DashboardMetricDTO.MetricID} ` +
                `data-unitofmeasureid=${DashboardMetricDTO.MetricDTO.UnitOfMeasureID} ` +
                `data-metricgoal=${DashboardMetricDTO.MetricDTO.Goal} data-metricname='${DashboardMetricDTO.MetricDTO.Name}' data-bs-toggle="modal" ` +
                `data-bs-target="#DashboardMetricTendencyModal" id=\"DashboardMetricTendencyBtn${DashboardMetricDTO.ID}\")\"" >` +
                `</i><i class=\"fas fa-chart-line me-2 fa-2x\"></i></a></td>` +
                `<td style="width: 95px;">${DashboardMetricDTO.MetricDTO.OwnerName}</td>` +
                `<td style="width: 400px;" class=\"bg-yellow\">${DashboardMetricDTO.MetricName}</td>` +
                `<td class=\"bg-info fw-bold\"> ${DashboardMetricDTO.MetricDTO.EquivalenceIcon} ${_fyGoalFormat}${_fyGoalSymbol}</td>`;
            Month_Enum.forEach(function (MonthDTO) {
                let _bgColor = "";
                let _fontColor = "";
                let _value;
                let _valueTypeIcon;

                let _dasboardLineDTO = DashboardMetricDTO.DashboardLineList.filter(function (x) {

                    return x.Month == MonthDTO.value
                });

                if (_dasboardLineDTO.length > 0 && _dasboardLineDTO[0].Validated) {
                    _bgColor = _dasboardLineDTO[0].MonthValue.MetricBackgroundColor;
                    _fontColor = "000";
                    _value = DashboardMetricDTO.MetricDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                        ConvertToMoney(_dasboardLineDTO[0].Value) : _dasboardLineDTO[0].Value;

                    _valueTypeIcon = _fyGoalSymbol;
                } else {
                    _value = ""
                    _bgColor = "FFF"
                    _fontColor = "000"
                    _valueTypeIcon = ""
                }

                _TQCFormatHTML += `<td class="btn-modal-metric" data-monthname=${MonthDTO.name} data-dashboardlineid=${(_dasboardLineDTO.length > 0) ? _dasboardLineDTO[0].ID : 0} style=background-color:#${_bgColor};cursor:pointer;><a class="fw-bold" style="color:#${_fontColor} !important;text-decoration:none;" id="MetricInformationByDashboardAndMonthBtn${DashboardMetricDTO.ID}" >${_value}${_valueTypeIcon}</a></td>`;
            });
        });
        _TQCFormatHTML += '</tr>'
        // Cost
        let _sortCostList = DashboardMetricList.filter(function (x) {
            return x.DashboardCategoryID == Dashboard_Category_Enum.Cost
        });
        //Start build body of the format
        let _costAmount = _sortCostList.length + 1;
        _TQCFormatHTML +=
            "<tbody>" +
            "<tr>" +
            "<td style=\"width: 95px;\" rowspan=\"" + _costAmount + "\">" +
            "<h5><strong>" + "C" + "</strong></h5><strong><p><strong>" + "Cost" + "</strong></p>" +
            "</td>" +
            "</tr>";
        _sortCostList.forEach(function (DashboardMetricDTO) {
            let _fyGoalSymbol = (DashboardMetricDTO.MetricDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(DashboardMetricDTO.MetricDTO);
            let _fyGoalFormat = DashboardMetricDTO.MetricDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                ConvertToMoney(DashboardMetricDTO.MetricDTO.Goal) : DashboardMetricDTO.MetricDTO.Goal;
            _TQCFormatHTML +=
                `<tr><td style="width: 80px;"><a class="btn-modal-tendency" ` +
                `data-dashboardcategoryid=${DashboardMetricDTO.DashboardCategoryID} data-valuetypeid=${DashboardMetricDTO.MetricDTO.ValueTypeID} ` +
                `data-equivalenceicon=${DashboardMetricDTO.MetricDTO.EquivalenceIcon} data-metricid=${DashboardMetricDTO.MetricID} ` +
                `data-unitofmeasureid=${DashboardMetricDTO.MetricDTO.UnitOfMeasureID} ` +
                `data-metricgoal=${DashboardMetricDTO.MetricDTO.Goal} data-metricname='${DashboardMetricDTO.MetricDTO.Name}' data-bs-toggle="modal" ` +
                `data-bs-target="#DashboardMetricTendencyModal" id=\"DashboardMetricTendencyBtn${DashboardMetricDTO.ID}\")\"" >` +
                `</i><i class=\"fas fa-chart-line me-2 fa-2x\"></i></a></td>` +
                `<td style="width: 95px;">${DashboardMetricDTO.MetricDTO.OwnerName}</td>` +
                `<td style="width: 400px;" class=\"bg-yellow\">${DashboardMetricDTO.MetricName}</td>` +
                `<td class=\"bg-info fw-bold\"> ${DashboardMetricDTO.MetricDTO.EquivalenceIcon} ${_fyGoalFormat}${_fyGoalSymbol}</td>`;
            Month_Enum.forEach(function (MonthDTO) {
                let _bgColor = "";
                let _fontColor = "";
                let _value;
                let _valueTypeIcon;

                let _dasboardLineDTO = DashboardMetricDTO.DashboardLineList.filter(function (x) {

                    return x.Month == MonthDTO.value
                });

                if (_dasboardLineDTO.length > 0 && _dasboardLineDTO[0].Validated) {
                    _bgColor = _dasboardLineDTO[0].MonthValue.MetricBackgroundColor;
                    _fontColor = "000";
                    _value = DashboardMetricDTO.MetricDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
                        ConvertToMoney(_dasboardLineDTO[0].Value) : _dasboardLineDTO[0].Value;

                    _valueTypeIcon = _fyGoalSymbol;
                } else {
                    _value = ""
                    _bgColor = "FFF"
                    _fontColor = "000"
                    _valueTypeIcon = ""
                }

                _TQCFormatHTML += `<td class="btn-modal-metric" data-monthname=${MonthDTO.name} data-dashboardlineid=${(_dasboardLineDTO.length > 0) ? _dasboardLineDTO[0].ID : 0} style=background-color:#${_bgColor};cursor:pointer;><a class="fw-bold" style="color:#${_fontColor} !important;text-decoration:none;" id="MetricInformationByDashboardAndMonthBtn${DashboardMetricDTO.ID}" >${_value}${_valueTypeIcon}</a></td>`;
            });
        });
        _TQCFormatHTML += '</tr>'
        _TQCFormatHTML +=
            "</tbody>" +
            "</table>";
        _panelBodyCategory.innerHTML = _TQCFormatHTML;
        //Add event to tendency information
        let _tendencyMetricList = document.querySelectorAll('.btn-modal-tendency');
        _tendencyMetricList.forEach(function (_metric) {
            _metric.addEventListener('click', TendencyMetricEventHandler);
        });

        //Add evento to update month value
        let _monthValueList = document.querySelectorAll('.btn-modal-metric');
        _monthValueList.forEach(function (_month) {
            _month.addEventListener('click', MonthValueEventHandler);
        })
    }
}
//#endregion
//#region Event handlers
function TendencyMetricEventHandler() {
    let _dashboardMetricDTO = {
        DashboardCategoryID: this.dataset.dashboardcategoryid,
        MetricDTO: {
            ID: this.dataset.metricid,
            Name: this.dataset.metricname,
            Goal: this.dataset.metricgoal,
            UnitOfMeasureID: parseInt(this.dataset.unitofmeasureid),
            EquivalenceIcon: this.dataset.equivalenceicon,
            ValueTypeID: this.dataset.valuetypeid
        }
    }
    $('#DashboardMetricTendencyModal').on('shown.bs.modal', function () {
        $("#dxMetricTendenceChart").dxChart("instance").render();
    });
    GetDashboardMetricTendence_Global(_dashboardMetricDTO);
}
function MonthValueEventHandler() {
    let _dashboardLineID = this.dataset.dashboardlineid;
    let _monthName = this.dataset.monthname;
    if (_dashboardLineID > 0) {
        GetDashboardLineInformation_Global(_dashboardLineID);
        document.getElementById('MetricValueMonth').innerHTML = _monthName;
    } else {
        toastr["error"]("There isn't information for this month", "Month not available");
    }
}
//#endregion
//#Update Metric Month Value
async function PopulateMetricInformationByDashboardAndMonth(DashboardLineDTO) {
    document.getElementById('MetricColumn').innerText = DashboardLineDTO.MetricDTO.Name;
    document.getElementById('MetricDescriptionColumn').innerText = DashboardLineDTO.MetricDTO.Description
    document.getElementById('GoalColumn').innerHTML = DashboardLineDTO.MetricDTO.EquivalenceIcon + ' ' + DashboardLineDTO.MetricDTO.Goal;
    document.getElementById('DashboardDataEntryValue').value = DashboardLineDTO.Value;
    document.getElementById('DashboardDataEntryComments').value = DashboardLineDTO.Comment;
    document.getElementById('hiddenDashboardLineID').value = DashboardLineDTO.ID;
    $("#DataEntryMetricInfoModal").modal("show");
}
function ClearMonthValueModal() {
    document.getElementById('hiddenDashboardLineID').value = "0"
    document.getElementById('DashboardDataEntryValue').value = "0"
    document.getElementById('DashboardDataEntryComments').value = ""
}
function SetSubtitles(MetricDTO) {
    console.log(MetricDTO)
    let _fyGoalSymbol = (MetricDTO.ValueTypeID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(MetricDTO);
    let _fyGoalFormat = MetricDTO.UnitOfMeasureID == UnitOfMeasure_Enum.USD ?
        ConvertToMoney(MetricDTO.Goal) : MetricDTO.Goal;

    let _subtitle;
    _subtitle = '<form class="form-inline">' +
        '<div class="form-group">' +
        '<label ><strong>KPI : </strong></label>' +
        '<label >' + MetricDTO.Name + '</label>' +
        '<label ><strong> - Goal : </strong></label>' +
        '<label >' + MetricDTO.EquivalenceIcon + _fyGoalFormat + _fyGoalSymbol + '</label>' +
        '</div>' +
        '</form>';
    $("#dxMetricTendenceChart").dxChart("instance").option("title", {
        subtitle: {
            text: _subtitle
        }
    });
}
function UnitOfMeasureFormat(MetricDTO) {

    //let valueType;
    switch (MetricDTO.UnitOfMeasureID) {
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
async function GetDashboardMetricListForTable(DashboardDTO) {
    await dxLoadPanel.show();
    if (DashboardDTO != null && DashboardDTO != undefined) {
        document.getElementById("dashboardtitle").innerHTML = DashboardDTO[0].Name;
    }
    const _dashboardMetricList = await GetDashboardMetricWithUI({
        DashboardID: document.getElementById('hiddenDashboardID').value,
        GetDashboardLineList: true
    });
    await FilterMetricListByCategory(_dashboardMetricList);
    //GetDashboardRevision();
    await dxLoadPanel.hide();
}
async function GetDashboardLineInformation_Global(DashboardLineID) {
    await dxLoadPanel.show();
    const _dashboardLineDTO = await GetDashboardLineInformation({
        ID: DashboardLineID,
        GetMetricDTO: true
    })
    PopulateMetricInformationByDashboardAndMonth(_dashboardLineDTO[0]);
    dxLoadPanel.hide();
}
async function GetDashboardMetricTendence_Global(DashboardMetricDTO) {
    await dxLoadPanel.show();
    const _dashboardMetricTendence = await GetDashboardMetricTendence({
        DashboardID: document.getElementById('hiddenDashboardID').value,
        DashboardCategoryID: DashboardMetricDTO.DashboardCategoryID,
        MetricID: DashboardMetricDTO.MetricDTO.ID,

    });
    console.log(_dashboardMetricTendence);
    $("#dxMetricTendenceChart").dxChart('option', 'dataSource', _dashboardMetricTendence);
    $("#dxMetricTendenceChart").dxChart('instance').render();
    SetSubtitles(DashboardMetricDTO.MetricDTO)
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
        $('#DataEntryMetricInfoModal').modal('hide');
    }
    HostResponse(_validation_resultDTO);
    $('#DashboardMetricTendencyModal').on('shown.bs.modal', function () {
        $("#dxMetricTendenceChart").dxChart("instance").render();
    });
    GetDashboardMetricListForTable();
    dxLoadPanel.hide();
}
//#endregion

//#endregion

//#region Assign KPI

async function InitializeTemplateAdministrationControls() {
    $("#dxDashboardMetric_MetricDataGrid").dxDataGrid({
        dataSource: await GetDXMetricDataSource({ IsActive: true }),
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
                caption: 'Metric',
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
    $("#dxQualityMetrics").dxDataGrid({
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
                    UpdateDashboardMetricOrder_Global(_data);
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
                            $("#hiddenDashboardMetricID").val(options.data.ID);
                            ShowDashboardMetricDeleteQuestion(options.data);
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
                dataField: 'MetricDTO.Name',
                caption: 'KPI',
            },
            {
                dataField: 'MetricDTO.Description',
                caption: 'Description',
            },
            {
                dataField: 'MetricDTO.ValueTypeName',
                caption: 'Value Type',
            },
            {
                dataField: 'MetricDTO.OwnerName',
                caption: 'KPI Owner',
            },
            {
                dataField: 'MetricDTO.OwnerDepartmentName',
                caption: 'Department',
            },
            {
                dataField: 'MetricDTO.ResponsibleName',
                caption: 'Responsible',
            },

            {
                dataField: 'MetricDTO.ResponsibleDepartmentName',
                caption: 'Responsible Department',
            },
            {
                dataField: 'MetricDTO.UnitOfMeasureName',
                caption: 'Unit Of Measure',
            },

            {
                dataField: 'MetricDTO.Goal',
                caption: 'Goal',
            },
            {
                dataField: 'MetricDTO.GoalRangeValue',
                caption: 'Goal Range',
            },
            {
                dataField: 'MetricDTO.FacilityName',
                caption: 'Facility',
            },
            {
                dataField: 'MetricDTO.EquivalenceName',
                caption: 'Equivalence',
            },
            {
                dataField: 'MetricDTO.LastUpdateByName',
                caption: 'Last Update By',
            },
            {
                dataField: 'MetricDTO.LastUpdate',
                caption: 'Last Update Date',
            }

        ]
    });

}
async function GetDashboardIDByURL() {
    let _dashboardID = GetURLParameter("DashboardID");
    let DashboardDTO = await GetDashboardInformation({ ID: _dashboardID })
    if (_dashboardID != null && _dashboardID != undefined && _dashboardID != 0 && !Number.isNaN(_dashboardID)) {
        await GetDashboardMetricListForGrid(DashboardDTO);
    }
    dxLoadPanel.hide();
}
function AssignDashboardMetricOrder(DashboardMetricDTO) {
    document.getElementById("hiddenDashboardMetricID").value = DashboardMetricDTO.ID;
    document.getElementById("hiddenDashboardID").value = DashboardMetricDTO.DashboardID;
    document.getElementById("hiddenMetricID").value = DashboardMetricDTO.MetricID;
    document.getElementById("hiddenDashboardCategoryID").value = DashboardMetricDTO.DashboardCategoryID;

}
function GetDashboardMetricDTO(DashboardDTO) {
    let _dashboardMetricDTO = {
        DashboardID: GetURLParameter("DashboardID"),
        MetricIDArray: ($("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").getSelectedRowsData()).map(m => m.ID),
        DashboardCategoryIDArray: ($("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").getSelectedRowsData()).map(m => m.DashboardCategoryID),
        GetMetricDTO: true,
        GetDashboardDTO: true,
        GetDashboardCategoryDTO: true,
        IsActive: true,
    }
    return _dashboardMetricDTO;
}
function ClearDashboardMetricFields() {
    let keys = $("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").getSelectedRowKeys();
    $("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").deselectRows(keys);
    $("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").option("focusedRowIndex", -1);
    $("#dxDashboardMetric_MetricDataGrid").dxDataGrid("instance").refresh();
}
async function ShowDashboardMetricDeleteQuestion(DashboardMetricDTO) {
    const _alert = await Swal.fire({
        text: 'You will remove this metric from current dashboard, are you sure?',
        title: 'Warning',
        confirmButtonText: `Delete`,
        icon: 'warning',
        showCancelButton: true
    });
    if (_alert.isConfirmed) {
        const _dashboardMetricDTO = DashboardMetricDTO;
        DeleteDashboardMetric_Global(_dashboardMetricDTO);
    }
}


//#region CRUD Functions
async function CreateDashboardMetric_Global() {
    await dxLoadPanel.show();
    const _dashboardMetricDTO = GetDashboardMetricDTO();
    const _validation_ResultDTO = await CreateDashboardMetricFromMetricList(_dashboardMetricDTO);
    //Poner aqui funcion que va a recargar las metricas mostradas en pantalla
    if (_validation_ResultDTO.Result) {
        $('#AddMetricsModal').modal('hide');
    }
    ClearDashboardMetricFields();
    //GetDashboardMetricList();
    HostResponse(_validation_ResultDTO);
    $("#dxQualityMetrics").dxDataGrid("instance").refresh();
    dxLoadPanel.hide();
}
async function DeleteDashboardMetric_Global(DashboardMetricDTO) {
    await dxLoadPanel.show();
    const _dashboardMetricDTO = DashboardMetricDTO;
    const _validation_ResultDTO = await DeleteDashboardMetric(_dashboardMetricDTO);
    if (_validation_ResultDTO.Result) {
        document.getElementById("hiddenDashboardMetricID").value = 0;
        //await GetDashboardMetricList();
    }
    HostResponse(_validation_ResultDTO);
    $("#dxQualityMetrics").dxDataGrid("instance").refresh();
    dxLoadPanel.hide();
}
//#endregion

//#region Business Logic functions
async function GetDashboardMetricListForGrid(DashboardDTO) {
    await dxLoadPanel.show();
    document.getElementById("dashboardtitle").innerHTML = DashboardDTO[0].Name;
    const _dashboardMetricDTO = GetDashboardMetricDTO(DashboardDTO);
    $("#dxQualityMetrics").dxDataGrid("instance").option("dataSource", await GetDXDashboardMetricDataSource(_dashboardMetricDTO));
    document.getElementById('hiddenDashboardID').value = DashboardDTO.DashboardID;
    document.getElementById('AddMetricBtn').classList.remove('disabled');
    dxLoadPanel.hide();
}
//#endregion


// #region Change Order Functions
function GetDashboardMetricOrderDTO(Data) {
    let _dashboardMetricDTO = {
        ID: Data.ID,
        DashboardID: Data.DashboardID,
        MetricID: Data.MetricID,
        DashboardCategoryID: Data.DashboardCategoryID,
        Order: Data.Order
    }
    return _dashboardMetricDTO;
}
async function UpdateDashboardMetricOrder_Global(Data) {
    await dxLoadPanel.show();
    const _dashboardDTO = GetDashboardMetricOrderDTO(Data);
    const _validation_ResultDTO = await UpdateDashboardMetricOrder(_dashboardDTO);
    HostResponse(_validation_ResultDTO);
    if (_validation_ResultDTO.Result) {
        $("#dxQualityMetrics").dxDataGrid("instance").refresh();
    }
    dxLoadPanel.hide();
}

//#endregion
//#endregion