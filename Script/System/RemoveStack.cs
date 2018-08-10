using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigPicture.Layers {
	public static class RemoveStack {
		public static Stack<Layer> stack = new Stack<Layer>();
		public static void Pop() {
			if (stack.Count > 0) {
				Layer layer = stack.Pop();

				LayerManager.LoadLayer(layer.mainText.Text, layer.memoText.Text, layer.IndexY, layer.TargetDate, (int)layer.BGColor.GetHue());

				layer.group.Dispose();
			}
		}
	}
}
