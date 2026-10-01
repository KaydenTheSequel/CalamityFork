using System.Collections.Generic;
using CalamityMod.Items.BaseItems;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Hellkite : CustomUseProjItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle SwingSound = new SoundStyle("CalamityMod/Sounds/Item/HellkiteSwing", 2);

	public static readonly SoundStyle SwingSoundBig = new SoundStyle("CalamityMod/Sounds/Item/HellkiteHeavySwing");

	public static readonly SoundStyle HitSoundSmall = new SoundStyle("CalamityMod/Sounds/Item/HellkiteSmallHit", 3);

	public static readonly SoundStyle HitSoundBig = new SoundStyle("CalamityMod/Sounds/Item/HellkiteBigHit", 2);

	public static readonly SoundStyle ChargeSound = new SoundStyle("CalamityMod/Sounds/Item/HellkiteCharge");

	public static readonly SoundStyle FullChargeSound = new SoundStyle("CalamityMod/Sounds/Item/HellkiteFullCharge");

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 124;
		base.Item.height = 124;
		base.Item.damage = 760;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 71);
		base.Item.useTurn = true;
		base.Item.knockBack = 13f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<HellkiteHoldout>();
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().mouseRight)
		{
			Projectile.NewProjectile(source, player.MountedCenter, Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 0f, 5f);
		}
		else
		{
			Projectile.NewProjectile(source, player.MountedCenter, Vector2.Zero, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/HellkiteGlow", (AssetRequestMode)2).Value);
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[GFB]", Lang.SupportGlyphs(this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal")));
	}
}
