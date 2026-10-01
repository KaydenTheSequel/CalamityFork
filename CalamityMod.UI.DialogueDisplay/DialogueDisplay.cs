using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CalamityMod.UI.DialogueDisplay.DisplayEffects;
using CalamityMod.UI.DialogueDisplay.TextEffects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;
using Terraria.UI.Chat;

namespace CalamityMod.UI.DialogueDisplay;

public class DialogueDisplay : UIElement
{
	public static readonly Dictionary<string, SoundStyle> DialogueSounds = new Dictionary<string, SoundStyle>
	{
		{
			"Amidias",
			SoundID.NPCHit1
		},
		{
			"Otonilou",
			SoundID.NPCHit25
		}
	};

	public int DialogueTimer;

	public Vector2 Position;

	public bool SwitchingPage;

	public bool ProgressDialogue;

	public bool ClosingDialogue;

	public bool ScreenLocked;

	[CompilerGenerated]
	private Vector2 _003CTextSize_003Ek__BackingField;

	[CompilerGenerated]
	private Vector2 _003CSizeOffsetFromStart_003Ek__BackingField;

	internal int SwitchCounter;

	internal DialoguePage DialoguePage;

	internal DisplayEffect DisplayEffects;

	internal string Text;

	private int TextTimer;

	internal int textIndex;

	internal int Uptime;

	internal Asset<DynamicSpriteFont> Font;

	internal Dictionary<int, (float IndexOffset, float gradiantSpeed, string[] hexcodes)> UniqueColors;

	internal Dictionary<int, (float IndexOffset, float gradiantSpeed, string[] hexcodes)> UniqueBorderColors;

	internal Dictionary<int, float> Pauses;

	internal Dictionary<int, List<(TextEffect Effect, float[] args)>> TextEffects;

	internal Dictionary<int, Vector2> UniqueScales;

	internal List<int> LineBreakIndexes;

	private DialogueCharacterData[] CharacterData;

	private Color BaseColor;

	private Color BaseBorderColor;

	internal bool Crawling;

	private int storedDelay;

	private bool lockDelay;

	private float WrapWidth;

	public Vector2 TextSize
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CTextSize_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CTextSize_003Ek__BackingField = value;
		}
	}

	public Vector2 SizeOffsetFromStart
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CSizeOffsetFromStart_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CSizeOffsetFromStart_003Ek__BackingField = value;
		}
	}

	public bool Switching
	{
		get
		{
			if (!SwitchingPage)
			{
				return ClosingDialogue;
			}
			return true;
		}
	}

	public DialogueDisplay(DialoguePage textData, DisplayEffect displayEffects, int startPage = 0, bool screenLocked = false, float wrapWidth = -1f, Asset<DynamicSpriteFont>? font = null)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		Position = Vector2.Zero;
		ProgressDialogue = true;
		Text = "";
		UniqueColors = new Dictionary<int, (float, float, string[])>();
		UniqueBorderColors = new Dictionary<int, (float, float, string[])>();
		Pauses = new Dictionary<int, float>();
		TextEffects = new Dictionary<int, List<(TextEffect, float[])>>();
		UniqueScales = new Dictionary<int, Vector2>();
		LineBreakIndexes = new List<int>();
		BaseColor = Color.White;
		BaseBorderColor = Color.Black;
		Crawling = true;
		WrapWidth = -1f;
		base._002Ector();
		DisplayEffects = displayEffects;
		ScreenLocked = screenLocked;
		DialoguePage = textData;
		DisplayEffects = displayEffects;
		Font = font ?? FontAssets.MouseText;
		WrapWidth = wrapWidth;
	}

	public override void OnActivate()
	{
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_074f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_08be: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		Text = "";
		UniqueColors = new Dictionary<int, (float, float, string[])>();
		UniqueBorderColors = new Dictionary<int, (float, float, string[])>();
		Pauses = new Dictionary<int, float>();
		TextEffects = new Dictionary<int, List<(TextEffect, float[])>>();
		UniqueScales = new Dictionary<int, Vector2>();
		if (DialoguePage.Event != null || Font == null || !Font.IsLoaded)
		{
			return;
		}
		int fullLength = 0;
		List<string> lines = new List<string>();
		for (int i = 0; i < DialoguePage.Lines.Length; i++)
		{
			string fullLine = DialoguePage.Lines[i];
			FindEffects(ref fullLine, fullLength);
			string text = fullLine;
			if (text[text.Length - 1] != ' ')
			{
				fullLine += " ";
			}
			lines.Add(fullLine);
			fullLength += fullLine.Length;
		}
		if (WrapWidth != -1f)
		{
			for (int j = 0; j < lines.Count; j++)
			{
				string line = lines[j];
				string text2 = line;
				if (text2[text2.Length - 1] == ' ')
				{
					line = line.Remove(line.Length - 1, 1);
				}
				int finalIndex = 0;
				if (!(MeasureString(line, Font.Value).X > WrapWidth))
				{
					continue;
				}
				string yoinked = "";
				do
				{
					finalIndex = line.LastIndexOf(' ');
					if (finalIndex < line.Length - 1)
					{
						finalIndex++;
					}
					yoinked = line.Substring(finalIndex) + yoinked;
					line = line.Remove(finalIndex);
				}
				while (MeasureString(line, Font.Value).X > WrapWidth);
				lines[j] = line;
				if (yoinked[0] == ' ')
				{
					yoinked = yoinked.Remove(0, 1);
				}
				if (j >= lines.Count - 1)
				{
					lines.Add(yoinked);
				}
				else
				{
					lines[j + 1] = yoinked + lines[j + 1];
				}
			}
		}
		fullLength = 0;
		int[] lineLengths = new int[lines.Count];
		LineBreakIndexes.Clear();
		for (int k = 0; k < lines.Count; k++)
		{
			lineLengths[k] = lines[k].Length;
			Text += lines[k];
			fullLength += lines[k].Length;
			LineBreakIndexes.Add(fullLength);
		}
		if (DialoguePage.BaseColor != null)
		{
			BaseColor = DialogueDisplaySystem.GetColorFromHex(DialoguePage.BaseColor);
		}
		if (DialoguePage.BaseBorderColor != null)
		{
			BaseBorderColor = DialogueDisplaySystem.GetColorFromHex(DialoguePage.BaseBorderColor);
		}
		else
		{
			BaseBorderColor = BaseColor * DialoguePage.BorderDarkening;
			((Color)(ref BaseBorderColor)).A = byte.MaxValue;
		}
		CharacterData = new DialogueCharacterData[Text.Length];
		for (int l = 0; l < Text.Length; l++)
		{
			int m = 0;
			int summedLength = 0;
			for (; m < lineLengths.Length; m++)
			{
				summedLength += lineLengths[m];
				if (l < summedLength)
				{
					break;
				}
			}
			CharacterData[l] = new DialogueCharacterData(l, Text.Length, m);
		}
		textIndex = 0;
		Crawling = true;
		TextTimer = 0;
		storedDelay = 0;
		lockDelay = false;
		DialogueTimer = 0;
		Uptime = 0;
		Vector2 zero = Vector2.Zero;
		bool newLine = true;
		float textWidth = 0f;
		float highestFirstLineYScale = 1f;
		for (int n = 0; n < Text.Length && Text[n] != '\n'; n++)
		{
			if (UniqueScales.TryGetValue(n, out var uniqueScale) && uniqueScale.Y > highestFirstLineYScale)
			{
				highestFirstLineYScale = uniqueScale.Y;
			}
		}
		SizeOffsetFromStart = new Vector2(8f, 16f * highestFirstLineYScale);
		for (int num = 0; num < Text.Length; num++)
		{
			char c = Text[num];
			Vector2 scale = Vector2.One;
			if (UniqueScales.TryGetValue(num, out var result))
			{
				scale = result;
			}
			else if (DialoguePage.TextScale != -1)
			{
				scale *= (float)DialoguePage.TextScale;
			}
			switch (c)
			{
			case '\n':
			{
				if (zero.X > textWidth)
				{
					textWidth = zero.X;
				}
				zero.X = 0f;
				float highestYscale = 1f;
				for (int num2 = num + 1; num2 < Text.Length && Text[num2] != '\n'; num2++)
				{
					if (UniqueScales.TryGetValue(num2, out var uniqueScale2) && uniqueScale2.Y > highestYscale)
					{
						highestYscale = uniqueScale2.Y;
					}
				}
				zero.Y += (float)Font.Value.LineSpacing * highestYscale;
				newLine = true;
				continue;
			}
			case '\r':
				continue;
			}
			if (LineBreakIndexes.Contains(num))
			{
				if (zero.X > textWidth)
				{
					textWidth = zero.X;
				}
				zero.X = 0f;
				float highestYscale2 = 1f;
				for (int num3 = num + 1; num3 < Text.Length && Text[num3] != '\n'; num3++)
				{
					if (UniqueScales.TryGetValue(num3, out var uniqueScale3) && uniqueScale3.Y > highestYscale2)
					{
						highestYscale2 = uniqueScale3.Y;
					}
				}
				zero.Y += (float)Font.Value.LineSpacing * highestYscale2;
				newLine = true;
			}
			SpriteCharacterData spriteData = Font.Value.SpriteCharacters[c];
			Vector3 kerning = spriteData.Kerning;
			Rectangle padding = spriteData.Padding;
			if (newLine)
			{
				kerning.X = Math.Max(kerning.X, 0f);
			}
			else
			{
				zero.X += Font.Value.CharacterSpacing * scale.X;
			}
			zero.X += kerning.X * scale.X;
			Vector2 position = zero + spriteData.Glyph.Size() * 0.5f;
			position.X += (float)padding.X * scale.X;
			position.Y += (float)padding.Y * scale.Y;
			CharacterData[num].TextPosition = position - Vector2.UnitY * scale.Y * (float)Font.Value.LineSpacing * 0.5f;
			zero.X += (kerning.Y + kerning.Z) * scale.X;
			newLine = false;
		}
		if (zero.X > textWidth)
		{
			textWidth = zero.X;
		}
		float textHeight = zero.Y;
		if (DialoguePage.AlignType != Alignment.Left)
		{
			int num4;
			for (num4 = 0; num4 < lineLengths.Length; num4++)
			{
				float xPos = CharacterData.Last((DialogueCharacterData d) => d.LineNumber == num4 && Text[d.Index] != '\n').TextPosition.X;
				float dif = textWidth - xPos;
				if (DialoguePage.AlignType == Alignment.Center)
				{
					foreach (DialogueCharacterData item in CharacterData.Where((DialogueCharacterData dialogueCharacterData) => dialogueCharacterData.LineNumber == num4))
					{
						item.TextPosition.X += dif / 2f;
					}
					continue;
				}
				foreach (DialogueCharacterData item2 in CharacterData.Where((DialogueCharacterData dialogueCharacterData) => dialogueCharacterData.LineNumber == num4))
				{
					item2.TextPosition.X += dif;
				}
			}
		}
		TextSize = new Vector2(textWidth + 8f, textHeight + 12f) + SizeOffsetFromStart;
	}

	private void FindEffects(ref string fullLine, int fullLength)
	{
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		Stack<int> returnPoints = new Stack<int>();
		Stack<string> returnString = new Stack<string>();
		Vector2 scale = default(Vector2);
		for (int j = 0; j < fullLine.Length; j++)
		{
			if (fullLine[j] != '[')
			{
				continue;
			}
			int k = j + 1;
			string currentData = "[";
			bool readingData = true;
			for (k = j + 1; k < fullLine.Length && fullLine[k] != ']'; k++)
			{
				if (fullLine[k] == '[')
				{
					returnPoints.Push(j);
					returnString.Push(currentData);
					currentData = "[";
					_ = fullLine[k];
					j = k;
					readingData = true;
				}
				else if (readingData)
				{
					currentData += fullLine[k];
					if (fullLine[k] == ':')
					{
						readingData = false;
					}
				}
			}
			if (fullLine[k] != ']')
			{
				throw new Exception("[ was found without a ] after it.");
			}
			string obj = fullLine;
			int num = j;
			string effect = obj.Substring(num, k - num);
			string ID = "";
			string Text = "";
			List<float> Params = new List<float>();
			List<string> ColorParams = new List<string>();
			List<string> BorderColorParams = new List<string>();
			string Param = "";
			bool readingText = false;
			bool readingParams = false;
			for (int l = 1; l < effect.Length; l++)
			{
				char ch = effect[l];
				switch (ch)
				{
				case '(':
					readingParams = true;
					continue;
				case ':':
					readingText = true;
					continue;
				default:
					if (readingText)
					{
						Text += ch;
					}
					else if (readingParams)
					{
						switch (ch)
						{
						case ')':
						case ',':
							if (ID == "Colors")
							{
								if (float.TryParse(Param, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var result))
								{
									Params.Add(result);
								}
								else
								{
									ColorParams.Add(Param);
								}
							}
							else if (ID == "BorderColors")
							{
								if (float.TryParse(Param, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var result2))
								{
									Params.Add(result2);
								}
								else
								{
									BorderColorParams.Add(Param);
								}
							}
							else
							{
								if (!float.TryParse(Param, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var result3))
								{
									throw new Exception("Invalid Parameter found");
								}
								Params.Add(result3);
							}
							Param = "";
							break;
						default:
							Param += ch;
							break;
						case ' ':
							break;
						}
					}
					else
					{
						ID += ch;
					}
					continue;
				case ']':
					break;
				}
				break;
			}
			fullLine = fullLine.Remove(j, k - j + 1);
			fullLine = fullLine.Insert(j, Text);
			if (ID == "Pause")
			{
				int index = j + fullLength;
				int storedLen = 0;
				foreach (string s in returnString)
				{
					storedLen += s.Length;
				}
				Pauses.Add(index - storedLen - 1, Params[0]);
			}
			else
			{
				for (int i = 0; i < Text.Length; i++)
				{
					int index2 = j + i + fullLength;
					switch (ID)
					{
					case "Colors":
					{
						int storedLen3 = 0;
						foreach (string s3 in returnString)
						{
							storedLen3 += s3.Length;
						}
						UniqueColors.Add(index2 - storedLen3, ((Params.Count == 0) ? 0f : Params[0], (Params.Count < 2) ? 1f : Params[1], ColorParams.ToArray()));
						continue;
					}
					case "BorderColors":
					{
						int storedLen2 = 0;
						foreach (string s2 in returnString)
						{
							storedLen2 += s2.Length;
						}
						UniqueBorderColors.Add(index2 - storedLen2, ((Params.Count == 0) ? 0f : Params[0], (Params.Count < 2) ? 1f : Params[1], BorderColorParams.ToArray()));
						continue;
					}
					case "Scale":
					{
						int storedLen4 = 0;
						foreach (string s4 in returnString)
						{
							storedLen4 += s4.Length;
						}
						if (Params.Count == 0)
						{
							scale = Vector2.One;
						}
						else if (Params.Count == 1)
						{
							((Vector2)(ref scale))._002Ector(Params[0], Params[0]);
						}
						else
						{
							((Vector2)(ref scale))._002Ector(Params[0], Params[1]);
						}
						UniqueScales.Add(index2 - storedLen4, scale);
						continue;
					}
					}
					int storedLen5 = 0;
					foreach (string s5 in returnString)
					{
						storedLen5 += s5.Length;
					}
					TextEffect te = (TextEffect)Activator.CreateInstance(Type.GetType("CalamityMod.UI.DialogueDisplay.TextEffects." + ID) ?? throw new Exception("Invalid text effect ID found"));
					if (TextEffects.TryGetValue(index2 - storedLen5, out List<(TextEffect, float[])> value))
					{
						value.Add((te, Params.ToArray()));
						continue;
					}
					Dictionary<int, List<(TextEffect Effect, float[] args)>> textEffects = TextEffects;
					int key = index2 - storedLen5;
					num = 1;
					List<(TextEffect, float[])> list = new List<(TextEffect, float[])>(num);
					CollectionsMarshal.SetCount(list, num);
					Span<(TextEffect, float[])> span = CollectionsMarshal.AsSpan(list);
					int index3 = 0;
					span[index3] = (te, Params.ToArray());
					textEffects.Add(key, list);
				}
			}
			if (returnPoints.Count > 0)
			{
				j = returnPoints.Pop() - 1;
				returnString.Pop();
			}
		}
	}

	private Vector2 MeasureString(string text, DynamicSpriteFont font)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		if (text.Length == 0)
		{
			return Vector2.Zero;
		}
		Vector2 zero = Vector2.Zero;
		zero.Y = font.LineSpacing;
		float val = 0f;
		int num = 0;
		float num2 = 0f;
		bool newLine = true;
		for (int i = 0; i < text.Length; i++)
		{
			char c = text[i];
			Vector2 scale = Vector2.One;
			if (UniqueScales.TryGetValue(i, out var result))
			{
				scale = result;
			}
			else if (DialoguePage.TextScale != -1)
			{
				scale *= (float)DialoguePage.TextScale;
			}
			switch (c)
			{
			case '\n':
			{
				zero.X = 0f;
				float highestYscale = 1f;
				for (int j = i + 1; j < text.Length && text[j] != '\n'; j++)
				{
					if (UniqueScales.TryGetValue(j, out var uniqueScale) && uniqueScale.Y > highestYscale)
					{
						highestYscale = uniqueScale.Y;
					}
				}
				zero.Y += (float)font.LineSpacing * highestYscale;
				newLine = true;
				continue;
			}
			case '\r':
				continue;
			}
			SpriteCharacterData spriteData = font.SpriteCharacters[c];
			Vector3 kerning = spriteData.Kerning;
			Rectangle padding = spriteData.Padding;
			if (newLine)
			{
				kerning.X = Math.Max(kerning.X, 0f);
			}
			else
			{
				zero.X += font.CharacterSpacing * scale.X;
			}
			zero.X += kerning.X * scale.X;
			Vector2 position = zero + spriteData.Glyph.Size() * 0.5f;
			position.X += (float)padding.X * scale.X;
			position.Y += (float)padding.Y * scale.Y;
			zero.X += (kerning.Y + kerning.Z) * scale.X;
			newLine = false;
		}
		zero.X += Math.Max(num2, 0f);
		zero.Y += num * font.LineSpacing;
		zero.X = Math.Max(zero.X, val);
		return zero;
	}

	public override void Update(GameTime gameTime)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		base.Update(gameTime);
		if (DialoguePage.Event != null)
		{
			DialoguePage.Event.UpdateEvent();
		}
		else if (!Switching)
		{
			if (DisplayEffects.FadeWhenTooFar)
			{
				float distFromSource = Vector2.Distance(Main.LocalPlayer.Center, Position);
				SwitchCounter = (int)(MathHelper.Clamp((distFromSource - DisplayEffects.FadeBuffer) / DisplayEffects.FadeDistance, 0f, 1f) * DisplayEffects.TimeToDisappear);
			}
			int textDelay = DialoguePage.TextDelay;
			int inPunctuationDelay = DialoguePage.InPunctuationDelay;
			if (DialoguePage.Event != null && !DialoguePage.Event.IsOver)
			{
				DialoguePage.Event.UpdateEvent();
			}
			if (textIndex < Text.Length - 1)
			{
				int loopCounter = 0;
				bool forcedPause = false;
				int delay;
				do
				{
					if (TextTimer == 0)
					{
						if (!lockDelay)
						{
							char currentChar = Text[textIndex];
							PunctuationData data = new PunctuationData();
							if (DialoguePage.BasePunctuationDelay != null)
							{
								data = DialoguePage.BasePunctuationDelay;
							}
							if (DialoguePage.PunctuationDelays != null && DialoguePage.PunctuationDelays.TryGetValue(currentChar.ToString(), out var value))
							{
								data = value;
							}
							if (IsStoppingPunctuation(currentChar, (textIndex == 0) ? ((char?)null) : new char?(Text[textIndex - 1]), (textIndex == Text.Length - 1) ? ((char?)null) : new char?(Text[textIndex + 1])))
							{
								if (data.ForceSet)
								{
									storedDelay = data.Delay;
								}
								else
								{
									storedDelay += data.Delay;
								}
							}
							if (data.Locks)
							{
								lockDelay = true;
							}
						}
						if (Pauses.TryGetValue(textIndex, out var pause))
						{
							storedDelay = (int)(pause * 60f);
							forcedPause = true;
						}
					}
					else if (Pauses.ContainsKey(textIndex))
					{
						forcedPause = true;
					}
					int delayToUse = ((IsPunctuation(Text[textIndex]) && textIndex > 0 && IsPunctuation(Text[textIndex - 1])) ? inPunctuationDelay : textDelay);
					delay = ((((Text[textIndex] == ' ' || Text[textIndex] == '\n') | forcedPause) && storedDelay > 0) ? (delayToUse + storedDelay) : delayToUse);
					if (loopCounter == 0)
					{
						TextTimer++;
					}
					if ((delay == 0 || (TextTimer + loopCounter) % delay == 0) && TextTimer >= 0)
					{
						if ((Text[textIndex] == ' ') | forcedPause)
						{
							storedDelay = 0;
							lockDelay = false;
						}
						else
						{
							string speaker = null;
							if (DialoguePage.Speaker != null)
							{
								speaker = DialoguePage.Speaker;
							}
							if (speaker != null && DialogueSounds.TryGetValue(speaker, out var value2))
							{
								SoundEngine.PlaySound(in value2);
							}
						}
						TextTimer = 0;
						textIndex++;
					}
					if (delay != 0)
					{
						break;
					}
					loopCounter++;
				}
				while (delay == 0 && textIndex < Text.Length && TextTimer >= 0);
			}
			else
			{
				Crawling = false;
			}
		}
		if (!Crawling)
		{
			Uptime++;
		}
		DialogueTimer++;
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_073f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 textTop = DisplayEffects.TextOffsetFromStart(Position, TextSize);
		Vector2 pageTop = textTop - SizeOffsetFromStart;
		DisplayEffects.PreDraw(spriteBatch, pageTop, TextSize, DialogueTimer, SwitchCounter);
		int i;
		for (i = 0; i < textIndex; i++)
		{
			char c = Text[i];
			if (c == '\r' || c == '\n')
			{
				continue;
			}
			if (CharacterData == null)
			{
				Activate();
			}
			float rotation = 0f;
			float opacity = 1f;
			Vector2 scale = Vector2.One;
			if (UniqueScales.TryGetValue(i, out var result))
			{
				scale = result;
			}
			Color color;
			if (UniqueColors.TryGetValue(i, out (float, float, string[]) textColors))
			{
				Color[] colors = (Color[])(object)new Color[textColors.Item3.Length];
				for (int j = 0; j < colors.Length; j++)
				{
					colors[j] = DialogueDisplaySystem.GetColorFromHex(textColors.Item3[j]);
				}
				color = ((!CalamityClientConfig.Instance.TextEffects) ? colors[0] : CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * textColors.Item2 + (float)i * textColors.Item1, colors));
			}
			else
			{
				color = BaseColor;
			}
			Color borderColor;
			if (UniqueBorderColors.TryGetValue(i, out (float, float, string[]) borderColors))
			{
				Color[] colors2 = (Color[])(object)new Color[borderColors.Item3.Length];
				for (int k = 0; k < colors2.Length; k++)
				{
					colors2[k] = DialogueDisplaySystem.GetColorFromHex(borderColors.Item3[k]);
				}
				borderColor = ((!CalamityClientConfig.Instance.TextEffects) ? colors2[0] : CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * borderColors.Item2 + (float)i * borderColors.Item1, colors2));
			}
			else
			{
				borderColor = BaseBorderColor;
			}
			Vector2 drawPos;
			if ((float)CharacterData[i].Timer < DisplayEffects.TimeToAppear)
			{
				drawPos = DisplayEffects.AppearPositioning(Position, textTop + CharacterData[i].TextPosition, CharacterData[i].Timer, CharacterData[i]);
				opacity = DisplayEffects.AppearOpacity(opacity, CharacterData[i].Timer, CharacterData[i]);
				color = DisplayEffects.AppearColoring(color, CharacterData[i].Timer, CharacterData[i]);
				rotation = DisplayEffects.AppearRotation(rotation, CharacterData[i].Timer, CharacterData[i]);
				scale = DisplayEffects.AppearScale(scale, CharacterData[i].Timer, CharacterData[i]);
			}
			else
			{
				drawPos = textTop + CharacterData[i].TextPosition;
			}
			if (SwitchCounter > 0)
			{
				drawPos = DisplayEffects.DisappearPositioning(drawPos, SwitchCounter, CharacterData[i]);
				opacity = DisplayEffects.DisappearOpacity(opacity, SwitchCounter, CharacterData[i]);
				color = DisplayEffects.DisappearColoring(color, SwitchCounter, CharacterData[i]);
				rotation = DisplayEffects.DisappearRotation(rotation, SwitchCounter, CharacterData[i]);
				scale = DisplayEffects.DisappearScale(scale, SwitchCounter, CharacterData[i]);
			}
			if (!ScreenLocked)
			{
				drawPos -= Main.screenPosition;
			}
			if (CalamityClientConfig.Instance.TextEffects)
			{
				foreach (KeyValuePair<int, List<(TextEffect, float[])>> item in TextEffects.Where((KeyValuePair<int, List<(TextEffect Effect, float[] args)>> v) => v.Key == i))
				{
					foreach (var item2 in item.Value)
					{
						TextEffect Effect = item2.Item1;
						float[] args = item2.Item2;
						drawPos = Effect.ModifyPos(drawPos, CharacterData[i], args);
						rotation = Effect.ModifyRot(rotation, CharacterData[i], args);
						color = Effect.ModifyColor(color, CharacterData[i], args);
						scale = Effect.ModifyScale(scale, CharacterData[i], args);
					}
				}
			}
			SpriteCharacterData spriteData = Font.Value.SpriteCharacters[c];
			Vector2 origin = spriteData.Glyph.Size() * 0.5f;
			CharacterData[i].SetDrawInfo(drawPos, spriteData.Glyph, color * opacity, rotation, scale);
			foreach (KeyValuePair<int, List<(TextEffect, float[])>> item3 in TextEffects.Where((KeyValuePair<int, List<(TextEffect Effect, float[] args)>> v) => v.Key == i))
			{
				foreach (var item4 in item3.Value)
				{
					TextEffect Effect2 = item4.Item1;
					Effect2.PreDraw(spriteBatch, spriteData.Texture, CharacterData[i]);
				}
			}
			for (int j2 = 0; j2 < ChatManager.ShadowDirections.Length; j2++)
			{
				spriteBatch.Draw(spriteData.Texture, drawPos + ChatManager.ShadowDirections[j2] * 2f, (Rectangle?)spriteData.Glyph, borderColor * opacity, rotation, origin, scale, (SpriteEffects)0, 0f);
			}
		}
		int i2;
		for (i2 = 0; i2 < textIndex; i2++)
		{
			char c2 = Text[i2];
			if (c2 == '\r' || c2 == '\n')
			{
				continue;
			}
			if (CharacterData == null)
			{
				Activate();
			}
			SpriteCharacterData spriteData2 = Font.Value.SpriteCharacters[c2];
			Vector2 origin2 = spriteData2.Glyph.Size() * 0.5f;
			spriteBatch.Draw(spriteData2.Texture, CharacterData[i2].DrawPosition, (Rectangle?)spriteData2.Glyph, CharacterData[i2].DrawColor, CharacterData[i2].Rotation, origin2, CharacterData[i2].Scale, (SpriteEffects)0, 0f);
			foreach (KeyValuePair<int, List<(TextEffect, float[])>> item5 in TextEffects.Where((KeyValuePair<int, List<(TextEffect Effect, float[] args)>> v) => v.Key == i2))
			{
				foreach (var item6 in item5.Value)
				{
					TextEffect Effect3 = item6.Item1;
					Effect3.PostDraw(spriteBatch, spriteData2.Texture, CharacterData[i2]);
				}
			}
			CharacterData[i2].Timer++;
		}
		DisplayEffects.PostDraw(spriteBatch, pageTop, TextSize, DialogueTimer, SwitchCounter);
	}

	private static bool IsStoppingPunctuation(char current, char? before, char? after)
	{
		if (IsPunctuation(current))
		{
			bool num = before.HasValue && char.IsLetter(before.Value);
			bool hasLetterAfter = after.HasValue && char.IsLetter(after.Value);
			if (!(num & hasLetterAfter))
			{
				return true;
			}
		}
		return false;
	}

	private static bool IsPunctuation(char c)
	{
		UnicodeCategory category = char.GetUnicodeCategory(c);
		if (category >= UnicodeCategory.ConnectorPunctuation)
		{
			return category <= UnicodeCategory.OtherPunctuation;
		}
		return false;
	}
}
