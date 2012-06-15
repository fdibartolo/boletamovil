using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace prode.domain
{
	public static class EncryptionManager
	{
		private static byte[] _key = ASCIIEncoding.ASCII.GetBytes("s2Frq[54$%fsEe4p"); //gotta be 16, 32, etc bytes
		private static byte[] _initVector = ASCIIEncoding.ASCII.GetBytes("ft%3P4$aw!)rTkG]"); //gotta be 16 bytes

		public static string Encrypt(string source)
		{
			if (string.IsNullOrEmpty(source))
			    return string.Empty;
			    
			try{
				using (var provider = new RijndaelManaged())
				using (var memoryStream = new MemoryStream())
				using (var cryptoStream = new CryptoStream(memoryStream, provider.CreateEncryptor(_key, _initVector), CryptoStreamMode.Write))
				using (var writer = new StreamWriter(cryptoStream))
				{
					writer.Write(source);
					writer.Flush();
					cryptoStream.FlushFinalBlock();
					writer.Flush();
					
					return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
				}
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		public static string Decrypt(string source)
		{
			if (string.IsNullOrEmpty(source))
			    return string.Empty;
			    
			using (var provider = new RijndaelManaged())
			using (var memoryStream = new MemoryStream(Convert.FromBase64String(source)))
			using (var cryptoStream = new CryptoStream(memoryStream, provider.CreateDecryptor(_key, _initVector), CryptoStreamMode.Read))
			using (var reader = new StreamReader(cryptoStream))
			{
				return reader.ReadToEnd();
			}
		}
	}
}
