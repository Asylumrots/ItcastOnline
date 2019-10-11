using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.Common
{
    public class GetMd5
    {
        public static string Get(string str)
        {
            //创建MD5的对象 //MD5对象是抽象的
            MD5 mD5 = MD5.Create();
            //将字符串转换成字节数组
            byte[] buffer = Encoding.Default.GetBytes(str);
            byte[] md5buffer = mD5.ComputeHash(buffer);
            //string newStr = Encoding.Default.GetString(md5buffer);
            //将字节数组转换成字符串
            //1.将字节数组中每个元素按照指定的编码格式转换成字符串
            //2.直接将数组ToString()
            //3.将字节数组里面的每个元素ToString()
            string newStr = "";
            for (int i = 0; i < md5buffer.Length; i++)
            {
                //将十进制转换成十六进制，ToString()加参数x 123---0xA 26---0x1A
                //为了对齐参数为x2                         123---0x0A 26---0x1A
                newStr += md5buffer[i].ToString("x2");
            }
            return newStr;
        }
    }
}
