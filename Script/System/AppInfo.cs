using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigPicture {
#if !Client
	public enum ProgramType {
		Updater,
		Setup,
	}
#endif
	public static class AppInfo {
		public static bool isExitRequest;
		public const string projectName = "BigPicture";
		public static string version = "1.4.1";
#if !Client
		public static ProgramType programType = ProgramType.Setup;
#endif
	}
}
