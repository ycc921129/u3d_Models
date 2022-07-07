/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Text;
using UnityEngine;

namespace FutureCore
{
    public static class StringBuilderExtensions
    {
        // These digits are here in a static array to support hex with simple, easily-understandable code. 
        // Since A-Z don't sit next to 0-9 in the ascii table.
        private static readonly char[] ms_digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        private static readonly uint ms_default_decimal_places = 5; //< Matches standard .NET formatting dp's
        private static readonly char ms_default_pad_char = '0';

        //! Convert a given unsigned integer value to a string and concatenate onto the stringbuilder. Any base value allowed.
        public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val, uint pad_amount, char pad_char, uint base_val)
        {
            Debug.Assert(pad_amount >= 0);
            Debug.Assert(base_val > 0 && base_val <= 16);

            // Calculate length of integer when written out
            uint length = 0;
            uint length_calc = uint_val;

            do
            {
                length_calc /= base_val;
                length++;
            }
            while (length_calc > 0);

            // Pad out space for writing.
            string_builder.Append(pad_char, (int)Math.Max(pad_amount, length));

            int strpos = string_builder.Length;

            // We're writing backwards, one character at a time.
            while (length > 0)
            {
                strpos--;

                // Lookup from static char array, to cover hex values too
                string_builder[strpos] = ms_digits[uint_val % base_val];

                uint_val /= base_val;
                length--;
            }

            return string_builder;
        }

        //! Convert a given unsigned integer value to a string and concatenate onto the stringbuilder. Assume no padding and base ten.
        public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val)
        {
            string_builder.Concat(uint_val, 0, ms_default_pad_char, 10);
            return string_builder;
        }

        //! Convert a given unsigned integer value to a string and concatenate onto the stringbuilder. Assume base ten.
        public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val, uint pad_amount)
        {
            string_builder.Concat(uint_val, pad_amount, ms_default_pad_char, 10);
            return string_builder;
        }

        //! Convert a given unsigned integer value to a string and concatenate onto the stringbuilder. Assume base ten.
        public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val, uint pad_amount, char pad_char)
        {
            string_builder.Concat(uint_val, pad_amount, pad_char, 10);
            return string_builder;
        }

        //! Convert a given signed integer value to a string and concatenate onto the stringbuilder. Any base value allowed.
        public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount, char pad_char, uint base_val)
        {
            Debug.Assert(pad_amount >= 0);
            Debug.Assert(base_val > 0 && base_val <= 16);

            // Deal with negative numbers
            if (int_val < 0)
            {
                string_builder.Append('-');
                uint uint_val = uint.MaxValue - ((uint)int_val) + 1; //< This is to deal with Int32.MinValue
                string_builder.Concat(uint_val, pad_amount, pad_char, base_val);
            }
            else
            {
                string_builder.Concat((uint)int_val, pad_amount, pad_char, base_val);
            }

            return string_builder;
        }

        //! Convert a given signed integer value to a string and concatenate onto the stringbuilder. Assume no padding and base ten.
        public static StringBuilder Concat(this StringBuilder string_builder, int int_val)
        {
            string_builder.Concat(int_val, 0, ms_default_pad_char, 10);
            return string_builder;
        }

        //! Convert a given signed integer value to a string and concatenate onto the stringbuilder. Assume base ten.
        public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount)
        {
            string_builder.Concat(int_val, pad_amount, ms_default_pad_char, 10);
            return string_builder;
        }

        //! Convert a given signed integer value to a string and concatenate onto the stringbuilder. Assume base ten.
        public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount, char pad_char)
        {
            string_builder.Concat(int_val, pad_amount, pad_char, 10);
            return string_builder;
        }

        //! Convert a given float value to a string and concatenate onto the stringbuilder
        public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places, uint pad_amount, char pad_char)
        {
            Debug.Assert(pad_amount >= 0);

            if (decimal_places == 0)
            {
                // No decimal places, just round up and print it as an int

                // Agh, Math.Floor() just works on doubles/decimals. Don't want to cast! Let's do this the old-fashioned way.
                int int_val;
                if (float_val >= 0.0f)
                {
                    // Round up
                    int_val = (int)(float_val + 0.5f);
                }
                else
                {
                    // Round down for negative numbers
                    int_val = (int)(float_val - 0.5f);
                }

                string_builder.Concat(int_val, pad_amount, pad_char, 10);
            }
            else
            {
                int int_part = (int)float_val;

                // First part is easy, just cast to an integer
                string_builder.Concat(int_part, pad_amount, pad_char, 10);

                // Decimal point
                string_builder.Append('.');

                // Work out remainder we need to print after the d.p.
                float remainder = Math.Abs(float_val - int_part);

                // Multiply up to become an int that we can print
                do
                {
                    remainder *= 10;
                    decimal_places--;
                }
                while (decimal_places > 0);

                // Round up. It's guaranteed to be a positive number, so no extra work required here.
                remainder += 0.5f;

                // All done, print that as an int!
                string_builder.Concat((uint)remainder, 0, '0', 10);
            }
            return string_builder;
        }

        //! Convert a given float value to a string and concatenate onto the stringbuilder. Assumes five decimal places, and no padding.
        public static StringBuilder Concat(this StringBuilder string_builder, float float_val)
        {
            string_builder.Concat(float_val, ms_default_decimal_places, 0, ms_default_pad_char);
            return string_builder;
        }

        //! Convert a given float value to a string and concatenate onto the stringbuilder. Assumes no padding.
        public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places)
        {
            string_builder.Concat(float_val, decimal_places, 0, ms_default_pad_char);
            return string_builder;
        }

        //! Convert a given float value to a string and concatenate onto the stringbuilder.
        public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places, uint pad_amount)
        {
            string_builder.Concat(float_val, decimal_places, pad_amount, ms_default_pad_char);
            return string_builder;
        }

        //! Concatenate a formatted string with arguments
        public static StringBuilder ConcatFormat<A>(this StringBuilder string_builder, String format_string, A arg1)
            where A : IConvertible
        {
            return string_builder.ConcatFormat<A, int, int, int>(format_string, arg1, 0, 0, 0);
        }

        //! Concatenate a formatted string with arguments
        public static StringBuilder ConcatFormat<A, B>(this StringBuilder string_builder, String format_string, A arg1, B arg2)
            where A : IConvertible
            where B : IConvertible
        {
            return string_builder.ConcatFormat<A, B, int, int>(format_string, arg1, arg2, 0, 0);
        }

        //! Concatenate a formatted string with arguments
        public static StringBuilder ConcatFormat<A, B, C>(this StringBuilder string_builder, String format_string, A arg1, B arg2, C arg3)
            where A : IConvertible
            where B : IConvertible
            where C : IConvertible
        {
            return string_builder.ConcatFormat<A, B, C, int>(format_string, arg1, arg2, arg3, 0);
        }

        //! Concatenate a formatted string with arguments
        public static StringBuilder ConcatFormat<A, B, C, D>(this StringBuilder string_builder, String format_string, A arg1, B arg2, C arg3, D arg4)
            where A : IConvertible
            where B : IConvertible
            where C : IConvertible
            where D : IConvertible
        {
            int verbatim_range_start = 0;

            for (int index = 0; index < format_string.Length; index++)
            {
                if (format_string[index] == '{')
                {
                    // Formatting bit now, so make sure the last block of the string is written out verbatim.
                    if (verbatim_range_start < index)
                    {
                        // Write out unformatted string portion
                        string_builder.Append(format_string, verbatim_range_start, index - verbatim_range_start);
                    }

                    uint base_value = 10;
                    uint padding = 0;
                    uint decimal_places = 5; // Default decimal places in .NET libs

                    index++;
                    char format_char = format_string[index];
                    if (format_char == '{')
                    {
                        string_builder.Append('{');
                        index++;
                    }
                    else
                    {
                        index++;

                        if (format_string[index] == ':')
                        {
                            // Extra formatting. This is a crude first pass proof-of-concept. It's not meant to cover
                            // comprehensively what the .NET standard library Format() can do.
                            index++;

                            // Deal with padding
                            while (format_string[index] == '0')
                            {
                                index++;
                                padding++;
                            }

                            if (format_string[index] == 'X')
                            {
                                index++;

                                // Print in hex
                                base_value = 16;

                                // Specify amount of padding ( "{0:X8}" for example pads hex to eight characters
                                if ((format_string[index] >= '0') && (format_string[index] <= '9'))
                                {
                                    padding = (uint)(format_string[index] - '0');
                                    index++;
                                }
                            }
                            else if (format_string[index] == '.')
                            {
                                index++;

                                // Specify number of decimal places
                                decimal_places = 0;

                                while (format_string[index] == '0')
                                {
                                    index++;
                                    decimal_places++;
                                }
                            }
                        }


                        // Scan through to end bracket
                        while (format_string[index] != '}')
                        {
                            index++;
                        }

                        // Have any extended settings now, so just print out the particular argument they wanted
                        switch (format_char)
                        {
                            case '0': string_builder.ConcatFormatValue<A>(arg1, padding, base_value, decimal_places); break;
                            case '1': string_builder.ConcatFormatValue<B>(arg2, padding, base_value, decimal_places); break;
                            case '2': string_builder.ConcatFormatValue<C>(arg3, padding, base_value, decimal_places); break;
                            case '3': string_builder.ConcatFormatValue<D>(arg4, padding, base_value, decimal_places); break;
                            default: Debug.Assert(false, "Invalid parameter index"); break;
                        }
                    }

                    // Update the verbatim range, start of a new section now
                    verbatim_range_start = (index + 1);
                }
            }

            // Anything verbatim to write out?
            if (verbatim_range_start < format_string.Length)
            {
                // Write out unformatted string portion
                string_builder.Append(format_string, verbatim_range_start, format_string.Length - verbatim_range_start);
            }

            return string_builder;
        }

        //! The worker method. This does a garbage-free conversion of a generic type, and uses the garbage-free Concat() to add to the stringbuilder
        private static void ConcatFormatValue<T>(this StringBuilder string_builder, T arg, uint padding, uint base_value, uint decimal_places) where T : IConvertible
        {
            switch (arg.GetTypeCode())
            {
                case System.TypeCode.UInt32:
                    {
                        string_builder.Concat(arg.ToUInt32(System.Globalization.NumberFormatInfo.CurrentInfo), padding, '0', base_value);
                        break;
                    }

                case System.TypeCode.Int32:
                    {
                        string_builder.Concat(arg.ToInt32(System.Globalization.NumberFormatInfo.CurrentInfo), padding, '0', base_value);
                        break;
                    }

                case System.TypeCode.Single:
                    {
                        string_builder.Concat(arg.ToSingle(System.Globalization.NumberFormatInfo.CurrentInfo), decimal_places, padding, '0');
                        break;
                    }

                case System.TypeCode.String:
                    {
                        string_builder.Append(Convert.ToString(arg));
                        break;
                    }

                default:
                    {
                        Debug.Assert(false, "Unknown parameter type");
                        break;
                    }
            }
        }

        public static void Empty(this StringBuilder string_builder)
        {
            string_builder.Remove(0, string_builder.Length);
        }


        public static string GetGarbageFreeString(this StringBuilder string_builder)
        {
            return (string)string_builder.GetType().GetField("_str", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(string_builder);
        }

        public static void GarbageFreeClear(this StringBuilder string_builder)
        {
            string_builder.Length = 0;
            string_builder.Append(' ', string_builder.Capacity);
            string_builder.Length = 0;
        }
    }
    public static class StrExt
    {
        //只允许拼接50个字符串
        private static StringBuilder s_StringBuilder = new StringBuilder(50, 50);

        public static string Format<A>(String format_string, A arg1)
                where A : IConvertible
        {
            s_StringBuilder.Empty();
            return s_StringBuilder.ConcatFormat<A, int, int, int>(format_string, arg1, 0, 0, 0).ToString();
        }

        public static string Format<A, B>(String format_string, A arg1, B arg2)
            where A : IConvertible
            where B : IConvertible
        {
            s_StringBuilder.Empty();
            return s_StringBuilder.ConcatFormat<A, B, int, int>(format_string, arg1, arg2, 0, 0).ToString();
        }

        public static string Format<A, B, C>(String format_string, A arg1, B arg2, C arg3)
            where A : IConvertible
            where B : IConvertible
            where C : IConvertible
        {
            s_StringBuilder.Empty();
            return s_StringBuilder.ConcatFormat<A, B, C, int>(format_string, arg1, arg2, arg3, 0).ToString();
        }

        public static string Format<A, B, C, D>(String format_string, A arg1, B arg2, C arg3, D arg4)
            where A : IConvertible
            where B : IConvertible
            where C : IConvertible
            where D : IConvertible
        {
            s_StringBuilder.Empty();
            return s_StringBuilder.ConcatFormat<A, B, C, D>(format_string, arg1, arg2, arg3, arg4).ToString();
        }
    }
}