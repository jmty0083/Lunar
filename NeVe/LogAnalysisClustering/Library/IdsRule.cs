using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LogAnalysisLibrary.DataType
{
    [Flags]
    public enum IdsTag
    {
        Ransomware = 1 << 1,
        Phish = 1 << 2,
        Spam = 1 << 3,
        Email = 1 << 4,
        Domain = 1 << 5,
        Mobile = 1 << 6,
    }

    public class IdsRule : ICloneable
    {
        [DisplayName("SID")]
        public string Sid { get; set; }

        [DisplayName("创建日期")]
        public string CreatedDate { get; set; }

        [DisplayName("更新日期")]
        public string UpdatedDate { get; set; }

        [DisplayName("分类")]
        public string Category { get; set; }

        [DisplayName("标签")]
        public IdsTag Tag { get; set; }

        //[DisplayName("原始信息")]
        //public string Origin { get; set; }

        private static readonly Regex msid = new Regex(@"sid:([0-9]*);");
        private static readonly Regex mcreate = new Regex(@"created_at ([0-9_]*)");
        private static readonly Regex mcategory = new Regex(@"classtype:([^;]*);");
        private static readonly Regex mupdate = new Regex(@"updated_at ([0-9_]*)");

        public object Clone()
        {
            return new IdsRule()
            {
                Category = this.Category,
                Sid = this.Sid,
                CreatedDate = this.CreatedDate,
                UpdatedDate = this.UpdatedDate,
                Tag = this.Tag
                //Origin = this.Origin
            };
        }

        public static IdsRule GetIdsRule(string line)
        {
            line = line.ToLower();
            if (line.Contains("sid"))
            {
                var sid = msid.Match(line);
                var create = mcreate.Match(line);
                var category = mcategory.Match(line);
                var update = mupdate.Match(line);
                if (sid.Success && ((create.Success && update.Success) || category.Success))
                {
                    var result = new IdsRule
                    {
                        Sid = sid.Groups[1].Value,
                        CreatedDate = create.Groups[1].Value,
                        UpdatedDate = update.Groups[1].Value,
                        Category = category.Groups[1].Value,
                    };

                    foreach (var item in Enum.GetValues(typeof(IdsTag)).Cast<IdsTag>())
                    {
                        if (line.Contains(item.ToString().ToLower()))
                        {
                            result.Tag |= item;
                        }
                    }

                    return result;
                }
            }

            return null;
        }

        public static string Serialization(List<IdsRule> data)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<IdsRule>));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, data);
                    return sww.ToString(); // Your XML
                }
            }
        }

        public static List<IdsRule> Deserialization(string data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<IdsRule>));

            using (TextReader reader = new StringReader(data))
            {
                return (List<IdsRule>)serializer.Deserialize(reader);
            }
        }
    }
}
