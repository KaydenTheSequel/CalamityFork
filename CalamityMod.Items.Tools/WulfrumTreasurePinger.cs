using System;
using System.IO;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Systems;
using CalamityMod.Systems.Mechanic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Tools;

public class WulfrumTreasurePinger : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ScanBeepSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumPing")
	{
		PitchVariance = 0.1f
	};

	public static readonly SoundStyle ScanBeepBreakSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumPingBreak");

	public static readonly SoundStyle RechargeBeepSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumPingReady")
	{
		PitchVariance = 0.1f
	};

	public int usesLeft = 20;

	public const int maxUses = 20;

	public static int breakTime = 90;

	public int timeBeforeBlast = 90;

	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 42;
		base.Item.useAnimation = (base.Item.useTime = 25);
		base.Item.autoReuse = false;
		base.Item.holdStyle = 16;
		base.Item.useStyle = 5;
		base.Item.UseSound = null;
		base.Item.noMelee = true;
		base.Item.rare = 1;
		base.Item.value = Item.sellPrice(0, 0, 10);
		usesLeft = 20;
		timeBeforeBlast = breakTime;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)820;
	}

	public override void HoldItem(Player player)
	{
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().mouseWorldListener = true;
		if (usesLeft <= 0 && timeBeforeBlast < breakTime)
		{
			timeBeforeBlast--;
			player.itemTime = 2;
			player.itemAnimation = 2;
			float breakProgress = 1f - (float)timeBeforeBlast / (float)breakTime;
			int smokeLikelyhood = 1 + (int)Math.Floor((float)timeBeforeBlast / (float)breakTime * 4f);
			if (Main.rand.NextBool(smokeLikelyhood))
			{
				Vector2 position = player.GetBackHandPosition(player.compositeBackArm.stretch, player.compositeBackArm.rotation).Floor();
				Vector2 velocity = -Vector2.UnitY.RotatedByRandom(1.0995573997497559) * Main.rand.NextFloat(6f, 9f + 6f * breakProgress);
				Color smokeStart = (Main.rand.NextBool() ? Color.GreenYellow : Color.DeepSkyBlue);
				Color smokeEnd = Color.Lerp(new Color(110, 110, 110), new Color(60, 60, 60), breakProgress);
				float smokeSize = Main.rand.NextFloat(1.4f, 2.2f) * (0.6f + 0.4f * breakProgress);
				GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(position, velocity, smokeStart, smokeEnd, smokeSize, 115 - Main.rand.Next(30)));
			}
		}
		if (timeBeforeBlast > 0)
		{
			return;
		}
		int scrapRefund = Main.rand.Next(0, 4);
		if (scrapRefund > 0)
		{
			player.QuickSpawnItem(base.Item.GetSource_FromThis(), ModContent.ItemType<WulfrumMetalScrap>(), scrapRefund);
		}
		base.Item.TurnToAir();
		int smokeCount = Main.rand.Next(5, 10);
		int shrapnelCount = Main.rand.Next(3, 5);
		int sparkCount = Main.rand.Next(4, 8);
		Vector2 centerPosition = player.GetBackHandPosition(player.compositeBackArm.stretch, player.compositeBackArm.rotation).Floor();
		Color smokeEnd2 = default(Color);
		for (int i = 0; i < smokeCount; i++)
		{
			Vector2 velocity2 = -Vector2.UnitY.RotatedByRandom(1.5707963705062866) * Main.rand.NextFloat(12f, 16f);
			Color smokeStart2 = (Main.rand.NextBool() ? Color.GreenYellow : Color.Aqua);
			((Color)(ref smokeEnd2))._002Ector(60, 60, 60);
			float smokeSize2 = Main.rand.NextFloat(1.4f, 2.2f);
			GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(centerPosition, velocity2, smokeStart2, smokeEnd2, smokeSize2, 135 - Main.rand.Next(30)));
		}
		if (!Main.dedServ)
		{
			for (int j = 0; j < shrapnelCount; j++)
			{
				Vector2 shrapnelVelocity = Main.rand.NextVector2Circular(9f, 9f);
				float shrapnelScale = Main.rand.NextFloat(0.8f, 1f);
				Gore.NewGore(base.Item.GetSource_FromThis(), centerPosition, shrapnelVelocity, base.Mod.Find<ModGore>("WulfrumPinger" + Main.rand.Next(1, 5)).Type, shrapnelScale);
			}
		}
		for (int k = 0; k < sparkCount; k++)
		{
			Vector2? velocity3 = Main.rand.NextVector2Circular(18f, 18f);
			float scale = Main.rand.NextFloat(0.4f, 1f);
			Dust.NewDustPerfect(centerPosition, 226, velocity3, 0, default(Color), scale);
		}
	}

	public override bool CanUseItem(Player player)
	{
		return !WulfrumPingTileEffect.Instance.Active;
	}

	public override bool? UseItem(Player player)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ && TilePingerSystem.AddPing(WulfrumPingTileEffect.Instance, player.Center, player))
		{
			if (player.name != "John Wulfrum")
			{
				usesLeft--;
			}
			if (usesLeft <= 0)
			{
				if (player.whoAmI == Main.myPlayer)
				{
					SoundEngine.PlaySound(in ScanBeepBreakSound);
				}
				timeBeforeBlast--;
			}
			else if (player.whoAmI == Main.myPlayer)
			{
				SoundEngine.PlaySound(in ScanBeepSound);
			}
			return true;
		}
		return false;
	}

	public void SetItemInHand(Player player, Rectangle heldItemFrame)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().mouseWorld.X > player.Center.X)
		{
			player.ChangeDir(1);
		}
		else
		{
			player.ChangeDir(-1);
		}
		float itemRotation = player.compositeBackArm.rotation + (float)Math.PI / 2f * player.gravDir;
		Vector2 itemPosition = player.GetBackHandPositionImproved(player.compositeBackArm).Floor();
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector(52f, 42f);
		Vector2 itemOrigin = default(Vector2);
		((Vector2)(ref itemOrigin))._002Ector(-20f, -13f);
		if (usesLeft == 0)
		{
			itemPosition += Main.rand.NextVector2CircularEdge(4f, 4f) * (1f - (float)timeBeforeBlast / (float)breakTime);
			timeBeforeBlast--;
		}
		CalamityUtils.CleanHoldStyle(player, itemRotation, itemPosition, itemSize, itemOrigin);
	}

	public void SetPlayerArms(Player player)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		float armPointingDirection = ((player.Calamity().mouseWorld - player.Center).SafeNormalize(Vector2.UnitX).ToRotation() + (float)Math.PI / 2f).Modulo((float)Math.PI * 2f);
		if (armPointingDirection < (float)Math.PI)
		{
			armPointingDirection = armPointingDirection / (float)Math.PI * ((float)Math.PI / 4f) * 0.5f - 0.23561947f;
		}
		else
		{
			armPointingDirection -= (float)Math.PI;
			armPointingDirection = armPointingDirection / (float)Math.PI * ((float)Math.PI / 4f) * 0.5f - 0.23561947f + (float)Math.PI;
		}
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, armPointingDirection * player.gravDir - (float)Math.PI / 2f);
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, armPointingDirection * player.gravDir - (float)Math.PI / 2f);
	}

	public override void HoldStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetItemInHand(player, heldItemFrame);
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetItemInHand(player, heldItemFrame);
	}

	public override void HoldItemFrame(Player player)
	{
		SetPlayerArms(player);
	}

	public override void UseItemFrame(Player player)
	{
		SetPlayerArms(player);
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		if (usesLeft != 20 && usesLeft != 0)
		{
			float barScale = 1.3f;
			Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
			Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
			Vector2 drawPos = position + Vector2.UnitY * (float)(frame.Height - 2) * scale + Vector2.UnitX * ((float)frame.Width - (float)barBG.Width * barScale) * scale * 0.5f;
			Rectangle frameCrop = default(Rectangle);
			((Rectangle)(ref frameCrop))._002Ector(0, 0, (int)((float)usesLeft / 20f * (float)barFG.Width), barFG.Height);
			Color colorBG = Color.RoyalBlue;
			Color colorFG = Color.Lerp(Color.Teal, Color.YellowGreen, (float)usesLeft / 20f);
			spriteBatch.Draw(barBG, drawPos, (Rectangle?)null, colorBG, 0f, origin, scale * barScale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(barFG, drawPos, (Rectangle?)frameCrop, colorFG * 0.8f, 0f, origin, scale * barScale, (SpriteEffects)0, 0f);
		}
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		if (usesLeft > 0)
		{
			return true;
		}
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		float blastProgress = 1f - (float)timeBeforeBlast / (float)breakTime;
		position += Main.rand.NextVector2CircularEdge(4f, 4f) * blastProgress;
		drawColor = Color.Lerp(drawColor, Color.OrangeRed, blastProgress);
		spriteBatch.Draw(tex, position, (Rectangle?)frame, drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override ModItem Clone(Item item)
	{
		ModItem modItem = base.Clone(item);
		if (modItem is WulfrumTreasurePinger a && item.ModItem is WulfrumTreasurePinger a2)
		{
			a.usesLeft = a2.usesLeft;
			a.timeBeforeBlast = a2.timeBeforeBlast;
		}
		return modItem;
	}

	public override void SaveData(TagCompound tag)
	{
		tag["usesLeft"] = usesLeft;
	}

	public override void LoadData(TagCompound tag)
	{
		usesLeft = tag.GetInt("usesLeft");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(usesLeft);
	}

	public override void NetReceive(BinaryReader reader)
	{
		usesLeft = reader.ReadInt32();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(5).Register().DisableDecraft();
	}
}
