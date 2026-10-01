using CalamityMod.Projectiles.Healing;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class BurntSienna : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 54;
		base.Item.damage = 32;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 21);
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 5.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.shootSpeed = 5f;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = player.GetSource_ItemUse(base.Item);
		if (target.life <= 0 && !player.moonLeech)
		{
			float randomSpeedX = Main.rand.Next(3);
			float randomSpeedY = Main.rand.Next(3, 5);
			Projectile.NewProjectile(source, target.Center.X, target.Center.Y, 0f - randomSpeedX, 0f - randomSpeedY, ModContent.ProjectileType<BurntSiennaProj>(), 0, 0f, player.whoAmI, player.whoAmI);
			Projectile.NewProjectile(source, target.Center.X, target.Center.Y, randomSpeedX, 0f - randomSpeedY, ModContent.ProjectileType<BurntSiennaProj>(), 0, 0f, player.whoAmI, player.whoAmI);
			Projectile.NewProjectile(source, target.Center.X, target.Center.Y, 0f, 0f - randomSpeedY, ModContent.ProjectileType<BurntSiennaProj>(), 0, 0f, player.whoAmI, player.whoAmI);
		}
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = player.GetSource_ItemUse(base.Item);
		if (target.statLife <= 0 && !player.moonLeech)
		{
			float randomSpeedX = Main.rand.Next(3);
			float randomSpeedY = Main.rand.Next(3, 5);
			Projectile.NewProjectile(source, target.Center.X, target.Center.Y, 0f - randomSpeedX, 0f - randomSpeedY, ModContent.ProjectileType<BurntSiennaProj>(), 0, 0f, player.whoAmI, player.whoAmI);
			Projectile.NewProjectile(source, target.Center.X, target.Center.Y, randomSpeedX, 0f - randomSpeedY, ModContent.ProjectileType<BurntSiennaProj>(), 0, 0f, player.whoAmI, player.whoAmI);
			Projectile.NewProjectile(source, target.Center.X, target.Center.Y, 0f, 0f - randomSpeedY, ModContent.ProjectileType<BurntSiennaProj>(), 0, 0f, player.whoAmI, player.whoAmI);
		}
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 246);
		}
	}
}
