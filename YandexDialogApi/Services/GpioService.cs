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

        public GpioService()
        {
            _controller = new GpioController(PinNumberingScheme.Logical, new LibGpiodDriver());

            if (!_controller.IsPinOpen(LED_PIN))
            {
                _controller.OpenPin(LED_PIN, PinMode.Output);
            }
        }

        public bool GetPinValue(int pin)
        {
            var pinValue = _controller.Read(LED_PIN);
            return ((bool)pinValue);
        }
    }
}
