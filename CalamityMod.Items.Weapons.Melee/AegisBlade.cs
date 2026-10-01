using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class AegisBlade : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 72;
		base.Item.height = 72;
		base.Item.damage = 152;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 16);
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 4.25f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 14f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.DamageType = DamageClass.Melee;
			base.Item.noMelee = true;
			base.Item.UseSound = SoundID.Item73;
			base.Item.shoot = ModContent.ProjectileType<AegisBeam>();
		}
		else
		{
			base.Item.DamageType = TrueMeleeDamageClass.Instance;
			base.Item.noMelee = false;
			base.Item.UseSound = SoundID.Item1;
			base.Item.shoot = 0;
		}
		return base.CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<AegisBeam>(), (int)((double)damage * 0.3), knockback, player.whoAmI);
		return false;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 246, 0f, 0f, 0, new Color(255, Main.DiscoG, 53));
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center, Vector2.Zero, ModContent.ProjectileType<AegisBlast>(), base.Item.damage, base.Item.knockBack, Main.myPlayer);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center, Vector2.Zero, ModContent.ProjectileType<AegisBlast>(), base.Item.damage, base.Item.knockBack, Main.myPlayer);
	}
}
