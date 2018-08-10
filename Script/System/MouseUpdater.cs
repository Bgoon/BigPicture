using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

using BigPicture.Forms;
using BigPicture.Layers;

namespace BigPicture {
	public class MouseUpdater {
		public int targetFPS = 30;
		public bool IsRunning {
			get {
				return isRunning;
			}
		}
		private bool isRunning;

		public MouseUpdater() {
		}
		public void Start() {
			if (!isRunning) {
				isRunning = true;

				Loop();
			}
		}
		public void Stop() {
			isRunning = false;
		}

		bool isLayerFocused;
		private async void Loop() {
			for(;;await Task.Delay(1000 / targetFPS)) {
				if(!isRunning) {
					return;
				}

				isLayerFocused = false;
				if (MainForm.instance.OnMouse() && (!WinInput.GetKey(KeyCode.LeftMouse) || MainForm.instance.layerNamer.OnMouse())) {
					//레이어 체크
					for (int i = 0; i < LayerManager.layerList.Count; i++) {
						Layer layer = LayerManager.layerList[i];
						if (layer.mainPanel.OnMouse()) {
							isLayerFocused = true;
							//레이어 포커스
							if (LayerManager.focusedLayer != layer) {
								LayerManager.UnFocusLayer();
								LayerManager.FocusLayer(layer);
							}
							//레이어네이머 붙이기
							MainForm.instance.layerNamer.AttachLayer(layer);
							break;
						}
					}
				}
				if (!isLayerFocused) {
					//레이어 포커스 해제
					LayerManager.UnFocusLayer();

					//레이어네이머 떼기
					if (!MainForm.instance.layerNamer.isEditing) {
						MainForm.instance.layerNamer.ClearLayer();
					}
					
				}

				WinInput.Update();
			}
		}

	}
}
