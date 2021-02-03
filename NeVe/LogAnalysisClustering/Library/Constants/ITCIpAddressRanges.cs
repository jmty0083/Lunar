using LogAnalysisLibrary.DataType;
using System.Collections.Generic;
using System.Net;

namespace LogAnalysisLibrary.Data.Constants
{
    public class ITCIpAddressRanges
    {
        public static List<IPv4AddressRange> IpRanges = new List<IPv4AddressRange>
        {
            IPv4AddressRange.NewInstance("36.254.0.0/16"),
            IPv4AddressRange.NewInstance("49.210.0.0/15"),
            IPv4AddressRange.NewInstance("60.245.128.0/17"),
            IPv4AddressRange.NewInstance("101.252.0.0/15"),
            IPv4AddressRange.NewInstance("103.2.208.0/22"),
            IPv4AddressRange.NewInstance("116.90.184.0/21"),
            IPv4AddressRange.NewInstance("116.215.0.0/16"),
            IPv4AddressRange.NewInstance("117.103.16.0/20"),
            IPv4AddressRange.NewInstance("119.78.0.0/15"),
            IPv4AddressRange.NewInstance("119.232.0.0/16"),
            IPv4AddressRange.NewInstance("119.233.0.0/17"),
            IPv4AddressRange.NewInstance("124.16.0.0/15"),
            IPv4AddressRange.NewInstance("150.242.4.0/22"),
            IPv4AddressRange.NewInstance("159.226.0.0/16"),
            IPv4AddressRange.NewInstance("202.38.128.0/23"),
            IPv4AddressRange.NewInstance("202.90.224.0/21"),
            IPv4AddressRange.NewInstance("202.122.32.0/21"),
            IPv4AddressRange.NewInstance("202.127.0.0/21"),
            IPv4AddressRange.NewInstance("202.127.16.0/20"),
            IPv4AddressRange.NewInstance("202.127.144.0/20"),
            IPv4AddressRange.NewInstance("202.127.200.0/21"),
            IPv4AddressRange.NewInstance("210.72.0.0/17"),
            IPv4AddressRange.NewInstance("210.72.128.0/19"),
            IPv4AddressRange.NewInstance("210.73.0.0/18"),
            IPv4AddressRange.NewInstance("210.75.160.0/19"),
            IPv4AddressRange.NewInstance("210.75.224.0/19"),
            IPv4AddressRange.NewInstance("210.76.192.0/19"),
            IPv4AddressRange.NewInstance("210.77.0.0/19"),
            IPv4AddressRange.NewInstance("210.77.64.0/19"),
            IPv4AddressRange.NewInstance("211.147.192.0/20"),
            IPv4AddressRange.NewInstance("211.156.64.0/20"),
            IPv4AddressRange.NewInstance("211.156.160.0/20"),
            IPv4AddressRange.NewInstance("211.167.160.0/20"),
            IPv4AddressRange.NewInstance("218.244.64.0/19"),
            IPv4AddressRange.NewInstance("223.192.0.0/15"),
        };

        public static bool IsInItcList(string IpAddress)
        {
            var ip = IPAddress.Parse(IpAddress);
            foreach (var address in IpRanges)
            {
                if (address.IsInRange(ip))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
