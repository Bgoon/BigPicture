using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

using BigPicture.Forms;
using BigPicture.Graphic;
using BigPicture.Layers;
using BigPicture.IO;
using BigPicture.Properties;
using System.Runtime.InteropServices;

namespace BigPicture {
	public partial class MainForm : Form {
		public static MainForm instance;

		OptionForm option;
		public Tutorial tutorial;
		public ProgramInfo programInfo;

		public MainUI mainUI;
		public TrashArea trashArea;
		public LayerNamer layerNamer;
		public DragEffector dragEffector;
		private DetailPanel detailPanel;

		private TickUpdater tickUpdater = new TickUpdater();
		private MouseUpdater mouseUpdater = new MouseUpdater();
		
		public MainForm() {
			InitializeComponent();
			Initialize();
			
			Renderer.Start();
		}
		private void Initialize() {
			instance = this;
			FontManager.Initialize();
			SaveManager.Initialize();
			SaveManager.Option.Load();

			UpdateCheck.StartCheck();

			mainUI = new MainUI();
			trashArea = new TrashArea();
			TrashArea.instance = trashArea;
			layerNamer = new LayerNamer();
			Controls.Add(layerNamer);
			layerNamer.BringToFront();

			dragEffector = new DragEffector();
			dragEffector.Visible = false;
			Controls.Add(dragEffector);
			dragEffector.BringToFront();

			detailPanel = new DetailPanel();
			detailPanel.Show(this);
			detailPanel.Hide();

			bool loadSuccess = true;
			if(!SaveManager.Data.Load(SaveManager.saveDataInfo)) {
				if(!SaveManager.Data.Load(SaveManager.saveDataBackupInfo)) {
					loadSuccess = false;
				}
			}
			if (!loadSuccess | LayerManager.Count == 0) {
				//초기화
				LayerManager.ClearLayer();
				LayerManager.AddLayer();
				LayerManager.AddLayer();
			}
			layerNamer.UpdateFont();

			Renderer.AddRenderQueue(mainUI);

			tickUpdater.Start();
			mouseUpdater.Start();
			
			if(!SaveManager.optionInfo.Exists) {
				ShowTutorial();
			}
		}
		private void Exit() {
			trayIcon.Visible = false;
			AppInfo.isExitRequest = true;
			Application.Exit();
		}
		private void OnDestroy(object sender, FormClosingEventArgs e) {
			if (!AppInfo.isExitRequest) {
				e.Cancel = true;
				Hide();
				return;
			}
			
			tickUpdater.Stop();
			mouseUpdater.Stop();
			LayerManager.Sort();
			SaveManager.Data.Save();
			SaveManager.Option.Save();
			Renderer.Stop();
		}

		//이벤트
		protected override void WndProc(ref Message m) {
			switch (m.Msg) {
				case 0x11: //시스템 종료시
				case ProcessProtocol.WM_ExitRequest:
					AppInfo.isExitRequest = true;
					Exit();
					break;
			}
			base.WndProc(ref m);
		}

		private void OnTrayClick_열기(object sender, EventArgs e) {
			if (Visible) {
				Hide();
			} else {
				Show();
				NativeFunc.SetForegroundWindow(Handle);
			}
		}
		private void OnTrayClick_종료(object sender, EventArgs e) {
			Exit();
		}
		private void OnTrayClick_설정(object sender, EventArgs e) {
			if (OptionForm.instance == null) {
				option = new OptionForm();
			}
			if (option.Visible == false) {
				option.Show(this);
			}
		}
		private void OnTrayClick_업데이트(object sender, EventArgs e) {
			UpdateCheck.Check(true);
		}
		private void OnTrayClick_정보(object sender, EventArgs e) {
			if (ProgramInfo.instance == null) {
				programInfo = new ProgramInfo();
			}
			if (programInfo.Visible == false) {
				programInfo.Show(this);
			}
		}
		private void OnTrayClick_사용방법(object sender, EventArgs e) {
			ShowTutorial();
		}
		private void OnTrayMenu(object sender, CancelEventArgs e) {
			trayMenu_열기.Text = !Visible ? "열기" : "숨기기";
		}
		private void ShowTutorial() {
			if (Tutorial.instance == null) {
				tutorial = new Tutorial();
			}
			if (tutorial.Visible == false) {
				tutorial.Show(this);
				tutorial.Reset();
			}
		}
	}
}
