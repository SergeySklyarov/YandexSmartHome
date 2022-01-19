using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.Json;
using YandexDialogApi.Models.Requests;
using YandexDialogApi.Services;

namespace YandexDialogApi.Controllers
{
    [ApiController]
    [Route("/v1.0/user")]
    public class DevicesController : Controller
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IGpioService _gpioService;

        public DevicesController(ILogger<DevicesController> logger, IGpioService gpioService)
        {
            _logger = logger;
            _gpioService = gpioService;
        }

        [HttpHead("/v1.0")]
        public IActionResult Ping()
        {
            return Ok();
        }

        [HttpGet("devices")]
        public IActionResult GetDevicesList([FromHeader(Name = "X-Request-Id")] string x_request_id, [FromHeader(Name = "Authorization")] string authorization)
        {
            var res = new
            {
                request_id = x_request_id,
                payload = new
                {
                    user_id = "289333111",
                    devices = new[] { new {
                            id = "c5dabb15",
                            name = "свет в спальне",
                            description = "Первый свет в спальне",
                            room="Спальня",
                            type="devices.types.light",
                            custom_data=new {foo=1},
                            capabilities=new[]
                            {
                                new
                                {
                                    type= "devices.capabilities.on_off",
                                    retrievable=true,
                                    state=new{instance="on", value=false }
                                }
                            },
                            properties=Array.Empty<object>()
                        }
                    }
                }
            };

            return Json(res);
        }

        [HttpPost("devices/action")]
        public IActionResult YandexAction(
            [FromHeader(Name = "X-Request-Id")] string x_request_id,
            [FromHeader(Name = "Authorization")] string authorization,
            [FromBody] PayloadListDevicesModel device_list)
        {
            _logger.LogInformation("YandexAction request body: {dto}", JsonSerializer.Serialize(device_list));

            foreach(var device in device_list.payload.devices)
            {
                foreach(var cap in device.capabilities)
                {
                    if (cap.type == "devices.capabilities.on_off")
                    {
                        var jState = (JsonElement)cap.state.value;
                        var newValue = jState.ValueKind == JsonValueKind.True;

                        _gpioService.SetPinValue(6, newValue);
                    }
                }
            }

            var res = new
            {
                request_id = x_request_id,
                payload = new
                {
                    devices = new[] { new {
                            id = "c5dabb15",
                            capabilities=new[]
                            {
                                new
                                {
                                    type = "devices.capabilities.on_off",
                                    state = new { instance = "on", action_result = new { status = "DONE"} }
                                }
                            }
                        }
                    }
                }
            };

            return Json(res);
        }

        [HttpPost("devices/query")]
        public IActionResult YandexQuery(
            [FromHeader(Name = "X-Request-Id")] string x_request_id,
            [FromHeader(Name = "Authorization")] string authorization,
            [FromBody] ListDevicesModel dev_list)
        {
            _logger.LogInformation("YandexQuery request body: {dto}", JsonSerializer.Serialize(dev_list));

            var ledValue = _gpioService.GetPinValue(6);

            var res = new
            {
                request_id = x_request_id,
                payload = new
                {
                    devices = new[] { new {
                            id = "c5dabb15",
                            capabilities=new[]
                            {
                                new
                                {
                                    type= "devices.capabilities.on_off",
                                    state=new { instance = "on", value=ledValue }
                                }
                            }
                        }
                    }
                }
            };

            return Json(res);
        }

    }
}
