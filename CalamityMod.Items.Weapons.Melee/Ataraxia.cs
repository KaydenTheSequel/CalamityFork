using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Ataraxia : ModItem, ILocalizedModType, IModType
{
	public bool hitsound = true;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 94;
		base.Item.height = 92;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.damage = 675;
		base.Item.knockBack = 2.5f;
		base.Item.useAnimation = 10;
		base.Item.useTime = 10;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<AtaraxiaMain>();
		base.Item.shootSpeed = 10f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item60, position);
		int centerID = ModContent.ProjectileType<AtaraxiaMain>();
		int centerDamage = damage / 2;
		Projectile.NewProjectile(source, position, velocity, centerID, centerDamage, knockback, player.whoAmI);
		int sideID = ModContent.ProjectileType<AtaraxiaSide>();
		int sideDamage = (int)(0.75f * (float)centerDamage) / 2;
		Vector2 originalVelocity = velocity;
		((Vector2)(ref velocity)).Normalize();
		velocity *= 22f;
		Vector2 rrp = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		Vector2 leftOffset = velocity.RotatedBy(0.7853981852531433);
		Vector2 rightOffset = velocity.RotatedBy(-0.7853981852531433);
		leftOffset -= 1.4f * velocity;
		rightOffset -= 1.4f * velocity;
		Projectile.NewProjectile(source, new Vector2(rrp.X + leftOffset.X, rrp.Y + leftOffset.Y), originalVelocity, sideID, sideDamage, knockback, player.whoAmI, 0f, 1f);
		Projectile.NewProjectile(source, new Vector2(rrp.X + rightOffset.X, rrp.Y + rightOffset.Y), originalVelocity, sideID, sideDamage, knockback, player.whoAmI, 0f, 2f);
		hitsound = true;
		return false;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(153, 480);
		OnHitEffects(player, target.Center);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Shadowflame>(), 480);
		OnHitEffects(player, target.Center);
	}

	private void OnHitEffects(Player player, Vector2 targetPos)
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		if (hitsound)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/CursedDaggerThrow");
			style.Volume = 0.5f;
			style.Pitch = 0.9f;
			style.PitchVariance = 0.2f;
			style.MaxInstances = -1;
			SoundEngine.PlaySound(in style, player.Center);
			hitsound = false;
		}
		int trueMeleeID = ModContent.ProjectileType<AtaraxiaBoom>();
		int trueMeleeDamage = (int)player.GetTotalDamage<MeleeDamageClass>().ApplyTo(0.7f * (float)base.Item.damage);
		Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), targetPos, Vector2.Zero, trueMeleeID, trueMeleeDamage, base.Item.knockBack, player.whoAmI);
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		int dustCount = Main.rand.Next(3, 6);
		Vector2 corner = default(Vector2);
		((Vector2)(ref corner))._002Ector((float)(hitbox.X + hitbox.Width / 4), (float)(hitbox.Y + hitbox.Height / 4));
		for (int i = 0; i < dustCount; i++)
		{
			int dustID;
			switch (Main.rand.Next(5))
			{
			case 0:
			case 1:
				dustID = 70;
				break;
			case 2:
				dustID = 71;
				break;
			default:
				dustID = 86;
				break;
			}
			int idx = Dust.NewDust(corner, hitbox.Width / 2, hitbox.Height / 2, dustID);
			Main.dust[idx].noGravity = true;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1570).AddIngredient<AuricBar>(5).AddIngredient<CosmiliteBar>(8)
			.AddIngredient<AscendantSpiritEssence>(2)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
