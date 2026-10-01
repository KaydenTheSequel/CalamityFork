using System;
using System.Linq;
using CalamityMod.Buffs.Summon.Whips;
using CalamityMod.DataStructures;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Systems.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class Cnidarian : ModItem, ILocalizedModType, IModType
{
	public static SummonTag summonTag = new SummonTag
	{
		FlatTagDamage = 2
	};

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override string Texture => "CalamityMod/Items/Weapons/Summon/CnidarianFishingRod";

	public override void SetStaticDefaults()
	{
		summonTag.TagItem = base.Type;
		CalamityBuffSets.SummonTagDebuff.Add(ModContent.BuffType<CnidarianSummonTagBuff>(), summonTag);
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 26;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.damage = 8;
		base.Item.knockBack = 3f;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.autoReuse = true;
		base.Item.holdStyle = 16;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<CnidarianJellyfishOnTheString>();
		base.Item.shootSpeed = 10f;
		base.Item.rare = 2;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
	}

	public override bool CanUseItem(Player player)
	{
		return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<CnidarianJellyfishOnTheString>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(2).AddTile(16).Register();
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public void SetItemInHand(Player player, Rectangle heldItemFrame)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().mouseWorld.X > player.Center.X)
		{
			player.ChangeDir(1);
		}
		else
		{
			player.ChangeDir(-1);
		}
		CalamityUtils.CleanHoldStyle(player, player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir, player.GetFrontHandPositionImproved(player.compositeFrontArm), new Vector2(42f, 34f), (Vector2?)new Vector2(-15f, 11f), true, false, true);
	}

	public void SetPlayerArms(Player player)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		float armPointingDirection = (player.Calamity().mouseWorld - player.Center).SafeNormalize(Vector2.UnitX).ToRotation();
		armPointingDirection = ((armPointingDirection < (float)Math.PI / 2f && armPointingDirection >= -(float)Math.PI / 2f) ? (-(float)Math.PI / 4f + (float)Math.PI / 2f * Utils.GetLerpValue(0f, (float)Math.PI, armPointingDirection + (float)Math.PI / 2f, clamped: true)) : ((!(armPointingDirection > 0f)) ? (-(float)Math.PI + (float)Math.PI / 4f * Utils.GetLerpValue(-(float)Math.PI, -(float)Math.PI / 4f, armPointingDirection, clamped: true)) : ((float)Math.PI * 3f / 4f + (float)Math.PI / 4f * Utils.GetLerpValue(0f, (float)Math.PI / 2f, armPointingDirection - (float)Math.PI / 2f, clamped: true))));
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

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		Texture2D properSprite = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/Cnidarian", (AssetRequestMode)2).Value;
		spriteBatch.DrawNewInventorySprite(properSprite, new Vector2(42f, 34f), position, drawColor, origin, scale);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Texture2D properSprite = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/Cnidarian", (AssetRequestMode)2).Value;
		spriteBatch.Draw(properSprite, base.Item.position - Main.screenPosition, (Rectangle?)null, lightColor, rotation, properSprite.Size() / 2f, scale, (SpriteEffects)0, 0f);
		return false;
	}
}
