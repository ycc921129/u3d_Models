/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.31
*/

using System;
using System.IO;
using System.Text;

namespace FutureCore
{
    public class BinaryMemoryStream : MemoryStream
    {
        #region Constructor
        public BinaryMemoryStream()
        {
        }

        public BinaryMemoryStream(byte[] buffer) : base(buffer)
        {
        }
        #endregion

        #region ByteArray
        public void WriteByteArray(byte[] value)
        {
            base.Write(value, 0, value.Length);
        }

        public void CombineByteArray(byte[] value1, byte[] value2)
        {
            base.Write(value1, 0, value1.Length);
            base.Write(value2, 0, value2.Length);
        }
        #endregion

        #region Short
        public short ReadShort()
        {
            int size = 2;
            byte[] arr = new byte[size];
            base.Read(arr, 0, size);
            return BitConverter.ToInt16(arr, 0);
        }

        public void WriteShort(short value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region UShort
        public ushort ReadUShort()
        {
            int size = 2;
            byte[] arr = new byte[size];
            base.Read(arr, 0, size);
            return BitConverter.ToUInt16(arr, 0);
        }

        public void WriteUShort(ushort value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region Int
        public int ReadInt()
        {
            int size = 4;
            byte[] arr = new byte[size];
            base.Read(arr, 0, size);
            return BitConverter.ToInt32(arr, 0);
        }

        public void WriteInt(int value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region UInt
        public uint ReadUInt()
        {
            int size = 4;
            byte[] arr = new byte[size];
            base.Read(arr, 0, size);
            return BitConverter.ToUInt32(arr, 0);
        }

        public void WriteUInt(uint value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region Long
        public long ReadLong()
        {
            int size = 8;
            byte[] arr = new byte[size];
            base.Read(arr, 0, size);
            return BitConverter.ToInt64(arr, 0);
        }

        public void WriteLong(long value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region ULong
        public ulong ReadULong()
        {
            int size = 4;
            byte[] arr = new byte[size];
            base.Read(arr, 0, size);
            return BitConverter.ToUInt64(arr, 0);
        }

        public void WriteULong(ulong value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region Float
        public float ReadFloat()
        {
            int size = 4;
            byte[] arr = new byte[size];
            base.Read(arr, 0, size);
            return BitConverter.ToSingle(arr, 0);
        }

        public void WriteFloat(float value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region Double
        public double ReadDouble()
        {
            int size = 8;
            byte[] arr = new byte[size];
            base.Read(arr, 0, size);
            return BitConverter.ToDouble(arr, 0);
        }

        public void WriteDouble(double value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region Bool
        public bool ReadBool()
        {
            return base.ReadByte() == 1;
        }

        public void WriteBool(bool value)
        {
            base.WriteByte((byte)(value == true ? 1 : 0));
        }
        #endregion

        #region UTF8String
        public string ReadUTF8String()
        {
            ushort len = ReadUShort();
            byte[] arr = new byte[len];
            base.Read(arr, 0, len);
            return Encoding.UTF8.GetString(arr);
        }

        public void WriteUTF8String(string str)
        {
            //MaxValue is 65535
            int maxValue = ushort.MaxValue;
            byte[] arr = Encoding.UTF8.GetBytes(str);
            if (arr.Length > maxValue)
            {
                throw new InvalidCastException("String out of range");
            }
            WriteUShort((ushort)arr.Length);
            base.Write(arr, 0, arr.Length);
        }
        #endregion
    }
}