using System;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Atlantis : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 81;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 12;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/VividClarityBeamAppear")
		{
			Volume = 0.5f,
			Pitch = 0.2f
		};
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AtlantisSpear>();
		base.Item.shootSpeed = 40f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		int points = 6;
		float radians = (float)Math.PI * 2f / (float)points;
		Vector2 spinningPoint = Vector2.Normalize(velocity);
		for (int k = 0; k < points; k++)
		{
			Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)k);
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(position + velocity2 * 7.5f + velocity * 2.5f, velocity2 * 0.5f, affectedByGravity: false, 11, 0.01f, Color.LightBlue * 0.9f, new Vector2(3.5f, 1f), quickShrink: true, glow: false));
			for (int b = 0; b < 3; b++)
			{
				Dust dust = Dust.NewDustPerfect(position + velocity * 2.5f, 278, (velocity2 * 10f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.5f, 0.9f));
				dust.scale = Main.rand.NextFloat(0.3f, 0.5f);
				dust.color = (Main.rand.NextBool() ? Color.Cyan : Color.LightBlue);
				dust.noGravity = true;
			}
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(position + velocity * 2.5f, Vector2.Zero, Color.CornflowerBlue, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 1.25f, 0f, 11, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(position + velocity * 2.5f, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.6f, 0.1f, 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		Projectile.NewProjectile(source, position + velocity * 2f, velocity, type, damage, knockback, player.whoAmI, 0f, 0f, 5f);
		return false;
	}
}
