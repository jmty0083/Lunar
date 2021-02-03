using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisLibrary.DataType
{
    public class IPv4AddressRange
    {
        public long StartIp { get; private set; }
        public long EndIp { get; private set; }

        public byte[] IpAddress { get; private set; }
        public byte[] Mask { get; private set; }

        protected IPv4AddressRange()
        {
        }

        public bool IsInRange(IPAddress ipAddress)
        {
            if (StartIp != EndIp)
            {
#pragma warning disable CS0618 // 类型或成员已过时
                var ipn = ipAddress.Address;
#pragma warning restore CS0618 // 类型或成员已过时
                return StartIp < ipn && EndIp > ipn;
            }
            else
            {
                var bytes = ipAddress.GetAddressBytes();
                return (this.IpAddress[0] & this.Mask[0]) == (bytes[0] & this.Mask[0])
                    && (this.IpAddress[1] & this.Mask[1]) == (bytes[1] & this.Mask[1])
                    && (this.IpAddress[2] & this.Mask[2]) == (bytes[2] & this.Mask[2])
                    && (this.IpAddress[3] & this.Mask[3]) == (bytes[3] & this.Mask[3]);
            }
        }

        public static IPv4AddressRange NewInstance(string startIp, string endIp)
        {
            return new IPv4AddressRange
            {
#pragma warning disable CS0618 // 类型或成员已过时
                StartIp = IPAddress.Parse(startIp).Address,
                EndIp = IPAddress.Parse(endIp).Address,
#pragma warning restore CS0618 // 类型或成员已过时
            };
        }
        
        public static IPv4AddressRange NewInstance(string ipAddressWithMask)
        {
            if (ipAddressWithMask.Contains("/"))
            {
                var value = ipAddressWithMask.Split('/');
                int cidrMask = int.Parse(value[1]);
                var bits = 0xffffffff ^ (1 << 32 - cidrMask) - 1;
                byte [] mask = new byte[4];
                mask[0] = Convert.ToByte((bits & 0xff000000) >> 24);
                mask[1] = Convert.ToByte((bits & 0xff0000) >> 16);
                mask[2] = Convert.ToByte((bits & 0xff00) >> 8);
                mask[3] = Convert.ToByte(bits & 0xff);

                return new IPv4AddressRange
                {
                    IpAddress = (IPAddress.Parse(value[0])).GetAddressBytes(),
                    Mask = mask
                };
            }
            else
            {
                throw new ArgumentException("Cannot Parse IP Address With Mask of " + ipAddressWithMask);
            }
        }
    }
}
