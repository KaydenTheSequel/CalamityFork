using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Events;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.GreatSandShark;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SunkenSea;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Utils;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.UI;

public class BossHealthBarManager : ModBossBarStyle
{
	public struct BossEntityExtension(LocalizedText name, params int[] types)
	{
		public LocalizedText NameOfExtensions = name;

		public int[] TypesToSearchFor = types;
	}

	public delegate bool NPCSpecialHPGetRequirement(NPC npc);

	public delegate long NPCSpecialHPGetFunction(NPC npc, bool checkingForMaxLife);

	public class BossHPUI
	{
		public int NPCIndex = -1;

		public int IntendedNPCType = -1;

		public int OpenAnimationTimer;

		public int CloseAnimationTimer;

		public int EnrageTimer;

		public int IncreasingDefenseOrDRTimer;

		public int ComboDamageCountdown;

		public long PreviousLife;

		public long HealthAtStartOfCombo;

		public long InitialMaxLife;

		public string OverridingName;

		public const int MainBarYOffset = 28;

		public const int SeparatorBarYOffset = 18;

		public const int BarMaxWidth = 400;

		public const int OpenAnimationTime = 80;

		public const int CloseAnimationTime = 120;

		public const int EnrageAnimationTime = 120;

		public const int IncreasedDefenseOrDRAnimationTime = 120;

		public const int VerticalOffsetPerBar = 70;

		public const float SmallTextScale = 0.75f;

		public static Color MainColor;

		public static Color MainBorderColour;

		public NPC AssociatedNPC
		{
			get
			{
				if (!Main.npc.IndexInRange(NPCIndex))
				{
					return null;
				}
				return Main.npc[NPCIndex];
			}
		}

		public int NPCType => AssociatedNPC?.type ?? (-1);

		public long CombinedNPCLife
		{
			get
			{
				if (AssociatedNPC == null || !AssociatedNPC.active)
				{
					return 0L;
				}
				long life = AssociatedNPC.life;
				foreach (KeyValuePair<NPCSpecialHPGetRequirement, NPCSpecialHPGetFunction> requirement in SpecialHPRequirements)
				{
					if (requirement.Key(AssociatedNPC))
					{
						return requirement.Value(AssociatedNPC, checkingForMaxLife: false);
					}
				}
				if (!OneToMany.ContainsKey(NPCType))
				{
					return life;
				}
				ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					NPC n = enumerator2.Current;
					if (n.life > 0 && OneToMany[NPCType].Contains(n.type))
					{
						life += n.life;
					}
				}
				return life;
			}
		}

		public long CombinedNPCMaxLife
		{
			get
			{
				if (AssociatedNPC == null || !AssociatedNPC.active)
				{
					return 0L;
				}
				long maxLife = AssociatedNPC.lifeMax;
				foreach (KeyValuePair<NPCSpecialHPGetRequirement, NPCSpecialHPGetFunction> requirement in SpecialHPRequirements)
				{
					if (requirement.Key(AssociatedNPC))
					{
						return requirement.Value(AssociatedNPC, checkingForMaxLife: true);
					}
				}
				if (!OneToMany.ContainsKey(NPCType))
				{
					return maxLife;
				}
				ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					NPC n = enumerator2.Current;
					if (n.life > 0 && OneToMany[NPCType].Contains(n.type))
					{
						maxLife += n.lifeMax;
					}
				}
				return maxLife;
			}
		}

		public bool NPCIsEnraged
		{
			get
			{
				if (AssociatedNPC == null || !AssociatedNPC.active)
				{
					return false;
				}
				if (AssociatedNPC.Calamity().CurrentlyEnraged)
				{
					return true;
				}
				if (!OneToMany.ContainsKey(NPCType))
				{
					return false;
				}
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.life > 0 && OneToMany[NPCType].Contains(n.type) && n.Calamity().CurrentlyEnraged)
					{
						return true;
					}
				}
				return false;
			}
		}

		public bool NPCIsIncreasingDefenseOrDR
		{
			get
			{
				if (AssociatedNPC == null || !AssociatedNPC.active)
				{
					return false;
				}
				if (AssociatedNPC.Calamity().CurrentlyIncreasingDefenseOrDR)
				{
					return true;
				}
				if (!OneToMany.ContainsKey(NPCType))
				{
					return false;
				}
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.life > 0 && OneToMany[NPCType].Contains(n.type) && n.Calamity().CurrentlyIncreasingDefenseOrDR)
					{
						return true;
					}
				}
				return false;
			}
		}

		public float NPCLifeRatio
		{
			get
			{
				float lifeRatio = (float)CombinedNPCLife / (float)InitialMaxLife;
				if (float.IsNaN(lifeRatio) || float.IsInfinity(lifeRatio))
				{
					return 0f;
				}
				return lifeRatio;
			}
		}

		public BossHPUI(int index, string overridingName = null)
		{
			NPCIndex = index;
			if (AssociatedNPC != null && AssociatedNPC.active)
			{
				IntendedNPCType = AssociatedNPC.type;
				PreviousLife = CombinedNPCLife;
			}
			OverridingName = overridingName;
		}

		public void Update()
		{
			if (CombinedNPCLife != PreviousLife && PreviousLife != 0L)
			{
				if (ComboDamageCountdown <= 0)
				{
					HealthAtStartOfCombo = CombinedNPCLife;
				}
				ComboDamageCountdown = 30;
			}
			PreviousLife = CombinedNPCLife;
			if (ComboDamageCountdown > 0)
			{
				ComboDamageCountdown--;
			}
			if (AssociatedNPC == null || !AssociatedNPC.active || NPCType != IntendedNPCType || AssociatedNPC.Calamity().ShouldCloseHPBar)
			{
				EnrageTimer = Utils.Clamp(EnrageTimer - 4, 0, 120);
				IncreasingDefenseOrDRTimer = Utils.Clamp(IncreasingDefenseOrDRTimer - 4, 0, 120);
				CloseAnimationTimer = Utils.Clamp(CloseAnimationTimer + 1, 0, 120);
				return;
			}
			OpenAnimationTimer = Utils.Clamp(OpenAnimationTimer + 1, 0, 80);
			EnrageTimer = Utils.Clamp(EnrageTimer + NPCIsEnraged.ToDirectionInt(), 0, 120);
			IncreasingDefenseOrDRTimer = Utils.Clamp(IncreasingDefenseOrDRTimer + NPCIsIncreasingDefenseOrDR.ToDirectionInt(), 0, 120);
			if (CombinedNPCMaxLife != 0L && (InitialMaxLife == 0L || InitialMaxLife < CombinedNPCMaxLife))
			{
				InitialMaxLife = CombinedNPCMaxLife;
			}
		}

		public void Draw(SpriteBatch sb, int x, int y)
		{
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0171: Unknown result type (might be due to invalid IL or missing references)
			//IL_0177: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Unknown result type (might be due to invalid IL or missing references)
			//IL_0199: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_014c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_0218: Unknown result type (might be due to invalid IL or missing references)
			//IL_021d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01da: Unknown result type (might be due to invalid IL or missing references)
			//IL_01df: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0201: Unknown result type (might be due to invalid IL or missing references)
			//IL_0206: Unknown result type (might be due to invalid IL or missing references)
			//IL_0264: Unknown result type (might be due to invalid IL or missing references)
			//IL_0269: Unknown result type (might be due to invalid IL or missing references)
			//IL_027a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0282: Unknown result type (might be due to invalid IL or missing references)
			//IL_0287: Unknown result type (might be due to invalid IL or missing references)
			//IL_028c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0296: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0324: Unknown result type (might be due to invalid IL or missing references)
			//IL_032e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0333: Unknown result type (might be due to invalid IL or missing references)
			//IL_0335: Unknown result type (might be due to invalid IL or missing references)
			//IL_033f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0344: Unknown result type (might be due to invalid IL or missing references)
			//IL_0411: Unknown result type (might be due to invalid IL or missing references)
			//IL_041b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0420: Unknown result type (might be due to invalid IL or missing references)
			//IL_0422: Unknown result type (might be due to invalid IL or missing references)
			//IL_042c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0431: Unknown result type (might be due to invalid IL or missing references)
			//IL_0631: Unknown result type (might be due to invalid IL or missing references)
			//IL_063b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0640: Unknown result type (might be due to invalid IL or missing references)
			//IL_0648: Unknown result type (might be due to invalid IL or missing references)
			//IL_0679: Unknown result type (might be due to invalid IL or missing references)
			//IL_067b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0681: Unknown result type (might be due to invalid IL or missing references)
			//IL_0686: Unknown result type (might be due to invalid IL or missing references)
			//IL_068c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0696: Unknown result type (might be due to invalid IL or missing references)
			//IL_0560: Unknown result type (might be due to invalid IL or missing references)
			//IL_056a: Unknown result type (might be due to invalid IL or missing references)
			//IL_056f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0577: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_035a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0361: Unknown result type (might be due to invalid IL or missing references)
			//IL_0366: Unknown result type (might be due to invalid IL or missing references)
			//IL_037d: Unknown result type (might be due to invalid IL or missing references)
			//IL_038a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0392: Unknown result type (might be due to invalid IL or missing references)
			//IL_0397: Unknown result type (might be due to invalid IL or missing references)
			//IL_0399: Unknown result type (might be due to invalid IL or missing references)
			//IL_039e: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0447: Unknown result type (might be due to invalid IL or missing references)
			//IL_044e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0453: Unknown result type (might be due to invalid IL or missing references)
			//IL_046a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0477: Unknown result type (might be due to invalid IL or missing references)
			//IL_047f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0484: Unknown result type (might be due to invalid IL or missing references)
			//IL_0486: Unknown result type (might be due to invalid IL or missing references)
			//IL_048b: Unknown result type (might be due to invalid IL or missing references)
			//IL_048d: Unknown result type (might be due to invalid IL or missing references)
			float animationCompletionRatio = MathHelper.Clamp((float)OpenAnimationTimer / 80f, 0f, 1f);
			if (CloseAnimationTimer > 0)
			{
				animationCompletionRatio = 1f - MathHelper.Clamp((float)CloseAnimationTimer / 120f, 0f, 1f);
			}
			float openAnimationFlicker = animationCompletionRatio;
			if (OpenAnimationTimer == 4 || OpenAnimationTimer == 8 || OpenAnimationTimer == 16)
			{
				openAnimationFlicker = Main.rand.NextFloat(0.7f, 0.8f);
			}
			if (OpenAnimationTimer == 3 || OpenAnimationTimer == 7 || OpenAnimationTimer == 15)
			{
				openAnimationFlicker = Main.rand.NextFloat(0.4f, 0.5f);
			}
			int mainBarWidth = (int)MathHelper.Min(400f * animationCompletionRatio, 400f * NPCLifeRatio);
			sb.Draw(BossMainHPBar, new Rectangle(x, y + 28, mainBarWidth, BossMainHPBar.Height), Color.White);
			if (ComboDamageCountdown > 0)
			{
				int comboBarWidth = (int)((float)(400 * HealthAtStartOfCombo) / (float)InitialMaxLife) - mainBarWidth;
				float alpha = 1f;
				if (ComboDamageCountdown < 6)
				{
					comboBarWidth = (int)((float)(comboBarWidth * ComboDamageCountdown) / 6f);
				}
				sb.Draw(BossComboHPBar, new Rectangle(x + mainBarWidth, y + 28, comboBarWidth, BossComboHPBar.Height), Color.White * alpha);
			}
			Color separatorColor = new Color(240, 240, 255) * animationCompletionRatio;
			if (NPCIsEnraged)
			{
				separatorColor = Color.Lerp(new Color(240, 240, 255), Color.Red * 0.5f, (float)EnrageTimer / 120f) * animationCompletionRatio;
			}
			else if (NPCIsIncreasingDefenseOrDR)
			{
				separatorColor = Color.Lerp(new Color(240, 240, 255), Color.LightGray * 0.5f, (float)IncreasingDefenseOrDRTimer / 120f) * animationCompletionRatio;
			}
			sb.Draw(BossSeperatorBar, new Rectangle(x, y + 18, 400, 6), separatorColor);
			string percentHealthText = (NPCLifeRatio * 100f).ToString("N1") + "%";
			if (NPCLifeRatio == 0f)
			{
				percentHealthText = "0%";
			}
			Vector2 textSize = HPBarFont.MeasureString(percentHealthText);
			CalamityUtils.DrawBorderStringEightWay(sb, HPBarFont, percentHealthText, new Vector2((float)x, (float)(y + 22) - textSize.Y), MainColor, MainBorderColour * 0.25f);
			string name = OverridingName ?? AssociatedNPC.FullName;
			Vector2 nameSize = FontAssets.MouseText.Value.MeasureString(name);
			if (NPCIsEnraged)
			{
				if (EnrageTimer > 0)
				{
					float pulse = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 4.5f) * 0.5f + 0.5f;
					float outwardness = (float)EnrageTimer / 120f * 1.5f + pulse * 2f;
					Color color1 = Color.Red * 0.6f;
					Color color2 = Color.Black * 0.2f;
					for (int i = 0; i < 4; i++)
					{
						Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 4f).ToRotationVector2() * outwardness;
						CalamityUtils.DrawBorderStringEightWay(sb, FontAssets.MouseText.Value, name, new Vector2((float)(x + 400) - nameSize.X, (float)(y + 23) - nameSize.Y) + drawOffset, color1, color2);
					}
				}
			}
			else if (NPCIsIncreasingDefenseOrDR && IncreasingDefenseOrDRTimer > 0)
			{
				float pulse2 = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 4.5f) * 0.5f + 0.5f;
				float outwardness2 = (float)IncreasingDefenseOrDRTimer / 120f * 1.5f + pulse2 * 2f;
				Color color3 = Color.LightGray * 0.6f;
				Color color4 = Color.Black * 0.2f;
				for (int j = 0; j < 4; j++)
				{
					Vector2 drawOffset2 = ((float)Math.PI * 2f * (float)j / 4f).ToRotationVector2() * outwardness2;
					CalamityUtils.DrawBorderStringEightWay(sb, FontAssets.MouseText.Value, name, new Vector2((float)(x + 400) - nameSize.X, (float)(y + 23) - nameSize.Y) + drawOffset2, color3, color4);
				}
			}
			CalamityUtils.DrawBorderStringEightWay(sb, FontAssets.MouseText.Value, name, new Vector2((float)(x + 400) - nameSize.X, (float)(y + 23) - nameSize.Y), Color.White, Color.Black * 0.2f);
			if (CanDrawExtraSmallText)
			{
				if (EntityExtensionHandler.TryGetValue(NPCType, out var extraEntityData))
				{
					int totalExtraEntities = CalamityUtils.CountNPCsBetter(extraEntityData.TypesToSearchFor);
					string extensionName = extraEntityData.NameOfExtensions.ToString();
					string text = CalamityUtils.GetText("UI.ExtensionDisplay").Format(extensionName, totalExtraEntities);
					Vector2 textAreaSize = FontAssets.ItemStack.Value.MeasureString(text) * 0.75f;
					float horizontalDrawPosition = Math.Max(x, (float)(x + mainBarWidth) - textAreaSize.X);
					float verticalDrawPosition = y + 28 + 17;
					Vector2 smallBarDrawPosition = default(Vector2);
					((Vector2)(ref smallBarDrawPosition))._002Ector(horizontalDrawPosition, verticalDrawPosition);
					CalamityUtils.DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, text, smallBarDrawPosition, Color.White * openAnimationFlicker, Color.Black * openAnimationFlicker * 0.24f, 0.75f);
				}
				else
				{
					string actualLifeText = $"({CombinedNPCLife} / {InitialMaxLife})";
					Vector2 textAreaSize2 = FontAssets.ItemStack.Value.MeasureString(actualLifeText) * 0.75f;
					float horizontalDrawPosition2 = Math.Max(x, (float)(x + mainBarWidth) - textAreaSize2.X);
					float verticalDrawPosition2 = y + 28 + 17;
					Vector2 smallBarDrawPosition2 = default(Vector2);
					((Vector2)(ref smallBarDrawPosition2))._002Ector(horizontalDrawPosition2, verticalDrawPosition2);
					CalamityUtils.DrawBorderStringEightWay(sb, FontAssets.ItemStack.Value, actualLifeText, smallBarDrawPosition2, Color.White * openAnimationFlicker, Color.Black * openAnimationFlicker * 0.24f, 0.75f);
				}
			}
		}

		static BossHPUI()
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			MainColor = new Color(229, 189, 62);
			MainBorderColour = new Color(197, 127, 46);
		}
	}

	public static bool CanDrawExtraSmallText = true;

	public static int MaximumBars = 4;

	public static List<BossHPUI> Bars;

	public static DynamicSpriteFont HPBarFont;

	public static Texture2D BossMainHPBar;

	public static Texture2D BossComboHPBar;

	public static Texture2D BossSeperatorBar;

	public static Dictionary<int, int[]> OneToMany;

	public static List<int> BossExclusionList;

	public static List<int> MinibossHPBarList;

	public static Dictionary<int, BossEntityExtension> EntityExtensionHandler = new Dictionary<int, BossEntityExtension>();

	public static Dictionary<NPCSpecialHPGetRequirement, NPCSpecialHPGetFunction> SpecialHPRequirements = new Dictionary<NPCSpecialHPGetRequirement, NPCSpecialHPGetFunction>();

	public override bool PreventDraw => true;

	public override void Load()
	{
		BossExclusionList = new List<int>();
		MinibossHPBarList = new List<int>();
		EntityExtensionHandler = new Dictionary<int, BossEntityExtension>();
		SpecialHPRequirements = new Dictionary<NPCSpecialHPGetRequirement, NPCSpecialHPGetFunction>();
	}

	public override void SetStaticDefaults()
	{
		Bars = new List<BossHPUI>();
		if (!Main.dedServ)
		{
			BossMainHPBar = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/BossHPMainBar", (AssetRequestMode)1).Value;
			BossComboHPBar = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/BossHPComboBar", (AssetRequestMode)1).Value;
			BossSeperatorBar = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/BossHPSeperatorBar", (AssetRequestMode)1).Value;
			HPBarFont = ModContent.Request<DynamicSpriteFont>("CalamityMod/Fonts/HPBarFont", (AssetRequestMode)1).Value;
		}
		OneToMany = new Dictionary<int, int[]>();
		int[] EoW = new int[3] { 13, 14, 15 };
		OneToMany[13] = EoW;
		OneToMany[14] = EoW;
		OneToMany[15] = EoW;
		int[] BoC = new int[2] { 266, 267 };
		OneToMany[266] = BoC;
		OneToMany[267] = BoC;
		int[] Skele = new int[2] { 35, 36 };
		OneToMany[35] = Skele;
		OneToMany[36] = Skele;
		int[] SkelePrime = new int[5] { 127, 129, 130, 128, 131 };
		OneToMany[127] = SkelePrime;
		OneToMany[129] = SkelePrime;
		OneToMany[130] = SkelePrime;
		OneToMany[128] = SkelePrime;
		OneToMany[131] = SkelePrime;
		int[] Golem = new int[4] { 245, 247, 248, 246 };
		OneToMany[245] = Golem;
		OneToMany[247] = Golem;
		OneToMany[248] = Golem;
		OneToMany[246] = Golem;
		int[] Saucer = new int[3] { 395, 393, 394 };
		OneToMany[395] = Saucer;
		OneToMany[393] = Saucer;
		OneToMany[394] = Saucer;
		int[] Ship = new int[2] { 491, 492 };
		OneToMany[491] = Ship;
		OneToMany[492] = Ship;
		int[] MoonLord = new int[3] { 396, 397, 398 };
		OneToMany[398] = MoonLord;
		int[] Void = new int[2]
		{
			ModContent.NPCType<CeaselessVoid>(),
			ModContent.NPCType<DarkEnergy>()
		};
		OneToMany[ModContent.NPCType<CeaselessVoid>()] = Void;
		OneToMany[ModContent.NPCType<DarkEnergy>()] = Void;
		int[] Rav = new int[6]
		{
			ModContent.NPCType<RavagerBody>(),
			ModContent.NPCType<RavagerClawRight>(),
			ModContent.NPCType<RavagerClawLeft>(),
			ModContent.NPCType<RavagerLegRight>(),
			ModContent.NPCType<RavagerLegLeft>(),
			ModContent.NPCType<RavagerHead>()
		};
		OneToMany[ModContent.NPCType<RavagerBody>()] = Rav;
		OneToMany[ModContent.NPCType<RavagerClawRight>()] = Rav;
		OneToMany[ModContent.NPCType<RavagerClawLeft>()] = Rav;
		OneToMany[ModContent.NPCType<RavagerLegRight>()] = Rav;
		OneToMany[ModContent.NPCType<RavagerLegLeft>()] = Rav;
		OneToMany[ModContent.NPCType<RavagerHead>()] = Rav;
		int[] SlimeGod = new int[4]
		{
			ModContent.NPCType<EbonianPaladin>(),
			ModContent.NPCType<SplitEbonianPaladin>(),
			ModContent.NPCType<CrimulanPaladin>(),
			ModContent.NPCType<SplitCrimulanPaladin>()
		};
		OneToMany[ModContent.NPCType<EbonianPaladin>()] = SlimeGod;
		OneToMany[ModContent.NPCType<CrimulanPaladin>()] = SlimeGod;
		SetupBossExclusionList();
		SetupMinibossHPBarList();
		SetupExtensionHandlerList();
		SetupRequirementsList();
	}

	public override void Unload()
	{
		BossMainHPBar = null;
		BossComboHPBar = null;
		BossSeperatorBar = null;
		HPBarFont = null;
		Bars = null;
		BossExclusionList = null;
		MinibossHPBarList = null;
		OneToMany = null;
		EntityExtensionHandler = null;
		SpecialHPRequirements = null;
	}

	public static void SetupBossExclusionList()
	{
		BossExclusionList.AddRange(new global::_003C_003Ez__ReadOnlyArray<int>(new int[30]
		{
			0,
			400,
			396,
			397,
			88,
			89,
			90,
			91,
			92,
			ModContent.NPCType<AquaticScourgeBody>(),
			ModContent.NPCType<AquaticScourgeBodyAlt>(),
			ModContent.NPCType<AquaticScourgeTail>(),
			ModContent.NPCType<AstrumDeusBody>(),
			ModContent.NPCType<AstrumDeusTail>(),
			ModContent.NPCType<BrainIllusion>(),
			ModContent.NPCType<DesertScourgeBody>(),
			ModContent.NPCType<DesertScourgeTail>(),
			ModContent.NPCType<FalseBrain>(),
			ModContent.NPCType<SlimeGodCore>(),
			ModContent.NPCType<StormWeaverBody>(),
			ModContent.NPCType<StormWeaverTail>(),
			ModContent.NPCType<DevourerofGodsBody>(),
			ModContent.NPCType<DevourerofGodsTail>(),
			ModContent.NPCType<ThanatosBody1>(),
			ModContent.NPCType<ThanatosBody2>(),
			ModContent.NPCType<ThanatosTail>(),
			ModContent.NPCType<AresGaussNuke>(),
			ModContent.NPCType<AresLaserCannon>(),
			ModContent.NPCType<AresPlasmaFlamethrower>(),
			ModContent.NPCType<AresTeslaCannon>()
		}));
	}

	public static void SetupMinibossHPBarList()
	{
		List<int> minibossHPBarList = MinibossHPBarList;
		int[] obj = new int[39]
		{
			551, 576, 577, 564, 565, 68, 471, 87, 290, 243,
			541, 473, 474, 475, 618, 325, 327, 344, 346, 345,
			477, 395, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0
		};
		obj[22] = ModContent.NPCType<GiantClam>();
		obj[23] = ModContent.NPCType<PerforatorHeadSmall>();
		obj[24] = ModContent.NPCType<PerforatorHeadMedium>();
		obj[25] = ModContent.NPCType<PerforatorHeadLarge>();
		obj[26] = ModContent.NPCType<CloudElemental>();
		obj[27] = ModContent.NPCType<EarthElemental>();
		obj[28] = ModContent.NPCType<GreatSandShark>();
		obj[29] = ModContent.NPCType<PlaguebringerMiniboss>();
		obj[30] = ModContent.NPCType<Cataclysm>();
		obj[31] = ModContent.NPCType<Catastrophe>();
		obj[32] = ModContent.NPCType<SupremeCataclysm>();
		obj[33] = ModContent.NPCType<SupremeCatastrophe>();
		obj[34] = ModContent.NPCType<ProvSpawnDefense>();
		obj[35] = ModContent.NPCType<ProvSpawnOffense>();
		obj[36] = ModContent.NPCType<ProvSpawnHealer>();
		obj[37] = ModContent.NPCType<ProfanedGuardianDefender>();
		obj[38] = ModContent.NPCType<ProfanedGuardianHealer>();
		minibossHPBarList.AddRange(new global::_003C_003Ez__ReadOnlyArray<int>(obj));
	}

	public static void SetupExtensionHandlerList()
	{
		Extensions.AddRange<int, BossEntityExtension>(EntityExtensionHandler, new Dictionary<int, BossEntityExtension>
		{
			[13] = new BossEntityExtension(CalamityUtils.GetText("UI.ExtensionName.Segments"), 13, 14, 15),
			[266] = new BossEntityExtension(CalamityUtils.GetText("UI.ExtensionName.Creepers"), 267),
			[35] = new BossEntityExtension(CalamityUtils.GetText("UI.ExtensionName.Hands"), 36),
			[127] = new BossEntityExtension(CalamityUtils.GetText("UI.ExtensionName.Arms"), 128, 129, 130, 131),
			[395] = new BossEntityExtension(CalamityUtils.GetText("UI.ExtensionName.Guns"), 393, 394),
			[491] = new BossEntityExtension(CalamityUtils.GetText("UI.ExtensionName.Cannons"), 492),
			[ModContent.NPCType<CeaselessVoid>()] = new BossEntityExtension(CalamityUtils.GetText("UI.ExtensionName.DarkEnergy"), ModContent.NPCType<DarkEnergy>()),
			[ModContent.NPCType<RavagerBody>()] = new BossEntityExtension(CalamityUtils.GetText("UI.ExtensionName.BodyParts"), ModContent.NPCType<RavagerClawLeft>(), ModContent.NPCType<RavagerClawRight>(), ModContent.NPCType<RavagerLegLeft>(), ModContent.NPCType<RavagerLegRight>())
		});
	}

	public static void SetupRequirementsList()
	{
		SpecialHPRequirements.Add((NPC npc) => npc.Calamity().SplittingWorm, delegate(NPC npc, bool checkingForMaxLife)
		{
			long num = 0L;
			NPC nPC = npc;
			int num2 = 0;
			while (Main.npc.IndexInRange((int)nPC.ai[0]) && Main.npc[(int)nPC.ai[0]].ai[1] == (float)nPC.whoAmI && nPC.active)
			{
				num += (checkingForMaxLife ? nPC.lifeMax : nPC.life);
				nPC = Main.npc[(int)nPC.ai[0]];
				num2++;
				if (num2 > Main.maxNPCs)
				{
					break;
				}
			}
			return num;
		});
		SpecialHPRequirements.Add((NPC npc) => npc.type == 398, delegate(NPC npc, bool checkingForMaxLife)
		{
			long num = (checkingForMaxLife ? npc.lifeMax : npc.life);
			if (npc.ai[0] == 2f)
			{
				num = 0L;
			}
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC current = enumerator.Current;
				if ((current.type == 397 || current.type == 396) && current.ai[3] == (float)npc.whoAmI && current.Calamity().newAI[0] != 1f)
				{
					num += (checkingForMaxLife ? current.lifeMax : current.life);
				}
			}
			return num;
		});
	}

	public override void Update(IBigProgressBar currentBar, ref BigProgressBarInfo info)
	{
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (!BossExclusionList.Contains(n.type))
			{
				bool isEoWSegment = n.type == 14 || n.type == 15;
				if ((n.IsABoss() && !isEoWSegment) || MinibossHPBarList.Contains(n.type) || n.Calamity().CanHaveBossHealthBar)
				{
					AttemptToAddBar(n.whoAmI);
				}
			}
		}
		for (int i = 0; i < Bars.Count; i++)
		{
			BossHPUI bossHPUI = Bars[i];
			bossHPUI.Update();
			if (bossHPUI.CloseAnimationTimer >= 120)
			{
				Bars.RemoveAt(i);
				i--;
			}
		}
	}

	public static void AttemptToAddBar(int index)
	{
		if (Bars.Count < MaximumBars)
		{
			NPC npc = Main.npc[index];
			bool canAddBar = npc.active && npc.life > 0 && Bars.All((BossHPUI b) => b.NPCIndex != index) && !npc.Calamity().ShouldCloseHPBar;
			string overridingName = null;
			if (npc.type == ModContent.NPCType<Artemis>())
			{
				canAddBar = false;
			}
			if (npc.type == ModContent.NPCType<Apollo>())
			{
				overridingName = CalamityUtils.GetTextValue("UI.ExoTwinsName" + (npc.ModNPC<Apollo>().exoMechdusa ? "Hekate" : "Normal"));
			}
			if (canAddBar)
			{
				Bars.Add(new BossHPUI(index, overridingName));
			}
		}
	}

	public override void Draw(SpriteBatch spriteBatch, IBigProgressBar currentBar, BigProgressBarInfo info)
	{
		int startHeight = 100;
		int x = Main.screenWidth - 420;
		int y = Main.screenHeight - startHeight;
		if (Main.playerInventory || Main.invasionType > 0 || Main.pumpkinMoon || Main.snowMoon || DD2Event.Ongoing || AcidRainEvent.AcidRainEventIsOngoing)
		{
			x -= 250;
		}
		foreach (BossHPUI bar in Bars)
		{
			bar.Draw(spriteBatch, x, y);
			y -= 70;
		}
	}
}
