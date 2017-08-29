using SpectroCoin.NET.SendStrategy.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectroCoin.NET.SendStrategy.Factory
{
    public enum Currency { BTC, DASH, EUR, USD, ETH, XAU, BNK }
    public sealed class SendStrategies
    {
        private static readonly SendStrategies instance = new SendStrategies();
        private Dictionary<Currency, ISendStrategy> strategies = new Dictionary<Currency, ISendStrategy>();
        
        private SendStrategies()
        {
            strategies.Add(Currency.BTC, new BitcoinStrategy());
        }

        public static SendStrategies Instance
        {
            get => instance;
        }
        
        public ISendStrategy this[Currency currency]
        {
            get => strategies[currency];
        }

    }
}
