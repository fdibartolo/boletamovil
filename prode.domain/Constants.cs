using System;

namespace prode.domain.constants
{
	public static class Constants
	{
		public static string APP_TITLE = "Comunidad Prode";
		public static string WEB_SERVER_URL = "stormy-autumn-8027.herokuapp.com";
		
		public static string ERROR_INVALID_CREDENTIALS = "El usuario o la contraseña son incorrectas";
	}
	
	public enum AppMode {
		Login,
		Tabs,
		Newbie
	}
}

