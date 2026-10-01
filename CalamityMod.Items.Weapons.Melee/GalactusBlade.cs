using System;
using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "GalacticaBlade" })]
public class GalactusBlade : ModItem, ILocalizedModType, IModType
{
	private int swordDirection;

	public int time;

	private float swingRotation;

	public Color useColor;

	public bool spawnProj;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 144;
		base.Item.height = 146;
		base.Item.damage = 185;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 27;
		base.Item.useStyle = 1;
		base.Item.useTime = 27;
		base.Item.useTurn = true;
		base.Item.knockBack = 17f;
		base.Item.UseSound = SoundID.Item105;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<GalacticaComet>();
		base.Item.shootSpeed = 13f;
		base.Item.scale = 0.75f;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
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
		spawnProj = true;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		int swordSize = (int)(33f * base.Item.scale);
		float rate = Main.GlobalTimeWrappedHourly * 18f;
		List<Color> eColors = new List<Color>
		{
			Color.Gold,
			Color.HotPink,
			Color.Cyan
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		useColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		player.itemRotation = swingRotation - 1.7f * (float)swordDirection;
		player.itemLocation = player.Center;
		player.direction = swordDirection;
		float val = MathF.Abs((float)time - (float)player.itemAnimationMax * 0.75f) / (float)player.itemAnimationMax;
		float goalRot = Utils.Remap(time, 0f, player.itemAnimationMax, -0.5f, 5.2f) * (float)swordDirection;
		float swingEasing = Utils.GetLerpValue(0f, (float)player.itemAnimationMax * 0.4f, time, clamped: true) * (0.35f - val);
		if (time < player.itemAnimationMax)
		{
			swingRotation = MathHelper.Lerp(swingRotation, goalRot, swingEasing);
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, swingRotation + MathHelper.ToRadians(120f * (float)swordDirection));
		if (val < 0.4f)
		{
			if (spawnProj)
			{
				for (int i = 0; i < 7; i++)
				{
					Vector2 spawnSpot = new Vector2(player.ClampedMouseWorld().X, player.Center.Y) + new Vector2(Main.rand.NextFloat(-850f, 850f), Main.rand.NextFloat(-750f, -1250f));
					Projectile.NewProjectileDirect(player.GetSource_ItemUse(base.Item), spawnSpot, spawnSpot.DirectionTo(player.Calamity().mouseWorld + Main.rand.NextVector2Circular(50f, 50f)) * base.Item.shootSpeed, base.Item.shoot, base.Item.damage, base.Item.knockBack, player.whoAmI).extraUpdates = Main.rand.Next(2, 4);
				}
				spawnProj = false;
			}
			for (int j = 0; j < 3; j++)
			{
				Vector2 dustVel = Utils.RotatedBy(new Vector2((float)(5 * swordDirection), -5f), (double)(swingRotation - 1.7f * (float)swordDirection), default(Vector2));
				float partScale = Main.rand.NextFloat(0.6f, 0.9f);
				Vector2 partVel = (dustVel * Main.rand.NextFloat(0.2f, 0.3f)).RotatedBy(MathHelper.ToRadians(90f * (float)swordDirection)).RotatedByRandom(-0.2) * -3f;
				GeneralParticleHandler.SpawnParticle(new SparkParticle(player.Center + dustVel.RotatedByRandom(0.4000000059604645) * (float)swordSize, partVel, affectedByGravity: false, 14, partScale * 0.6f, useColor));
			}
		}
		Vector2 dustVel2 = Utils.RotatedBy(new Vector2((float)(5 * swordDirection), -5f), (double)(swingRotation - 1.7f * (float)swordDirection), default(Vector2));
		float partScale2 = Main.rand.NextFloat(0.3f, 0.7f);
		Vector2 partVel2 = dustVel2 * Main.rand.NextFloat(0.2f, 0.3f);
		for (int k = 0; k < 5; k++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(player.Center + dustVel2 * (float)Main.rand.Next(1, swordSize + 1) + Main.rand.NextVector2Circular(12f, 12f), partVel2.RotatedBy(MathHelper.ToRadians(90f * (float)swordDirection)).RotatedBy(-0.3 * (double)swordDirection) * Main.rand.NextFloat(-1f, -10f), "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 12, partScale2 * 0.5f, useColor * 0.4f, Vector2.One, useAddativeBlend: true, glowCenter: false, 3f));
		}
		Vector2 position = player.Center + dustVel2;
		Color white = Color.White;
		Lighting.AddLight(position, ((Color)(ref white)).ToVector3() * 1.2f);
		time++;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3065).AddIngredient<DivineGeode>(10).AddIngredient<GalacticaSingularity>(5)
			.AddTile(134)
			.Register();
	}

	public GalactusBlade()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		useColor = Color.White;
		spawnProj = true;
		base._002Ector();
	}
}
