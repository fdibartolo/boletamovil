using System;

namespace prode.domain.constants
{
	public static class Constants
	{
		public static string APP_TITLE = "Comunidad Prode";
//		public static string WEB_SERVER_URL = "desolate-bayou-3667.herokuapp.com";
		public static string WEB_SERVER_URL = "stormy-autumn-8027.herokuapp.com";

		public static string ERROR_INVALID_CREDENTIALS = "El usuario o la contraseña son incorrectas";
		public static string ERROR_GENERAL_MESSAGE = "Lamentablemente no es posible establecer la comunicación con el servidor";
	}
	
	public enum AppMode {
		Login,
		Tabs,
		Newbie
	}
}

