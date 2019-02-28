using System;
using System.Collections.Generic;
using System.Text;

namespace Roulette1
{
    public enum CommandType
    {
        None,
        Login,
    }

    public enum ApiResult
    {
        None,
        Success,
        InvalidBetting,
        NotEnoughMoney,
    }
}
