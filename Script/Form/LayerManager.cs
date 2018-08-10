using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BigPicture.Forms;
using BigPicture.Graphic;
using BigPicture.Properties;

namespace BigPicture.Layers {
	public static class LayerManager {
		public static List<Layer> layerList = new List<Layer>();
		public const int MaxCount = 15;
		public const string defaultText = "새로운 일정";
		public static Layer focusedLayer;

		public static int Count {
			get {
				return layerList.Count;
			}
		}

		public static Layer AddLayer() {
			if (Count < MaxCount) {
				Layer layer = new Layer();
				layer.IndexY = LayerManager.Count;
				layer.mainText.Text = defaultText;
				layerList.Add(layer);

				if (LayerResize()) {
					RenderAllLayers();
				} else {
					Renderer.AddRenderQueue(layer);
				}
				Renderer.AddRenderQueue(MainForm.instance.mainUI);

				if(Count == MaxCount) {
					MainForm.instance.addBtn.Image = Resources.Root_PlusBtn_disabled;
				}
				return layer;
			} else {
				//토스트메세지 띄우기
				return null;
			}
		}
		public static void LoadLayer(string mainText, string memoText, float indexY, DateTime targetDate, int colorHue) {
			Layer layer = LayerManager.AddLayer();
			layer.mainText.Text = mainText;
			layer.memoText.Text = memoText;
			layer.IndexY = indexY - 0.5f;
			layer.TargetDate = targetDate;
			layer.SetColorHue(colorHue);
			Sort();
			layer.mainText.UpdateWidth();

		}
		public static void RemoveLayer(Layer layer) {
			if (layerList.Contains(layer)) {
				if(Count == MaxCount) {
					MainForm.instance.addBtn.Image = Resources.Root_PlusBtn;
				}
				layerList.Remove(layer);
				layer.group.Parent.Controls.Remove(layer.group);


				LayerResize();
				Sort();
				RenderAllLayersDirect();
				Renderer.AddRenderQueue(MainForm.instance.mainUI);
			}
		}
		public static void ClearLayer() {
			for(int i=0; i<layerList.Count; i++) {
				layerList[i].group.Parent.Controls.Remove(layerList[i].group);
				layerList[i].group.Dispose();
			}
			layerList.Clear();

			LayerResize();
			Renderer.AddRenderQueue(MainForm.instance.mainUI);
		}

		public static void FocusLayer(Layer layer) {
			LayerManager.focusedLayer = layer;
			Renderer.AddRenderQueue(layer);
			layer.Render();
			layer.mainText.UpdateWidth();
		}
		public static void UnFocusLayer() {
			if (LayerManager.focusedLayer != null) {
				Layer layer = LayerManager.focusedLayer;
				LayerManager.focusedLayer = null;
				Renderer.AddRenderQueue(layer);
				layer.Render();
				layer.UpdateFont();
			}
		}

		//전체 렌더
		public static void RenderAllLayers() {
			for(int i=0; i<layerList.Count; i++) {
				Renderer.AddRenderQueue(layerList[i]);
			}
		}
		public static void RenderAllLayersDirect() {
			float speedBuffer = Layer.speed;
			Layer.speed = 1f;
			for(int i=0; i<layerList.Count; i++) {

				layerList[i].Render();
			}
			Layer.speed = speedBuffer;
		}
		public static void UpdateFont() {
			for(int i=0; i<layerList.Count; i++) {
				layerList[i].UpdateFont();
			}
			MainForm.instance.layerNamer?.UpdateFont();
		}
		public static void Sort() {
			layerList.Sort((Layer A, Layer B) => {
				if (A.IndexY < B.IndexY) {
					return -1;
				} else if (A.IndexY > B.IndexY) {
					return 1;
				} else {
					return 0;
				}
			});
			for (int i = 0; i < Count; i++) {
				layerList[i].IndexY = i;
			}
			RenderAllLayers();
		}
		public static void CancelTask() {
			for(int i=0; i<layerList.Count; i++) {
				layerList[i].CancelTask();
			}
		}

		private static float compareGridHeight;
		private static bool LayerResize() {
			switch (Count) {
				case 0:
				case 1:
				case 2:
					Layer.gridHeight = 2.3f;
					break;
				case 3:
					Layer.gridHeight = 1.8f;
					break;
				case 4:
					Layer.gridHeight = 1.6f;
					break;
				case 5:
					Layer.gridHeight = 1.4f;
					break;
				case 6:
					Layer.gridHeight = 1.3f;
					break;
				case 7:
					Layer.gridHeight = 1.1f;
					break;
				case 8:
					Layer.gridHeight = 1f;
					break;
				default:
					Layer.gridHeight = 1f;
					break;
			}
			if(compareGridHeight != Layer.gridHeight) {
				compareGridHeight = Layer.gridHeight;
				return true;
			} else {
				return false;
			}
		}
	}
}
