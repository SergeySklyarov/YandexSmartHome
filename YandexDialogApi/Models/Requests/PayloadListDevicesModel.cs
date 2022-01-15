using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YandexDialogApi.Models.Requests
{
    public class DeviceModel
    {
        public string id { get; set; }
        public List<object> capabilities { get; set; }
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
