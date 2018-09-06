using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Encrypt 的摘要说明
/// </summary>
public class Encrypt
{
    public Encrypt()
    {
    }

    //默认密钥向量
    private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

    private static string Key = "11092718";
    private static string _ServerKey = "82930127";
    private static string _ClientKey = "54332432";
    private static string Key3 = "53211124";
    
    /// <summary>
    /// DES加密字符串
    /// </summary>
    /// <param name="encryptstring">待加密的字符串</param>
    /// <param name="type">类型：后台为1，客户端为2 </param>
    /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
    public static string EncryptDES(string encryptstring, int type)
    {
        try
        {
            string KeyStr="";
            if (type == 1) KeyStr = _ServerKey;
            else if (type == 2) KeyStr = _ClientKey;
            else  KeyStr = Key3;

            byte[] rgbKey = Encoding.UTF8.GetBytes(KeyStr.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptstring);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
        catch
        {
            return encryptstring;
        }
    }    

    /// <summary>
    /// DES解密字符串
    /// </summary>
    /// <param name="decryptstring">待解密的字符串</param>
    /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
    public static string DecryptDES(string decryptstring, int type)
    {
        try
        {
            string KeyStr = "";
            if (type == 1) KeyStr = _ServerKey;
            else if (type == 2) KeyStr = _ClientKey;
            else KeyStr = Key3;

            byte[] rgbKey = Encoding.UTF8.GetBytes(KeyStr);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(decryptstring);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
        catch
        {
            return decryptstring;
        }
    }


}

