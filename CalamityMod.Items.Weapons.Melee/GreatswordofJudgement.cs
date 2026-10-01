using System;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
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

public class GreatswordofJudgement : ModItem, ILocalizedModType, IModType
{
	public int time;

	public Vector2 bladeHitboxPos;

	public float bladeRotation;

	public int bladeDirection;

	public float completion;

	public int swingCount;

	public bool spawnProj;

	public bool spawnTrueMeleeProj;

	public bool playSound;

	public Color clr;

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
		base.Item.width = 78;
		base.Item.height = 78;
		base.Item.damage = 310;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useStyle = 1;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useTurn = true;
		base.Item.knockBack = 7f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<JudgementProj>();
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
		bool hitCheck = Collision.CheckAABBvLineCollision(target.Hitbox.TopLeft(), target.Hitbox.Size(), player.Center - shootDir * 30f, player.Center + shootDir * 145f, (float)base.Item.width * 3f, ref _);
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
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
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
		clr = (Main.rand.NextBool() ? Color.MediumPurple : Color.MediumOrchid);
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
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
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
				}
				GeneralParticleHandler.SpawnParticle(new CustomSpark(player.Center - shootDir * 5f, shootDir.RotatedBy(0.4f * (float)(dir * ((swingCount % 2 == 0) ? 1 : (-1)))) * 2.5f, "CalamityMod/Particles/VerticalSmearLarge", affectedByGravity: false, (int)(14f / player.GetAttackSpeed(DamageClass.Melee)), 0.6f, clr, new Vector2(1f, 1f)));
				SoundEngine.PlaySound(in SoundID.Item60, player.Center);
				spawnProj = false;
			}
			float lerp2 = Utils.GetLerpValue(cutoff, cutoff2, completion, clamped: true);
			player.itemRotation = player.Center.DirectionTo(mPos).ToRotation() + MathHelper.Lerp(minRot, endRot, CalamityUtils.EaseInOutExp(lerp2, 6f, 2f));
			player.itemRotation += (float)Math.PI * (float)((dir != 1) ? 1 : 0) + (float)Math.PI / 4f * (float)dir;
		}
		float extraRot = ((dir == 1) ? (-(float)Math.PI / 4f) : MathHelper.ToRadians(225f));
		bladeHitboxPos = player.Center + (player.itemRotation + extraRot).ToRotationVector2() * 180f;
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, player.itemRotation + MathHelper.ToRadians(-130f) * (float)dir);
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, player.itemRotation + MathHelper.ToRadians(-130f) * (float)dir);
		player.itemLocation = player.Center;
		player.direction = dir;
		time++;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		if (spawnTrueMeleeProj)
		{
			int beamDamage = player.CalcIntDamage<MeleeDamageClass>((float)base.Item.damage * 0.35f);
			player.ClampedMouseWorld();
			SoundStyle style = SoundID.Item84 with
			{
				Volume = 1f,
				Pitch = Main.rand.NextFloat(0.5f, 0.7f)
			};
			SoundEngine.PlaySound(in style, player.Center);
			for (int i = 0; i < 2; i++)
			{
				Vector2 vel = player.Center.DirectionFrom(target.Center).RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(18f, 20f);
				Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), player.Center, vel, ModContent.ProjectileType<StarofJudgement>(), beamDamage, 0f, player.whoAmI, 0f, (i != 0) ? 1 : (-1), 1f);
			}
			spawnTrueMeleeProj = false;
		}
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GreatswordofJudgementGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3467, 7).AddIngredient<CoreofCalamity>().AddIngredient<GalacticaSingularity>(5)
			.AddTile(412)
			.Register();
	}

	public GreatswordofJudgement()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		spawnProj = true;
		spawnTrueMeleeProj = true;
		playSound = true;
		clr = Color.MediumOrchid;
		base._002Ector();
	}
}
