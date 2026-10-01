using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.DataStructures;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Weapons.Melee;

public class FourSeasonsGalaxia : ModItem, ILocalizedModType, IModType
{
	public Attunement mainAttunement;

	public int UseTimer;

	public bool OnHitProc;

	public static int BaseDamage = 250;

	public static int PhoenixAttunement_BaseDamage = 300;

	public static int PhoenixAttunement_LocalIFrames = 30;

	public static float PhoenixAttunement_BoltDamageReduction = 0.5f;

	public static float PhoenixAttunement_BoltThrowDamageMultiplier = 1f;

	public static float PhoenixAttunement_BaseDamageReduction = 0.5f;

	public static float PhoenixAttunement_FullChargeDamageBoost = 2.1f;

	public static float PhoenixAttunement_ThrowDamageBoost = 3.2f;

	public static int PhoenixAttunement_FlamePillarLocalIFrames = 10;

	public static int PolarisAttunement_BaseDamage = 400;

	public static int PolarisAttunement_FullChargeDamage = 630;

	public static int PolarisAttunement_ShredIFrames = 10;

	public static int PolarisAttunement_LocalIFrames = 30;

	public static int PolarisAttunement_LocalIFramesCharged = 16;

	public static float PolarisAttunement_SlashDamageBoost = 6f;

	public static int PolarisAttunement_SlashIFrames = 20;

	public static float PolarisAttunement_ShotDamageBoost = 0.8f;

	public static float PolarisAttunement_ShredChargeupGain = 1f;

	public static int AndromedaAttunement_BaseDamage = 1120;

	public static int AndromedaAttunement_DashHitIFrames = 20;

	public static float AndromedaAttunement_FullChargeMult = 3.5f;

	public static float AndromedaAttunement_StarDamageMultiplier = 1f;

	public static float AndromedaAttunement_ChargeupBoltDamageMultiplier = 0.2f;

	public static int AriesAttunement_BaseDamage = 375;

	public static int AriesAttunement_LocalIFrames = 10;

	public static int AriesAttunement_Reach = 650;

	public static float AriesAttunement_ChainDamageReduction = 0.2f;

	public static float AriesAttunement_OnHitBoltDamageReduction = 0.5f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/Galaxia";

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		if (list == null)
		{
			return;
		}
		SafeCheckAttunements();
		if (Main.LocalPlayer == null)
		{
			return;
		}
		TooltipLine effectDescTooltip = list.FirstOrDefault((TooltipLine x) => x.Text.Contains("[FUNC]") && x.Mod == "Terraria");
		TooltipLine mainAttunementTooltip = list.FirstOrDefault((TooltipLine x) => x.Text.Contains("[ATT]") && x.Mod == "Terraria");
		if (mainAttunement == null)
		{
			CalamityMod.Log.Error((object)"No main attunement on galaxia, couldn't edit its tooltip properly. How the hell did that happen.");
			return;
		}
		if (effectDescTooltip != null)
		{
			effectDescTooltip.Text = Lang.SupportGlyphs(mainAttunement.FunctionText.ToString());
			effectDescTooltip.OverrideColor = mainAttunement.tooltipColor;
		}
		if (mainAttunementTooltip != null)
		{
			mainAttunementTooltip.Text = mainAttunementTooltip.Text.Replace("ATT", mainAttunement.AttunementName.ToString());
			mainAttunementTooltip.OverrideColor = Color.Lerp(mainAttunement.tooltipColor, mainAttunement.tooltipColor2, 0.5f + (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.5f);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 128);
		base.Item.damage = BaseDamage;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 18;
		base.Item.useTime = 18;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 9f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = 10;
		base.Item.shootSpeed = 24f;
		base.Item.reuseDelay = 12;
	}

	public override ModItem Clone(Item item)
	{
		ModItem modItem = base.Clone(item);
		if (Main.mouseItem.type == ModContent.ItemType<FourSeasonsGalaxia>())
		{
			item.ModItem?.HoldItem(Main.LocalPlayer);
		}
		if (modItem is FourSeasonsGalaxia a && item.ModItem is FourSeasonsGalaxia a2)
		{
			a.mainAttunement = a2.mainAttunement;
		}
		return modItem;
	}

	public override void SaveData(TagCompound tag)
	{
		int attunement1 = ((mainAttunement == null) ? (-1) : ((int)mainAttunement.id));
		tag["mainAttunement"] = attunement1;
	}

	public override void LoadData(TagCompound tag)
	{
		int attunement1 = tag.GetInt("mainAttunement");
		mainAttunement = AttunementSystem.FindOrNull(attunement1);
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write((mainAttunement != null) ? ((int)mainAttunement.id) : AttunementSystem.EmptyID);
	}

	public override void NetReceive(BinaryReader reader)
	{
		mainAttunement = AttunementSystem.FindOrNull(reader.ReadInt32());
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (mainAttunement == null || player.altFunctionUse != 0)
		{
			return false;
		}
		return true;
	}

	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		damage += (mainAttunement?.DamageMultiplier ?? 1f) - 1f;
	}

	public void SafeCheckAttunements()
	{
		if (mainAttunement == null)
		{
			mainAttunement = AttunementSystem.FindOrNull(AttunementID.Phoenix);
		}
		else
		{
			mainAttunement = AttunementSystem.FindOrNull(ClampAttunementRange((int)mainAttunement.id));
		}
	}

	private static int ClampAttunementRange(int input)
	{
		if (input < 14)
		{
			return 14;
		}
		if (input > 17)
		{
			return 17;
		}
		return input;
	}

	public override void HoldItem(Player player)
	{
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().rightClickListener = true;
		player.Calamity().mouseWorldListener = true;
		if (CanUseItem(player))
		{
			player.Calamity().LungingDown = false;
		}
		else
		{
			UseTimer++;
		}
		SafeCheckAttunements();
		mainAttunement.ApplyStats(base.Item);
		if (player.Calamity().mouseRight && CanUseItem(player) && player.whoAmI == Main.myPlayer && !Main.mapFullscreen && !Main.projectile.Any((Projectile n) => n.active && n.type == ModContent.ProjectileType<GalaxiaHoldout>() && n.owner == player.whoAmI))
		{
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), player.Top, Vector2.Zero, ModContent.ProjectileType<GalaxiaHoldout>(), 0, 0f, player.whoAmI, 0f, Math.Sign(player.position.X - Main.MouseWorld.X));
		}
	}

	public override void UpdateInventory(Player player)
	{
		SafeCheckAttunements();
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 0)
		{
			return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && (n.type == ModContent.ProjectileType<PhoenixsPride>() || n.type == ModContent.ProjectileType<AndromedasStride>() || n.type == ModContent.ProjectileType<PolarisGaze>() || n.type == ModContent.ProjectileType<AriesWrath>()));
		}
		return false;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		if (mainAttunement == null)
		{
			mainAttunement = AttunementSystem.FindOrNull(AttunementID.Phoenix);
		}
		Texture2D itemTexture = ModContent.Request<Texture2D>((mainAttunement.id == AttunementID.Polaris || mainAttunement.id == AttunementID.Andromeda) ? "CalamityMod/Items/Weapons/Melee/GalaxiaDusk" : "CalamityMod/Items/Weapons/Melee/GalaxiaDawn", (AssetRequestMode)2).Value;
		Texture2D outlineTexture = ModContent.Request<Texture2D>((mainAttunement.id == AttunementID.Polaris || mainAttunement.id == AttunementID.Andromeda) ? "CalamityMod/Items/Weapons/Melee/GalaxiaDuskOutline" : "CalamityMod/Items/Weapons/Melee/GalaxiaDawnOutline", (AssetRequestMode)2).Value;
		int currentFrame = (int)Math.Floor(Main.GlobalTimeWrappedHourly * 15f) % 7;
		Rectangle animFrame = default(Rectangle);
		((Rectangle)(ref animFrame))._002Ector(0, 128 * currentFrame, 126, 126);
		spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.NonPremultiplied, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		spriteBatch.Draw(outlineTexture, position, (Rectangle?)animFrame, Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		spriteBatch.Draw(itemTexture, position, (Rectangle?)animFrame, Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		if (mainAttunement == null)
		{
			mainAttunement = AttunementSystem.FindOrNull(AttunementID.Phoenix);
		}
		Texture2D itemTexture = ModContent.Request<Texture2D>((mainAttunement.id == AttunementID.Polaris || mainAttunement.id == AttunementID.Andromeda) ? "CalamityMod/Items/Weapons/Melee/GalaxiaDusk" : "CalamityMod/Items/Weapons/Melee/GalaxiaDawn", (AssetRequestMode)2).Value;
		Texture2D outlineTexture = ModContent.Request<Texture2D>((mainAttunement.id == AttunementID.Polaris || mainAttunement.id == AttunementID.Andromeda) ? "CalamityMod/Items/Weapons/Melee/GalaxiaDuskOutline" : "CalamityMod/Items/Weapons/Melee/GalaxiaDawnOutline", (AssetRequestMode)2).Value;
		int currentFrame = (int)Math.Floor(Main.GlobalTimeWrappedHourly * 15f) % 7;
		Rectangle animFrame = default(Rectangle);
		((Rectangle)(ref animFrame))._002Ector(0, 128 * currentFrame, 126, 126);
		spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.NonPremultiplied, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.Transform);
		spriteBatch.Draw(outlineTexture, base.Item.Center - Main.screenPosition, (Rectangle?)animFrame, lightColor, rotation, base.Item.Size * 0.5f, scale, (SpriteEffects)0, 0f);
		spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.Transform);
		spriteBatch.Draw(itemTexture, base.Item.Center - Main.screenPosition, (Rectangle?)animFrame, lightColor, rotation, base.Item.Size * 0.5f, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OmegaBiomeBlade>().AddIngredient<ArmoredShell>().AddIngredient<TwistingNether>()
			.AddIngredient<DarkPlasma>()
			.AddTile(134)
			.Register();
	}
}
