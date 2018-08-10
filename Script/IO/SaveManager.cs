using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Win32;
#if Client
using BigPicture.Layers;
#endif

namespace BigPicture.IO {
	public static class SaveManager {
		public static DirectoryInfo documentDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
		public static DirectoryInfo dataDir = new DirectoryInfo(Path.Combine(documentDir.FullName, "빅픽처"));
		public static DirectoryInfo programDir = new DirectoryInfo(Application.StartupPath);
#if Client
		public static DirectoryInfo programDataDir = new DirectoryInfo(Path.Combine(programDir.FullName, "BigPicture_Data"));
		public static FileInfo updaterInfo = new FileInfo(Path.Combine(programDataDir.FullName, "Updater.exe"));
#else
		public static FileInfo FileListInfo {
			get {
				return new FileInfo(Path.Combine(DestDir.FullName, "BigPicture_Data", "filelist.list"));
			}
		}
		public static FileInfo MainProgramInfo {
			get {
				return new FileInfo(Path.Combine(DestDir.FullName, "BigPicture.exe"));
			}
		}
		public static DirectoryInfo DestDir {
			get {
				switch (AppInfo.programType) {
					default:
					case ProgramType.Updater:
						return programDir.Parent;
					case ProgramType.Setup:
						return new DirectoryInfo(Path.Combine(programDir.FullName, "빅픽처"));
				}
			}
		}
#endif
		public static FileInfo saveDataInfo = new FileInfo(Path.Combine(dataDir.FullName, "saveData.data"));
		public static FileInfo saveDataBackupInfo = new FileInfo(saveDataInfo.FullName + ".backup");
		public static FileInfo optionInfo = new FileInfo(Path.Combine(dataDir.FullName, "option.config"));
		public static FileInfo clientInfo = new FileInfo(Path.Combine(dataDir.FullName, "client.zip"));
		private static object saveLocker = new object();

		public static FileInfo ToReleative(this FileInfo fileInfo) {
#if Client
			return new FileInfo(fileInfo.FullName.Replace(SaveManager.programDir.FullName, ""));
#else
			return new FileInfo(fileInfo.FullName.Replace(DestDir.FullName, ""));
#endif
		}

		public static string ToReleative(this string filePath) {
#if Client
			return filePath.Replace(SaveManager.programDir.FullName, "");
#else
			return filePath.Replace(DestDir.FullName, "");
#endif
		}

		private static string GetStartSep(string sep) {
			StringBuilder sepBuilder = new StringBuilder();
			sepBuilder.Append("<");
			sepBuilder.Append(sep);
			sepBuilder.Append(">");
			return sepBuilder.ToString();
		}
		private static string GetEndSep(string sep) {
			StringBuilder sepBuilder = new StringBuilder();
			sepBuilder.Append("</");
			sepBuilder.Append(sep);
			sepBuilder.Append(">");
			return sepBuilder.ToString();
		}
		private static string GetStartString(string Var) {
			return Var + "=\"" + stringSep;
		}
		private static string GetEndString() {
			return stringSep + "\"" + endKeyword;
		}
		private static string GetStartVar(string Var) {
			return Var + '=';
		}
		
		public static void AppendString(this StringBuilder builder, string name, string value) {
			builder.Append(GetStartString(name));
			builder.Append(value.Replace(stringSep, ' '));
			builder.Append(GetEndString());
			builder.Append(Environment.NewLine);
		}
		public static void AppendVar(this StringBuilder builder, string name, string value) {
			builder.Append(GetStartVar(name));
			builder.Append(value);
			builder.Append(endKeyword);
			builder.Append(Environment.NewLine);
		}
		public static T LoadVar<T>(this string doc, string name) {
			var converter = TypeDescriptor.GetConverter(typeof(T));
			if (converter != null) {
				return (T)converter.ConvertFromString(StringParser.Parse(doc, GetStartVar(name), endKeyword));
			}
			return default(T);
		}
		public static string LoadString(this string doc, string name) {
			if (doc.Contains(name)) {
				return StringParser.Parse(doc, GetStartString(name), GetEndString());
			} else {
				throw new Exception("변수를 찾을 수 없습니다.");
			}
		}

		const char stringSep = '\b';
		static string endKeyword = ";";
		public static void Initialize() {
			dataDir.Create();
		}
#if Client
		public static class Data {
			private const string
				VerText20 = "Version2.0",
				EndBlock = "EndData";
			private const string
					dataSep = "BigPictureData",
					scheduleSep = "Schedule";
			private const string
				textVar = "Text",
				memoVar = "Memo",
				showMemoVar = "ShowMemo",
				priolityVar = "IndexY",
				targetDateVar = "TargetDate",
				colorVar = "ColorHue";

			public static void Save() {
				Save(saveDataInfo);
			}
			public static bool Load() {
				return Load(saveDataInfo);
			}

			public static void Save(FileInfo fileInfo) {
				string data = "";
				try {
					lock (saveLocker) {
						if (!dataDir.Exists) {
							dataDir.Create();
						}
						StringBuilder dataBuilder = new StringBuilder();
						//버전 기록
						dataBuilder.Append(VerText20);
						dataBuilder.Append(Environment.NewLine);
						//수집 시작
						dataBuilder.Append(GetStartSep(dataSep));
						dataBuilder.Append(Environment.NewLine);
						for (int i = 0; i < LayerManager.Count; i++) {
							Layer layer = LayerManager.layerList[i];
							dataBuilder.Append(GetStartSep(scheduleSep));
							dataBuilder.Append(Environment.NewLine);

							dataBuilder.AppendString(textVar, layer.mainText.Text);
							dataBuilder.AppendString(memoVar, layer.memoText.Text);
							dataBuilder.AppendVar(showMemoVar, layer.showMemo.ToString());
							dataBuilder.AppendVar(priolityVar, ((double)layer.IndexY).ToString());
							dataBuilder.AppendVar(targetDateVar, layer.TargetDate.ToOADate().ToString());
							dataBuilder.AppendVar(colorVar, ((int)(layer.BGColor.GetHue())).ToString());

							dataBuilder.Append(GetEndSep(scheduleSep));
							dataBuilder.Append(Environment.NewLine);
						}
						dataBuilder.Append(GetEndSep(dataSep));
						dataBuilder.Append(Environment.NewLine);
						dataBuilder.Append(EndBlock);
						//수집 완료

						data = dataBuilder.ToString();
						dataBuilder.Clear();
						MaskBackup();

						//덮어쓰기
						using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Create, FileAccess.Write)) {
							using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8)) {
								streamWriter.Write(data);
								streamWriter.Flush();
							}
						}

						MaskBackup();
					}
				} catch(Exception e) {
				}
			}
			public static bool Load(FileInfo fileInfoOrigin) {
				FileInfo fileInfo;
				try {
					lock (saveLocker) {
						if (saveDataBackupInfo.Exists) {
							fileInfo = saveDataBackupInfo;
						} else if (saveDataInfo.Exists) {
							fileInfo = fileInfoOrigin;
						} else {
							return false;
						}
						RemoveStack.stack.Clear();

						string doc;
						using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read)) {
							using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8)) {
								doc = streamReader.ReadToEnd();
							}
						}
						//버전 읽기
						int version = 10;
						if (doc.Contains(VerText20)) {
							version = 20;
						}
						//신뢰성 확인
						switch(version) {
							case 20:
								if (!doc.Contains(EndBlock)) {
									return false;
								}
								break;
						}

						//파싱 시작
						if (!doc.Contains(GetStartSep(dataSep))) {
							return false;
						}
						doc = StringParser.Parse(doc, GetStartSep(dataSep), GetEndSep(dataSep));

						while (doc.Contains(GetStartSep(scheduleSep))) {
							string block = StringParser.Parse(doc, GetStartSep(scheduleSep), GetEndSep(scheduleSep));
							doc = StringParser.Parse(doc, GetEndSep(scheduleSep));

							//block에서 변수를 읽어들이고 적용
							Layer layer = LayerManager.AddLayer();
							if (layer == null) {
								continue;
							}
							if (block.Contains(textVar))
								layer.mainText.Text = block.LoadString(textVar);
							if (block.Contains(memoVar))
								layer.memoText.Text = block.LoadString(memoVar);
							if (block.Contains(showMemoVar)) {
								layer.showMemo = block.LoadVar<bool>(showMemoVar);
								layer.memoText.Select(0, 0);
							}
							if (block.Contains(priolityVar))
								layer.IndexY = (float)(block.LoadVar<double>(priolityVar));
							if (block.Contains(targetDateVar))
								layer.TargetDate = DateTime.FromOADate(block.LoadVar<double>(targetDateVar));
							if (block.Contains(colorVar))
								layer.SetColorHue(block.LoadVar<int>(colorVar));

							layer.Render();
							layer.mainText.UpdateWidth();
						}
						//파싱 완료
						return true;
					}
				} catch(Exception e) {
					return false;
				}
			}

			private static void MaskBackup() {
				if (saveDataInfo.Exists) {
					if (saveDataBackupInfo.Exists) {
						saveDataBackupInfo.Delete();
					}
					saveDataInfo.CopyTo(saveDataBackupInfo.FullName);
				}
			}
		}
		public static class Option {
			const string 
				dataSep = "BigPictureOption";
			const string
				startProc = "StartupProcess",
				topMost = "AlwaysTopWindow",
				alert = "Alert",
				alertSound = "AlertSound",
				alert_com = "Alert_Complete",
				alert_1h = "Alert_1Hour",
				alert_1d = "Alert_1Day",

				opacity = "Opacity",
				fontValue = "FontValue",
				fontName = "FontName",
				fontStyle = "FontStyle",
				fontSize = "FontSize",
				animSpeed = "AnimationSpeed",
				widthOffset = "WidthOffset",
				dateType = "DateType";

			public static void Save() {
				Save(optionInfo);
			}
			public static bool Load() {
				return Load(optionInfo);
			}
			public static void Save(FileInfo fileInfo) {
				lock (saveLocker) {
					if (!dataDir.Exists) {
						dataDir.Create();
					}
					StringBuilder dataBuilder = new StringBuilder();
					//수집 시작
					dataBuilder.Append(GetStartSep(dataSep));
					dataBuilder.Append(Environment.NewLine);

					dataBuilder.AppendVar(startProc, BigPicture.Option.System.startProcess.ToString());
					dataBuilder.AppendVar(topMost, BigPicture.Option.System.topMost.ToString());
					dataBuilder.AppendVar(alert, BigPicture.Option.System.alert.ToString());
					dataBuilder.AppendVar(alertSound, BigPicture.Option.System.alertSound.ToString());
					dataBuilder.AppendVar(alert_com, BigPicture.Option.System.alert_com.ToString());
					dataBuilder.AppendVar(alert_1h, BigPicture.Option.System.alert_1h.ToString());
					dataBuilder.AppendVar(alert_1d, BigPicture.Option.System.alert_1d.ToString());

					dataBuilder.AppendVar(opacity, BigPicture.Option.Display.opacity.ToString());
					dataBuilder.AppendVar(fontValue, BigPicture.Option.Display.fontValue.ToString());
					dataBuilder.AppendString(fontName, BigPicture.Option.Display.font.FontFamily.Name);
					dataBuilder.AppendVar(fontStyle, ((int)BigPicture.Option.Display.font.Style).ToString());
					dataBuilder.AppendVar(fontSize, BigPicture.Option.Display.fontSize.ToString());
					dataBuilder.AppendVar(animSpeed, BigPicture.Option.Display.animSpeed.ToString());
					dataBuilder.AppendVar(widthOffset, BigPicture.Option.Display.widthOffset.ToString());
					dataBuilder.AppendVar(dateType, BigPicture.Option.Display.dateType.ToString());
					
					dataBuilder.Append(GetEndSep(dataSep));
					//수집 완료
					using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Create, FileAccess.Write)) {
						using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8)) {
							streamWriter.Write(dataBuilder.ToString());
							streamWriter.Flush();
						}
					}
				}
			}
			public static bool Load(FileInfo fileInfo) {
				lock (saveLocker) {
					if (!fileInfo.Exists) {
						return false;
					}
					string doc;
					using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read)) {
						using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8)) {
							doc = streamReader.ReadToEnd();
						}
					}
					//파싱 시작
					if(!doc.Contains(GetStartSep(dataSep))) {
						return false;
					}
					doc = StringParser.Parse(doc, GetStartSep(dataSep), GetEndSep(dataSep));

					if (doc.Contains(GetStartVar(startProc)))
						BigPicture.Option.System.startProcess = doc.LoadVar<bool>(startProc);
					if (doc.Contains(GetStartVar(topMost)))
						BigPicture.Option.System.topMost = doc.LoadVar<bool>(topMost);
					if (doc.Contains(GetStartVar(alert)))
						BigPicture.Option.System.alert = doc.LoadVar<bool>(alert);
					if (doc.Contains(GetStartVar(alertSound)))
						BigPicture.Option.System.alertSound = doc.LoadVar<bool>(alertSound);
					if (doc.Contains(GetStartVar(alert_com)))
						BigPicture.Option.System.alert_com = doc.LoadVar<bool>(alert_com);
					if (doc.Contains(GetStartVar(alert_1h)))
						BigPicture.Option.System.alert_1h = doc.LoadVar<bool>(alert_1h);
					if (doc.Contains(GetStartVar(alert_1d)))
						BigPicture.Option.System.alert_1d = doc.LoadVar<bool>(alert_1d);


					if (doc.Contains(GetStartVar(opacity)))
						BigPicture.Option.Display.opacity = doc.LoadVar<int>(opacity);
					if (doc.Contains(GetStartVar(fontValue)))
						BigPicture.Option.Display.fontValue = doc.LoadVar<int>(fontValue);
					if (doc.Contains(GetStartVar(fontStyle)) && doc.Contains(fontName)) {
						int fontStyleValue = doc.LoadVar<int>(fontStyle);
						BigPicture.Option.Display.font = new System.Drawing.Font(doc.LoadString(fontName), 12, (FontStyle)fontStyleValue);
					}
					if (doc.Contains(GetStartVar(fontSize)))
						BigPicture.Option.Display.fontSize = doc.LoadVar<int>(fontSize);
					if (doc.Contains(GetStartVar(widthOffset)))
						BigPicture.Option.Display.widthOffset = doc.LoadVar<int>(widthOffset);
					if (doc.Contains(GetStartVar(dateType)))
						BigPicture.Option.Display.dateType = doc.LoadVar<int>(dateType);
					if (doc.Contains(GetStartVar(animSpeed)))
						BigPicture.Option.Display.animSpeed = doc.LoadVar<int>(animSpeed);

					BigPicture.Option.OnOptionChanged();
					//파싱 완료
					return true;
				}
			}
		}
		public static class Startup {
			public static void Set(string AppName, bool enable) {
				const string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
				RegistryKey startupKey = Registry.CurrentUser.OpenSubKey(runKey);

				if (enable) {
					Set(AppName, false);
					startupKey = Registry.CurrentUser.OpenSubKey(runKey, true);
					startupKey.SetValue(AppName, "\"" + Application.ExecutablePath.ToString().Replace('/', '\\') + "\" /autostart");
					startupKey.Close();
				} else {
					startupKey = Registry.CurrentUser.OpenSubKey(runKey, true);
					startupKey.DeleteValue(AppName, false);
					startupKey.Close();
				}
			}
		}
#endif
	}
}
