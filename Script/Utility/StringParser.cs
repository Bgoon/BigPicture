using System;
using System.Collections.Generic;
using System.Text;

public static class StringParser {
	public static string Parse(string doc, string startKeyword, string endKeyword) {
		return Parse(doc, startKeyword, endKeyword, 1);
	}
	public static string Parse(string doc, string startKeyword, string endKeyword, int startCount) {
		for (int i = 0; i < startCount; i++) {
			int startPos = doc.IndexOf(startKeyword) + startKeyword.Length;
			doc = doc.Substring(startPos, doc.Length - startPos);
		}
		int endPos = doc.IndexOf(endKeyword);
		return doc.Substring(0, endPos);
	}
	public static string Parse(string doc, string startKeyword) {
		return Parse(doc, startKeyword, 1);
	}
	public static string Parse(string doc, string startKeyword, int startCount) {
		for (int i = 0; i < startCount; i++) {
			int startPos = doc.IndexOf(startKeyword) + startKeyword.Length;
			doc = doc.Substring(startPos, doc.Length - startPos);
		}
		return doc;
	}

}