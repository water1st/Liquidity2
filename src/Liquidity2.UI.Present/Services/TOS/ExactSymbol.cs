using System;

namespace Liquidity2.UI.Services.TOS
{
    public class ExactSymbol : IEquatable<ExactSymbol>
    {
        public string Symbol { get; set; }
        public int Precision { get; set; }

        public static bool operator ==(ExactSymbol x, ExactSymbol y) => Equals(x, y);

        public static bool operator !=(ExactSymbol x, ExactSymbol y) => !Equals(x, y);

        public override bool Equals(object obj)
        {
            if (obj != null && obj is ExactSymbol @class)
            {
                return Equals(@class);
            }
            return false;
        }
        public bool Equals(ExactSymbol other)
        {
            if (Symbol == other.Symbol && Precision == other.Precision)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
