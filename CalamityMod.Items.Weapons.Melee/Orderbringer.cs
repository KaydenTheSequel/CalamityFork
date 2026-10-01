using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "GreatswordofBlah" })]
[LegacyName(new string[] { "IridescentExcalibur" })]
public class Orderbringer : ModItem, ILocalizedModType, IModType
{
	public int time;

	public Vector2 bladeHitboxPos;

	public float bladeRotation;

	public int bladeDirection;

	public float completion;

	public int swingCount;

	public bool spawnProj = true;

	public bool spawnTrueMeleeProj = true;

	public bool playSound = true;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public bool canHit
	{
		get
		{
			if (completion >= 0.35f)
			{
				return completion <= 0.8f;
			}
			return false;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 112);
		base.Item.damage = 800;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 16);
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.knockBack = 8f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.shoot = ModContent.ProjectileType<OrderbringerWaveProj>();
		base.Item.shootSpeed = 5.5f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		return false;
	}

	public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		float scale = 8f;
		Vector2 newSize = Utils.ToVector2(new Point(hitbox.Width, hitbox.Height)) * scale;
		hitbox = new Rectangle((int)(bladeHitboxPos.X - newSize.X / 2f), (int)(bladeHitboxPos.Y - newSize.Y / 2f), (int)newSize.X, (int)newSize.Y);
	}

	public override bool? CanHitNPC(Player player, NPC target)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mPos = player.Calamity().mouseWorld;
		Vector2 shootDir = player.Center.DirectionTo(mPos);
		float _ = float.NaN;
		bool hitCheck = Collision.CheckAABBvLineCollision(target.Hitbox.TopLeft(), target.Hitbox.Size(), player.Center - shootDir * 30f, player.Center + shootDir * 245f, (float)base.Item.width * 3f, ref _);
		if (!(canHit & hitCheck))
		{
			return false;
		}
		return null;
	}

	public override void UseAnimation(Player player)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		swingCount++;
		time = 0;
		bladeDirection = player.direction;
		bladeHitboxPos = player.Center;
		bladeRotation = 0f;
		spawnProj = true;
		spawnTrueMeleeProj = true;
		playSound = true;
		int dir = -Math.Sign(player.Center.X - player.Calamity().mouseWorld.X);
		MathHelper.ToRadians(-90f);
		_ = swingCount % 2;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mPos = player.Calamity().mouseWorld;
		completion = (float)time / ((float)base.Item.useAnimation / player.GetAttackSpeed(DamageClass.Melee));
		int dir = -Math.Sign(player.Center.X - mPos.X);
		float startRot = MathHelper.ToRadians(-110f) * (float)dir * (float)((swingCount % 2 == 0) ? 1 : (-1));
		float endRot = MathHelper.ToRadians(-110f) * (float)dir * (float)((swingCount % 2 != 0) ? 1 : (-1));
		float minRot = MathHelper.ToRadians(-150f) * (float)dir * (float)((swingCount % 2 == 0) ? 1 : (-1));
		float cutoff = 0.2f;
		float cutoff2 = 0.95f;
		Vector2 shootDir = player.Center.DirectionTo(mPos) * base.Item.shootSpeed;
		if (completion <= cutoff)
		{
			float lerp = Utils.GetLerpValue(0f, cutoff, completion, clamped: true);
			player.itemRotation = player.Center.DirectionTo(mPos).ToRotation() + MathHelper.Lerp(startRot, minRot, CalamityUtils.EaseInOutExp(lerp, 4f, 4f));
			player.itemRotation += (float)Math.PI * (float)((dir != 1) ? 1 : 0) + (float)Math.PI / 4f * (float)dir;
		}
		else
		{
			if (playSound)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SwooshMid");
				style.Pitch = Main.rand.NextFloat(0.1f, 0.3f);
				style.Volume = 1f;
				SoundEngine.PlaySound(in style, player.Center);
				playSound = false;
			}
			if (completion >= 0.65f && spawnProj)
			{
				if (!player.Calamity().bladeArmEnchant)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, shootDir, base.Item.shoot, base.Item.damage, base.Item.knockBack, player.whoAmI);
					for (int i = 0; i < 4; i++)
					{
						float rot = Main.rand.NextFloat(0.5f, 0.65f) * (float)(Main.rand.NextBool() ? 1 : (-1));
						Vector2 vel = (shootDir * 2.5f).RotatedByRandom(0.6000000238418579);
						Projectile.NewProjectile(player.GetSource_FromThis(), player.ClampedMouseWorld() - shootDir * 40f + Main.rand.NextVector2Circular(130f, 130f), vel.RotatedBy(rot) * Main.rand.NextFloat(0.9f, 1.2f), ModContent.ProjectileType<StarofOrder>(), (int)((float)base.Item.damage * 0.12f), (int)(base.Item.knockBack * 0.2f), player.whoAmI, 0f, rot, 1f);
					}
				}
				GeneralParticleHandler.SpawnParticle(new CustomSpark(player.Center - shootDir * 5f, shootDir.RotatedBy(0.4f * (float)(dir * ((swingCount % 2 == 0) ? 1 : (-1)))) * 2.5f, "CalamityMod/Particles/VerticalSmearLarge", affectedByGravity: false, (int)(14f / player.GetAttackSpeed(DamageClass.Melee)), 0.8f, player.Calamity().lightRGB, new Vector2(1f, 1f)));
				SoundEngine.PlaySound(in SoundID.Item60, player.Center);
				spawnProj = false;
			}
			float lerp2 = Utils.GetLerpValue(cutoff, cutoff2, completion, clamped: true);
			player.itemRotation = player.Center.DirectionTo(mPos).ToRotation() + MathHelper.Lerp(minRot, endRot, CalamityUtils.EaseInOutExp(lerp2, 6f, 2f));
			player.itemRotation += (float)Math.PI * (float)((dir != 1) ? 1 : 0) + (float)Math.PI / 4f * (float)dir;
		}
		float extraRot = ((dir == 1) ? (-(float)Math.PI / 4f) : MathHelper.ToRadians(225f));
		bladeHitboxPos = player.Center + (player.itemRotation + extraRot).ToRotationVector2() * 240f;
		_ = player.Center + (player.itemRotation + extraRot).ToRotationVector2() * 180f;
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, player.itemRotation + MathHelper.ToRadians(-130f) * (float)dir);
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, player.itemRotation + MathHelper.ToRadians(-130f) * (float)dir);
		player.itemLocation = player.Center;
		player.direction = dir;
		time++;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		if (!spawnTrueMeleeProj)
		{
			return;
		}
		int beamDamage = player.CalcIntDamage<MeleeDamageClass>((float)base.Item.damage * 0.35f);
		Vector2 mouseClamped = player.ClampedMouseWorld();
		SoundStyle style = SoundID.Item84 with
		{
			Volume = 1f,
			Pitch = Main.rand.NextFloat(0.5f, 0.7f)
		};
		SoundEngine.PlaySound(in style, player.Center);
		for (int i = 0; i < 2; i++)
		{
			Vector2 targetPos = Main.MouseWorld;
			NPC target2 = Main.MouseWorld.ClosestNPCAt(650f);
			if (target2 != null)
			{
				targetPos = target2.Center;
			}
			Vector2 spawnPos = mouseClamped + new Vector2(Main.rand.NextFloat(-300f, 300f), -900f);
			Vector2 vel = (targetPos - spawnPos).SafeNormalize(Vector2.UnitY) * 10f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), spawnPos, vel, ModContent.ProjectileType<OrderbringerBeam>(), beamDamage, 0f, player.whoAmI);
		}
		spawnTrueMeleeProj = false;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		if (Main.LocalPlayer != null)
		{
			float rate = Main.GlobalTimeWrappedHourly * 7f;
			List<Color> eColors = new List<Color>
			{
				Color.PaleVioletRed,
				Color.Coral,
				Color.Khaki,
				Color.PaleGreen,
				Color.Turquoise,
				Color.Violet
			};
			int colorIndex = (int)(rate / 2f % (float)eColors.Count);
			Color val = eColors[colorIndex];
			Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
			Color finalColor = Color.Lerp(val, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
			TooltipLine line = list.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "Tooltip2");
			if (line != null)
			{
				line.OverrideColor = Color.Lerp(finalColor, Color.White, 0.3f);
			}
		}
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/OrderbringerGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<GreatswordofJudgement>().AddIngredient<AuricBar>(5).AddIngredient<LifeAlloy>(5)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
