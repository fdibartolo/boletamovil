using System;
using System.IO;

namespace prode.domain
{
	public class UserStore
	{
		readonly string authPath = Path.Combine(Environment.GetFolderPath (Environment.SpecialFolder.Personal), "user.txt");
		
		public User ReadUser(){
			Console.WriteLine("Pulling user info...");
			var user = new User();
			try {
				if (!File.Exists(authPath))
					return user;
				string[] text = File.ReadAllLines(authPath);
				if (text.Length==0)
					return user;
				
				var encryptedNickName = text[0];
				user.NickName = EncryptionManager.Decrypt(encryptedNickName);
				
				var encryptedFirstName = text[1];
				user.FirstName = EncryptionManager.Decrypt(encryptedFirstName);
				
				var encryptedLastName = text[2];
				user.LastName = EncryptionManager.Decrypt(encryptedLastName);
				
				var encryptedPassword = text[3];
				user.Password = EncryptionManager.Decrypt(encryptedPassword);
				
				return user;
				
			} catch (Exception){
				return null;
			}
		}
				
		public void SaveUser(User user){
			try {
				Console.WriteLine("Saving user info...");
				if (user == null)
					user = new User();
				
				var encryptedNickName = EncryptionManager.Encrypt(user.NickName);
				var encryptedFirstName = EncryptionManager.Encrypt(user.FirstName);
				var encryptedLastName = EncryptionManager.Encrypt(user.LastName);
				var encryptedPassword = EncryptionManager.Encrypt(user.Password);

				File.WriteAllText(authPath, String.Format("{0}\n{1}\n{2}\n{3}", 
					encryptedNickName, 
					encryptedFirstName, 
					encryptedLastName, 
					encryptedPassword));
			} catch (Exception){}
		}
	}
}
