using SpectroCoin.NET.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpectroCoin.NET.SendStrategy.Interface
{
    public interface ISendStrategy
    {
        Task<Payment> Send(string receiver, double amount, string accessToken);
    }
}
