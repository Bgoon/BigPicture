using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using BigPicture.Layers;

namespace BigPicture.Forms {
	public abstract class UIObject {
		/// <summary>
		/// 렌더링이 필요없어지면 false를 반환한다.
		/// </summary>
		public abstract bool Render();
	}
	public static class Renderer {
		public static bool IsRunning {
			get {
				return isRunning;
			}
		}
		private static bool isRunning;
		private static List<UIObject> renderQueue = new List<UIObject>();
		private static Stopwatch stopwatch = new Stopwatch();

		public static float targetFPS = 70f;
		private static float lag;

		public static async void Start() {
			if(isRunning) {
				return;
			}
			isRunning = true;

			LayerManager.RenderAllLayersDirect();

			float frameDelay = 0f;
			for (; ; ) {
				if(lag < 0.2f) {
					await Task.Delay((int)frameDelay);
				}
				if(!isRunning) {
					break;
				}
				stopwatch.Reset();
				stopwatch.Start();

				Render();

				stopwatch.Stop();
				frameDelay = Math.Max(0, 1000 / targetFPS - stopwatch.ElapsedMilliseconds);
				lag = frameDelay / 1000 / targetFPS;

				Layer.speed = Option.Display.Speed * (lag + 1);
			}
		}
		private static void Render() {
			for (int i = 0; i < renderQueue.Count; i++) {
				UIObject obj = renderQueue[i];

				if (!obj.Render()) {
					renderQueue.Remove(obj);
				}
			}
		}
		public static void Stop() {
			isRunning = false;
		}

		public static void AddRenderQueue(UIObject obj) {
			if (!renderQueue.Contains(obj)) {
				renderQueue.Add(obj);
			}
		}
		public static void RemoveRenderQueue(UIObject obj) {
			if(renderQueue.Contains(obj)) {
				renderQueue.Remove(obj);
			}
		}
		public static void ClearRenderQueue(UIObject obj) {
			renderQueue.Clear();
		}

	}
}
