using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace CryptoCastor
{
	class Crypto
	{
		RijndaelManaged rm;

		public Crypto()
		{
			rm = new RijndaelManaged();
		}

		private string GenerateKey(string n,string pn,string p)
		{
			return "" + n[1] + pn[1] + n[0] + pn[0] + p[0] + p[1] + p[2] + p[3] + p[4] + p[5] + p[1] + p[2] + p[3] + p[3] + p[2] + p[1];
		}
		private string GenerateIV(string n, string pn, string p)
		{
			return "" + n[1] + pn[1] + n[0] + pn[0] + p[0] + p[1] + p[2] + p[3] + p[4] + p[5] + p[1] + p[2] + p[3] + p[3] + p[2] + p[1];
		}

		public string Crypt(string clearText, string n, string pn, string p)
		{
			// Place le texte à chiffrer dans un tableau d'octets
			byte[] plainText = Encoding.UTF8.GetBytes(clearText);

			// Place la clé de chiffrement dans un tableau d'octets
			byte[] key = Encoding.UTF8.GetBytes(GenerateKey(n,pn,p));

			// Place le vecteur d'initialisation dans un tableau d'octets
			byte[] iv = Encoding.UTF8.GetBytes(GenerateIV(n, pn, p));

			RijndaelManaged rijndael = new RijndaelManaged();

			// Définit le mode utilisé
			rijndael.Mode = CipherMode.CBC;

			// Crée le chiffreur AES - Rijndael
			ICryptoTransform aesEncryptor = rijndael.CreateEncryptor(key, iv);

			MemoryStream ms = new MemoryStream();

			// Ecris les données chiffrées dans le MemoryStream
			CryptoStream cs = new CryptoStream(ms, aesEncryptor, CryptoStreamMode.Write);
			cs.Write(plainText, 0, plainText.Length);
			cs.FlushFinalBlock();

			// Place les données chiffrées dans un tableau d'octet
			byte[] CipherBytes = ms.ToArray();


			ms.Close();
			cs.Close();

			// Place les données chiffrées dans une chaine encodée en Base64
			return Convert.ToBase64String(CipherBytes);
		}

		public string Decrypt(string cipherText, string n, string pn, string p)
		{
			// Place le texte à déchiffrer dans un tableau d'octets
			byte[] cipheredData = Convert.FromBase64String(cipherText);

			// Place la clé de chiffrement dans un tableau d'octets
			byte[] key = Encoding.UTF8.GetBytes(GenerateKey(n, pn, p));

			// Place le vecteur d'initialisation dans un tableau d'octets
			byte[] iv = Encoding.UTF8.GetBytes(GenerateIV(n, pn, p));

			RijndaelManaged rijndael = new RijndaelManaged();
			rijndael.Mode = CipherMode.CBC;


			// Ecris les données déchiffrées dans le MemoryStream
			ICryptoTransform decryptor = rijndael.CreateDecryptor(key, iv);
			MemoryStream ms = new MemoryStream(cipheredData);
			CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

			// Place les données déchiffrées dans un tableau d'octet
			byte[] plainTextData = new byte[cipheredData.Length];

			int decryptedByteCount = cs.Read(plainTextData, 0, plainTextData.Length);

			ms.Close();
			cs.Close();

			return Encoding.UTF8.GetString(plainTextData, 0, decryptedByteCount);
		}
	}
}
