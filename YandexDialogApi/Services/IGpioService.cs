using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YandexDialogApi.Services
{
    public interface IGpioService
    {
        bool GetPinValue(int pin);
    }
}
