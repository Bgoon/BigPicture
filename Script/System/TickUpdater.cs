using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BigPicture.Layers;
using BigPicture.IO;
using BigPicture.Forms;

namespace BigPicture {
	public class TickUpdater {
		private bool isRunning;
		private int delaySec = 30;
		private Timer timer = new Timer();
		private Queue<Layer> alert_1hQueue, alert_1dQueue, alert_comQueue;

		public TickUpdater() {
			Initialize();
		}
		private void Initialize() {
			Update();
			alert_1dQueue = new Queue<Layer>();
			alert_1hQueue = new Queue<Layer>();
			alert_comQueue = new Queue<Layer>();
		}

		public void Start() {
			if (!isRunning) {
				isRunning = true;
				Update();
			}
		}
		public void Stop() {
			if(isRunning)
				isRunning = false;
		}
		private async void Update() {
			for (; ; ) {
				if (!isRunning)
					return;

				CheckTime();
				SaveManager.Data.Save();

				await Task.Delay(delaySec * 1000);
			}
		}


		private void CheckTime() {
			if (Option.System.alert) {
				bool alert = false;
				for (int i = 0; i < LayerManager.layerList.Count; i++) {
					Layer layer = LayerManager.layerList[i];

					layer.Render();

					if (layer.isComplete) {
						if (Option.System.alert_com) {
							if (layer.alert_complete == false) { //마감 시
								layer.alert_1h = layer.alert_1d = layer.alert_complete = true;
								if (layer.isPeriodPressed == false) {
									alert_comQueue.Enqueue(layer);
								}
							}
						}
					} else if (((layer.PeriodMinutes <= 60 && layer.PeriodHours == 0) || (layer.PeriodMinutes == 0 && layer.PeriodHours == 1)) && layer.PeriodDays == 0) {
						if (Option.System.alert_1h) { //1시간 남았을 때
							if (layer.alert_1h == false) {
								layer.alert_1h = layer.alert_1d = true;
								if (layer.isPeriodPressed == false) {
									alert_1hQueue.Enqueue(layer);
								}
							}
						}
					} else if (layer.PeriodDays == 0) { //1일 남았을 때
						if (Option.System.alert_1d) {
							if (layer.alert_1d == false) {
								layer.alert_1d = true;
								if (layer.isPeriodPressed == false) {
									alert_1dQueue.Enqueue(layer);
								}
							}
						}
					}
				}
				if(alert_comQueue.Count > 0) {
					alert = true;
					Layer layer = alert_comQueue.Dequeue();
					if (alert_comQueue.Count > 0) {
						Alert.Show(layer.mainText.Text, alert_comQueue.Count);
					} else {
						Alert.Show(layer.mainText.Text);
					}
					alert_comQueue.Clear();
				}
				if(alert_1hQueue.Count > 0) {
					alert = true;
					Layer layer = alert_1hQueue.Dequeue();
					if (alert_1hQueue.Count > 0) {
						Alert.Show(layer.mainText.Text, alert_1hQueue.Count, "1", "시간 이내");
					} else {
						Alert.Show(layer.mainText.Text, "1", "시간 이내");
					}
					alert_1hQueue.Clear();
				}
				if(alert_1dQueue.Count > 0) {
					alert = true;
					Layer layer = alert_1dQueue.Dequeue();
					if (alert_1dQueue.Count > 0) {
						Alert.Show(layer.mainText.Text, alert_1dQueue.Count, "1", "일 이내");
					} else {
						Alert.Show(layer.mainText.Text, "1", "일 이내");
					}
					alert_1dQueue.Clear();
				}
				if (Option.System.alertSound) {
					if (alert) {
						Alert.Sound();
					}
				}
			} else {
				LayerManager.RenderAllLayers();
			}
			
		}

	}
}
