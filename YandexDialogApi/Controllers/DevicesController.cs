using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using YandexDialogApi.Models.Requests;

namespace YandexDialogApi.Controllers
{
    [ApiController]
    [Route("/v1.0/user")]
    public class DevicesController : Controller
    {
        private readonly ILogger<DevicesController> _logger;

        public DevicesController(ILogger<DevicesController> logger)
        {
            _logger = logger;
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
                                    state=new { instance = "on", action_result=new{ status = "DONE"} }
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
                                    state=new { instance = "on", value=true }
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
