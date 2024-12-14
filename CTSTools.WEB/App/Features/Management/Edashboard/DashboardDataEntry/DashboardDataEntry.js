import { GetDXDashboardDataSource } from '../../eDashboard/Settings/Dashboard/Dashboard_Service.js';
import { AddMonthlyValue,GetDashboardLineInformation,GetDashboardMetricTendence } from './DashboardLine/DashboardLine_Service.js';
import { GetDashboardMetricWithUI } from '../TemplateAdministration/DashboardMetric/DashboardMetric_Service.js';
import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { GetURLParameter } from '../../../../Common/Utils/GetURLParameter.js'
import { Dashboard_Category_Enum } from '../Settings/DashboardCategory/Dashboard_Category_Enum.js';
import { Month_Enum } from '../../../../Common/Utils/Month_Enum.js';
import { ValueType_Enum } from '../Settings/KPISettings/ValueType/ValueType_Enum.js';
import { UnitOfMeasure_Enum } from '../Settings/KPISettings/UnitOfMeasure/UnitOfMeasure_Enum.js'

document.addEventListener("DOMContentLoaded", async function () {
    await InitializeDashboardDataEntryControls();
    document.getElementById('SaveDashboardLine').addEventListener('click', UpdateDashboardLine);
    await GetDashboardIDByURL();
});
async function GetDashboardIDByURL() {
    let _dashboardID = GetURLParameter("DashboardID");
    if (_dashboardID != null && _dashboardID != undefined && _dashboardID != 0 && !Number.isNaN(_dashboardID)) {
        await $("#dxDashboardMetric_DashboardSelectBox").dxSelectBox("instance").option("value", Number(_dashboardID));
    } else {
        $("#dxDashboardMetric_DashboardSelectBox").dxSelectBox("instance").option("readOnly", false)
    }
    dxLoadPanel.hide();
}
async function InitializeDashboardDataEntryControls() {
    $("#dxDashboardMetric_DashboardSelectBox").dxSelectBox({
        dataSource: await GetDXDashboardDataSource({IsActive:true}),
        valueExpr: "ID",
        displayExpr: "Name",
        searchEnabled: true,
        readOnly:true,
        placeholder: "Select Dashboard",
        onSelectionChanged: function (e) {
            if ($("#dxDashboardMetric_DashboardSelectBox").dxSelectBox("instance").option("value") != null) {
                GetDashboardMetricList();
            } else {
                toastr["error"]("Please, select a dashboard to get the information", "Dashboard Not selected");
            }
        }
    });
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
function FilterMetricListByCategory(DashboardMetricList) {
    /*debugger*/
    //Quality
    let _sortMetricQuality = DashboardMetricList.filter(function (x) {
        return x.DashboardCategoryDTO.ID == Dashboard_Category_Enum.Quality
    });
    BuildTQCFormat(_sortMetricQuality, "Quality", "Q");
    //Cost
    let _sortMetricCost = DashboardMetricList.filter(function (x) {
        return x.DashboardCategoryDTO.ID == Dashboard_Category_Enum.Cost
    });
    BuildTQCFormat(_sortMetricCost, "Cost", "C");
    //Delivery
    let _sortMetricDelivery = DashboardMetricList.filter(function (x) {
        return x.DashboardCategoryDTO.ID == Dashboard_Category_Enum.Delivery
    });
    BuildTQCFormat(_sortMetricDelivery, "Delivery", "D");
    //Safety
    let _sortMetricSafety = DashboardMetricList.filter(function (x) {
        return x.DashboardCategoryDTO.ID == Dashboard_Category_Enum.Safety
    });
    BuildTQCFormat(_sortMetricSafety, "Safety", "S");
    //Moral
    let _sortMetricMoral = DashboardMetricList.filter(function (x) {
        return x.DashboardCategoryDTO.ID == Dashboard_Category_Enum.Moral
    });
    BuildTQCFormat(_sortMetricMoral, "Moral", "M");
    //Improvement
    let _sortMetricImprovement = DashboardMetricList.filter(function (x) {
        return x.DashboardCategoryDTO.ID == Dashboard_Category_Enum.Improvement
    });
    BuildTQCFormat(_sortMetricImprovement, "Continuos_Improvement", "I");
    document.getElementById('NoDashboardMessage').classList.add('d-none');
    document.getElementById('DashboardMetricList').classList.remove('d-none');
    //document.getElementById('PrintDashboardMetricData').classList.remove('d-none');

}
function BuildTQCFormat(DashboardMetricList, Category, Letter) {
    let _TQCFormatHTML = "";
    let _panelBodyCategory = document.getElementById(`${Category}Panel`);
    _panelBodyCategory.innerHTML = "";
    Category = Category.replace("_", " ")
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
            "<th>Jan</th>" +
            "<th>Feb</th>" +
            "<th>Mar</th>" +
            "<th>Apr</th>" +
            "<th>May</th>" +
            "<th>Jun</th>" +
            "<th>Jul</th>" +
            "<th>Aug</th>" +
            "<th>Sep</th>" +
            "<th>Oct</th>" +
            "<th>Nov</th>" +
            "<th>Dec</th>" +
            "</tr>" +
            "</thead>";

        //Start build body of the format
        let _metricAmount = DashboardMetricList.length + 1;
        _TQCFormatHTML +=
            "<tbody>" +
            "<tr>" +
            "<td style=\"width: 95px;\" rowspan=\"" + _metricAmount + "\">" +
            "<h5><strong>" + Letter + "</strong></h5><strong><p><strong>" + Category + "</strong></p>" +
            "</td>" +
            "</tr>";

        DashboardMetricList.forEach(function (DashboardMetricDTO) {
            let _fyGoalSymbol = (DashboardMetricDTO.MetricDTO.ValueTypeDTO.ID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(DashboardMetricDTO.MetricDTO);
            let _fyGoalFormat = DashboardMetricDTO.MetricDTO.UnitOfMeasureDTO.ID == UnitOfMeasure_Enum.USD ?
                ConvertToMoney(DashboardMetricDTO.MetricDTO.Goal) : DashboardMetricDTO.MetricDTO.Goal;
            _TQCFormatHTML +=
                `<tr><td style="width: 80px;"><a class="btn-modal-tendency" ` +
                `data-dashboardcategoryid=${DashboardMetricDTO.DashboardCategoryDTO.ID} data-valuetypeid=${DashboardMetricDTO.MetricDTO.ValueTypeDTO.ID} ` +
                `data-equivalenceicon=${DashboardMetricDTO.MetricDTO.EquivalenceIcon} data-metricid=${DashboardMetricDTO.MetricDTO.ID} ` +
                `data-unitofmeasureid=${DashboardMetricDTO.MetricDTO.UnitOfMeasureID} ` +
                `data-metricgoal=${DashboardMetricDTO.MetricDTO.Goal} data-metricname='${DashboardMetricDTO.MetricDTO.Name}' data-bs-toggle="modal" ` +
                `data-bs-target="#DashboardMetricTendencyModal" id=\"DashboardMetricTendencyBtn${DashboardMetricDTO.ID}\")\"" >` +
                `</i><i class=\"fas fa-chart-line me-2 fa-2x\"></i></a></td>` +
                `<td style="width: 95px;">${DashboardMetricDTO.MetricDTO.OwnerDTO.Name}</td>` +
                `<td style="width: 400px;" class=\"bg-yellow\">${DashboardMetricDTO.MetricDTO.Name}</td>` +
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
                    _value = DashboardMetricDTO.MetricDTO.UnitOfMeasureDTO.ID == UnitOfMeasure_Enum.USD ?
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
            _TQCFormatHTML += '</tr>'
        });

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
            UnitOfMeasureDTO: { ID: parseInt(this.dataset.unitofmeasureid) },
            EquivalenceIcon: this.dataset.equivalenceicon,
            ValueTypeDTO: { ID: this.dataset.valuetypeid }
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
    let _fyGoalSymbol = (MetricDTO.ValueTypeDTO.ID == ValueType_Enum.Percent) ? "%" : UnitOfMeasureFormat(MetricDTO);
    let _fyGoalFormat = MetricDTO.UnitOfMeasureDTO.ID == UnitOfMeasure_Enum.USD ?
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
    switch (MetricDTO.UnitOfMeasureDTO.ID) {
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
//#endregion
//#region Call service functions
async function GetDashboardMetricList() {
    await dxLoadPanel.show();
    const _dashboardMetricList = await GetDashboardMetricWithUI({
        DashboardDTO: { ID: $("#dxDashboardMetric_DashboardSelectBox").dxSelectBox("instance").option("value") },
        GetDashboardLineList:true
    });
    FilterMetricListByCategory(_dashboardMetricList);
    //GetDashboardRevision();
    dxLoadPanel.hide();
}
async function GetDashboardLineInformation_Global(DashboardLineID) {
    await dxLoadPanel.show();
    const _dashboardLineDTO = await GetDashboardLineInformation({
        ID: DashboardLineID,GetMetricDTO:true
    })
    PopulateMetricInformationByDashboardAndMonth(_dashboardLineDTO[0]);
    dxLoadPanel.hide();
}
async function GetDashboardMetricTendence_Global(DashboardMetricDTO) {
    await dxLoadPanel.show();
    const _dashboardMetricTendence = await GetDashboardMetricTendence({
        DashboardDTO: { ID: $("#dxDashboardMetric_DashboardSelectBox").dxSelectBox("instance").option("value") },
        DashboardCategoryDTO: { ID: DashboardMetricDTO.DashboardCategoryID },
        MetricDTO: { ID: DashboardMetricDTO.MetricDTO.ID },

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
    GetDashboardMetricList();
    dxLoadPanel.hide();
}
//#endregion