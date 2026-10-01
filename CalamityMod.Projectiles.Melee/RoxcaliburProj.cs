using System;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class RoxcaliburProj : ModProjectile, ILocalizedModType, IModType
{
	private const int Charging = 0;

	private const int Swinging = 1;

	private const int Plunging = 2;

	private const int Recoil = 3;

	private int Animation;

	private const int plungeSpeed = 20;

	public float BaseChargeTime = 120f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/Roxcalibur";

	private ref float ChargeLevel => ref base.Projectile.ai[0];

	private ref float HitTimer => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.penetrate = -1;
		base.Projectile.width = 100;
		base.Projectile.height = 100;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.scale = 1.3f;
		base.Projectile.timeLeft = 3600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 150;
	}

	private Vector2 RockOffset()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = base.Projectile.velocity * 0.25f;
		Vector2 size = base.Projectile.Size;
		return val * ((Vector2)(ref size)).Length();
	}

	public override bool? CanDamage()
	{
		if (!(HitTimer < 5f) || Animation == 0)
		{
			return false;
		}
		return base.CanDamage();
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (HitTimer > 0f && HitTimer < 5f)
		{
			float hitboxModifier = 2f + 1.5f * ChargeLevel;
			Vector2 HitboxSize = default(Vector2);
			((Vector2)(ref HitboxSize))._002Ector((float)base.Projectile.width * hitboxModifier, (float)base.Projectile.height * hitboxModifier);
			Vector2 HitboxCenter = base.Projectile.Center + RockOffset();
			hitbox = new Rectangle((int)(HitboxCenter.X - HitboxSize.X / 2f), (int)(HitboxCenter.Y - HitboxSize.Y / 2f), (int)HitboxSize.X, (int)HitboxSize.Y);
		}
	}

	public override void AI()
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0685: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		ref float origAnimMax = ref base.Projectile.ai[2];
		ref float rotation = ref base.Projectile.localAI[0];
		ref float playerDirection = ref base.Projectile.localAI[1];
		ref float PlayedChargeSound = ref base.Projectile.localAI[2];
		Player player = Main.player[base.Projectile.owner];
		if (player == null || !player.active || player.dead)
		{
			base.Projectile.Kill();
			return;
		}
		player.heldProj = base.Projectile.whoAmI;
		if (origAnimMax == 0f)
		{
			origAnimMax = player.itemAnimationMax;
		}
		Vector2 center = base.Projectile.Size;
		float HoldoutRadius = ((Vector2)(ref center)).Length() / 2f;
		float chargeTime = origAnimMax / (float)Roxcalibur.BaseUseTime * BaseChargeTime;
		if (player.channel)
		{
			Animation = 0;
		}
		else if (Animation == 0)
		{
			Animation = ((!(Main.MouseWorld.Y > player.Center.Y)) ? 1 : 2);
			float chargeModifier = ChargeLevel * 2.5f;
			if (Animation == 2)
			{
				base.Projectile.timeLeft = 120;
				chargeModifier = ChargeLevel * 3.5f;
			}
			base.Projectile.damage = (int)((float)base.Projectile.damage * (1f + chargeModifier));
			base.Projectile.netUpdate = true;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/LoudSwingWoosh");
			style.Pitch = -0.5f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		switch (Animation)
		{
		case 0:
		{
			if (ChargeLevel < 1f)
			{
				ChargeLevel += 1f / chargeTime;
			}
			else
			{
				ChargeLevel = 1f;
			}
			int direction = -Math.Sign(player.DirectionTo(Main.MouseWorld).X);
			playerDirection = -direction;
			float rotModifier = (float)Math.Pow(ChargeLevel, 0.4000000059604645);
			rotation = Utils.SmoothStep(-(float)Math.PI / 8f, (float)Math.PI / 3f, rotModifier);
			rotation *= direction;
			base.Projectile.timeLeft = (int)origAnimMax;
			if (ChargeLevel >= 1f && player.whoAmI == Main.myPlayer && PlayedChargeSound == 0f)
			{
				PlayedChargeSound = 1f;
				SoundStyle style = SoundID.NPCHit42 with
				{
					Pitch = 0.4f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			break;
		}
		case 1:
		{
			if ((float)base.Projectile.timeLeft > origAnimMax)
			{
				base.Projectile.timeLeft = (int)origAnimMax;
			}
			float progress = (float)base.Projectile.timeLeft / origAnimMax;
			float chargeRotModifier = (ChargeLevel + 1f) / 2f;
			float animRotModifier = Math.Clamp(progress, 0f, 1f);
			if (animRotModifier > 0.5f)
			{
				animRotModifier = 1f - animRotModifier;
			}
			animRotModifier *= animRotModifier;
			animRotModifier += 0.02f;
			rotation += playerDirection * animRotModifier * chargeRotModifier * ((float)Math.PI * 2f) * 0.2f;
			base.Projectile.localNPCHitCooldown = (int)Math.Ceiling(origAnimMax);
			int shardTimer = ((ChargeLevel >= 0.95f) ? 2 : ((ChargeLevel >= 0.5f) ? 4 : 6));
			if (progress < 0.8f && progress > 0.2f && base.Projectile.timeLeft % shardTimer == 0)
			{
				Vector2 roxSpeed = (base.Projectile.velocity * 8f).RotatedByRandom(MathHelper.ToRadians(10f));
				int rox = ModContent.ProjectileType<Rox1>();
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + RockOffset(), roxSpeed, rox, (int)((float)base.Projectile.damage * 0.4f), 1f, player.whoAmI, Main.rand.Next(3));
			}
			break;
		}
		case 2:
		{
			Vector2 plunge = Vector2.UnitY * player.gravDir * 20f * (ChargeLevel + 1f) / 2f;
			if ((player.velocity.Y == 0f && base.Projectile.velocity.Y * player.gravDir > 0f) || Collision.SolidCollision(player.position + plunge, player.width, player.height))
			{
				CollisionEffects();
				break;
			}
			player.position += plunge;
			player.velocity.X *= 0.95f;
			player.velocity.Y = 0f;
			float chargeRotModifier2 = (ChargeLevel + 1f) / 2f;
			float anglediff = RotationDifference(base.Projectile.velocity, Vector2.UnitY * player.gravDir);
			int dir = ((Math.Abs(anglediff) > (float)Math.PI / 2f) ? ((int)playerDirection) : Math.Sign(anglediff));
			rotation += (float)dir * Math.Abs(anglediff) * 0.12f * chargeRotModifier2;
			base.Projectile.localNPCHitCooldown = (int)Math.Ceiling(origAnimMax);
			if (Collision.SolidTiles(base.Projectile.position, base.Projectile.width, base.Projectile.height, allowTopSurfaces: false))
			{
				CollisionEffects();
			}
			break;
		}
		case 3:
		{
			if ((float)base.Projectile.timeLeft > origAnimMax / 3f)
			{
				base.Projectile.timeLeft = (int)(origAnimMax / 3f);
			}
			float modifier = Math.Clamp((float)base.Projectile.timeLeft / origAnimMax, 0f, 1f);
			rotation -= playerDirection * modifier * ((float)Math.PI * 2f) * 2f / origAnimMax;
			break;
		}
		}
		if (HitTimer > 0f && HitTimer <= 5f)
		{
			HitTimer++;
		}
		Projectile projectile = base.Projectile;
		Vector2 spinningpoint = -Vector2.UnitY * player.gravDir;
		double radians = rotation;
		center = default(Vector2);
		projectile.velocity = spinningpoint.RotatedBy(radians, center);
		base.Projectile.Center = player.HandPosition.Value + base.Projectile.velocity * HoldoutRadius;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.spriteDirection = (base.Projectile.direction = (int)playerDirection);
		player.ChangeDir(base.Projectile.direction);
		player.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		player.itemTime = Math.Max(player.itemTime, 2);
		player.itemAnimation = Math.Max(player.itemAnimation, 2);
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation += MathHelper.ToRadians(-45f) + (float)Math.PI;
		}
		else
		{
			base.Projectile.rotation += MathHelper.ToRadians(-135f) + (float)Math.PI;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		CollisionEffects();
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.75f);
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	private void CollisionEffects()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		if (HitTimer != 0f)
		{
			return;
		}
		HitTimer = 1f;
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		if (Animation == 2)
		{
			Player player = Main.player[base.Projectile.owner];
			player.velocity.Y = (0f - player.gravDir) * 20f * (ChargeLevel + 1f) / 2f;
		}
		Animation = 3;
		for (int i = 0; i < 24; i++)
		{
			Vector2 dir = Vector2.UnitY.RotatedBy((float)Math.PI * 2f * (float)i / 24f);
			int d = Dust.NewDust(base.Projectile.Center + RockOffset() + dir * 20f * base.Projectile.scale, 0, 0, 1, dir.X * 12f * base.Projectile.scale, dir.Y * 12f * base.Projectile.scale, 0, Color.Black, base.Projectile.scale);
			if (d.WithinBounds(6000))
			{
				Main.dust[d].noGravity = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle rectangle = value.Bounds;
		Vector2 origin2 = rectangle.Size() / 2f;
		SpriteEffects effects = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Vector2 pos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		if (Animation == 0 && ChargeLevel > 0.95f)
		{
			float shake = 2.5f * (ChargeLevel - 0.95f) / 0.05f;
			pos += Main.rand.NextVector2Circular(shake, shake);
		}
		Main.EntitySpriteDraw(value, pos, rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, effects);
		return false;
	}

	public static float RotationDifference(Vector2 from, Vector2 to)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		return (float)Math.Atan2(to.Y * from.X - to.X * from.Y, from.X * to.X + from.Y * to.Y);
	}
}
