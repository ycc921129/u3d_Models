/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/****************************************
 * 
 * Author     : Sambit Samal
 * Class Name : BigDecimal
 * Created On : 2018-03-09
 * 
*****************************************/

using System;
using System.Text;

namespace FutureCore.Data
{
    public class BigDecimal
    {
        private string value { get; set; }
        static int _scale = 100;
        const string ONE = "1";
        const string ZERO = "0";
        const string TEN = "10";

        #region Constructors
        public BigDecimal()
        {
        }
        public BigDecimal(BigDecimal num)
        {
            value = num.value;
        }
        public BigDecimal(char num)
        {
            value = num.ToString();
        }
        public BigDecimal(string num)
        {
            value = num;
        }
        public BigDecimal(int num)
        {
            value = num.ToString();
        }
        public BigDecimal(long num)
        {
            value = num.ToString();
        }
        public BigDecimal(ulong num)
        {
            value = num.ToString();
        }
        public BigDecimal(uint num)
        {
            value = num.ToString();
        }
        public BigDecimal(float num)
        {
            value = num.ToString();
        }
        public BigDecimal(double num)
        {
            value = num.ToString();
        }
        public BigDecimal(BigInteger num)
        {
            value = num.ToString();
        }
        #endregion

        #region Global Helper Functions
        static string getLeftOfDot(string value)
        {
            int dot = value.IndexOf('.');
            if(dot != -1)
            {
                if(dot == 0)
                    return "0";
                if(dot == 1 && value[0] == '-')
                    return "-0";
                return value.Substring(0, dot);
            }
            else
            {
                return value;
            }
        }

        private static StringBuilder createBigDecimalString(char character, int string_size)
        {
            StringBuilder result = new StringBuilder("");
            for (int rl = 0; rl < string_size; rl++)
            {
                result = result.Append(character);
            }
            return result;
        }

        private static string trimTrailingZeros(string input)
        {
            if (input.Contains("."))
            {
                //Remove all trailing zeros
                input = input.TrimEnd('0');
                //If all we are left with is a decimal point
                if (input.EndsWith(".")) //then remove it
                    input = input.TrimEnd('.');
            }
            return input;
        }

        private static string _round(string left, int lint, int ldot, int lfrac, int lscale, int scale, int sign, bool add_trailing_zeroes, bool round_last = false)
        {
            StringBuilder sbleft = new StringBuilder(left);
            while (sbleft[lint] == '0' && lint + 1 < ldot)
            {
                lint++;
            }
            if (!(lint > 0 && lscale >= 0 && scale >= 0))
            {
                System.Environment.Exit(-1);
            }

            if (sign < 0 && sbleft[lint] == '0')
            {
                sign = 1;
                for (int i = 0; i < lscale; i++)
                {
                    if (sbleft[lfrac + i] != '0')
                    {
                        sign = -1;
                        break;
                    }
                }
            }

            if (round_last)
            {
                if (lscale > scale)
                {
                    while (scale > 0 && sbleft[lfrac + scale - 1] == '9' && sbleft[lfrac + scale] >= '5')
                    {
                        scale--;
                    }
                    lscale = scale;
                    if (sbleft[lfrac + scale] >= '5')
                    {
                        if (scale > 0)
                        {
                            sbleft[lfrac + scale - 1]++;
                        }
                        else
                        {
                            lfrac--;
                            if (!(lfrac == ldot))
                            {
                                System.Environment.Exit(-1);
                            }
                            int i;
                            sbleft[lint - 1] = '0';
                            for (i = 0; sbleft[ldot - i - 1] == '9'; i++)
                            {
                                sbleft[ldot - i - 1] = '0';
                            }
                            sbleft[ldot - i - 1]++;
                            if (ldot - i - 1 < lint)
                            {
                                lint = ldot - i - 1;
                            }
                        }
                    }
                }

                while (lscale > 0 && sbleft[lfrac + lscale - 1] == '0')
                {
                    lscale--;
                }
            }
            else
            {
                if (lscale > scale)
                {
                    lscale = scale;
                }
            }

            if (lscale == 0 && lfrac > ldot)
            {
                lfrac--;
                if (!(lfrac == ldot))
                {
                    System.Environment.Exit(-1);
                }
            }

            if (sign < 0)
            {
                sbleft[--lint] = '-';
            }

            if (lscale == scale || !add_trailing_zeroes)
            {
                return left.Substring(lint).Substring(0, lfrac + lscale - lint).ToString();
            }
            else
            {
                StringBuilder sbresult = new StringBuilder(left.Substring(lint).Substring(0, lfrac + lscale - lint));
                if (lscale == 0)
                {
                    sbresult = sbresult.Append('.');
                }
                for (int kI = 0; kI < scale - lscale; ++kI)
                    sbresult = sbresult.Append('0');
                return sbresult.ToString();
            }
        }

        private static string _zero(int scale)
        {
            if (scale == 0)
            {
                return ZERO;
            }
            string result = new string('0', scale + 2);
            result = result.Insert(1, ".");
            return result;
        }

        static int parse_number(ref string s, ref int lsign, ref int lint, ref int ldot, ref int lfrac, ref int lscale)
        {
            int i = 0;
            lsign = 1;
            if (s[i] == '-' || s[i] == '+')
            {
                if (s[i] == '-')
                {
                    lsign = -1;
                }
                i++;
            }
            int len = s.Length;
            if (i >= len)
            {
                return -1;
            }
            lint = i;
            while (i < len && '0' <= s[i] && s[i] <= '9')
            {
                i++;
            }
            ldot = i;
            lscale = 0;
            if (i < len && s[i] == '.')
            {
                lscale = (int)s.Length - i - 1;
                i++;
            }
            lfrac = i;
            while (i < len && '0' <= s[i] && s[i] <= '9')
            {
                i++;
            }
            if (i < len)
            {
                return -1;
            }

            while (s[lint] == '0' && lint + 1 < ldot)
            {
                lint++;
            }
            //  while (lscale > 0 && s[lfrac + lscale - 1] == '0') {
            //    lscale--;
            //  }
            if (lscale == 0 && lfrac > ldot)
            {
                lfrac--;
                if (!(lfrac == ldot))
                {
                    System.Environment.Exit(-1);
                }
            }
            if (lsign < 0 && (lscale == 0 && s[lint] == '0'))
            {
                lsign = 1;
            }
            return lscale;
        }
        #endregion

        #region Conversion Function
        public override string ToString()
        {
            return value;
        }

        public int toInt()
        {
            return Convert.ToInt32(value);
        }

        public uint toUInt()
        {
            return Convert.ToUInt32(value);
        }

        public long toLong()
        {
            return Convert.ToInt64(value);
        }

        public ulong toULong()
        {
            return Convert.ToUInt64(value);
        }

        public double toDouble()
        {
            return Convert.ToDouble(value);
        }

        public float toFloat()
        {
            return Convert.ToSingle(value);
        }
        #endregion

        #region Binary Operator
        public static BigDecimal operator +(BigDecimal left, BigDecimal right)
        {
            BigDecimal bigDecimal = new BigDecimal(Add(left.ToString(), right.ToString()));
            return CheckBigDecimalPM(bigDecimal);
        }

        public static BigDecimal operator -(BigDecimal left, BigDecimal right)
        {
            BigDecimal bigDecimal = new BigDecimal(Subtract(left.ToString(), right.ToString()));
            return CheckBigDecimalPM(bigDecimal);
        }

        public static BigDecimal operator *(BigDecimal left, BigDecimal right)
        {
            BigDecimal bigDecimal = new BigDecimal(Multiply(left.ToString(), right.ToString()));
            return CheckBigDecimalPM(bigDecimal);
        }

        public static BigDecimal operator %(BigDecimal left, BigDecimal right)
        {
            BigDecimal bigDecimal = new BigDecimal(Modulus(left.ToString(), right.ToString()));
            return CheckBigDecimalPM(bigDecimal);
        }

        public static BigDecimal operator /(BigDecimal left, BigDecimal right)
        {
            BigDecimal bigDecimal = new BigDecimal(Divide(left.ToString(), right.ToString()));
            return CheckBigDecimalPM(bigDecimal);
        }

        public static BigDecimal operator ^(BigDecimal left, BigDecimal right)
        {
            BigDecimal bigDecimal = new BigDecimal(Pow(left.ToString(), right.ToString()));
            return CheckBigDecimalPM(bigDecimal);
        }

        private const char zeroStr = '0';
        private const char dotStr = '.';
        private const char loseStr = '-';
        private static BigDecimal CheckBigDecimalPM(BigDecimal bigDecimal)
        {
            string value = bigDecimal;
            if (value.Length > 1 && value[0] == zeroStr && value[1] != dotStr)
            {
                bigDecimal = loseStr + value.Remove(0, 1);
            }
            return bigDecimal;
        }

        #endregion

        #region Comparison Operator
        public static bool operator >(BigDecimal left, BigDecimal right)
        {
            return CompareTo(left.ToString(), right.ToString()) > 0;
        }

        public static bool operator >=(BigDecimal left, BigDecimal right)
        {
            return CompareTo(left.ToString(), right.ToString()) >= 0;
        }

        public bool Equals(BigDecimal other)
        {
            if (other == null)
                throw new ArgumentNullException();
            return (other.value == value);
        }

        public override bool Equals(object other)
        {
            if (other == null || GetType() != other.GetType())
                return false;
            return (((BigDecimal)other).value) == value;
        }

        public static bool operator ==(BigDecimal left, BigDecimal right)
        {
            return CompareTo(left.ToString(), right.ToString()) == 0;
        }

        public new static bool Equals(object left, object right)
        {
            if (left == null && right == null) return true;
            if (left == null || right == null) return false;
            return left.GetType() == right.GetType() && (((BigDecimal)left).Equals((BigDecimal)right));
        }

        public static bool operator !=(BigDecimal left, BigDecimal right)
        {
            return CompareTo(left.ToString(), right.ToString()) != 0;
        }
        public static bool operator <(BigDecimal left, BigDecimal right)
        {
            return CompareTo(left.ToString(), right.ToString()) < 0;
        }
        public static bool operator <=(BigDecimal left, BigDecimal right)
        {
            return CompareTo(left.ToString(), right.ToString()) <= 0;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        #endregion     
        
        #region Addition Helper Functions
        private static string Add(string left, string right, int scale = int.MinValue)
        {
            if (left == string.Empty)
            {
                return Add(ZERO, right, scale);
            }
            if (right == string.Empty)
            {
                return Add(left, ZERO, scale);
            }
            if (scale == int.MinValue)
            {
                scale = _scale;
            }
            if (scale < 0)
            {
                scale = 0;
            }
            int lsign = 0, lint = 0, ldot = 0, lfrac = 0, lscale = 0;
            if (parse_number(ref left, ref lsign, ref lint, ref ldot, ref lfrac, ref lscale) < 0)
            {
                return _zero(scale);
            }
            int rsign = 0, rint = 0, rdot = 0, rfrac = 0, rscale = 0;
            if (parse_number(ref right, ref rsign, ref rint, ref rdot, ref rfrac, ref rscale) < 0)
            {
                return _zero(scale);
            }
            return trimTrailingZeros(_add(left, lsign, lint, ldot, lfrac, lscale, right, rsign, rint, rdot, rfrac, rscale, Math.Max(lscale, rscale)));
        }

        private static string _add(string left, int lsign, int lint, int ldot, int lfrac, int lscale, string right, int rsign, int rint, int rdot, int rfrac, int rscale, int scale)
        {
            if (lsign > 0 && rsign > 0)
            {
                return add_positive(left, lint, ldot, lfrac, lscale, right, rint, rdot, rfrac, rscale, scale, 1);
            }

            if (lsign > 0 && rsign < 0)
            {
                if (_compareTo(left, lint, ldot, lfrac, lscale, right, rint, rdot, rfrac, rscale, 1000000000) >= 0)
                {
                    return subtract_positive(left, lint, ldot, lfrac, lscale, right, rint, rdot, rfrac, rscale, scale, 1);
                }
                else
                {
                    return subtract_positive(right, rint, rdot, rfrac, rscale, left, lint, ldot, lfrac, lscale, scale, -1);
                }
            }

            if (lsign < 0 && rsign > 0)
            {
                if (_compareTo(left, lint, ldot, lfrac, lscale, right, rint, rdot, rfrac, rscale, 1000000000) <= 0)
                {
                    return subtract_positive(right, rint, rdot, rfrac, rscale, left, lint, ldot, lfrac, lscale, scale, 1);
                }
                else
                {
                    return subtract_positive(left, lint, ldot, lfrac, lscale, right, rint, rdot, rfrac, rscale, scale, -1);
                }
            }

            if (lsign < 0 && rsign < 0)
            {
                return add_positive(left, lint, ldot, lfrac, lscale, right, rint, rdot, rfrac, rscale, scale, -1);
            }

            return ZERO; //Is dummy...

        }

        private static string add_positive(string left, int lint, int ldot, int lfrac, int lscale, string right, int rint, int rdot, int rfrac, int rscale, int scale, int sign)
        {
            int llen = ldot - lint;
            int rlen = rdot - rint;

            int resint = 0, resdot = 0, resfrac = 0, resscale = 0, cur_pos = 0, result_size = 0;

            int result_len = Math.Max(llen, rlen) + 1;
            int result_scale = Math.Max(lscale, rscale);
            result_size = result_len + result_scale + 3;
            StringBuilder result = createBigDecimalString('0', result_size);

            int i = 0, um = 0;
            cur_pos = result_size;
            bool was_frac = false;
            for (i = result_scale - 1; i >= 0; i--)
            {
                if (i < lscale)
                {
                    um += left[lfrac + i] - '0';
                }
                if (i < rscale)
                {
                    um += right[rfrac + i] - '0';
                }

                if (um != 0 || was_frac)
                {
                    result[--cur_pos] = (char)(um % 10 + '0');
                    um /= 10;
                    was_frac = true;
                }
            }
            resscale = result_size - cur_pos;
            resfrac = cur_pos;
            if (was_frac)
            {
                result[--cur_pos] = '.';
            }
            resdot = cur_pos;

            for (int j = 0; j < result_len; j++)
            {
                if (j < llen)
                {
                    um += left[ldot - j - 1] - '0';
                }
                if (j < rlen)
                {
                    um += right[rdot - j - 1] - '0';
                }

                result[--cur_pos] = (char)(um % 10 + '0');
                um /= 10;
            }
            resint = cur_pos;
            if (!(cur_pos > 1))
            {
                System.Environment.Exit(-1);
            }
            return _round(result.ToString(), resint, resdot, resfrac, resscale, scale, sign, true);
        }
        #endregion

        #region Subtraction Helper Functions
        private static string Subtract(string left, string right, int scale = int.MinValue)
        {
            if (left == string.Empty)
            {
                return Subtract(ZERO, right, scale);
            }
            if (right == string.Empty)
            {
                return Subtract(left, ZERO, scale);
            }
            if (scale == int.MinValue)
            {
                scale = _scale;
            }
            if (scale < 0)
            {
                scale = 0;
            }
            int lsign = 0, lint = 0, ldot = 0, lfrac = 0, lscale = 0;
            if (parse_number(ref left, ref lsign, ref lint, ref ldot, ref lfrac, ref lscale) < 0)
            {
                return _zero(scale);
            }
            int rsign = 0, rint = 0, rdot = 0, rfrac = 0, rscale = 0;
            if (parse_number(ref right, ref  rsign, ref  rint, ref  rdot, ref  rfrac, ref  rscale) < 0)
            {
                return _zero(scale);
            }
            rsign *= -1;
            return trimTrailingZeros(_add(left, lsign, lint, ldot, lfrac, lscale, right, rsign, rint, rdot, rfrac, rscale, Math.Max(lscale, rscale)));
        }
        private static string subtract_positive(string left, int lint, int ldot, int lfrac, int lscale, string right, int rint, int rdot, int rfrac, int rscale, int scale, int sign)
        {
            int llen = ldot - lint;
            int rlen = rdot - rint;
            int resint, resdot, resfrac, resscale;
            int result_len = llen;
            int result_scale = Math.Max(lscale, rscale);
            int result_size = result_len + result_scale + 3;
            StringBuilder result = createBigDecimalString('0', result_size);

            int i, um = 0, next_um = 0;
            int cur_pos = result_size;
            bool was_frac = false;
            for (i = result_scale - 1; i >= 0; i--)
            {
                um = next_um;
                if (i < lscale)
                {
                    um += left[lfrac + i] - '0';
                }
                if (i < rscale)
                {
                    um -= right[rfrac + i] - '0';
                }
                if (um < 0)
                {
                    next_um = -1;
                    um += 10;
                }
                else
                {
                    next_um = 0;
                }

                if (um != 0 || was_frac)
                {
                    result[--cur_pos] = (char)(um + '0');
                    was_frac = true;
                }
            }
            resscale = result_size - cur_pos;
            resfrac = cur_pos;
            if (was_frac)
            {
                result[--cur_pos] = '.';
            }
            resdot = cur_pos;

            for (int j = 0; j < result_len; j++)
            {
                um = next_um;
                um += left[ldot - j - 1] - '0';
                if (j < rlen)
                {
                    um -= right[rdot - j - 1] - '0';
                }
                if (um < 0)
                {
                    next_um = -1;
                    um += 10;
                }
                else
                {
                    next_um = 0;
                }
                result[--cur_pos] = (char)(um + '0');
            }
            resint = cur_pos;
            if (!(cur_pos > 0))
            {
                System.Environment.Exit(-1);
            }
            return _round(result.ToString(), resint, resdot, resfrac, resscale, scale, sign, true);
        }
        #endregion

        #region Multiplication Helper Functions
        private static string Multiply(string left, string right, int scale = int.MinValue)
        {
            if (left == string.Empty)
            {
                return Multiply(ZERO, right, scale);
            }
            if (right == string.Empty)
            {
                return Multiply(left, ZERO, scale);
            }
            if (scale == int.MinValue)
            {
                scale = _scale;
            }
            if (scale < 0)
            {
                scale = 0;
            }
            int lsign = 0, lint = 0, ldot = 0, lfrac = 0, lscale = 0;
            if (parse_number(ref left, ref lsign, ref lint, ref ldot, ref lfrac, ref lscale) < 0)
            {
                return _zero(scale);
            }
            int rsign = 0, rint = 0, rdot = 0, rfrac = 0, rscale = 0;
            if (parse_number(ref right, ref  rsign, ref  rint, ref  rdot, ref  rfrac, ref  rscale) < 0)
            {
                return _zero(scale);
            }
            return trimTrailingZeros(multiply_positive(left, lint, ldot, lfrac, lscale, right, rint, rdot, rfrac, rscale, lscale + rscale, lsign * rsign));
        }

        private static string multiply_positive(string left, int lint, int ldot, int lfrac, int lscale, string right, int rint, int rdot, int rfrac, int rscale, int scale, int sign)
        {
            int llen = ldot - lint;
            int rlen = rdot - rint;

            int resint, resdot, resfrac, resscale;

            int result_len = llen + rlen;
            int result_scale = lscale + rscale;
            int result_size = result_len + result_scale + 3;
            StringBuilder result = createBigDecimalString('0', result_size);

            int[] res = new int[sizeof(int) * result_size];
            for (int i = -lscale; i < llen; i++)
            {
                int x = (i < 0 ? left[lfrac - i - 1] : left[ldot - i - 1]) - '0';
                for (int j = -rscale; j < rlen; j++)
                {
                    int y = (j < 0 ? right[rfrac - j - 1] : right[rdot - j - 1]) - '0';
                    res[i + j + result_scale] += x * y;
                }
            }
            for (int i = 0; i + 1 < result_size; i++)
            {
                res[i + 1] += res[i] / 10;
                res[i] %= 10;
            }

            int cur_pos = result_size;
            for (int i = 0; i < result_scale; i++)
            {
                result[--cur_pos] = (char)(res[i] + '0');
            }
            resscale = result_size - cur_pos;
            resfrac = cur_pos;
            if (result_scale > 0)
            {
                result[--cur_pos] = '.';
            }
            resdot = cur_pos;

            for (int i = result_scale; i < result_len + result_scale; i++)
            {
                result[--cur_pos] = (char)(res[i] + '0');
            }
            resint = cur_pos;
            if (!(cur_pos > 0))
            {
                System.Environment.Exit(-1);
            }
            return _round(result.ToString(), resint, resdot, resfrac, resscale, scale, sign, false);
        }
        #endregion

        #region Division Helper Functions
        private static string Divide(string left, string right, int scale = int.MinValue)
        {
            if (left == string.Empty)
            {
                return _zero(scale);
            }
            if (right == string.Empty)
            {
                throw new DivideByZeroException("Divide By ZERO");
            }
            if (scale == int.MinValue)
            {
                scale = _scale;
            }
            if (scale < 0)
            {
                scale = 0;
            }
            int lsign = 0, lint = 0, ldot = 0, lfrac = 0, lscale = 0;
            if (parse_number(ref left, ref lsign, ref lint, ref ldot, ref lfrac, ref lscale) < 0)
            {
                return ZERO;
            }
            int rsign = 0, rint = 0, rdot = 0, rfrac = 0, rscale = 0;
            if (parse_number(ref right, ref  rsign, ref  rint, ref  rdot, ref  rfrac, ref  rscale) < 0)
            {
                return ZERO;
            }
            return trimTrailingZeros(divide_positive(left, lint, ldot, lfrac, lscale, right, rint, rdot, rfrac, rscale, scale, lsign * rsign));
        }

        private static string divide_positive(string left, int lint, int ldot, int lfrac, int lscale, string right, int rint, int rdot, int rfrac, int rscale, int scale, int sign)
        {
            int llen = ldot - lint;
            int rlen = rdot - rint;

            int resint, resdot = -1, resfrac = -1, resscale;

            int result_len = Math.Max(llen + rscale - rlen + 1, 1);
            int result_scale = scale;
            int result_size = result_len + result_scale + 3;

            if (rscale == 0 && right[rint] == '0')
            {
                throw new DivideByZeroException("Division By ZERO");
            }

            int dividend_len = llen + lscale;
            int divider_len = rlen + rscale;
            int[] dividend = new int[(sizeof(int) * (result_size + dividend_len + divider_len))];
            int[] divider = new int[(sizeof(int) * divider_len)];

            for (int i = -lscale; i < llen; i++)
            {
                int x = (i < 0 ? left[lfrac - i - 1] : left[ldot - i - 1]) - '0';
                dividend[llen - i - 1] = x;
            }

            for (int i = -rscale; i < rlen; i++)
            {
                int x = (i < 0 ? right[rfrac - i - 1] : right[rdot - i - 1]) - '0';
                divider[rlen - i - 1] = x;
            }

            int divider_skip = 0, k = 0;
            while (divider_len > 0 && divider[0] == 0)
            {
                divider[k]++;
                divider_skip++;
                divider_len--;
                k++;
            }
            if (!(divider_len > 0))
            {
                System.Environment.Exit(-1);
            }

            int cur_pow = llen - rlen + divider_skip;
            int cur_pos = 2, l = 0;

            if (cur_pow < -scale)
            {
                divider[l] -= divider_skip;
                divider_len += divider_skip;
                l++;
                return _zero(scale);
            }

            StringBuilder result = createBigDecimalString('0', result_size);
            resint = cur_pos;
            if (cur_pow < 0)
            {
                result[cur_pos++] = '0';
                resdot = cur_pos;
                result[cur_pos++] = '.';
                resfrac = cur_pos;
                for (int i = -1; i > cur_pow; i--)
                {
                    result[cur_pos++] = '0';
                }
            }

            int beg = 0, real_beg = 0;
            while (cur_pow >= -scale)
            {
                char dig = '0';
                while (true)
                {
                    if (real_beg < beg && dividend[real_beg] == 0)
                    {
                        real_beg++;
                    }

                    bool less = false;
                    if (real_beg == beg)
                    {
                        for (int i = 0; i < divider_len; i++)
                        {
                            if (dividend[beg + i] != divider[i])
                            {
                                less = (dividend[beg + i] < divider[i]);
                                break;
                            }
                        }
                    }
                    if (less)
                    {
                        break;
                    }

                    for (int i = divider_len - 1; i >= 0; i--)
                    {
                        dividend[beg + i] -= divider[i];
                        if (dividend[beg + i] < 0)
                        {
                            dividend[beg + i] += 10;
                            dividend[beg + i - 1]--;
                        }
                    }
                    dig++;
                }

                result[cur_pos++] = dig;

                if (cur_pow == 0)
                {
                    resdot = cur_pos;
                    if (scale > 0)
                    {
                        result[cur_pos++] = '.';
                    }
                    resfrac = cur_pos;
                }
                cur_pow--;
                beg++;
            }
            resscale = cur_pos - resfrac;
            return _round(result.ToString(), resint, resdot, resfrac, resscale, scale, sign, false);
        }
        #endregion

        #region Modulus Helper Functions
        private static string Modulus(string left, string right, int scale = int.MinValue)
        {
            if (left == string.Empty)
            {
                return _zero(scale);
            }
            if (right == string.Empty)
            {
                throw new DivideByZeroException("Divide By ZERO");
            }
            if (scale == int.MinValue)
            {
                scale = _scale;
            }
            if (scale < 0)
            {
                scale = 0;
            }
            int lsign = 0, lint = 0, ldot = 0, lfrac = 0, lscale = 0;
            if (parse_number(ref left, ref lsign, ref lint, ref ldot, ref lfrac, ref lscale) < 0)
            {
                return ZERO;
            }
            int rsign = 0, rint = 0, rdot = 0, rfrac = 0, rscale = 0;
            if (parse_number(ref right, ref  rsign, ref  rint, ref  rdot, ref  rfrac, ref  rscale) < 0)
            {
                return ZERO;
            }
            ulong mod = 0;
            for (int i = rint; i < rdot; i++)
            {
                mod = mod * 10 + right[i] - '0';
            }

            if (rdot - rint > 18 || mod == 0)
            {
                return ZERO;
            }

            ulong res = 0;
            for (int i = lint; i < ldot; i++)
            {
                res = res * 2;
                if (res >= mod)
                {
                    res -= mod;
                }
                res = res * 5 + left[i] - '0';
                while (res >= mod)
                {
                    res -= mod;
                }
            }

            char[] buffer = new char[20];
            int cur_pos = 20;
            do
            {
                buffer[--cur_pos] = (char)(res % 10 + '0');
                res /= 10;
            }
            while (res > 0);

            if (lsign < 0)
            {
                buffer[--cur_pos] = '-';
            }
            return (trimTrailingZeros(buffer.ToString().Substring(cur_pos).Substring(0, 20 - cur_pos)));
        }
        #endregion

        #region Power Helper Functions
        private static string Pow(string left, string right, int scale = int.MinValue)
        {
            if (left == string.Empty)
            {
                return ZERO;
            }
            if (right == string.Empty)
            {
                return ONE;
            }

            int lsign = 0, lint = 0, ldot = 0, lfrac = 0, lscale = 0;
            if (parse_number(ref left, ref lsign, ref lint, ref ldot, ref lfrac, ref lscale) < 0)
            {
                return ZERO;
            }
            int rsign = 0, rint = 0, rdot = 0, rfrac = 0, rscale = 0;
            if (parse_number(ref right, ref  rsign, ref  rint, ref  rdot, ref  rfrac, ref  rscale) < 0)
            {
                return ZERO;
            }
            ulong deg = 0;
            for (int i = rint; i < rdot; i++)
            {
                deg = deg * 10 + right[i] - '0';
            }

            if (rdot - rint > 18 || (rsign < 0 && deg != 0))
            {
                return ZERO;
            }

            if (deg == 0)
            {
                return ONE;
            }

            string result = ONE;
            string mul = left;
            while (deg > 0)
            {
                if (deg == 1)
                {
                    result = Multiply(result, mul, 0);
                }
                mul = Multiply(mul, mul, 0);
                deg >>= 1;
            }
            return trimTrailingZeros(result);
        }
        #endregion

        #region Comparison Helper Functions
        private static int CompareTo(string left, string right, int scale = int.MinValue)
        {
            if (left == string.Empty)
            {
                return CompareTo(ZERO, right, scale);
            }
            if (right == string.Empty)
            {
                return CompareTo(left, ZERO, scale);
            }
            if (scale == int.MinValue)
            {
                scale = _scale;
            }
            if (scale < 0)
            {
                scale = 0;
            }
            int lsign = 0, lint = 0, ldot = 0, lfrac = 0, lscale = 0;
            if (parse_number(ref left, ref lsign, ref lint, ref ldot, ref lfrac, ref lscale) < 0)
            {
                return 0;
            }
            int rsign = 0, rint = 0, rdot = 0, rfrac = 0, rscale = 0;
            if (parse_number(ref right, ref  rsign, ref  rint, ref  rdot, ref  rfrac, ref  rscale) < 0)
            {
                return 0;
            }
            if (lsign != rsign)
            {
                return (lsign - rsign) / 2;
            }
            if (lsign < 0)
            {
                return -1 * _compareTo(left, lint, ldot, lfrac, lscale, right, rint, rdot, rfrac, rscale, scale);
            }
            else
            {
                return _compareTo(left, lint, ldot, lfrac, lscale, right, rint, rdot, rfrac, rscale, scale);
            }
        }

        private static int _compareTo(string left, int lint, int ldot, int lfrac, int lscale, string right, int rint, int rdot, int rfrac, int rscale, int scale)
        {
            int llen = ldot - lint;
            int rlen = rdot - rint;

            if (llen != rlen)
            {
                return (llen < rlen ? -1 : 1);
            }

            for (int i = 0; i < llen; i++)
            {
                if (left[lint + i] != right[rint + i])
                {
                    return (left[lint + i] < right[rint + i] ? -1 : 1);
                }
            }


            for (int i = 0; (i < lscale || i < rscale) && i < scale; i++)
            {
                int lchar = (i < lscale ? left[lfrac + i] : '0');
                int rchar = (i < rscale ? right[rfrac + i] : '0');
                if (lchar != rchar)
                {
                    return (lchar < rchar ? -1 : 1);
                }
            }
            return 0;
        }
        #endregion

        #region Rounding Off Helper Functions
        void round (int scale)
        {
            if (scale >= 1)
            {
                value = round(value, scale);
            }
        }

        public string round (string left, int scale)
        {
            if (left == string.Empty)
            {
                return round (ZERO, scale);
            }

            if (scale == int.MinValue)
            {
                scale = _scale;
            }

            if (scale < 0)
            {
                scale = 0;
            }

            int lsign = 0, lint = 0, ldot = 0, lfrac = 0, lscale = 0;
            if (parse_number(ref left, ref lsign, ref lint, ref ldot, ref lfrac, ref lscale) < 0)
            {
                return _zero(scale);
            }

            int len = left.Length;
            StringBuilder result = createBigDecimalString('0', len + 1);
            for(int i = len-1;i>=lint;--i)
            {
                result[i+1] = left[i];
            }
            return _round (result.ToString(), lint+1, ldot+1, lfrac+1, lscale, scale, lsign, true, true);
        }

        #endregion

        #region Scale Functions
        void setscale (int scale)
        {
            if (scale < 0)
            {
                _scale = 0;
            }
            else
            {
                _scale = scale;
            }
        }
        int getscale()
        {
            return _scale;
        }
        #endregion

        #region Get Left Or Right Part Of BigDecimal
        BigDecimal getIntPart(BigDecimal left)
        {
            return new BigDecimal(left.getIntPart());
        }

        BigDecimal getDecPart(BigDecimal right)
        {
            return new BigDecimal(right.getDecPart());
        }

        string getIntPart()
        {
            int dot = value.IndexOf('.');
            if(dot != -1)
            {
                if(dot == 0)
                    return"0";
                if(dot == 1 && value[0] == '-')
                    return "-0";
                return value.Substring(0, dot);
            }
            else
            {
                return value;
            }
        }

        string getDecPart()
        {
            int dot = value.IndexOf('.');
            if(dot != -1)
                return value.Length>dot+1?value.Substring(dot+1):"0";
            else
                return "0";
        }
        #endregion

        // [修改]
        #region Implicit Type Operators Overloads

        /// <summary>
        /// Creates a BigDecimal from a long.
        /// </summary>
        /// <param name="value">A long.</param>
        /// <returns>A BigDecimal initialzed by <paramref name="value" />.</returns>
        public static implicit operator BigDecimal(long value)
        {
            return (new BigDecimal(value));
        }

        /// <summary>
        /// Creates a BigDecimal from a ulong.
        /// </summary>
        /// <param name="value">A ulong.</param>
        /// <returns>A BigDecimal initialzed by <paramref name="value" />.</returns>
        public static implicit operator BigDecimal(ulong value)
        {
            return (new BigDecimal(value));
        }

        /// <summary>
        /// Creates a BigDecimal from a int.
        /// </summary>
        /// <param name="value">A int.</param>
        /// <returns>A BigDecimal initialzed by <paramref name="value" />.</returns>
        public static implicit operator BigDecimal(int value)
        {
            return (new BigDecimal(value));
        }

        /// <summary>
        /// Creates a BigDecimal from a uint.
        /// </summary>
        /// <param name="value">A uint.</param>
        /// <returns>A BigDecimal initialzed by <paramref name="value" />.</returns>
        public static implicit operator BigDecimal(uint value)
        {
            return (new BigDecimal(value));
        }

        /// <summary>
        /// [修改]
        /// </summary>
        public static implicit operator BigDecimal(float value)
        {
            return (new BigDecimal(value));
        }

        /// <summary>
        /// [修改]
        /// </summary>
        public static implicit operator BigDecimal(double value)
        {
            return (new BigDecimal(value));
        }

        /// <summary>
        /// [修改]
        /// </summary>
        public static implicit operator BigDecimal(string value)
        {
            return (new BigDecimal(value));
        }

        /// <summary>
        /// [修改]
        /// </summary>
        public static implicit operator string(BigDecimal value)
        {
            return value.ToString();
        }

        #endregion
    }
}