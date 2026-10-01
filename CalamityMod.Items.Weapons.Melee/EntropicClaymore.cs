using System;
using CalamityMod.Enums;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "XerocsGreatsword" })]
public class EntropicClaymore : ModItem, ILocalizedModType, IModType
{
	private int swordDirection;

	public int time;

	private float swingRotation;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 130;
		base.Item.height = 130;
		base.Item.damage = 90;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 1;
		base.Item.useTime = 30;
		base.Item.useTurn = true;
		base.Item.knockBack = 5.25f;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/SwingMid")
		{
			Volume = 0.5f,
			Pitch = Main.rand.NextFloat(-0.3f, -0.4f)
		};
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.shoot = ModContent.ProjectileType<EntropicFlechette>();
		base.Item.shootSpeed = 12f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.7f, 1.1f), type, damage / 2, knockback * 0.5f, player.whoAmI);
		}
		return false;
	}

	public override void UseAnimation(Player player)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		swordDirection = ((!((player.Center - player.Calamity().mouseWorld).X > 1f)) ? 1 : (-1));
		time = 0;
		swingRotation = 0f;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		player.itemRotation = swingRotation - 1.7f * (float)swordDirection;
		player.itemLocation = player.Center;
		player.direction = swordDirection;
		float val = MathF.Abs((float)time - (float)player.itemAnimationMax * 0.75f) / (float)player.itemAnimationMax;
		float goalRot = Utils.Remap(time, 0f, player.itemAnimationMax, -0.5f, 4.9f * (float)swordDirection);
		float swingEasing = Utils.GetLerpValue(0f, (float)player.itemAnimationMax * 0.4f, time, clamped: true) * (0.5f - val);
		if (time < player.itemAnimationMax)
		{
			swingRotation = MathHelper.Lerp(swingRotation, goalRot, swingEasing);
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, swingRotation + MathHelper.ToRadians(120f * (float)swordDirection));
		if (Main.rand.NextBool())
		{
			Vector2 dustVel = Utils.RotatedByRandom(new Vector2((float)(5 * swordDirection), -5f), 1.5499999523162842) * Main.rand.NextFloat(0.7f, 1.3f) * 2f;
			Dust dust = Dust.NewDustPerfect(player.Center + dustVel * 9f, 66);
			dust.scale = Main.rand.NextFloat(0.5f, 0.75f);
			dust.velocity = dustVel * 0.85f;
			dust.color = Color.LightGreen;
			dust.noGravity = true;
		}
		if (Main.rand.NextBool())
		{
			Vector2 dustVel2 = Utils.RotatedBy(new Vector2((float)(5 * swordDirection), -5f), (double)(swingRotation - 1.7f * (float)swordDirection), default(Vector2));
			float partScale = Main.rand.NextFloat(0.6f, 0.9f);
			Vector2 partVel = (dustVel2 * Main.rand.NextFloat(0.2f, 0.3f)).RotatedBy(MathHelper.ToRadians(90f * (float)swordDirection)).RotatedByRandom(-0.4) * -3f;
			Vector2 relativePosition = player.Center + dustVel2 * 25f + Main.rand.NextVector2Circular(12f, 12f);
			GeneralParticleHandler.SpawnParticle(new AltSparkParticle(relativePosition, partVel, affectedByGravity: false, 24, partScale, Color.Black));
			SparkParticle sparkParticle = new SparkParticle(relativePosition, partVel, affectedByGravity: false, 24, partScale * 0.6f, Color.LightGreen);
			GeneralParticleHandler.SpawnParticle(sparkParticle);
			sparkParticle.DrawLayer = GeneralDrawLayer.AfterEverything;
		}
		Vector2 dustVel3 = Utils.RotatedBy(new Vector2((float)(5 * swordDirection), -5f), (double)(swingRotation - 1.7f * (float)swordDirection), default(Vector2));
		float partScale2 = Main.rand.NextFloat(0.8f, 1.2f);
		Vector2 partVel2 = dustVel3 * Main.rand.NextFloat(0.2f, 0.3f);
		GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(player.Center + dustVel3 * 20f + Main.rand.NextVector2Circular(12f, 12f), partVel2.RotatedBy(MathHelper.ToRadians(90f * (float)swordDirection)).RotatedBy(-0.3 * (double)swordDirection) * -5f, Color.Black, 19, partScale2, 0.5f, Main.rand.NextFloat(-0.2f, 0.2f)));
		time++;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MeldConstruct>(15).AddTile(412).Register();
	}
}
