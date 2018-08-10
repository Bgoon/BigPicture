using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigPicture {
	public static class Random {
		public static System.Random random = new System.Random();

		public static int Next(int min, int max) {
			return random.Next(min, max);
		}
	}
}
