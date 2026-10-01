using System.Collections.Generic;
using System.Linq;
using CalamityMod.Buffs.Summon;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

[LegacyName(new string[] { "StaffoftheMechworm" })]
public class VoidEaterMarionette : ModItem, ILocalizedModType, IModType
{
	public bool FocusFetch;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		tooltips.FindAndReplaceAll("ff00ff", DevourerofGodsHead.SpecialMoveColor.Hex3());
	}

	public override void SetDefaults()
	{
		base.Item.width = 68;
		base.Item.height = 68;
		base.Item.damage = 110;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.UseSound = SoundID.Item113;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<VoidEaterMarionetteBuff>();
		base.Item.shoot = ModContent.ProjectileType<VoidEaterMarionetteProjectile>();
		base.Item.DamageType = DamageClass.Summon;
		base.Item.shootSpeed = 8f;
	}

	public override bool CanRightClick()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.keyState.PressingShift())
		{
			return false;
		}
		return true;
	}

	public override void RightClick(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] > 0)
		{
			Projectile p = Main.projectile.First((Projectile x) => x.active && x.type == base.Item.shoot && x.owner == player.whoAmI);
			p.ModProjectile<VoidEaterMarionetteProjectile>().FocusOnFetching = !p.ModProjectile<VoidEaterMarionetteProjectile>().FocusOnFetching;
			p.netUpdate = true;
		}
	}

	public override bool ConsumeItem(Player player)
	{
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if (player.ownedProjectileCounts[type] > 0)
		{
			Projectile projectile = Main.projectile.First((Projectile x) => x.active && x.type == type && x.owner == player.whoAmI);
			projectile.ai[0]++;
			projectile.netUpdate = true;
			return false;
		}
		position = Main.MouseWorld;
		Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI, 2f);
		Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<DoGWeaponTeleportRift>(), 0, 0f, player.whoAmI);
		return false;
	}
}
