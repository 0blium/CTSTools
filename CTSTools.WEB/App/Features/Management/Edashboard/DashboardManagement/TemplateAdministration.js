import { HostResponse, ClearErrorFeedback } from '../../../../Common/Utils/Response.js'
import { dxLoadPanel } from '../../../../Common/Components/dxLoadPanel.js'
import { GetURLParameter } from '../../../../Common/Utils/GetURLParameter.js'
import { GetDXDashboardMetricDataSource, CreateDashboardMetric, UpdateDashboardMetricOrder, DeleteDashboardMetric, GetDashboardMetricInformation, CreateDashboardMetricFromMetricList } from './DashboardMetric/DashboardMetric_Service.js';

import { GetDXDashboardDataSource, GetDashboardInformation } from './Dashboard/Dashboard_Service.js'
import { GetDXDashboardCategoryDataSource } from '../Settings/DashboardCategory/DashboardCategory_Service.js'
import { Dashboard_Category_Enum } from '../Settings/DashboardCategory/Dashboard_Category_Enum.js'
import { GetDXMetricDataSource } from '../KPIManagement/Metric/Metric_Service.js'


document.addEventListener("DOMContentLoaded", async function () {
    
    await GetDashboardIDByURL();
});

