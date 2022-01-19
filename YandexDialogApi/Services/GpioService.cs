using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Linq;
using System.Threading.Tasks;

namespace YandexDialogApi.Services
{
    public class GpioService : IGpioService
    {
        private const int LED_PIN = 6;
        private GpioController _controller;

        private readonly ILogger<GpioService> _logger;

        public GpioService(ILogger<GpioService> logger)
        {
            _logger = logger;

            try
            {
                _controller = new GpioController(PinNumberingScheme.Logical, new LibGpiodDriver());

                if (!_controller.IsPinOpen(LED_PIN))
                {
                    _controller.OpenPin(LED_PIN, PinMode.Output);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
        }

        public void SetPinValue(int pin, bool value)
        {
            try
            {
                _controller.Write(pin, ((value) ? PinValue.High : PinValue.Low));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public bool GetPinValue(int pin)
        {
            try
            {
                var pinValue = _controller.Read(LED_PIN);
                return ((bool)pinValue);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }
    }
}
