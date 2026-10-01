using System;
using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

[LegacyName(new string[] { "Taser" })]
public class ShortCircuit : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle Fire = new SoundStyle("CalamityMod/Sounds/Item/TaserLaunch")
	{
		Volume = 0.6f
	};

	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(10);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 42;
		base.Item.height = 24;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.damage = 11;
		base.Item.knockBack = 7f;
		base.Item.useTime = (base.Item.useAnimation = 8);
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.shoot = ModContent.ProjectileType<ShortCircuitShot>();
		base.Item.shootSpeed = 6f;
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			return 0.3f;
		}
		return 1f;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse != 2 || player.ownedProjectileCounts[ModContent.ProjectileType<ShortCircuitHook>()] > 0)
		{
			return player.altFunctionUse == 0;
		}
		return true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2 && player.whoAmI == Main.myPlayer && !Main.mapFullscreen && !Main.blockMouse && player.Calamity().arsenalCooldown <= 0)
		{
			SoundStyle style = Fire with
			{
				Pitch = -0.1f
			};
			SoundEngine.PlaySound(in style, position);
			Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<ShortCircuitHook>(), (int)((float)damage * 1.2f), 0f, player.whoAmI);
			return false;
		}
		if (player.altFunctionUse == 0)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/GunShotSmall");
			style.Pitch = Main.rand.NextFloat(0.5f, 0.65f);
			style.Volume = 0.2f;
			style.MaxInstances = -1;
			SoundEngine.PlaySound(in style, position);
			Projectile.NewProjectileDirect(source, position + velocity - Vector2.UnitY * 5f, (velocity * 1.5f).RotatedByRandom(0.15000000596046448), type, damage, 0f, player.whoAmI);
			return false;
		}
		return false;
	}

	public override void HoldItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			player.Calamity().rightClickListener = true;
		}
		player.Calamity().mouseWorldListener = true;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 0)
		{
			player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
			float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
			float pullback = 7f;
			float animProgress = 0.5f - (float)player.itemTime / (float)player.itemTimeMax;
			(player.Center - player.Calamity().mouseWorld).ToRotation();
			_ = player.gravDir;
			if (animProgress < 0.4f)
			{
				pullback -= 2.75f * (float)Math.Pow((0.6f - animProgress) / 0.6f, 2.0);
			}
			Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * pullback;
			Vector2 itemSize = default(Vector2);
			((Vector2)(ref itemSize))._002Ector(42f, 24f);
			Vector2 itemOrigin = default(Vector2);
			((Vector2)(ref itemOrigin))._002Ector(-24f, 4f);
			CalamityUtils.CleanHoldStyle(player, itemRotation, itemPosition, itemSize, itemOrigin);
			base.UseStyle(player, heldItemFrame);
		}
	}

	public override void UseItemFrame(Player player)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 0)
		{
			player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
			float animProgress = 0.5f - (float)player.itemTime / (float)player.itemTimeMax;
			float rotation = (player.Center - player.Calamity().mouseWorld).ToRotation() * player.gravDir + (float)Math.PI / 2f;
			if (animProgress < 0.4f)
			{
				rotation += ((player.altFunctionUse == 2) ? (-0.15f) : 0f) * (float)Math.Pow((0.6f - animProgress) / 0.6f, 2.0) * (float)player.direction;
			}
			player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
		}
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(0f, 0f);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(7).AddIngredient<DubiousPlating>(5).AddIngredient<AerialiteBar>(4)
			.AddIngredient<SeaPrism>(7)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(1, out var condition), condition)
			.AddTile(16)
			.Register();
	}
}
