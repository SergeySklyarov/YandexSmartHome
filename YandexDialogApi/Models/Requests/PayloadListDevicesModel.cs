using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace YandexDialogApi.Models.Requests
{
    public class CapabilityStateModel
    {
        public string instance { get; set; }
        public object value { get; set; }
    }
    public class CapabilityModel
    {
        public string type { get; set; }
        public CapabilityStateModel state { get; set; }
    }

    public class DeviceModel
    {
        public string id { get; set; }
        public List<CapabilityModel> capabilities { get; set; }
        public object custom_data { get; set; }

    }

    public class ListDevicesModel
    {
        public List<DeviceModel> devices { get; set; }
    }

    public class PayloadListDevicesModel
    {
        public ListDevicesModel payload { get; set; }
    }
}
